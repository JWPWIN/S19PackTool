/***** (C) Copyright, Shenzhen SHINRY Technology Co.,Ltd. ******source file*****
* File name : Sha256.c
* Author    : bfsu
* Brief     : Sha256 & HMAC-Sha256
********************************************************************************
* modify
* Version      Date(YYYY/MM/DD)       Author        Described
* V1.00      2022/03/04                bfsu        First Issue
*******************************************************************************/

/*=======[I N C L U D E S]====================================================*/
#include "Sha256.h"
#include "SysTypes.h"

#if SHA256_SUPPORT == DE_ENABLE

#define SHA256_LENGTH                        32u

/*=======[typedef]====================================================*/
typedef struct Sha256Calc
{
    u08  Value[SHA256_LENGTH];
    u32  DwordBufBytes;
    u32  ByteNumLo;
    u32  ByteNumHi;
    u32  reg[ 8 ]; /** h0 to h 7 -- old value store*/
    u32  DwordBuf[ 16 ]; /** data store */
    u32  Padding[ 64 ];
}Sha256Calc;

static Sha256Calc gtSha;
/*=======[I N T E R N A L   F U N C T I O N   D E C L A R A T I O N S]========*/
/* <calculate 64 d-words block,data is at first 16 d-words> */
static void Sha256Calc_calcBlock( u32* dp, u32* rp );

/*Security Public Key : 256Bytes,sha256 initial value*/
const static u32 Sha256Calc_k[ 64 ] =
{
       0x428a2f98U, 0x71374491U, 0xb5c0fbcfU, 0xe9b5dba5U, 0x3956c25bU, 0x59f111f1U, 0x923f82a4U, 0xab1c5ed5U,
       0xd807aa98U, 0x12835b01U, 0x243185beU, 0x550c7dc3U, 0x72be5d74U, 0x80deb1feU, 0x9bdc06a7U, 0xc19bf174U,
       0xe49b69c1U, 0xefbe4786U, 0x0fc19dc6U, 0x240ca1ccU, 0x2de92c6fU, 0x4a7484aaU, 0x5cb0a9dcU, 0x76f988daU,
       0x983e5152U, 0xa831c66dU, 0xb00327c8U, 0xbf597fc7U, 0xc6e00bf3U, 0xd5a79147U, 0x06ca6351U, 0x14292967U,
       0x27b70a85U, 0x2e1b2138U, 0x4d2c6dfcU, 0x53380d13U, 0x650a7354U, 0x766a0abbU, 0x81c2c92eU, 0x92722c85U,
       0xa2bfe8a1U, 0xa81a664bU, 0xc24b8b70U, 0xc76c51a3U, 0xd192e819U, 0xd6990624U, 0xf40e3585U, 0x106aa070U,
       0x19a4c116U, 0x1e376c08U, 0x2748774cU, 0x34b0bcb5U, 0x391c0cb3U, 0x4ed8aa4aU, 0x5b9cca4fU, 0x682e6ff3U,
       0x748f82eeU, 0x78a5636fU, 0x84c87814U, 0x8cc70208U, 0x90befffaU, 0xa4506cebU, 0xbef9a3f7U, 0xc67178f2U
};
/*=======[F U N C T I O N   I M P L E M E N T A T I O N S]====================*/
/******************************************************************************/
/**
 * @brief               <initialization>
 *
 * <Sha 256 calculation initialization> .
 * Service ID   :       <NONE>
 * Sync/Async   :       <Synchronous>
 * Reentrancy           <Reentrant>
 * @param[in]           <t(IN)>
 * @param[out]          <t(OUT)>
 * @param[in/out]       <NONE>
 * @return              <NONE>
 */
/******************************************************************************/
__declspec(dllexport) void Sha256_Init(void)
{
    u32 x;
    Sha256Calc* t = &gtSha;
    /* init value of input variable : t*/
    /* init reg[0-7] constant value */

    t->reg[0]=0x6a09e667U;
    t->reg[1]=0xbb67ae85U;
    t->reg[2]=0x3c6ef372U;
    t->reg[3]=0xa54ff53aU;
    t->reg[4]=0x510e527fU;
    t->reg[5]=0x9b05688cU;
    t->reg[6]=0x1f83d9abU;
    t->reg[7]=0x5be0cd19U;

    /* clear value   */
    for(x = 0; x < 32u; x ++)
    {
        t->Value[x] = 0;
    }
    /* clear Padding and DwordBuf */
    for( x = 0; x < 16u; x ++ )
    {
        t->Padding[ x ] = 0;
        t->DwordBuf[ x ] = 0;
    }
    t->ByteNumLo =  0;
    t->ByteNumHi =  0;
    t->DwordBufBytes = 0;

}

/******************************************************************************/
/**
 * @brief               <Sha256Calc_calculate>
 *
 * <Sha 256 calculation> .
 * Service ID   :       <NONE>
 * Sync/Async   :       <Synchronous>
 * Reentrancy           <Reentrant>
 * @param[in]           <t(IN),dp(IN),dl(IN)>
 * @param[out]          <t(OUT)>
 * @param[in/out]       <NONE>
 * @return              <ret>
 */
/******************************************************************************/
__declspec(dllexport) void Sha256_PushData(u08* dp, u32 dl )
{
    register u32 w,x,y,z;
    u32 reg[8u];
    Sha256Calc* t = &gtSha;

    /**
       Set x, y, z, w
       x -- Dword Buffer Offset
       y -- Bytes Offset in a Dword * 8
       z -- Source Data Idx
       w -- 64Bytes Block Number
    */
    z = ( u32 )(( t->ByteNumLo & 0x7fffffffuL ) + dl);
    x = (( t->ByteNumLo >> 31u ) & 0x01uL ) + (( z >> 31u ) & 0x01uL );
    z = ( z & 0x7fffffffuL ) | ( x << 31u );
    y = t->ByteNumHi + ( x >> 1u );
    if( y > 0x1fffffffuL )
    {
        /* do nothing */
    }
    else
    {
        t->ByteNumLo = z;
        t->ByteNumHi = y;

        for( w = 0; w < 16u; w ++ )
        {
            t->Padding[ w ] = t->DwordBuf[ w ];
        }
        for( w = 0; w < 8u; w ++ )
        {
            reg[ w ] = t->reg[ w ];
        }
        x = t->DwordBufBytes / 4u;
        y = ( t->DwordBufBytes & 3u ) * 8u;
        w = ( u32 )((( t->DwordBufBytes + dl ) >> 6u ) & 0x03ffffffuL );
        z = 0;

        /**
            64Bytes Full Blocks Loop -- SHA1 is Big endian Order !
        */
        while( w > 0u )
        {
            while( x < 16u )
            {
                t->Padding[ x ] = ( t->Padding[ x ] & ~( 0x000000ffuL << ( 24u - y ))) | (( dp[ z ] &
                0x000000ffuL ) << ( 24u - y ));
                y = y + 8u;
                x += ( y >> 5u );
                y &= 0x1fuL;
                z ++;
            }
            (void)Sha256Calc_calcBlock( t->Padding, reg );
            x = 0;
            y = 0;
            w --;
        }

        /** Clear Dirty Data */
        if(( x | y ) == 0u )
        {
            for( w = 0; w < 16u; w ++ )
            {
                t->DwordBuf[ w ] = 0u;
                t->Padding[ w ] = 0u;
            }
        }

        /**
           Last Blocks Loop
           z is how many bytes used. dl is bigger than z. -- Also Big endian Order !
        */
        w = ( u32 )( dl - z );
        while( w > 0u ){
            t->DwordBuf[ x ] =
                ( t->Padding[ x ] & ~( 0x000000ffuL << ( 24u - y ))) | (( dp[ z ] &
                0x000000ffuL ) << ( 24u - y ));
            t->Padding[ x ] =
                ( t->Padding[ x ] & ~( 0x000000ffuL << ( 24u - y ))) | (( dp[ z ] &
                            0x000000ffuL ) << ( 24u - y ));
            y = y + 8u;
            x += ( y >> 5u );
            y &= 0x1fuL;
            z ++;
            w --;
        }

        /** Append 0x80u */
        t->Padding[ x ] |= 0x00000080uL << ( 24u - y );

        /** Save Old Value */
        for( w = 0; w < 8u; w ++ )
        {
            t->reg[ w ] = reg[ w ];
        }

        /** Check if Length + 0x80 could append */
        t->DwordBufBytes =  (x * 4u)  + ( y >> 3u );
        if( t->DwordBufBytes > 55u )
        {
            (void)Sha256Calc_calcBlock( t->Padding, reg );
            for( w = 0; w < 16u; w ++ )
            {
                t->Padding[ w ] = 0;
            }
        }

        /** Append Length */
        t->Padding[ 15u ] = t->ByteNumLo << 3u;
        t->Padding[ 14u ] = (( t->ByteNumLo >> 29u ) & 0x3uL ) | ( t->ByteNumHi << 3u );
        (void)Sha256Calc_calcBlock( t->Padding, reg );

        /** Output ordered Value -- big endian order */
        x = 0;
        for(w = 0; w < 32u; w++ )
        {
            /** high byte first output value */
            t->Value[ w ] = ( u08 )( reg[ w >> 2u ] >> ( 24u - x ));
            x = ( x + 8u ) & 0x1fuL;
        }
    }
}

/******************************************************************************/
/**
 * @brief               <SHA256 COMPUTE BLOCK>
 *
 * <calculate 64 dwords block,data is at first 16 dwords> .
 * Service ID   :       <NONE>
 * Sync/Async   :       <Synchronous>
 * Reentrancy           <Reentrant>
 * @param[in]           <dp(IN),dl(IN)>
 * @param[out]          <t(OUT)>
 * @param[in/out]       <NONE>
 * @return              <NONE>
 */
/******************************************************************************/
static void Sha256Calc_calcBlock( u32* dp, u32* rp )
{
    register u32 a,b,c,d,e,f,g,h;
    u32 t0,t1,t2;
    u32 x;

    /** extend 16s dword to 64 dwords */
    for( x = 16u; x < 64u; x ++ )
    {

        t0 =  ((( dp[ x - 15u ] >> 7u ) & 0x1ffffffuL )  | ( dp[ x - 15u ] << 25u )) ^
              ((( dp[ x - 15u ] >> 18u ) & 0x3fffuL )    | ( dp[ x - 15u ] << 14u )) ^
              (( dp[ x - 15u ] >> 3u  ) & 0x1fffffffuL );

        t1 =  ((( dp[ x - 2u ] >> 17u ) & 0x7fffuL ) | ( dp[ x - 2u ] << 15u )) ^
              ((( dp[ x - 2u ] >> 19u ) & 0x1fffuL ) | ( dp[ x - 2u ] << 13u )) ^
              (( dp[ x - 2u ] >> 10u ) & 0x3fffffuL );

        dp[ x ] = dp[ x - 16u ] + t0 + dp[ x - 7u ] + t1;
    }

    /** init value */
     a = rp[0];
     b = rp[1];
     c = rp[2];
     d = rp[3];
     e = rp[4];
     f = rp[5];
     g = rp[6];
     h = rp[7];

    /** main loop */
    for( x = 0; x < 64u; x ++ )
    {
        t2 = ((( a >> 2u ) & 0x3fffffffuL ) | ( a << 30u )) ^
             ((( a >> 13u ) & 0x7ffffuL ) | ( a << 19u )) ^
             ((( a >> 22u ) & 0x3ffuL ) | ( a <<10u ));
        t0 = ( a & b ) ^ ( a & c ) ^ ( b & c );
        t2 = t2 + t0;

        t1 = ((( e >> 6u ) & 0x3ffffffuL ) | ( e << 26u )) ^
             ((( e >> 11u ) & 0x1fffffuL ) | ( e << 21u )) ^
             ((( e >> 25u ) & 0x7fuL ) | ( e << 7u ));
        t0 = ( e & f ) ^ ((~e) & g );
        t1 = h + t1 + t0 + Sha256Calc_k[ x ] + dp[ x ];
        h=g;
        g=f;
        f=e;
        e=d+t1;
        d=c;
        c=b;
        b=a;
        a=t1+t2;
    }

    /* set value to rp[0-7]*/
    rp[0] += a;
    rp[1] += b;
    rp[2] += c;
    rp[3] += d;
    rp[4] += e;
    rp[5] += f;
    rp[6] += g;
    rp[7] += h;
}

__declspec(dllexport) u08 *Sha256_GetShaValue(void)
{
    return gtSha.Value;
}


#if SHA256_HMAC_SUPPORT == DE_ENABLE

#define SHA256_BLOCKSIZE                    64u

typedef struct Sha1_HmacKey
{
    u08 cKey[SHA256_BLOCKSIZE];
    u08 uLen;
}Sha256_HmacKey;

static Sha256_HmacKey gtSha256HmacKey;

void Sha256_SetHmacKey(const u08 *pKey, u32 uKeyLen)
{
    Sha256_HmacKey *pKeyBuf = &gtSha256HmacKey;
    u32 i;

    if (uKeyLen > SHA256_BLOCKSIZE)
    {
        Sha256_Init();
        Sha256_PushData(pKey, uKeyLen);
        pKey = Sha256_GetShaValue();
        uKeyLen = SHA256_LENGTH;
    }

    /* Pad the key for inner digest */
    for (i = 0; i < uKeyLen; ++i)
    {
        pKeyBuf->cKey[i] = pKey[i];
    }
    pKeyBuf->uLen = uKeyLen & 0xFF; /* if uKeyLen > SHA1_BLOCKSIZE then uKeyLen = SHA256_LENGTH */
}

void Sha256_PadHmacKey(u08 cPad)
{
    Sha256_HmacKey *pKey = &gtSha256HmacKey;
    u32 i;
    for (i = 0; i < pKey->uLen; ++i)
    {
        pKey->cKey[i] ^= cPad;
    }
    for (i = pKey->uLen; i < SHA256_BLOCKSIZE; ++i)
    {
        pKey->cKey[i] = cPad;
    }
}

void MemCpy(u08* dest, u08* source, u32 length)
{
    u08* pDest = (u08*)dest;
    const u08* pSrc = (const u08*)source;

    while (length > 0UL)
    {
        if ((pDest != 0) && (pSrc != 0))
        {
            *pDest = *pSrc;
            pDest++;
            pSrc++;
        }
        else
        {
            break;
        }

        length--;
    }

}

u08* Sha256_GetHmacValue(const u08 *pSrcData, u32 uSrcLen)
{
    const u08* pcKey = gtSha256HmacKey.cKey;
    static u08 ucInData[SHA256_LENGTH];
    u08 *pRet;

    /**** Inner Digest ****/
    Sha256_Init();
    /* Pad the key for inner digest */
    Sha256_PadHmacKey(0x36);
    Sha256_PushData(pcKey, SHA256_BLOCKSIZE);
    Sha256_PadHmacKey(0x36);/*Clear Pad*/
    Sha256_PushData(pSrcData, uSrcLen);
    pRet = Sha256_GetShaValue();
    MemCpy(ucInData, pRet, SHA256_LENGTH);

    /**** Outer Digest ****/
    Sha256_Init();
    /* Pad the key for outter digest */
    Sha256_PadHmacKey(0x5C);
    Sha256_PushData(pcKey, SHA256_BLOCKSIZE);
    Sha256_PadHmacKey(0x5C);/*Clear Pad*/
    Sha256_PushData(ucInData, SHA256_LENGTH);

    return Sha256_GetShaValue();
}

#endif /* end of #if SHA256_HMAC_SUPPORT == DE_ENABLE */

#endif /* end of #if SHA256_ENABLE == DE_ENABLE */

/* end of file */
