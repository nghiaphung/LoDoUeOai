/*
 * filename: motor.c
 * date: 9/9/2018
 */
/******************************************************************************/
/**!                               INCLUDE                                    */
/******************************************************************************/
#include "stm32f0xx.h"
#include "stm32f0xx_gpio.h"
#include "motor.h"
/******************************************************************************/
/**!                            LOCAL TYPEDEF                                 */
/******************************************************************************/

/******************************************************************************/
/**!                            LOCAL SYMBOLS                                 */
/******************************************************************************/
#define MOTOR_X_DIR_PIN     GPIO_Pin_11
#define MOTOR_X_STEP_PIN     GPIO_Pin_12
#define MOTOR_X_DIR_PORT    GPIOA
#define MOTOR_X_STEP_PORT    GPIOA
#define MOTOR_Y_DIR_PIN    GPIO_Pin_11
#define MOTOR_Y_STEP_PIN    GPIO_Pin_12
#define MOTOR_Y_DIR_PORT   GPIOB
#define MOTOR_Y_STEP_PORT   GPIOB
/******************************************************************************/
/**!                         EXPORTED VARIABLES                               */
/******************************************************************************/

/******************************************************************************/
/**!                          LOCAL VARIABLES                                 */
/******************************************************************************/

/******************************************************************************/
/**!                    LOCAL FUNCTIONS PROTOTYPES                            */
/******************************************************************************/
static uint8_t x_status = 0;
static uint8_t y_status = 0;
/******************************************************************************/
/**!                        EXPORTED FUNCTIONS                                */
/******************************************************************************/
void Motor_Init(void)
{
	GPIO_InitTypeDef  GPIO_InitStruct;
	
	/* Configure for GPIO */
	GPIO_InitStruct.GPIO_Mode    = GPIO_Mode_OUT;
	GPIO_InitStruct.GPIO_OType   = GPIO_OType_PP;
	GPIO_InitStruct.GPIO_PuPd    = GPIO_PuPd_DOWN;
	GPIO_InitStruct.GPIO_Speed   = GPIO_Speed_Level_1;
	
	/* Initializing MOTOR_X_DIR */
	GPIO_InitStruct.GPIO_Pin     = MOTOR_X_DIR_PIN;
	GPIO_Init(MOTOR_X_DIR_PORT, &GPIO_InitStruct);
	/* Initializing MOTOR_Y_DIR */
	GPIO_InitStruct.GPIO_Pin     = MOTOR_Y_DIR_PIN;
	GPIO_Init(MOTOR_Y_DIR_PORT, &GPIO_InitStruct);
	/* Initializing MOTOR_X_STEP */
	GPIO_InitStruct.GPIO_Pin     = MOTOR_X_STEP_PIN;
	GPIO_Init(MOTOR_X_STEP_PORT, &GPIO_InitStruct);
	/* Initializing MOTOR_Y_STEP */
	GPIO_InitStruct.GPIO_Pin     = MOTOR_Y_STEP_PIN;
	GPIO_Init(MOTOR_Y_STEP_PORT, &GPIO_InitStruct);
}

void MotorX_Rotate (void)
{
	if (x_status == 0)
	{
		GPIO_SetBits(MOTOR_X_STEP_PORT, MOTOR_X_STEP_PIN);
		x_status = 1;
	}
	else
	{
		GPIO_ResetBits(MOTOR_X_STEP_PORT, MOTOR_X_STEP_PIN);
		x_status = 0;
	}
}

void MotorY_Rotate (void)
{
	if (y_status == 0)
	{
		GPIO_SetBits(MOTOR_Y_STEP_PORT, MOTOR_Y_STEP_PIN);
		y_status = 1;
	}
	else
	{
		GPIO_ResetBits(MOTOR_Y_STEP_PORT, MOTOR_Y_STEP_PIN);
		y_status = 0;
	}
}

void MotorX_CW(void)
{
	GPIO_SetBits(MOTOR_X_DIR_PORT, MOTOR_X_DIR_PIN);
}

void MotorX_CCW(void)
{
	GPIO_ResetBits(MOTOR_X_DIR_PORT, MOTOR_X_DIR_PIN);
}

void MotorY_CW(void)
{
	GPIO_SetBits(MOTOR_Y_DIR_PORT, MOTOR_Y_DIR_PIN);
}

void MotorY_CCW(void)
{
	GPIO_ResetBits(MOTOR_Y_DIR_PORT, MOTOR_Y_DIR_PIN);
}

void MotorX_Stop (void)
{
	GPIO_SetBits(MOTOR_X_STEP_PORT, MOTOR_X_STEP_PIN);
}

void MotorY_Stop (void)
{
	GPIO_SetBits(MOTOR_Y_STEP_PORT, MOTOR_Y_STEP_PIN);
}

/******************************************************************************/
/**!                          LOCAL FUNCTIONS                                 */
/******************************************************************************/

/******************************************************************************/
/*                              END OF FILE                                   */
/******************************************************************************/
