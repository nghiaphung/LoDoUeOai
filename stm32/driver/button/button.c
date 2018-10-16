/*
 * filename: button.c
 * date: 9/9/2018
 */
/******************************************************************************/
/**!                               INCLUDE                                    */
/******************************************************************************/
#include "stm32f0xx.h"
#include "stm32f0xx_gpio.h"
#include "button.h"
/******************************************************************************/
/**!                            LOCAL TYPEDEF                                 */
/******************************************************************************/

/******************************************************************************/
/**!                            LOCAL SYMBOLS                                 */
/******************************************************************************/
#define BUTTON_HOME_X_PORT       GPIOB
#define BUTTON_HOME_X_PIN        GPIO_Pin_1
#define BUTTON_HOME_Y_PORT       GPIOB
#define BUTTON_HOME_Y_PIN        GPIO_Pin_13
#define BUTTON_END_X_PORT        GPIOB
#define BUTTON_END_X_PIN         GPIO_Pin_14
#define BUTTON_END_Y_PORT        GPIOB
#define BUTTON_END_Y_PIN         GPIO_Pin_15
/******************************************************************************/
/**!                         EXPORTED VARIABLES                               */
/******************************************************************************/

/******************************************************************************/
/**!                          LOCAL VARIABLES                                 */
/******************************************************************************/

/******************************************************************************/
/**!                    LOCAL FUNCTIONS PROTOTYPES                            */
/******************************************************************************/

/******************************************************************************/
/**!                        EXPORTED FUNCTIONS                                */
/******************************************************************************/
void Button_Init(void)
{
	GPIO_InitTypeDef  GPIO_InitStruct;
	
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_IN;
	GPIO_InitStruct.GPIO_PuPd = GPIO_PuPd_NOPULL;
	
	GPIO_InitStruct.GPIO_Pin  = BUTTON_HOME_X_PIN;
	GPIO_Init(BUTTON_HOME_X_PORT, &GPIO_InitStruct);
	
	GPIO_InitStruct.GPIO_Pin  = BUTTON_HOME_Y_PIN;
	GPIO_Init(BUTTON_HOME_Y_PORT, &GPIO_InitStruct);
	
	GPIO_InitStruct.GPIO_Pin  = BUTTON_END_X_PIN;
	GPIO_Init(BUTTON_END_X_PORT, &GPIO_InitStruct);
	
	GPIO_InitStruct.GPIO_Pin  = BUTTON_END_Y_PIN;
	GPIO_Init(BUTTON_END_Y_PORT, &GPIO_InitStruct);
}

uint8_t Button_HomeX(void)
{
	GPIO_ReadInputDataBit(BUTTON_HOME_X_PORT, BUTTON_HOME_X_PIN);
}

uint8_t Button_HomeY(void)
{
	GPIO_ReadInputDataBit(BUTTON_HOME_Y_PORT, BUTTON_HOME_Y_PIN);
}

uint8_t Button_EndX(void)
{
	GPIO_ReadInputDataBit(BUTTON_END_X_PORT, BUTTON_END_X_PIN);
}

uint8_t Button_EndY(void)
{
	GPIO_ReadInputDataBit(BUTTON_END_Y_PORT, BUTTON_END_Y_PIN);
}

/******************************************************************************/
/**!                          LOCAL FUNCTIONS                                 */
/******************************************************************************/

/******************************************************************************/
/*                              END OF FILE                                   */
/******************************************************************************/