#include <stddef.h>
#include <stdint.h>
#include <stdbool.h>
#include "serial.h"
#include "motor.h"
#include "button.h"
#include "timer.h"
#include "stm32f0xx.h"
#include "stm32f0xx_gpio.h"
typedef enum
{
	STATE_IDLE,
	STATE_CMD,
}state_t;

typedef enum
{
	CMD_START = 0x01,
	CMD_STOP  = 0x02,
}cmd_t;

volatile state_t gState = STATE_IDLE;
volatile cmd_t gCmd = CMD_STOP;
void call (void);
void prvHardwareSetup(void);
void CMD_Service(uint8_t byte);
static int step = 0;
int main(void)
{
	prvHardwareSetup();
	int id;
	id = xCreateSwTimer(10, &call);
	xTimerRun(id);
	step++;
	while(1)
	{
//		switch (gState)
//		{
//			case STATE_IDLE:
//				break;
//			case STATE_CMD:
//				gState = STATE_IDLE;
//			
//				break;
//			default:
//				break;
//		}
//		Motor_Rotate();
		
	}
}

void prvHardwareSetup(void)
{
	Serial_t serial_obj;
	serial_obj.baud     = SERIAL_BAUDRATE_115200;
	serial_obj.callback = &CMD_Service;
	Serial_Init(&serial_obj);
	Motor_Init();
	Button_Init();
	xSysTimerInit();
}

void CMD_Service(uint8_t byte)
{
	gState = STATE_CMD;
	gCmd = CMD_STOP;
}

void call (void)
{
	static uint8_t i = 0;
	if (i == 0)
	{
		GPIO_SetBits(GPIOA, GPIO_Pin_12);
		GPIO_SetBits(GPIOB, GPIO_Pin_12);
		i = 1;
	}
	else if (i == 1)
	{
		GPIO_ResetBits(GPIOA, GPIO_Pin_12);
		GPIO_ResetBits(GPIOB, GPIO_Pin_12);
		i = 0;
	}
	int id;
		id = xCreateSwTimer(10, &call);
		xTimerRun(id);

	
}
