//=================================================================================================
//
// NAME:    Sample664_helper.c
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
#include "Sample664_helper.h"
#include "Convert_AR664_cfg.h"

/* 
 * Creates a map of what output port on endsystem1 is
 * mapped to what input port on the opposite endsystem.
 *
 * Expects two endsystems, and 1 output port per input port, e.g.:
 * - Endsystem1: Out Port S VL:104.0 T:32 src:(0,1)-1-50014 dst:(0,3)-1-50034
 * - Endsystem2: In  Port S VL:104.0 T:32 src:(0,1)-1-0 dst:(0,3)-1-50034
 *
 */
int build_test_structure ( pAR664_PORT_CFG pInPort, int inPortSize,
                           pAR664_PORT_CFG pOutPort, int outPortSize,
						   S664_HANDLES* in_handles, S664_HANDLES* out_handles,
						   pS664_TEST_DATA* ppTestData, int* pTestdataSize
						   
) {
	pS664_TEST_DATA testdata = NULL;
	int i=0,j=0,size=0;

	*pTestdataSize = 0;
	*ppTestData = NULL;

	// Guess the maximum size of the structures
	size = min(inPortSize,outPortSize);	// because each Testdata corresponds to an output port
	if (size<= 0) {
		return 0;
	}

	// Allocate space for test data
	if (NULL == (*ppTestData = testdata = malloc((size)*sizeof(S664_TEST_DATA)))) return FALSE;
	memset(testdata, 0, (size)*sizeof(S664_TEST_DATA));

	// Match ports
	for (j=0;j<outPortSize;j++)
	{
		for (i=0;i<inPortSize;i++)
		{
			if ((pOutPort[j].VLID == pInPort[i].VLID) &&				// must be transported through the same VL from one endsys to the other
			    (pOutPort[j].endsys_dst == pInPort[i].endsys_dst) &&	// and destination endsys/part/udpport is the same
				(pOutPort[j].part_dst == pInPort[i].part_dst) &&
				(pOutPort[j].udp_dst_port == pInPort[i].udp_dst_port))
			{
				// they match, pair them
				if (testdata[j].output_port == NULL)
				{
					// unique key pair 1
					testdata[j].in_port_handle = look_up_handle(in_handles,pInPort[i].VLID,pInPort[i].portID);
					// unique key pair 2
					testdata[j].output_port = &pOutPort[j];
					testdata[j].input_port = &pInPort[i];
					(*pTestdataSize)++;
				}
				else // more than one input per output port is not implemented (drop the newest one)
					printf("WARNING: build_test_structure() is implemented to map only one input port to same output port, and it's already mapped - dropped [%d]->[%d] %d-%d-%d:%d.\n",
							i,j,pOutPort[j].VLID,pOutPort[j].endsys_dst,pOutPort[j].part_dst,pOutPort[j].udp_dst_port);
					
			}
		}
	}

	return 0;
}

void print_test_structure(int id, pS664_TEST_DATA pTestdata, int length)
{
	int i;

	printf("Test Data #%d size=%d:\n",id,length);

	for (i=0;i<length;i++)
	{
		if (pTestdata[i].input_port==NULL)
			return;

		printf("* in_vl:%d in_h:%d out_vl:%d len:%d str:\"%s\"\n",
               pTestdata[i].input_port->VLID,
			   pTestdata[i].in_port_handle,
               pTestdata[i].output_port->VLID,
               pTestdata[i].msglen,
			   pTestdata[i].lastmsg);
	}
}

/**
 * Initializes the cube
 */

int init_cube(char* ip, int* hd, pDQRDCFG* pDQRdCfg)
{
	int ret;
	size_t i;
	char str[4096];
	pDQRDCFG DQRdCfg;
    time_t now = time(0);
	DQWRRD32 wrrdcmd;
	uint32 data_rd[1];
	uint32 timeoutvalue = TIMEOUT_DELAY;
	
	// open communication with IOM and receive IOM crucial identification data
    if ((ret = DqOpenIOM(ip, DQ_UDP_DAQ_PORT, TIMEOUT_DELAY, hd, pDQRdCfg)) < 0) {
        printf("\nError In Initializing Communication with IOM.  ");
		DqTranslateError(ret);
        return -1;
    }
	DQRdCfg = *pDQRdCfg;                // DQRdCfg is just easier to read and use

    if (DQRdCfg == NULL) {
        printf("\nError in receiving the response for Echo Command");
        if (hd) {
          DqCloseIOM(*hd);
        }
        return -1;
    }

	// Print program start time
    strftime (str, sizeof(str)/sizeof(char), "Sample664.exe started at %Y-%m-%d %H:%M:%S.000\n", localtime (&now));

	// Print information about cube
    sprintf(str,"%sipaddr  = %d.%d.%d.%d\n", str,
                  (DQRdCfg->ipaddr & 0xff000000)>>24,
                  (DQRdCfg->ipaddr & 0x00ff0000)>>16,
                  (DQRdCfg->ipaddr & 0x0000ff00)>>8,
                  (DQRdCfg->ipaddr & 0x000000ff));

    sprintf(str,"%smodel   = %04x\n", str, DQRdCfg->model);
    sprintf(str,"%ssernum  = %07d\n", str, DQRdCfg->sernum);
    sprintf(str,"%smfgdate = %x/%x/%x\n", str, (DQRdCfg->mfgdate & 0xff000000)>>24, 
                                   (DQRdCfg->mfgdate & 0xff0000)>>16, 
                                   (DQRdCfg->mfgdate & 0xffff));
    sprintf(str,"%scaldate = %x/%x/%x\n", str,
                                   (DQRdCfg->caldate & 0xff000000)>>24, 
                                   (DQRdCfg->caldate & 0xff0000)>>16, 
                                   (DQRdCfg->caldate & 0xffff));

    for (i = 0; i < DQ_MAXDEVN; i++) {
        if (DQRdCfg->devmod[i]) {
			if (DQRdCfg->devmod[i] != 0)
			{
				wrrdcmd.dev = i;
				wrrdcmd.read_1st = 0;
				wrrdcmd.addr_w = wrrdcmd.size_w = 0;
				wrrdcmd.addr_r = 0x000C;
				wrrdcmd.size_r = 1;
				ret = DqCmdWriteRead32(*hd, &wrrdcmd, NULL, data_rd);

				sprintf(str,"%s%Dev%d  Model: %3x Option: %3x Logic: %X\n", str, i, DQRdCfg->devmod[i], DQRdCfg->option[i], data_rd[0]);
			}
			else
				sprintf(str,"%s%d: Model: %x Option: %x\n\n", str, i, DQRdCfg->devmod[i], DQRdCfg->option[i]);
        } else {
            break;
        }
    }

	if(DQRdCfg->model < 0x3000) {
		printf("This CPU has not been prepared for this example. Quitting.");
		exit(-1);
	}

	printf("%s\n",str);
	write_ramlog(str);

	return 0;
}

// ------------------ fill buffer with alphabet separated by numbers -------------------
void* memset_alphabet(void *s, int c, unsigned n) {
    unsigned i;
    char* dpb = (char*) s;
    char start;
    int num = '0';

    if (!isalpha(c)) return NULL;
    start = toupper(c);

	if ((s != NULL) && (n > 0))	{
        for (i = 0; i < n; i++) {
            dpb[i] = c++;
            if (c > 'Z') {
                c = 'A';
                dpb[++i] = num++;
                if (num > '9') num = '0';
            }
        }
	}
	return s;
}

// ------------------ fill buffer with 664 debug data separated by numbers -------------------
void* memset_arincdbg(void *s, uint8 sn, uint32 gsn, AR664_VL_CFG vl, AR664_PORT_CFG port) {
	uint32 i, n;
	char c = '0';
	char str[MAX_MSG_SZ] = {'0'};
	SYSTEMTIME time;
	GetSystemTime(&time);

	if (s == NULL)
		return s;

	//sprintf(str, "sn: %d, gsn: %d, time: %02dh%02dm%02d.%03d", sn, gsn, time.wHour, time.wMinute, time.wSecond, time.wMilliseconds);
	sprintf(str, "%d, %d, %02dh%02dm%02d.%03d", sn, gsn, time.wHour, time.wMinute, time.wSecond, time.wMilliseconds);

	//sprintf(str, "%s, VL:%d, v_mode:%x, v_m_s:%s%s%s%s%s%s, bag:%d, mtu:%d, mj:%d", str,
	sprintf(str, "%s, %d, %x, %s%s%s%s%s%s, %d, %d, %d", str,
			vl.VLID, vl.mode, 
            (vl.mode & AR664_VL_MULTI)?"M":"U",
			((vl.mode & AR664_VL_FRAG_EN)>>7 != vl.frag_en)?"?F":(vl.mode & AR664_VL_FRAG_EN)?"+F":"-F",
            ((vl.mode & AR664_VL_ICMP_EN)>>6 != vl.ICMP_en)?"?I":(vl.mode & AR664_VL_ICMP_EN)?"+I":"-I",
            (vl.mode & AR664_VL_INTGR_EN)?"+IC":"-IC",
            (vl.mode & AR664_VL_RED_MGM)?"+RM":"-RM",
            ((vl.mode & AR664_VL_DUAL)==AR664_VL_DUAL)?"AB":(vl.mode & AR664_VL_USE_B)?"BB":(vl.mode & AR664_VL_USE_A)?"AA":"--",
			vl.bag/1000, vl.lmax, vl.max_jitter);

	//sprintf(str, "%s, subVL:%d, p_mode:%x, p_m_s: %s, T:%d, src:(%d:%d)-%d-%d, dst:(%d:%d)-%d-%d, len:%d, q:%d", str, 
	sprintf(str, "%s, %d, %x, %s, %d, (%d:%d)-%d-%d, (%d:%d)-%d-%d, %d, %d; ", str, 
		port.subvl_id, port.port_type, 
		((port.port_type & AR664_PORT_SMPL)>0)?"S":((port.port_type & AR664_PORT_QUEUE)>0)?"Q":((port.port_type & AR664_PORT_SAP)>0)?"SAP":"?", 
		port.period/1000, 
		(port.endsys_src >> 8) & 0xff, (port.endsys_src >> 0) & 0xff, port.part_src, port.udp_src_port,  // src: (netid:eqid)part-port
		(port.endsys_dst >> 8) & 0xff, (port.endsys_dst >> 0) & 0xff, port.part_dst, port.udp_dst_port,  // dst: (netid:eqid)part-port
		port.d_size, port.depth);

	//sprintf(str, "%s, junkdata:", str);

	n = strlen(str);
	
	for (i=0; n<sizeof(str); n++)
	{
		if (i<100)
			str[n]='0'+(i++/10);
		else if (i<((10+26)*10))
			str[n]='A'+((i++-100)/10);
		else if (i<((10+26+26)*10))
			str[n]='a'+((i++-360)/10);
		else
		{
			i=0;
			str[n]='0'+(i++/10);
		}
	}
	str[8190]=']';
	str[8191]='\0';
	if(port.d_size>2)
	{
		str[port.d_size-2]=']';
		str[port.d_size-1]='\0';
	}

	// finish up
	strcpy(s,str);

	return s;
}

uint32 ip_string_to_uint32(char* ip_addr) {
    uint8 a, b, c, d;
    sscanf(ip_addr, "%hhu.%hhu.%hhu.%hhu", &a, &b, &c, &d );
    return ( a << 24 ) | ( b << 16 ) | ( c << 8 ) | d;
}

// Simple search - having VLIDs sorted and perform golden ratio search would speed things up
// It is assumed that the last VLID is zero in the table
// If <portid> is zero return handle to VL otherwise return a handle to the port
int look_up_handle(S664_HANDLES* handle_table, int vlid, int portid) {
    int i, idx = 0;
    while (handle_table[idx].vlid) {
        if (vlid == handle_table[idx].vlid)
            if (0 == portid)
                return handle_table[idx].vl_handle;
            else
                for (i = 0; i < handle_table[idx].n_ports; i++)
                    if (handle_table[idx].port_id[i] == portid)
                        return handle_table[idx].port_handle[i];
        idx++;
    }
    return -1;
}

// Non-reenterable
char* handle_string(uint16 handle) {
    static char str[64];
    
    sprintf(str, "%s %s %s%s %d %x",
            (handle & AR664_HANDLE_VL)?"VL":"PRT",
            (handle & AR664_HANDLE_TX)?"TX":"RX",
            (handle & AR664_HANDLE_A)?"A":"",
            (handle & AR664_HANDLE_B)?"B":"",
            (handle & (DQ_AR664_VL_ARR_SZ-1)),
            handle);

    return str;
}

void print_common_structure( pAR664_VL_CFG pInVL, int InVLSize,
                             pAR664_VL_CFG pOutVL, int OutVLSize,
                             pAR664_PORT_CFG pInPort, int InPortSize,
                             pAR664_PORT_CFG pOutPort, int OutPortSize
) {
	int i, j;

	char str[15240] = "";

	// Screen output
    for (i = 0; i < OutVLSize; i++)
        sprintf(str, "%sOut VL:%d mode:%x\n", str, pOutVL[i].VLID, pOutVL[i].mode);
    for (i = 0; i < InVLSize; i++)
        sprintf(str, "%sIn VL:%d mode:%x\n", str, pInVL[i].VLID, pInVL[i].mode);
	puts(str);
	sprintf(str,"");

	// File output
    for (i = 0; i < OutVLSize; i++)
	{
		int mode = pOutVL[i].mode;
		sprintf(str, "%sOut VL:%d mode:%x bag:%d mtu:%d max_jitter:%d\n", str, pOutVL[i].VLID, mode, pOutVL[i].bag/1000, pOutVL[i].lmax, pOutVL[i].max_jitter);
		sprintf(str, "%s  * VL mode:%s %s %s %s(%d%s) %s(%d%s) %s %s %s\n", str, 
                     (mode & AR664_VL_EN)?"EN":"",
                     (mode & AR664_VL_TX)?"TX":"RX",
					 (mode & AR664_VL_MULTI)?"MULTICAST":"  UNICAST",
					 (mode & AR664_VL_FRAG_EN)?"   FRAG":"NO_FRAG", pOutVL[i].frag_en, ((mode & AR664_VL_FRAG_EN)>>7 != pOutVL[i].frag_en)?"!":"",
					 (mode & AR664_VL_ICMP_EN)?"   ICMP":"NO_ICMP", pOutVL[i].ICMP_en, ((mode & AR664_VL_ICMP_EN)>>6 != pOutVL[i].ICMP_en)?"!":"",
					 (mode & AR664_VL_INTGR_EN)?"   IC":"NO_IC",
					 (mode & AR664_VL_RED_MGM)?"   RM":"NO_RM",
                     ((mode & AR664_VL_DUAL)==AR664_VL_DUAL)?"A&B":(mode & AR664_VL_USE_B)?"B":(mode & AR664_VL_USE_A)?"A":"NO_BUS!"
					 );

		// Output UDP ports for those VLs
		for (j = 0; j < OutPortSize; j++)
		{
			if (pOutVL[i].VLID == pOutPort[j].VLID)
			{
				sprintf(str, "%s  * Out Port %s VL:%d.%d T:%d src:(%d,%d)-%d-%d dst:(%d,%d)-%d-%d len:%d msg:%d [10.%d.%d.%d:%d -> 10.%d.%d.%d:%d]\n", str, 
				((pOutPort[j].port_type & AR664_PORT_SMPL)>0)?"S":((pOutPort[j].port_type & AR664_PORT_QUEUE)>0)?"Q":((pOutPort[j].port_type & AR664_PORT_SAP)>0)?"SAP":"?", 
				pOutPort[j].VLID, pOutPort[j].subvl_id, pOutPort[j].period/1000, 
				(pOutPort[j].endsys_src >> 8) & 0xff, (pOutPort[j].endsys_src >> 0) & 0xff, pOutPort[j].part_src, pOutPort[j].udp_src_port, // (netid:eqid)part-port
				(pOutPort[j].endsys_dst >> 8) & 0xff, (pOutPort[j].endsys_dst >> 0) & 0xff, pOutPort[j].part_dst, pOutPort[j].udp_dst_port,
				pOutPort[j].d_size, pOutPort[j].depth,
				(pOutPort[j].endsys_src >> 8) & 0xff, (pOutPort[j].endsys_src >> 0) & 0xff, pOutPort[j].part_src, pOutPort[j].udp_src_port,
				(pOutPort[j].endsys_dst >> 8) & 0xff, (pOutPort[j].endsys_dst >> 0) & 0xff, pOutPort[j].part_dst, pOutPort[j].udp_dst_port);

				if (((pOutPort[j].port_type & AR664_PORT_SMPL)==1)&&(pOutVL[i].mode & AR664_VL_FRAG_EN))
					sprintf(str, "%s    Warning: VL%d port cannot support fragmentation (sampling must truncate)!\n", str, pOutPort[j].VLID);

				if (((pOutPort[j].port_type & AR664_PORT_SMPL)==1)&&(pOutVL[i].mode & AR664_VL_FRAG_EN))
					sprintf(str, "%s    Warning: VL%d port cannot support fragmentation (sampling must truncate)!\n", str, pOutPort[j].VLID);
			}
		}

		// Input UDP ports for those VLs (this should never print, since VLs are unidirectional)
		for (j = 0; j < InPortSize; j++)
		{
			if (pOutVL[i].VLID == pInPort[j].VLID)
			{
				sprintf(str, "%s  * In  Port %s VL:%d.%d T:%d src:(%d,%d)-%d-%d dst:(%d,%d)-%d-%d len:%d msg:%d\n", str, 
				((pInPort[j].port_type & AR664_PORT_SMPL)>0)?"S":((pInPort[j].port_type & AR664_PORT_QUEUE)>0)?"Q":((pInPort[j].port_type & AR664_PORT_SAP)>0)?"SAP":"?", 
				pInPort[j].VLID, pInPort[j].subvl_id, pInPort[j].period/1000, 
				(pInPort[j].endsys_src >> 8) & 0xff, (pInPort[j].endsys_src >> 0) & 0xff, pInPort[j].part_src, pInPort[j].udp_src_port,	    // (netid:eqid)part-port
				(pInPort[j].endsys_dst >> 8) & 0xff, (pInPort[j].endsys_dst >> 0) & 0xff, pInPort[j].part_dst, pInPort[j].udp_dst_port,
				pInPort[j].d_size, pInPort[j].depth);

				if (((pInPort[j].port_type & AR664_PORT_SMPL)==1)&&(pOutVL[i].mode & AR664_VL_FRAG_EN))
					sprintf(str, "%s    Warning: VL%d port cannot support fragmentation (sampling must truncate)!\n", str, pInPort[j].VLID);
			}
		}

		sprintf(str,"%s\n",str);
	}

	// Input
    for (i = 0; i < InVLSize; i++)
	{
		int mode = pInVL[i].mode;
		sprintf(str, "%sIn  VL:%d mode:%x bag:%d mtu:%d max_jitter:%d\n", str, pInVL[i].VLID, mode, pInVL[i].bag/1000, pInVL[i].lmax, pInVL[i].max_jitter);
		sprintf(str, "%s  * VL mode:%s %s %s %s(%d%s) %s(%d%s) %s %s %s\n", str, 
                     ((mode & AR664_VL_EN)>0)?"EN":"",
                     ((mode & AR664_VL_TX)>0)?"TX":"RX",
					 ((mode & AR664_VL_MULTI)>0)?"MULTICAST":"  UNICAST",
					 ((mode & AR664_VL_FRAG_EN)>0)?"   FRAG":"NO_FRAG", pInVL[i].frag_en, ((mode & AR664_VL_FRAG_EN)>>7 != pInVL[i].frag_en)?"!":"", 
					 ((mode & AR664_VL_ICMP_EN)>0)?"   ICMP":"NO_ICMP", pInVL[i].ICMP_en, ((mode & AR664_VL_ICMP_EN)>>6 != pInVL[i].ICMP_en)?"!":"",
					 ((mode & AR664_VL_INTGR_EN)>0)?"   IC":"NO_IC",
					 ((mode & AR664_VL_RED_MGM)>0)?"   RM":"NO_RM",
                     ((mode & AR664_VL_DUAL)==AR664_VL_DUAL)?"A&B":((mode & AR664_VL_USE_B)>0)?"B":((mode & AR664_VL_USE_A)>0)?"A":"NO_BUS!"
					 );

		// Output UDP ports for those VLs (shouldn't print since VLs are unidirectional)
		for (j = 0; j < OutPortSize; j++)
		{
			if (pInVL[i].VLID == pOutPort[j].VLID)
			{
				sprintf(str, "%s  * Out Port %s VL:%d.%d T:%d src:(%d,%d)-%d-%d dst:(%d,%d)-%d-%d len:%d msg:%d [10.%d.%d.%d:%d -> 10.%d.%d.%d:%d]\n", str, 
				((pOutPort[j].port_type & AR664_PORT_SMPL)>0)?"S":((pOutPort[j].port_type & AR664_PORT_QUEUE)>0)?"Q":((pOutPort[j].port_type & AR664_PORT_SAP)>0)?"SAP":"?", 
				pOutPort[j].VLID, pOutPort[j].subvl_id, pOutPort[j].period/1000, 
				(pOutPort[j].endsys_src >> 8) & 0xff, (pOutPort[j].endsys_src >> 0) & 0xff, pOutPort[j].part_src, pOutPort[j].udp_src_port,
				(pOutPort[j].endsys_dst >> 8) & 0xff, (pOutPort[j].endsys_dst >> 0) & 0xff, pOutPort[j].part_dst, pOutPort[j].udp_dst_port,
				pOutPort[j].d_size, pOutPort[j].depth);
			}
		}

		// Input UDP ports for those VLs
		for (j = 0; j < InPortSize; j++)
		{
			if (pInVL[i].VLID == pInPort[j].VLID)
			{
				sprintf(str, "%s  * In  Port %s VL:%d.%d T:%d src:(%d,%d)-%d-%d dst:(%d,%d)-%d-%d len:%d msg:%d \n", str, 
				((pInPort[j].port_type & AR664_PORT_SMPL)>0)?"S":((pInPort[j].port_type & AR664_PORT_QUEUE)>0)?"Q":((pInPort[j].port_type & AR664_PORT_SAP)>0)?"SAP":"?", 
				pInPort[j].VLID, pInPort[j].subvl_id, pInPort[j].period/1000, 
				(pInPort[j].endsys_src >> 8) & 0xff, (pInPort[j].endsys_src >> 0) & 0xff, pInPort[j].part_src, pInPort[j].udp_src_port,
				(pInPort[j].endsys_dst >> 8) & 0xff, (pInPort[j].endsys_dst >> 0) & 0xff, pInPort[j].part_dst, pInPort[j].udp_dst_port,
				pInPort[j].d_size, pInPort[j].depth);
			}
		}

		sprintf(str,"%s\n",str);
	}

	write_ramlog(str);

	sprintf(str,""); // clear
}

void compare664_input(int vl_handle, int port_handle, S664_HANDLES* in_handles, pAR664_PORT_CFG pInPort, uint8* message, int rcvlen, 
					  pAR664_HEADERS headers, uint32 available, uint32 status, LARGE_INTEGER recvtime, pS664_TEST_DATA pTestdata, int TestdataSize,
					  int printHexBits)
{
	const int j=0;
	int k;
	int hasmatch    = FALSE;
	int msgvaliddat = FALSE;
	int msgvalidlen = FALSE;
	int msgiszero = TRUE;
	int msgnodata = TRUE;
	int msglen = 0;
	int lastpktlen = 0;
	LONGLONG timediff;
	char outstr[65536]={0};
	char hstr[8192]={0};
	char sstr[8192]={0};
	int index, len;

				#ifndef REGION_HEADER_DECODE	// Collapse this region, not important
				if (headers->fields) {
					sprintf(hstr,"[Header ts:%d sn:%d crc:%x",headers->timestamp,headers->sn,headers->eth_CRC);
					sprintf(hstr,"%s ETH[",hstr);
					if (!(AR664_SAP_ETH_TYPE & headers->fields)) sprintf(hstr,"%s noETH_TYPEflag:",hstr);
						sprintf(hstr,"%sethtype:0x%x(%s) ",hstr,headers->eth.type,(headers->eth.type==0x800)?"(IP)":"");
					if (!(AR664_SAP_ETH_MAC & headers->fields)) sprintf(hstr,"%s noETH_MACflag:",hstr);
					sprintf(hstr,"%ssrcmac:%02X:%02X:%02X:%02X:%02X:%02X dstmac:%02X:%02X:%02X:%02X:%02X:%02X",hstr,
							headers->eth.src[0],headers->eth.src[1],headers->eth.src[2],
							headers->eth.src[3],headers->eth.src[4],headers->eth.src[5],
							headers->eth.dst[0],headers->eth.dst[1],headers->eth.dst[2],
							headers->eth.dst[3],headers->eth.dst[4],headers->eth.dst[5]);
					sprintf(hstr,"%s]",hstr);
					sprintf(hstr,"%s IP[",hstr);
					if (!(AR664_SAP_IP_ADDR & headers->fields)) sprintf(hstr,"%snoIP_ADDRflag:",hstr);
						sprintf(hstr,"%ssrcip:%d.%d.%d.%d dstip:%d.%d.%d.%d ",hstr,
							(headers->ip.src>>24)&0xFF,(headers->ip.src>>16)&0xFF,
							(headers->ip.src>>8)&0xFF,(headers->ip.src)&0xFF,
							(headers->ip.dst>>24)&0xFF,(headers->ip.dst>>16)&0xFF,
							(headers->ip.dst>>8)&0xFF,(headers->ip.dst)&0xFF);
					if (!(AR664_SAP_IP_TTLPROT & headers->fields)) sprintf(hstr,"%s noIP_TTLPROTflag:",hstr);
						sprintf(hstr,"%sTTL:%d PROT:%d%s ",hstr,headers->ip.ttl_proto >> 8, 
							headers->ip.ttl_proto&0xFF, ((headers->ip.ttl_proto&0xFF)==AR664_IP_PROTO_UDP)?"(UDP)":"");
					if (!(AR664_SAP_IP_DSF & headers->fields)) sprintf(hstr,"%s noIP_DSFflag:",hstr);
						sprintf(hstr,"%sDSF:0x%x ",hstr,headers->ip.ttl_proto);
					if (!(AR664_SAP_IP_CHKSUM & headers->fields)) sprintf(hstr,"%s noIP_CHKSUMflag:",hstr);
						sprintf(hstr,"%schk=0x%x ",hstr,headers->ip.chksum);
					if (!(AR664_SAP_IP_OFFS & headers->fields)) sprintf(hstr,"%s noIP_OFFSflag:",hstr);
						sprintf(hstr,"%soffset=0x%x ",hstr,headers->ip.offset);
					if (!(AR664_SAP_IP_ID & headers->fields)) sprintf(hstr,"%s noIP_IDflag:",hstr);
						sprintf(hstr,"%sid=0x%x ",hstr,headers->ip.id);
					if (!(AR664_SAP_IP_LEN & headers->fields)) sprintf(hstr,"%s noIP_LENflag:",hstr);
						sprintf(hstr,"%sLEN=%dB ",hstr,headers->ip.len);

					if ((AR664_SAP_IP_TTLPROT & headers->fields) && ((headers->ip.ttl_proto&0xFF)==AR664_IP_PROTO_UDP))
					{
						sprintf(hstr,"%s UDP[",hstr);
						if (AR664_SAP_UDP_CHKSUM & headers->fields)
						sprintf(hstr,"%schk=0x%x ",hstr,headers->icmp_udp.udp.chksum);
						if (!(AR664_SAP_UDP_PORTS & headers->fields)) sprintf(hstr,"%snoUDP_PORTSflag:",hstr);
						sprintf(hstr,"%ssrc=%d dst=%d ",hstr,headers->icmp_udp.udp.src, headers->icmp_udp.udp.dst);
						sprintf(hstr,"%slen=%d ",hstr,headers->icmp_udp.udp.len);
						sprintf(hstr,"%s(data=%dB)",hstr,headers->icmp_udp.udp.len - AR664_UDP_HDR_SZ);
						sprintf(hstr,"%s]]",hstr);
					}
					if ((AR664_SAP_IP_TTLPROT & headers->fields) && ((headers->ip.ttl_proto&0xFF)==AR664_IP_PROTO_ICMP))
					{
						sprintf(hstr,"%s ICMP[",hstr);
						if (AR664_SAP_ICMP_CODE & headers->fields)
						{
							sprintf(hstr,"%stype=%x ",hstr,headers->icmp_udp.icmp.type_code);
							if (headers->icmp_udp.icmp.type_code == AR664_ICMP_ECHO)
								sprintf(hstr,"%s(echo)",hstr);
							if (headers->icmp_udp.icmp.type_code == AR664_ICMP_ER)
								sprintf(hstr,"%s(echo reply)",hstr);
						}
						if (AR664_SAP_ICMP_CHKSUM & headers->fields)
							sprintf(hstr,"%schk=%x ",hstr,headers->icmp_udp.icmp.chksum);
						if (AR664_SAP_ICMP_ID & headers->fields)
							sprintf(hstr,"%sid=%x ",hstr,headers->icmp_udp.icmp.id);
						if (AR664_SAP_ICMP_SEQNO & headers->fields)
							sprintf(hstr,"%ssn=%x ",hstr,headers->icmp_udp.icmp.seqno);
						sprintf(hstr,"%s]",hstr);
					}
				}
				#endif  REGION_HEADER_DECODE

				#ifndef REGION_PORT_STATUS_DECODE
					if (status != 0)
					{
						sprintf(sstr,"%s=",sstr);
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

				// TEST & COMPARE
                for(k = 0; k < rcvlen; k++)
				{
					if (message[k] != 0x00)
						msgiszero = FALSE;
					if (message[j] != 0xCC || message[j] != 0x00)
						msgnodata = FALSE;
				}

                for(k=0;k<TestdataSize;k++) // iterate through test-struct, search for input ports that match, and compare results
                {
					if (((pTestdata[k].input_port)==&(pInPort[j])) && (pTestdata[k].in_port_handle == port_handle))
					{
						hasmatch = TRUE;
						msgvaliddat = (strncmp(pTestdata[k].lastmsg,message,rcvlen) == 0);
						msgvalidlen = (pTestdata[k].msglen == rcvlen);
						msglen = pTestdata[k].msglen;

						// alternate validation for sampling ports using the length of last packet received
						if ((msgvaliddat == FALSE || msgvalidlen == FALSE) && (pInPort[j].port_type == AR664_PORT_SMPL) && (headers->fields))
						{
							lastpktlen = headers->icmp_udp.udp.len - AR664_UDP_HDR_SZ;
							if (lastpktlen > 0 && lastpktlen < 8192)
							{
								msgvaliddat = (strncmp(pTestdata[k].lastmsg,message,lastpktlen) == 0);
								msgvalidlen = (pTestdata[k].msglen == lastpktlen);
							}
						}

						timediff = udiff(pTestdata[k].sendtime,recvtime);	// time difference between send and receive subroutines

						if (msgvaliddat == TRUE && msgvalidlen == TRUE)
						{
							sprintf(outstr,"RCV vl=%d port=%d v_h=%x p_h=%x p_m=%x avl=%d stat=%x%s t=%0.3fms PASS data_rcv[%d]=\"%0.115s\"", 
                        		pInPort[j].VLID,  pInPort[j].portID,
                        		vl_handle, port_handle,
								pInPort[j].port_type,
                                available, status, sstr,
								(float)timediff/1000,
								rcvlen,message);
						}
						else if (msgnodata)
						{
							sprintf(outstr,"RCV vl=%d port=%d v_h=%x p_h=%x p_m=%x avl=%d stat=%x%s t=%0.3fms NO_DATA data_rcv[rcvlen=%d]=\"%s\"", 
                        		pInPort[j].VLID,  pInPort[j].portID,
                        		vl_handle, port_handle,
								pInPort[j].port_type,
                                available, status, sstr,
								(float)timediff/1000,
								rcvlen,message);
						}
						else if (msgvaliddat == FALSE && msgvalidlen == FALSE)
						{
							sprintf(outstr,"RCV vl=%d port=%d v_h=%x p_h=%x p_m=%x avl=%d stat=%x%s t=%0.3fms FAIL-BOTH!\n data_snd[sndlen=%d]=\"%s\"!=\n data_rcv[rcvlen=%d]=\"%s\"", 
                        		pInPort[j].VLID,  pInPort[j].portID,
                        		vl_handle, port_handle,
								pInPort[j].port_type,
                                available, status, sstr,
								(float)timediff/1000,
								pTestdata[k].msglen, pTestdata[k].lastmsg,
								rcvlen,message);
						}
						else if (msgvaliddat == FALSE && msgvalidlen == TRUE)
						{
							sprintf(outstr,"RCV vl=%d port=%d v_h=%x p_h=%x p_m=%x avl=%d stat=%x%s t=%0.3fms FAIL-CONTENT!\n data_snd[sndlen=%d]=\"%s\"!=\n data_rcv[rcvlen=%d]=\"%s\"", 
                        		pInPort[j].VLID,  pInPort[j].portID,
                        		vl_handle, port_handle,
								pInPort[j].port_type,
                                available, status, sstr,
								(float)timediff/1000,
								pTestdata[k].msglen,pTestdata[k].lastmsg,
								rcvlen,message);
						}
						else if (msgvaliddat == TRUE && msgvalidlen == FALSE)
						{
							if (rcvlen)
								sprintf(outstr,"RCV vl=%d port=%d v_h=%x p_h=%x p_m=%x avl=%d stat=%x%s t=%0.3fms FAIL-LEN!\n data_rcv[rcvlen=%d]=\"%0.115s\" ", 
                        			pInPort[j].VLID,  pInPort[j].portID,
                        			vl_handle, port_handle,
									pInPort[j].port_type,
									available, status, sstr,
									(float)timediff/1000,
									rcvlen,message);
						}

						if ((printHexBits != FALSE) && (msgiszero == FALSE))
						{
							len = sprintf(outstr,"%s={", outstr);
							for(index = 0; index < rcvlen; index++)
							{
								len+= sprintf(&outstr[len],"%x ",message[index]);
							}
							sprintf(outstr,"%s}",outstr);
						}

						printf_ramlog("%s%s RCVEND\n",outstr,hstr);
					}
                }

				if (strlen(outstr) == 0)
				{
					len = sprintf(outstr,"RCV vl=%d port=%d v_h=%x p_h=%x p_m=%x avl=%d stat=%x%s data_rcv[%d]=\"%s\"=", 
        				pInPort[j].VLID,  pInPort[j].portID,
        				vl_handle, port_handle,
						pInPort[j].port_type,
						available, status, sstr,
						rcvlen,
						message);
					if ( ((AR664_SAP_IP_TTLPROT & headers->fields) &&
						 ((headers->ip.ttl_proto&0xFF)==AR664_IP_PROTO_UDP)) &&
						 (headers->icmp_udp.udp.len > 8) )
					{
						rcvlen = headers->icmp_udp.udp.len - 8;	// length from header
					}
					for(index = 0; index < rcvlen; index++)
					{
						len+= sprintf(&outstr[len],"%x",message[index]);
					}

					if (status != 0)
						printf_ramlog("%s%s RCVEND\n",outstr,hstr);

					if (status == 0)
					{
						printf("                 (no data)\r");
						return;
					}
				}

				if (!(msgvaliddat&&msgvalidlen))
				{
					switch(pInPort[j].port_type) {
						case AR664_PORT_SMPL:
							printf("R SMP VL=%d port=%d per=%d stat=%x read=%d", pInPort[j].VLID, pInPort[j].portID, pInPort[j].period, status, rcvlen);
							break;
						case AR664_PORT_QUEUE:
							printf("R Q VL=%d port=%d stat=%x read=%d", pInPort[j].VLID, pInPort[j].portID, status, rcvlen);
							break;
						case AR664_PORT_SAP:
							printf("R SAP VL=%d port=%d stat=%x read=%d", pInPort[j].VLID, pInPort[j].portID, status, rcvlen);
							break;
						case AR664_PORT_ICMP:
							printf("R ICMP VL=%d port=%d stat=%x read=%d", pInPort[j].VLID, pInPort[j].portID, status, rcvlen);
							break;
					}

					if (hasmatch == TRUE)
						 printf(" (%d sent)",msglen);

					if (msglen == 0)
						printf("\n");
					else if (hasmatch==FALSE && TestdataSize > 0)
						printf(" no_test_match\n");	// no matching test structure could be found
					else if (msgiszero)
						printf(" msg_is_zeros!\n");
					else if (msgnodata)
						printf(" msg_is_empty(0xCC)!\n");
					else if (msgvaliddat&&msgvalidlen)
						printf(" good_msg\n");
					else if (!(msgvaliddat||msgvalidlen)&&(TestdataSize > 0)&&(msglen==0))
						printf("stale!\n");
					else
					{
						if (!msgvalidlen)
							printf(" bad_len");
						if (!msgvaliddat)
						{
							printf(" bad_msg_dat=\"");
							for (k=0;(k<rcvlen && k<13);k++)
							{
								printf("%c", message[k]);
							}
							printf("\"...");
						}
						printf("\n");
					}
				}
}

/**
 * Helper function that prints bus log info to either
 * * ramlog, or
 * * printf (only in the case that there's an error)
 */
void print_bus_stats(int hd, int devn, int bus, int toScreen, int toLog, int clear, char* prelog)
{
	char str[8192]={0};
	AR664_BUS_STAT BusStat;
	DqAdv664GetBusStat(hd, devn, bus, &BusStat, clear);

		sprintf(str,"");
		
		if (BusStat.pkts_snd)
			sprintf(str,"%ssnd[%d = s:%d q:%d a:%d ovf:%d] ",str,BusStat.pkts_snd, BusStat.pkts_snd_s, BusStat.pkts_snd_q, BusStat.pkts_snd_sap, BusStat.buf_snd_ovf);
		if (BusStat.pkts_rcv)
			sprintf(str,"%srcv[%d = s:%d q:%d a:%d ovf:%d] ",str,BusStat.pkts_rcv, BusStat.pkts_rcv_s, BusStat.pkts_rcv_q, BusStat.pkts_rcv_sap, BusStat.buf_rcv_ovf);
		if (BusStat.bus_not_match + BusStat.bus_dis+BusStat.vl_dis+BusStat.port_dis+BusStat.link_err)
			sprintf(str,"%sMAC[bm:%d bd:%d vl:%d po:%d ln:%d]",str,BusStat.bus_not_match, BusStat.bus_dis, BusStat.vl_dis, BusStat.port_dis, BusStat.link_err);
		if (BusStat.ip_hdr_err+BusStat.ip_addr_err+BusStat.ip_chksum+BusStat.ip_frag_err+BusStat.ip_too_big)
			sprintf(str,"%sIP[h:%d a:%d c:%d l:%d frag:%d]",str,BusStat.ip_hdr_err, BusStat.ip_addr_err, BusStat.ip_chksum, BusStat.ip_too_big, BusStat.ip_frag_err);
		if (BusStat.udp_port_err+BusStat.udp_chksum)
			sprintf(str,"%sUDP[ports:%d crc:%d] ",str,BusStat.udp_port_err, BusStat.udp_chksum);
		if (BusStat.intg_drop+BusStat.rdnd_drop+BusStat.rdnd_skewmax)
			sprintf(str,"%sIC[drop:%d snreset:%d] RM[drop:%d skew:%d]",str,BusStat.intg_drop, BusStat.rdnd_reset, BusStat.rdnd_drop, BusStat.rdnd_skewmax);

	if (toLog == TRUE)
		//if(strlen(str)>0)
			printf_ramlog("Statistics for %d%s%s: ts=%d %s\n", devn, (bus==0)?"A":"B", prelog, BusStat.timestamp,str);

	if (toScreen == TRUE)
	{
		if(strlen(str)>0)
			printf("STATS %d%s%s: %s\n",devn, (bus==0)?"A":"B", prelog, str);
	}
}
