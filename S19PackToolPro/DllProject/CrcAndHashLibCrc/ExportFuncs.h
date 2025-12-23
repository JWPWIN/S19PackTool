#pragma once
#include "SysTypes.h"

//brief: all of the external functions which supported by this dll

//Hash algorithm
void Sha256_Init(void);
void Sha256_PushData(u08* pData, u32 ulDataLen);
u08* Sha256_GetShaValue(void);

//CRC algorithm
u32 GetCrc32ForForValidityInfo(void* buffer, u32 len);
u32 GetCrc32(u08* buffer, u32 len);
u16 CRC16_CCITT_FALSE(u08* data, u32 datalen);