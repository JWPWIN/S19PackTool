/***** (C) Copyright, Shenzhen SHINRY Technology Co.,Ltd. ******header file*****
* File name : Sha256.h
* Author    : bfsu
* Brief     : Sha256 & HMAC-Sha256
********************************************************************************
* modify
* Version     Date(YYYY/MM/DD)            Author        Described
* V1.00        2022/03/04                 bfsu         First Issue
*******************************************************************************/


#ifndef _SHA256CALC_H
#define _SHA256CALC_H
#include "SysTypes.h"

#ifndef SHA256_SUPPORT
    #define SHA256_SUPPORT DE_DISABLE
#endif
#ifndef SHA256_HMAC_SUPPORT
    #define SHA256_HMAC_SUPPORT DE_DISABLE
#endif

#if SHA256_SUPPORT == DE_ENABLE

__declspec(dllexport) void Sha256_Init(void);
__declspec(dllexport) void Sha256_PushData(u08* pData, u32 ulDataLen);
__declspec(dllexport) u08* Sha256_GetShaValue(void);

#if SHA256_HMAC_SUPPORT == DE_ENABLE
void Sha256_SetHmacKey(const u08 *pKey, u32 uKeyLen);
u08* Sha256_GetHmacValue(const u08 *pSrcData, u32 uSrcLen);
#endif

#endif /*#if SHA256_SUPPORT == DE_ENABLE*/

#endif
