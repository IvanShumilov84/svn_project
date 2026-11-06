/* this file contains the actual definitions of */
/* the IIDs and CLSIDs */

/* link this file in with the server and any clients */


/* File created by MIDL compiler version 5.01.0164 */
/* at Wed Oct 01 12:06:33 2008
 */
/* Compiler settings for E:\Nigel\KineticaRTOPC\KineticaRTOPC.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )
#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

const IID DIID_IKRT_GroupUpdate = {0xBD1F6AE4,0x440C,0x44bf,{0x94,0x7A,0x35,0x83,0x5B,0x07,0x97,0xDA}};


const IID IID_IGroup = {0xE26B69FF,0x32EB,0x4683,{0x82,0xAD,0x72,0x8D,0x0B,0x0B,0xB1,0x08}};


const IID IID_IClient = {0x6B1EB334,0x5DF5,0x4B55,{0x93,0x9F,0xC2,0x45,0x38,0x10,0xD9,0x0C}};


const IID LIBID_KineticaRT_OPC = {0xB80EAF9C,0x0979,0x4063,{0xA7,0xDD,0xD2,0x64,0x40,0x2B,0x21,0x25}};


const CLSID CLSID_Client = {0x0225BC86,0x34E7,0x4D03,{0xBA,0x27,0x87,0xD6,0xA8,0x7F,0xB5,0x9A}};


const CLSID CLSID_Group = {0xC68958AC,0xAE95,0x4DCF,{0x8D,0xAF,0xEF,0x0D,0xF2,0x9B,0x3E,0x11}};


#ifdef __cplusplus
}
#endif

