//=================================================================================================
//
// NAME:    Convert_AR664_cfg.c
//
// DESCRIPTION:
//
//          This example demonstrates how convert ARINC-664 configuration stored in files into
//          a sequence of calls to DNR-ARINC-664 board to build up an array of VLs and ports
//
//
// ------------------------------------------------------------------------------------------------
//
//      Copyright (C) 2010-2013 United Electronic Industries, Inc.
//      All rights reserved.
//      United Electronic Industries Confidential Information.
//
//=================================================================================================
#include <stdio.h>
#include <signal.h>
#include <time.h>

#include "PDNA.h"
#include "Convert_AR664_cfg.h"

//+
// ------------------------------------------------------------------------------------------------
// Function:   char* splittoken(char* str, char* separs, char* cpto, int maxlen)
//
// Parameters: char* str -- a pointer to the string to start from
//             char* separs -- a string of separators
//             char* cpto -- a pointer to copy separated string
//             int maxlen -- maximum lenght of <cpto>
//
// Returns:    pointer to the string after delimiting token, this string to be passed as <str>
//             on a subsequent calls. NULL for the end of the string
//
// Description: Advances to the separator defined in the <separs> string.
//              Copies portion of the string from <*str> to the *cpto
//
// Notes:      Use this function for CSV files.
//             The return pointer is the next character after encountering a separator.
//             In case of two separators next to each other NULL string is returned
//
// ------------------------------------------------------------------------------------------------
//-
char* splittoken(char* str, char* separs, char* cpto, int maxlen) {
    char *tok;
    int cnt = 0;

    if (!str) return NULL;
    if (!separs) return NULL;
    if (!cpto) return NULL;

    *cpto = 0;    // start from zero string
    while(*str) {
        tok = separs;
        while (*tok) {
            if (*str == *tok) {
                break;
            }
            tok++;  // if (0 == *tok) - this is not separator
        }

        if (0 == *tok) {
            *cpto++ = *str++;
        } else {
            str++;
            break;
        }
        
        if (cnt++ >= maxlen) 
            break;
    }
    *cpto = 0;
    return str;
}

// Function takes filename as <file_in> and converts UEI ARINC-664 configuration file into set of
// ready to call structures pVL and pPort. Function allocates memory for these structures and also returns
// equipment name
// It is user responsibility to call free() on allocated memory to free it
//
int DqAdv664CreateStructures(char* file_in, char* eq_name,
                             pAR664_VL_CFG* pVL, int* VLSize, 
                             pAR664_PORT_CFG* pPort, int* PortSize) {
    FILE* file_cfg;
    int ret = DQ_SUCCESS;
    int line_sz, line_n = 0, i;
    char line[512];
    char Token[128], *pNextLine;
    int in_vl, out_vl, in_port, out_port;
    pAR664_VL_CFG pVLarr;
    pAR664_PORT_CFG pPortarr;
    int vl_idx, pr_idx;


    file_cfg = fopen(file_in, "r");
    if (NULL == file_cfg)
        return DQ_BAD_PARAMETER;

    in_vl = out_vl = in_port = out_port = 0;
    while (fgets(line, sizeof(line), file_cfg) != NULL) {

        line_sz = strlen(line);
        line_n++;

        if ('#' == line[0]) // skip lines with comments - i.e. lines with # in the first position
            continue;

        // find what the line is
        pNextLine = line;
        pNextLine = splittoken(pNextLine, ";", Token, 1024);

        // figure out what we got
        if (0 == strcmp("OUTPUT_VL", Token)) {
            in_vl++;
        } else if (0 == strcmp("INPUT_VL", Token)) {
            out_vl++;
        } else if (0 == strcmp("OUTPUT_PORT", Token)) {
            in_port++;
        } else if (0 == strcmp("INPUT_PORT", Token)) {
            out_port++;
        } else if (0 == strcmp("FORMAT", Token)) {
            for (i = 0; i < 4; i++) {
                pNextLine = splittoken(pNextLine, ";", Token, 1024);
                strncpy(eq_name, Token, 32);
            } 
        }


    }
    
    fseek(file_cfg, 0, 0);   // move pointer back to the beginning

    // allocate required structure
    pVLarr = malloc(sizeof(AR664_VL_CFG)*(in_vl+out_vl));
    if (NULL == pVLarr) return DQ_NO_MEMORY;
    pPortarr = malloc(sizeof(AR664_PORT_CFG)*(in_port+out_port));
    if (NULL == pPortarr) return DQ_NO_MEMORY;

    *pVL = pVLarr;
    *pPort = pPortarr;

    vl_idx = 0;
    pr_idx = 0;

    while (fgets(line, sizeof(line), file_cfg) != NULL) {

        // find what the line is
        pNextLine = line;
        pNextLine = splittoken(pNextLine, ";", Token, 1024);

        // figure out what we got
        if (0 == strcmp("OUTPUT_VL", Token)) {
            sscanf(pNextLine, "%d;%d;%d;%d;%x;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d",
                    &pVLarr[vl_idx].VLID,
                    &pVLarr[vl_idx].n_subVL,
                    &pVLarr[vl_idx].frag_en,
                    &pVLarr[vl_idx].ICMP_en,
                    &pVLarr[vl_idx].mode,
                    &pVLarr[vl_idx].bag,
                    &pVLarr[vl_idx].lmax,
                    &pVLarr[vl_idx].max_jitter,
                    &pVLarr[vl_idx].skew_max,
                    &pVLarr[vl_idx].skew_delay,
                    &pVLarr[vl_idx].prev_sn_range,
                    &pVLarr[vl_idx].ede_en,
                    &pVLarr[vl_idx].ede_sid,
                    &pVLarr[vl_idx].ede_skew_max,
                    &pVLarr[vl_idx].ede_age_max,
                    &pVLarr[vl_idx].ede_lag_max);
                    vl_idx++;
        } else if (0 == strcmp("INPUT_VL", Token)) {
            sscanf(pNextLine, "%d;%d;%d;%x;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d",
                    &pVLarr[vl_idx].VLID,
                    &pVLarr[vl_idx].frag_en,
                    &pVLarr[vl_idx].ICMP_en,
                    &pVLarr[vl_idx].mode,
                    &pVLarr[vl_idx].bag,
                    &pVLarr[vl_idx].lmax,
                    &pVLarr[vl_idx].max_jitter,
                    &pVLarr[vl_idx].skew_max,
                    &pVLarr[vl_idx].skew_delay,
                    &pVLarr[vl_idx].prev_sn_range,
                    &pVLarr[vl_idx].ede_en,
                    &pVLarr[vl_idx].ede_sid,
                    &pVLarr[vl_idx].ede_skew_max,
                    &pVLarr[vl_idx].ede_age_max,
                    &pVLarr[vl_idx].ede_lag_max);
                    vl_idx++;
        } else if (0 == strcmp("OUTPUT_PORT", Token)) {
            sscanf(pNextLine, "%x;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d",
                    &pPortarr[pr_idx].mode,
                    &pPortarr[pr_idx].portID,
                    &pPortarr[pr_idx].VLID,
                    &pPortarr[pr_idx].period,
                    &pPortarr[pr_idx].period_skew,
                    &pPortarr[pr_idx].endsys_src,
                    &pPortarr[pr_idx].part_src,
                    &pPortarr[pr_idx].endsys_dst,
                    &pPortarr[pr_idx].part_dst,
                    &pPortarr[pr_idx].subvl_id,
                    &pPortarr[pr_idx].udp_src_port,
                    &pPortarr[pr_idx].udp_dst_port,
                    &pPortarr[pr_idx].port_type,
                    &pPortarr[pr_idx].d_size,
                    &pPortarr[pr_idx].depth);
                    pr_idx++;
        } else if (0 == strcmp("INPUT_PORT", Token)) {
            sscanf(pNextLine, "%x;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d;%d",
                    &pPortarr[pr_idx].mode,
                    &pPortarr[pr_idx].portID,
                    &pPortarr[pr_idx].VLID,
                    &pPortarr[pr_idx].period,
                    &pPortarr[pr_idx].period_skew,
                    &pPortarr[pr_idx].endsys_src,
                    &pPortarr[pr_idx].part_src,
                    &pPortarr[pr_idx].endsys_dst,
                    &pPortarr[pr_idx].part_dst,
                    &pPortarr[pr_idx].subvl_id,
                    &pPortarr[pr_idx].udp_src_port,
                    &pPortarr[pr_idx].udp_dst_port,
                    &pPortarr[pr_idx].port_type,
                    &pPortarr[pr_idx].d_size,
                    &pPortarr[pr_idx].depth);
                    pr_idx++;
        }

        line_sz = strlen(line);
        line_n++;

    }
    *VLSize = vl_idx;
    *PortSize = pr_idx;

    return ret;
}

// The function takes an array of allocated VL and Port maps and combine it
// into a single 

#define DQAR664_COMBINESTR_FREE_TABLES  (1L<<0)     // free separate tables after processing
#define DQAR664_COMBINESTR_INVERSE_SIM  (1L<<1)     // inverse port direction for simulation purposes

extern FILE* diag;

int DqAdv664CombineStructures(int num_tables, int flags,                     // number of VL and port tables, use (1L<<0) to free() original tables
                              pAR664_VL_CFG parVL[], int VLSize[],           // Array of pointers to VL tables and size
                              pAR664_PORT_CFG parPort[], int PortSize[],     // Array of pointers to Port tables and size
                              pAR664_VL_CFG* ppInVL, int* InVLSize,          // Combined input VL and its size (allocate here)
                              pAR664_VL_CFG* ppOutVL, int* OutVLSize,        // Combined input VL and its size (allocate here)
                              pAR664_PORT_CFG* ppInPort, int* InPortSize,    // Combined input ports and size (allocate here)
                              pAR664_PORT_CFG* ppOutPort, int* OutPortSize   // Combined output ports and size (allocate here)
                             ) {
    int table, i, j;
    int cur_port, VLID, PortID;
    int num_0v = 0, num_1v = 0;
    int num_0p = 0, num_1p = 0;
    int num_in_v = 0, num_out_v = 0;
    int num_in_p = 0, num_out_p = 0;
    pAR664_VL_CFG pInVL;
    pAR664_VL_CFG pOutVL;
    pAR664_PORT_CFG pInPort;
    pAR664_PORT_CFG pOutPort;
    pAR664_VL_CFG pVL;
    pAR664_PORT_CFG pPort;
    int in_vl_idx = 0, out_vl_idx = 0;
    int in_port_idx = 0, out_port_idx = 0;
    int* temp = NULL;

    // go over all tables and calculate number of input and output entries
    for (table = 0; table < num_tables; table++) {  // by tables
        pVL = parVL[table];
        pPort = parPort[table];
        for (i = 0; i < VLSize[table]; i++) {
            if (pVL[i].mode & AR664_VL_TX) {
                for (j = 0; j < PortSize[table]; j++) {
                    if (pPort[j].VLID == pVL[i].VLID) 
                        num_0p++;   // output
                }
                num_0v++;
            } else {
                for (j = 0; j < PortSize[table]; j++) {
                    if (pPort[j].VLID == pVL[i].VLID) 
                        num_1p++;   // input
                }
                num_1v++;
            }
        }
    }

    if (flags & DQAR664_COMBINESTR_INVERSE_SIM) {
        num_in_v = num_0v;  // reverse
        num_out_v = num_1v;
        num_in_p = num_0p;
        num_out_p = num_1p;
    } else {
        num_in_v = num_1v;  // straight
        num_out_v = num_0v;
        num_in_p = num_1p;
        num_out_p = num_0p;
    }

    *InVLSize = num_in_v;
    *OutVLSize = num_out_v;
    *InPortSize = num_in_p;
    *OutPortSize = num_out_p;

	if (num_in_v)
		if (NULL == (*ppInVL = pInVL = malloc(sizeof(AR664_VL_CFG)*num_in_v))) return FALSE;
	if (num_out_v)
		if (NULL == (*ppOutVL = pOutVL = malloc(sizeof(AR664_VL_CFG)*num_out_v))) return FALSE;
	if (num_in_p)
		if (NULL == (*ppInPort = pInPort = malloc(sizeof(AR664_PORT_CFG)*num_in_p))) return FALSE;
	if (num_out_p)
		if (NULL == (*ppOutPort = pOutPort = malloc(sizeof(AR664_PORT_CFG)*num_out_p))) return FALSE;
    
    for (table = 0; table < num_tables; table++) {  // by tables
        pVL = parVL[table];
        pPort = parPort[table];
        for (i = 0; i < VLSize[table]; i++) {
            if (flags & DQAR664_COMBINESTR_INVERSE_SIM) {
                if (pVL[i].mode & AR664_VL_TX) {   // if it was a transmit (i.e. output) VL stores it into input VL

                    //fprintf(diag, "TX Inv VL:%d mode:%x\n", pVL[i].VLID, pVL[i].mode);

                    memcpy(&pInVL[in_vl_idx], &pVL[i], sizeof(AR664_VL_CFG));
                    pInVL[in_vl_idx].mode &= ~AR664_VL_TX;
                    
                    for (j = 0; j < PortSize[table]; j++) {
                        if (pPort[j].VLID == pVL[i].VLID) {
                            memcpy(&pInPort[in_port_idx], &pPort[j], sizeof(AR664_PORT_CFG));

#if (1==ENABLE_QUEUEING)
                            pInPort[in_port_idx].d_size = pVL[i].lmax/4 + (pVL[i].lmax>>2)%4;
                            pInPort[in_port_idx].depth = 8;
#endif
#if 0
                            fprintf(diag, "RX VL:%d mode:%x port_type:%x period:%x mtu:%d d_size:%d\n", 
                                    pVL[in_vl_idx].VLID, pVL[in_vl_idx].mode, pPort[in_port_idx].port_type, 
                                    pPort[in_port_idx].period, pInVL[in_vl_idx].mtu, pPort[in_port_idx].d_size);
#endif

                            in_port_idx++;
                        }
                    }
                    in_vl_idx++;
                } else {

                    //fprintf(diag, "RX Inv VL:%d mode:%x\n", pVL[i].VLID, pVL[i].mode);

                    memcpy(&pOutVL[out_vl_idx], &pVL[i], sizeof(AR664_VL_CFG));
                    pOutVL[out_vl_idx].mode |= AR664_VL_TX;

                    for (j = 0; j < PortSize[table]; j++) {
                        if (pPort[j].VLID == pVL[i].VLID) {
                            memcpy(&pOutPort[out_port_idx], &pPort[j], sizeof(AR664_PORT_CFG));

                            // port information does need to be approximated in this case
                            pOutPort[out_port_idx].endsys_src = pPort[j].VLID / 100;
                            pOutPort[out_port_idx].part_src = pPort[j].VLID % 100;
                            pOutPort[out_port_idx].udp_src_port = pOutPort[out_port_idx].udp_dst_port;

                            //AI: for test purposes make one sampling port aperiodic
#if (1==ENABLE_SAMPLING)
                            if (pOutPort[out_port_idx].VLID == 15251) 
                                pOutPort[out_port_idx].period = 0;
#endif
#if (1==ENABLE_QUEUEING)
                                pOutPort[out_port_idx].d_size = pVL[i].lmax/4 + (pVL[i].lmax>>2)%4;
                                pOutPort[out_port_idx].depth = 8;
#endif
#if 0
                            fprintf(diag, "TX VL:%d mode:%x port_type:%x period:%x mtu:%d d_size:%d\n", 
                                    pVL[out_vl_idx].VLID, pVL[out_vl_idx].mode, pPort[out_port_idx].port_type, 
                                    pPort[out_port_idx].period, pOutVL[out_vl_idx].mtu, pPort[out_port_idx].d_size);
#endif
                            out_port_idx++;

                        }
                    }
                    out_vl_idx++;
                }
            } else {
                if (pVL[i].mode & AR664_VL_TX) {   // if it was a tranmit (i.e. output) VL store it into input VL

                    //fprintf(diag, "TX Str VL:%d mode:%x\n", pVL[i].VLID, pVL[i].mode);
                    memcpy(&pOutVL[out_vl_idx], &pVL[i], sizeof(AR664_VL_CFG));
                    
                    for (j = 0; j < PortSize[table]; j++) {
                        if (pPort[j].VLID == pVL[i].VLID) {
                            memcpy(&pOutPort[out_port_idx], &pPort[j], sizeof(AR664_PORT_CFG));
                            out_port_idx++;
                        }
                    }
                    out_vl_idx++;
                } else {

                    //fprintf(diag, "RX Str VL:%d mode:%x\n", pVL[i].VLID, pVL[i].mode);

                    memcpy(&pInVL[in_vl_idx], &pVL[i], sizeof(AR664_VL_CFG));
                    for (j = 0; j < PortSize[table]; j++) {
                        if (pPort[j].VLID == pVL[i].VLID) {
                            memcpy(&pInPort[in_port_idx], &pPort[j], sizeof(AR664_PORT_CFG));
                            in_port_idx++;
                        }
                    }
                    in_vl_idx++;
                }
            }
        }
    }

    // we need to de-dup ports, most likely they are among input ports since most equipment's made for receive
#if 1
    temp = malloc(sizeof(int)*65536);
    memset(temp, 0, sizeof(int)*65536);
    for (i = 0; i < num_in_v; i++) {
        VLID = pInVL[i].VLID;
        if (temp[VLID]) {
            //fprintf(diag, "In Dup VLID = %d\n", VLID);
            pInVL[i].VLID = 0;
        } else {
            //fprintf(diag, "In Dup VLID Unique = %d\n", VLID);
            temp[VLID] = TRUE;
        }
    }
    memset(temp, 0, sizeof(int)*65536);
    for (i = 0; i < num_out_v; i++) {
        VLID = pOutVL[i].VLID;
        if (temp[VLID]) {
            //fprintf(diag, "Out Dup VLID = %d\n", VLID);
            pOutVL[i].VLID = 0;
        } else {
            //fprintf(diag, "Out Dup VLID Unique = %d\n", VLID);
            temp[VLID] = TRUE;
        }
    }

    // the same information from FMS
    cur_port = 0;
    for (i = 0; i < num_in_v; i++) {
        VLID = pInVL[i].VLID;
        if (0 == VLID) continue;
        memset(temp, 0, sizeof(int)*65536);

        for (j = 0; j < num_in_p; j++) {
            PortID = pInPort[j].portID;
            if (0 == PortID) continue;
            if (VLID != pInPort[j].VLID) continue;
            if (temp[PortID]) {
                //fprintf(diag, "In Dup PortID:%d of VL:%d is removed pos=%d\n", pInPort[j].portID, pInPort[j].VLID, j);
                pInPort[j].portID = pInPort[j].VLID = 0;
            } else {
                temp[PortID] = TRUE;
            }
        }
    }

    // if the table is inverted for simulation the same could happen for the output ports
    for (i = 0; i < num_out_v; i++) {
        VLID = pOutVL[i].VLID;
        if (0 == VLID) continue;
        memset(temp, 0, sizeof(int)*65536);

        for (j = 0; j < num_out_p; j++) {
            PortID = pOutPort[j].portID;
            if (0 == PortID) continue;
            if (VLID != pOutPort[j].VLID) continue;
            if (temp[PortID]) {
                //fprintf(diag, "Out Dup PortID:%d of VL:%d is removed pos=%d\n", pOutPort[j].portID, pOutPort[j].VLID, j);
                pOutPort[j].portID = pOutPort[j].VLID = 0;
            } else {
                temp[PortID] = TRUE;
            }
        }
    }
    free(temp);

#endif

    if (flags & DQAR664_COMBINESTR_FREE_TABLES) 
        for (table = 0; table < num_tables; table++) {  // by tables
            free(parVL[table]);
            free(parPort[table]);
            parVL[table] = NULL;
            parPort[table] = NULL;
        }
    return TRUE;
}

