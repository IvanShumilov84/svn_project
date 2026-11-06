//=================================================================================================
//
// NAME:    Sample664_basictest.c
//
// DESCRIPTION:
//
//          This test-program demonstrates how to configure ARINC-664 layer from a file
//          and then performs a message exchange from one bus to the other.
//
//  NOTES:  MF-2013
//
// ------------------------------------------------------------------------------------------------
//
//      Copyright (C) 2013 United Electronic Industries, Inc.
//      All rights reserved.
//      United Electronic Industries Confidential Information.
//
//=================================================================================================
#include <stdio.h>
#include <signal.h>
#include <time.h>

#include "PDNA.h"
#include "logger.h"
#include "Convert_AR664_cfg.h"
#include "Sample664_helper.h"

#define IOM_IPADDR_DEV1      "192.168.1.226"
#define IOM_IPADDR_DEV2      "192.168.1.227" 

#define NUM_DEV              1												// only designed for 2 devices
#define DEVN1                0
#define DEVN2                0
#define MAX_CYCLES			 256											//2147483646

#define CFG_RESET_IOM		 TRUE
#define CFG_TEST_MODE        0
#define CFG_SLEEP_MSEC		 3100											// 768ms = ceil(8192/1471)*128ms*1subvl (Note: 3072ms when 4subVL)

char ramlog[MAX_RAMLOG];
unsigned long ramndx;
LARGE_INTEGER hpet_freq;
LARGE_INTEGER tick;
LARGE_INTEGER start_tick;

int stop = FALSE;
// -- 
// Handler for SIGINT
// 
void handler(int sig)
{
    stop = TRUE;
}


// Builds a set of combined structures to hold an array of VL and Ports
//
//
int build_common_structure( char* file_prefix, int devn,
						    pAR664_VL_CFG* ppInVL, int* pInVLSize,          // Combined  input VL and its size (allocate here)
                            pAR664_VL_CFG* ppOutVL, int* pOutVLSize,        // Combined output VL and its size (allocate here)
                            pAR664_PORT_CFG* ppInPort, int* pInPortSize,    // Combined  input ports and size (allocate here)
                            pAR664_PORT_CFG* ppOutPort, int* pOutPortSize   // Combined output ports and size (allocate here)							
) {
	// Structures
    pAR664_VL_CFG pInVL=NULL;
    pAR664_VL_CFG pOutVL=NULL;
    pAR664_PORT_CFG pInPort=NULL;
    pAR664_PORT_CFG pOutPort=NULL;
    int InVLSize=0,OutVLSize=0,InPortSize=0,OutPortSize=0;
	// Tables
    pAR664_VL_CFG pVL[NUM_TBL];
    pAR664_PORT_CFG pPort[NUM_TBL];
    int VLSize[NUM_TBL], tvl = 0;
    int PortSize[NUM_TBL], tpp = 0;
	// Stuff
    char eq_name[32];
	char filename[128];
    int i,ret;

    for (i = 0; i < NUM_TBL; i++) {

		if (NUM_TBL == 1)
			sprintf(filename, "%s.txt", file_prefix);
		else
			sprintf(filename, "%s%d.txt", file_prefix, i+1);

        Chk4Err(DqAdv664CreateStructures(filename, eq_name, &pVL[i], &VLSize[i], &pPort[i], &PortSize[i]), 
			printf("Error reading %s!\n", filename); return ret);

        tvl += VLSize[i];
        tpp += PortSize[i];
        printf("Got %d VL and %d ports for equipment %s from %s for DEV%d\n", VLSize[i], PortSize[i], eq_name, filename, devn);
    }

    ret = DqAdv664CombineStructures(NUM_TBL,                        // number of VL and port tables, use (1L<<0) to free() original tables
                                    DQAR664_COMBINESTR_FREE_TABLES, //|DQAR664_COMBINESTR_INVERSE_SIM,
                                    pVL, VLSize,
                                    pPort, PortSize,
                                    &pInVL, &InVLSize,          // Combined  input VL and its size (allocate here)
                                    &pOutVL, &OutVLSize,        // Combined output VL and its size (allocate here)
                                    &pInPort, &InPortSize,      // Combined  input ports and size (allocate here)
                                    &pOutPort, &OutPortSize     // Combined output ports and size (allocate here)
                                    );
    printf("InVLSize=%d OutVLSize=%d InPortSize=%d OutPortSize=%d\ntotvl=%d totport=%d tvl=%d tpp=%d\n", 
            InVLSize, OutVLSize, InPortSize, OutPortSize, InVLSize + OutVLSize, InPortSize + OutPortSize, tvl, tpp);

    *ppInVL =       pInVL;
    *ppOutVL =      pOutVL;
    *ppInPort =     pInPort;
    *ppOutPort =    pOutPort;
    *pInVLSize =    InVLSize; 
    *pOutVLSize =   OutVLSize;
    *pInPortSize =  InPortSize; 
    *pOutPortSize = OutPortSize;

    return ret;
}



int config664_VLs_and_ports( int hd, int devn,
							 pAR664_VL_CFG pInVL, int InVLSize,
                             pAR664_VL_CFG pOutVL, int OutVLSize,
                             pAR664_PORT_CFG pInPort, int InPortSize,
                             pAR664_PORT_CFG pOutPort, int OutPortSize,
							 S664_HANDLES** pInHandles, S664_HANDLES** pOutHandles
) {
	int error_found = 0;
    int ret, i = 0, j = 0;

    pAR664_VL_CFG ptVL;
    pAR664_PORT_CFG ptPort;
    int VLSize, PortSize, is_output;
    S664_HANDLES* in_handles;
    S664_HANDLES* out_handles;
    S664_HANDLES* handles;
    int vl_idx;

	// Allocate space for handles needed by addVL and addPort functions
    in_handles = malloc((InVLSize)*sizeof(S664_HANDLES));     // Array of handles equal to the number of  input VLs
    memset(in_handles, 0, (InVLSize)*sizeof(S664_HANDLES));
    out_handles = malloc((OutVLSize)*sizeof(S664_HANDLES));   // Array of handles equal to the number of output VLs
    memset(out_handles, 0, (OutVLSize)*sizeof(S664_HANDLES));
	*pInHandles  = in_handles;
	*pOutHandles = out_handles;

	////
	// Create VLs and ports
	////
    Chk4Err(DqAdv664ClearConfig(hd, devn, AR664_VL_USE_A|AR664_VL_USE_B), goto finish_up);
	
	// foreach input and output VL:
	//  foreach VL, set enable flag and 664AddVL, which returns VL handle, for which we create a out/in_handles struct,
	//   foreach port of this VL, enable port, do 644AddPort to get port_handle and port_id
    for (is_output = 0; is_output < 2; is_output++) {
        
        switch(is_output) {
            case 0:
				#if (0==ENABLE_INPUT)
                            continue;
				#endif
                handles = in_handles;
                ptVL = pInVL;
                ptPort = pInPort;
                VLSize = InVLSize;
                PortSize = InPortSize;
                break;
            case 1:
				#if (0==ENABLE_OUTPUT)
                            continue;
				#endif
                handles = out_handles;
                ptVL = pOutVL;
                ptPort = pOutPort;
                VLSize = OutVLSize;
                PortSize = OutPortSize;
                break;
        }

        vl_idx = 0;
        for (i = 0; i < VLSize; i++) {
            int port_idx;

            if (0 == ptVL[i].VLID) continue;

            ptVL[i].mode |= AR664_VL_EN; // enable VL before adding it
            Chk4Err(DqAdv664AddVL(hd, devn, 
				AR664_VL_USE_A|AR664_VL_USE_B, 
				((ptVL[i].mode & AR664_VL_TX)!=0), &ptVL[i], &handles[vl_idx].vl_handle), goto finish_up);
            printf_ramlog("VL=%d #%d %s added with handle %s on dev=%d\n", ptVL[i].VLID, i, (ptVL[i].mode & AR664_VL_TX)?"TX":"RX", handle_string(handles[vl_idx].vl_handle), devn);
            handles[vl_idx].vlid = ptVL[i].VLID;

            // count the number of ports and allocate enough memory for handles
            port_idx = 0;
            for (j = 0; j < PortSize; j++) {
                if (ptVL[i].VLID == ptPort[j].VLID) {
                    port_idx++;
                }
            }
            
            handles[vl_idx].port_handle = malloc(port_idx*sizeof(int));
            handles[vl_idx].port_id = malloc(port_idx*sizeof(int));
            handles[vl_idx].n_ports = port_idx;
            port_idx = 0;

            // look up all ports that belong to this VL.
            for (j = 0; j < PortSize; j++) {
                if (ptVL[i].VLID == ptPort[j].VLID) {

                    // check for port types. "continue" lines to block some VL/Port type combinations from test
                    switch (ptPort[j].port_type) {
                        case AR664_PORT_SMPL:
                            if (ptPort[j].period) {
#if (0 == ENABLE_PERIODIC)
                            continue;
#endif
                            } else {
#if (0 == ENABLE_SAMPLING)
                            continue;
#endif
                            }
                            break;
                        case AR664_PORT_QUEUE:
#if (0 == ENABLE_QUEUEING)
                            continue;
#endif
                            break;
                        case AR664_PORT_SAP:
#if (0 == ENABLE_SAP)
                            continue;
#endif
                            break;
                    }

                    ptPort[j].mode |= AR664_PORT_EN; // enable port from the moment it is loaded

                    ret = DqAdv664AddPort(hd, devn, handles[vl_idx].vl_handle, &ptPort[j], &handles[vl_idx].port_handle[port_idx]);
                    handles[vl_idx].port_id[port_idx] = ptPort[j].portID;

                    printf_ramlog(" Port %d for VL %d #%d add handle %s vx=%d px=%d md=%x dev=%d\n", 
                                  ptPort[j].portID, ptPort[j].VLID, i, handle_string(handles[vl_idx].port_handle[port_idx]),
                                  vl_idx, port_idx, ptPort[j].port_type, devn);
                    port_idx++;
                    Chk4Err(ret, goto finish_up);
                    Sleep(10);  // to allow firmware to print debug message
                }
            }
            vl_idx++;
        }
    }

    // store some intial data into the output periodic ports - this needs to be done once
	// TODO: what happens if no data is stored?
#if ((1==ENABLE_OUTPUT)&&(1 == ENABLE_PERIODIC))

	// for each VL, for each port of the selected VL, if the port is sampling and periodic, get the port handle,
	// and set the message for the port corresponding to that handle
    for (i = 0; i < OutVLSize; i++) {
        int port_handle;
        uint8 message[2048];
        uint32 flags = 0;
        int written;
        uint32 stored, available, status;
        
        if (0 == pOutVL[i].VLID) continue;   // skip removed duplicate VLs

        for (j = 0; j < OutPortSize; j++) {
            if (pOutVL[i].VLID == pOutPort[j].VLID) {
                if (pOutPort[j].port_type == AR664_PORT_SMPL) {
                    if (pOutPort[j].period) {
                        // update periodic port with a message
                        port_handle = look_up_handle(out_handles, pOutPort[j].VLID, pOutPort[j].portID);
                        if (port_handle < 0) continue;
                        if (pOutPort[j].d_size > 1518) {
							printf("data size larger than 1518B: %d\n", pOutPort[j].d_size);
                        }

                        // fill the message with some data - we actually have description of all data in the A350 database file except for values
                        memset_alphabet(message, '\0', pOutPort[j].d_size);

                        ((uint16*)message)[0] = htons(pOutPort[j].VLID);
                        ((uint16*)message)[1] = htons(pOutPort[j].portID);

                        ret = DqAdv664SendMessage(hd, devn, port_handle, flags, pOutPort[j].d_size, message, &written, &stored, &available, &status);
                        printf_ramlog("Sent VL %d port %d period %d size %d\n", pOutPort[j].VLID, pOutPort[j].portID, pOutPort[j].period, written);

                        Chk4Err(ret, goto finish_up);
                    }
                }
            }
        }
    }
#endif

finish_up:
	printf_ramlog("----\n");
	return ret;
}

// store data into non-periodic ports, watch how often data needs to be stored
int execute664_output( int hd, int devn,
                       pAR664_VL_CFG pOutVL, int OutVLSize,
                       pAR664_PORT_CFG pOutPort, int OutPortSize,
                       S664_HANDLES* out_handles,
					   pS664_TEST_DATA pTestdata, int TestdataSize
) {
	int i, j, k;
	int ret = 0;
	static uint8 *sn = NULL;		// dynamic version of uint8[OutVLSize][OutPortSize]
	static uint32 gsn = 0;
	char sstr[8192]={0};

#ifndef REGION_ICMP_HEADER_CREATION
	UDP_ICMP_HDR icmp_hdr;
	AR664_HEADERS sap_header;

//	memset(&icmp_hdr,0,sizeof(UDP_ICMP_HDR));
//	memset(&sap_header,0,sizeof(AR664_HEADERS));

	icmp_hdr.icmp.type_code = AR664_ICMP_ECHO;	// subtype code
	icmp_hdr.icmp.chksum = 0;				// checksum
	icmp_hdr.icmp.id = 0;					// request ID
	icmp_hdr.icmp.seqno = 0;				// sequence number

	//sap_header.eth = 0;				// ETH_HEADER
	//sap_header.ip = 0;				// IP4_HEADER
	sap_header.icmp_udp = icmp_hdr;		// UDP_ICMP_HDR
	sap_header.fields = 0;				//AR664_SAP_ICMP_CODE|AR664_SAP_ICMP_CHKSUM|AR664_SAP_ICMP_ID|AR664_SAP_ICMP_SEQNO;
	sap_header.sn = 0;
	sap_header.timestamp = 0;
#endif

	if (sn == NULL)	// TODO: free() on cleanup
	{
		sn = malloc(OutVLSize * OutPortSize * sizeof(uint8));

		//memset(sn,0,OutVLSize*OutPortSize);
		for(i=0;i<OutVLSize*OutPortSize;i++)
			sn[i]=0;
	}
	
	// for each OutVL, get the handle
    //  for each port, get the handle
	//   664Sendmessage for (per|imm)sampling, queueing, (icmp|udp)SAP
    for (i = 0; i < OutVLSize; i++) {
            int port_handle, vl_handle;
            uint8 message[MAX_MSG_SZ];
            uint32 flags = 0;
            int written, t;
            uint32 stored, available, status;
			float pressure,speed;
			uint32 altitude;
			char *pPressure, *pAltitude, *pSpeed;

			uint32 *pUint32;
			uint32 networkOrder;


            if (0 == pOutVL[i].VLID) continue;   // skip removed duplicate VLs

            vl_handle = look_up_handle(out_handles, pOutVL[i].VLID, 0);

            // uncomment "continue" lines to block some VL/Port type combinations from test
            for (j = 0; j < OutPortSize; j++) {
                if (pOutVL[i].VLID == pOutPort[j].VLID) {
                    switch(pOutPort[j].port_type) {
                        case AR664_PORT_SMPL:
                            if (pOutPort[j].period) {
#if (0 == ENABLE_PERIODIC)
                            continue;
#endif
                            } else {
#if (0 == ENABLE_SAMPLING)
                            continue;
#endif
                            }
                            break;
                        case AR664_PORT_QUEUE:
#if (0 == ENABLE_QUEUEING)
                            continue;
#endif
                            break;
                        case AR664_PORT_SAP:
#if (0 == ENABLE_SAP)
                            continue;
#endif
                            break;
                    }

                    if (0 == pOutPort[j].portID) continue;

                    // update periodic port with a message
                    port_handle = look_up_handle(out_handles, pOutPort[j].VLID, pOutPort[j].portID);

                    if (port_handle < 0) continue;   // skip removed duplicate VLs

#if (CFG_TEST_MODE == 0)
					memset(message, '\0' ,pOutPort[j].d_size);

					//Quand on transmet on doit convertir de Host a Network Byte Order (hton)

					//Quand on recoit on doit convertir de network a host Byte Order (ntoh)

					if (pOutPort[j].VLID == 5653)
					{

						message[4] = 0x03;
						message[5] = 0x00;
						message[6] = 0x00;
						message[7] = 0x03;
					
						/*message[4] = 0x03;
						message[5] = 0x03;
						message[6] = 0x03;
						message[7] = 0x03;*/

						pressure =1013;
					
						//pPressure = (char *)&pressure;

						pUint32 = (uint32 *)&pressure;
						networkOrder = htonl(*pUint32);
						pPressure = (char *)&networkOrder;

#if USE_VARIABLE_PRESSURE
						message[28] = pPressure[0];
						message[29] = pPressure[1];
						message[30] = pPressure[2];
						message[31] = pPressure[3];
#else
						//Pressure (const)
						message[28] = 0x44;
						message[29] = 0x7B;
						message[30] = 0xFF;
						message[31] = 0xFF;
#endif


						//Le parametre speed a une valeur entre 0 et 1, si la valeur est > 1 la VL sera tout de meme acceptée par ECB, mais l'ECB mettra la valeur
						//a la valeur maximum possible i.e. la valeur 1
						speed = 0.72;
			

						pUint32 = (uint32 *)&speed;
						networkOrder = htonl(*pUint32);
						pSpeed = (char *)&networkOrder;
#if USE_VARIABLE_PRESSURE
						//speed
						message[40] = pSpeed[0];
						message[41] = pSpeed[1];
						message[42] = pSpeed[2];
						message[43] = pSpeed[3];
#else

						//speed
						message[40] = 0x3F;
						message[41] = 0x3B;
						message[42] = 0xFF;
						message[43] = 0xFF;
#endif

						message[64]= 0x03;
						message[65]= 0x03;
						message[67]= 0x03;
						//Temp1
						message[68] = 0x41;
						message[69] = 0xBB;
						message[70] = 0xFF;
						message[71] = 0xFF;
					
						//Temp2
						message[72] = 0x41;
						message[73] = 0xBB;
						message[74] = 0xFF;
						message[75] = 0xFF;

							
						
						altitude =21000;
					
						//pPressure = (char *)&pressure;

						pUint32 = (uint32 *)&altitude;
						networkOrder = htonl(*pUint32);
						pAltitude = (char *)&networkOrder;

#if USE_VARIABLE_PRESSURE
						message[84] = pAltitude[0];
						message[85] = pAltitude[1];
						message[86] = pAltitude[2];
						message[87] = pAltitude[3];

					
#else
						//Altitude
						message[84] = 0x00;
						message[85] = 0x00;
						//message[86] = 0xFF; SI ce byte a FF, L'ECB va rejeter l'information complete de la VL y compris pression, spped, ou temperature
						message[86] = 0x00;
						message[87] = 0x00;
#endif


					}
#elif (CFG_TEST_MODE == 1)
					memset_arincdbg(message, sn[OutVLSize*j]++, gsn++, pOutVL[i], pOutPort[j]);
                    //memset_alphabet(message, 'a', pOutPort[j].d_size);
#elif (CFG_TEST_MODE == 2)
                    switch(pOutPort[j].port_type) {
                        case AR664_PORT_SMPL:
                            memset_alphabet(message, 'a', pOutPort[j].d_size);
                            ((uint16*)message)[0] = htons(pOutPort[j].VLID);
                            ((uint16*)message)[1] = htons(pOutPort[j].portID);
                            if (pOutPort[j].period)
                                printf("SND W SPer VL %d port %d h=%x period %d size %d\n", pOutPort[j].VLID, pOutPort[j].portID, port_handle, pOutPort[j].period, written);
                            else
                                printf("SND W SImm VL %d port %d h=%x period %d size %d\n", pOutPort[j].VLID, pOutPort[j].portID, port_handle, pOutPort[j].period, written);
                            break;
                        case AR664_PORT_QUEUE:
                            memset_alphabet(message, 'a', MAX_MSG_SZ);
                            ((uint16*)message)[0] = htons(pOutPort[j].VLID);
                            ((uint16*)message)[1] = htons(pOutPort[j].portID);
                            printf("SND W Q VL %d port %d h=%x period %d size %d\n", pOutPort[j].VLID, pOutPort[j].portID, port_handle, pOutPort[j].period, written);
                            break;
                        case AR664_PORT_SAP:
                            memset_alphabet(message, 'a', MAX_MSG_SZ);
                            ((uint16*)message)[0] = htons(pOutPort[j].VLID);
                            ((uint16*)message)[1] = htons(pOutPort[j].portID);
                            printf("SND W SAP VL %d port %d h=%x period %d size %d\n", pOutPort[j].VLID, pOutPort[j].portID, port_handle, pOutPort[j].period, written);
                            break;

                    }
#endif

                    switch(pOutPort[j].port_type) {
                        case AR664_PORT_SMPL:
                        case AR664_PORT_QUEUE:
						case AR664_PORT_SAP:
							flags = 0;

							for(t=0;t<3;t++)
							{
								Chk4Err(DqAdv664SendMessage(hd, devn, port_handle, flags, pOutPort[j].d_size, message, &written, &stored, &available, &status),);

								if (ret<0) {
									printf_ramlog("Send error #%d%s on DEV%d for message={%s} as %d.\n",t,(t==0)?"(last)":"",devn,message,ret);
									Sleep(32);
								} else break;
							}
                            break;
                        //case AR664_PORT_SAP:
						case AR664_PORT_ICMP:
							flags = DQL_WR664_IMCP_DATA;
							ret = DqAdv664SendMessageHdr(hd, devn, port_handle, flags, pOutPort[j].d_size, message, &sap_header, &written, &stored, &available, &status);
                            break;
						default:
							printf("Port not supported.\n");
                    }

					#ifndef REGION_PORT_STATUS_DECODE
					if (status != 0)
					{
						sprintf(sstr,"=");
						if (AR664_PORT_EXEC & status)
							sprintf(sstr,"%sEXEC|",sstr);
						if (AR664_PORT_ERR & status)
							sprintf(sstr,"%sERR|",sstr);
						if (AR664_PORT_BUSY & status)
							sprintf(sstr,"%sBUSY|",sstr);
						if (AR664_PORT_OVF & status)
							sprintf(sstr,"%sOVF|",sstr);
						if (AR664_PORT_MEMERR & status)
							sprintf(sstr,"%sMEMERR|",sstr);
						if (AR664_PORT_ADDROK & status)
							sprintf(sstr,"%sADDROK|",sstr);
						if (AR664_PORT_READY & status)
							sprintf(sstr,"%sREADY|",sstr);
						if (AR664_PORT_JITTER & status)
							sprintf(sstr,"%sJITTER|",sstr);
						if (AR664_PORT_NEW_DATA & status)
							sprintf(sstr,"%sNEW_DATA|",sstr);
						if (AR664_PORT_NEW_DATA_OVR & status)
							sprintf(sstr,"%sNEW_DATA_OVR|",sstr);
						sstr[strlen(sstr)-1]=0;
					}
					#endif  REGION_PORT_STATUS_DECODE
					
                    //Sleep(1);  // to prevent ARINC-664 firmware debug prints to get lost on serial buffer overflow
                    Chk4Err(ret, return ret);

                    printf_ramlog("SND vl=%d p_id=%d v_h=%x p_h=%x p_mode=%x wrt=%d/%d avl=%d stat=%x%s data_snd=\"%0.115s\" SNDEND\n", 
                            pOutPort[j].VLID,  pOutPort[j].portID,
                            vl_handle, port_handle,
                            pOutPort[j].port_type,
							stored, written, available,
							status, sstr,
                            message);

					// for testing, find the test structure for this port pair and add the message for later comparison (optional)
					for(k=0; k<TestdataSize; k++)
					{
						if ((pTestdata[k].output_port)==&(pOutPort[j]))
						{
							memset(pTestdata[k].lastmsg,0,MAX_MSG_SZ);
							strcpy(pTestdata[k].lastmsg,message);
							if (written >= 0 && written <= 8192)
								pTestdata[k].lastmsg[written]='\0';	
							pTestdata[k].msglen=stored;
							QueryPerformanceCounter(&(pTestdata[k].sendtime));
						}
					}

                }
            }
        }

	return ret;
}




/**
 * Retrieve data from all input VL/Ports:
 *
 * for each VL
 * { for each port on that VL
 *   { feed port handle to 664RecvMessage 
 *     and log it to a file.
 * } }
 */
int execute664_input( int hd, int devn,
                       pAR664_VL_CFG pInVL, int InVLSize,
                       pAR664_PORT_CFG pInPort, int InPortSize,
                       S664_HANDLES* in_handles,
					   pS664_TEST_DATA pTestdata, int TestdataSize
) {
	int i;
    int m=0;
	int portnum = 0;
	int ret = 0;
	
    for (i = 0; i < InVLSize; i++)
	{
        int port_handle, vl_handle;
        uint8 message[8192] = {0xCC};
        int read=0;
		int msgvalid = FALSE;
		uint32 flags = 0;
        uint32 available, status;
		int t, match = 0;
		LARGE_INTEGER recvtime;
		AR664_HEADERS headers;

        if (0 == pInVL[i].VLID) continue;   // skip removed duplicate VLs

        vl_handle = look_up_handle(in_handles, pInVL[i].VLID, 0);

        // uncomment "continue" lines to block some VL/Port type combinations from test
        for (portnum = 0; portnum < InPortSize; portnum++) {
            if (pInVL[i].VLID == pInPort[portnum].VLID) {
                switch(pInPort[portnum].port_type) {
                    case AR664_PORT_SMPL:
                        if (pInPort[portnum].period) {
                #if (0 == ENABLE_PERIODIC)
                            continue;
                #endif
                        } else {
                #if (0 == ENABLE_SAMPLING)
                            continue;
                #endif
                        }
                        break;
                    case AR664_PORT_QUEUE:
                #if (0 == ENABLE_QUEUEING)
                        continue;
                #endif
                        break;
                    case AR664_PORT_SAP:
				#if (0 == ENABLE_SAP)
                        continue;
                #endif
                        break;
				}

                port_handle = look_up_handle(in_handles, pInPort[portnum].VLID, pInPort[portnum].portID);
                if (port_handle < 0) continue;

				do {
					memset(&headers, 0, sizeof(AR664_HEADERS));
					memset(message, 0, sizeof(message));

					switch(pInPort[portnum].port_type) {
						case AR664_PORT_QUEUE:
								for(t=0;t<3;t++)
								{
									Chk4Err(DqAdv664RecvMessage(hd, devn, port_handle, flags, sizeof(message), message, &read, &available, &status),);
									if (ret<0)
									{
										printf_ramlog("Receiver error #%d%s as %d on DEV%d for message={%s}.\n",t,(t==3)?"(last)":"",ret,devn,message);
										Sleep(1);
									} else break;
								}
							break;
						case AR664_PORT_SMPL:
						case AR664_PORT_SAP:
						case AR664_PORT_ICMP:
							memset(message,0,sizeof(message));
								for(t=0;t<3;t++)
								{
									DqAdv664RecvMessageHdr(hd, devn, port_handle, flags, sizeof(message), message, &read, &headers, &available, &status);
									Chk4Err(ret,);
									if (ret<0)
									{
										printf_ramlog("Receiver error #%d%s as %d on DEV%d for message={%s}.\n",t,(t==3)?"(last)":"",ret,devn,message);
										Sleep(1);
									} else break;
								}
							break;
					}
					QueryPerformanceCounter(&recvtime);

					compare664_input(vl_handle, port_handle, in_handles, &(pInPort[portnum]), message, read, &headers, available, status, recvtime, pTestdata, TestdataSize, (CFG_TEST_MODE==0));
				} while (available > 0 && available < 8192);
            }
        }
    }
    return 1;
}

// ----------------- main routine -----------------------------
//
int main(int argc, char* argv[]) { 
    int ret, error_found = 0;
	int hd[2] = {0,0};
    int i = 0, j = 0;
    int cycles = 1;
    DQRDCFG *DQRdCfg = NULL;

	// Common Structures
	pAR664_VL_CFG pInVL[NUM_DEV];
    int InVLSize[NUM_DEV];
    pAR664_VL_CFG pOutVL[NUM_DEV];
    int OutVLSize[NUM_DEV];    
    pAR664_PORT_CFG pInPort[NUM_DEV];
    int InPortSize[NUM_DEV];
    pAR664_PORT_CFG pOutPort[NUM_DEV];
    int OutPortSize[NUM_DEV];
	// VL&Port handles from AddVL & AddPort
    S664_HANDLES* in_handles[NUM_DEV];
    S664_HANDLES* out_handles[NUM_DEV];
	// Data to compare
	S664_TEST_DATA* pTestdata[NUM_DEV];
	int TestdataSize[NUM_DEV];
	
	// Initialize
	init_hpet();
    DqInitDAQLib(); 
    signal(SIGINT, handler);

	Chk4Err(init_cube(IOM_IPADDR_DEV1, &hd[0],&DQRdCfg), goto finish_up);

	#if (NUM_DEV == 2)
		if (strcmp(IOM_IPADDR_DEV1,IOM_IPADDR_DEV2) == 0)
			hd[1]=hd[0];
		else
			Chk4Err(init_cube(IOM_IPADDR_DEV2, &hd[1],&DQRdCfg), goto finish_up);
	#elif (NUM_DEV < 1 || NUM_DEV > 2)
		printf("Only one or two devices are supported by this test program.\n").
		exit(1);
	#endif

	for (i=0;i<NUM_DEV;i++) // Disable any running devices in case of previous bad exit
	 	Chk4Err(DqAdv664Enable(hd[i], (i==0)?DEVN1:DEVN2, FALSE), goto finish_up);
	
	for (i=0;i<NUM_DEV;i++)
	{
		// Populate ARINC-664 structures from the configuration file
		
		ret = build_common_structure((i==0)?"afdx_dev1_5_sam1":"afdx_dev2_5_sam1",
                                     (i==0)?DEVN1:DEVN2,
                                     &pInVL[i],    &InVLSize[i],
                                     &pOutVL[i],   &OutVLSize[i],
                                     &pInPort[i],  &InPortSize[i],
                                     &pOutPort[i], &OutPortSize[i]
                                     );
        Chk4Err(ret, goto finish_up);

		// Report the structures that were added
		print_common_structure(pInVL[i],    InVLSize[i],
							   pOutVL[i],   OutVLSize[i],
							   pInPort[i],  InPortSize[i],
							   pOutPort[i], OutPortSize[i]);
	}

	for (i=0;i<NUM_DEV;i++)
	{
		// Send to 664 device the structs containing configuration VLs and Ports
		ret = config664_VLs_and_ports(hd[i], (i==0)?DEVN1:DEVN2,
                                      pInVL[i],    InVLSize[i],
                                      pOutVL[i],   OutVLSize[i],
                                      pInPort[i],  InPortSize[i],
                                      pOutPort[i], OutPortSize[i],
                                      &in_handles[i], &out_handles[i]);
        Chk4Err(ret, goto finish_up);
	}

		for (i=0;i<NUM_DEV;i++)
		{
			#if (NUM_DEV == 2)
			 // Populate structs used to compare test data
				build_test_structure(pInPort[i],  InPortSize[i],
									 pOutPort[i==0?1:0], OutPortSize[i==0?1:0],
									 in_handles[i], out_handles[i],
									 &pTestdata[i==0?1:0], &TestdataSize[i==0?1:0]);
				print_test_structure(i, pTestdata[i==0?1:0], TestdataSize[i==0?1:0]);
			#else
				TestdataSize[i] = 0;
				pTestdata[i] = NULL;
			#endif
		}

    // Enable channels
	for (i=0;i<NUM_DEV;i++)
		ret = DqAdv664Enable(hd[i], (i==0)?DEVN1:DEVN2, AR664_VL_USE_A|AR664_VL_USE_B);

    while (!stop) {
        printf("== cycle %d ==\r", cycles++);
		printf_ramlog("== cycle %d ==\n", cycles);

		#if (1==ENABLE_OUTPUT)
			for (i=0;i<NUM_DEV;i++)
				if (OutVLSize[i] > 0 && OutPortSize[i] > 0)
				{
					Chk4Err(execute664_output(hd[i], (i==0)?DEVN1:DEVN2, 
						                      pOutVL[i], OutVLSize[i], pOutPort[i], OutPortSize[i], out_handles[i], 
											  pTestdata[i], TestdataSize[i])
											  ,goto handle_error);
//					for (j=0;j<2;j++)
//						print_bus_stats(hd[i],i,j,TRUE,TRUE,TRUE," TX");
				}
		#endif

        //Sleep(CFG_SLEEP_MSEC);		// sleep AT LEAST this many ms before receiving (should be long enough for all packets to transmit, so that they can be compared)
		Sleep(64);		// sleep AT LEAST this many ms before receiving (should be long enough for all packets to transmit, so that they can be compared)
		#if (1==ENABLE_INPUT)
			for (i=0;i<NUM_DEV;i++)
			{
				Chk4Err(execute664_input(hd[i], (i==0)?DEVN1:DEVN2,
				                         pInVL[i], InVLSize[i], pInPort[i], InPortSize[i], in_handles[i], 
										 pTestdata[i==0?1:0], TestdataSize[i==0?1:0])
										 ,goto handle_error);
//				for (j=0;j<2;j++)
//					print_bus_stats(hd[i],i,j,TRUE,TRUE,TRUE," RX");
			}
		#endif

		if (cycles > MAX_CYCLES)
			stop=1;
    }
handle_error:

	printf_ramlog("----\n");
	for (i=0;i<NUM_DEV;i++)
		for (j=0;j<2;j++)
			print_bus_stats(hd[i],i,j,FALSE,TRUE,TRUE," FINAL");

finish_up: 
	for (i=0;i<NUM_DEV;i++) {
		if (hd[i]) {
			Chk4Err(DqAdv664Enable(hd[i], (i==0)?DEVN1:DEVN2, 0), goto end_of_story);
		}
	}

	for (i=0;i<NUM_DEV;i++) {
		if (hd[i]) {
			#if (CFG_RESET_IOM)
				//ft DqCmdHwReset(hd[i]);
			#else
				DqCloseIOM(hd[i]);
			#endif
			hd[i]=0;
		}
	}

end_of_story:
	printf_ramlog("Sample664.exe done.\n");
	print_ramlog_length();
	save_ramlog("_results.txt");

	//if (diag) fclose(diag);
    DqCleanUpDAQLib();

    if (error_found) {
        return -1;
    } else {
        return 0;
    }
}
