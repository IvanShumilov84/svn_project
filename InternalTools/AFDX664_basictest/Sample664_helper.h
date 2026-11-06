#ifndef SAMPLE664_HELPER_H
#define SAMPLE664_HELPER_H

/* BEGIN CUSTOM CONFIGURATION */

#define TIMEOUT_DELAY  1000    // milliseconds

#define MAX_FW_SIZE (4*1024*1024)   // 4MB max in flash

// Structure for storing VL handles
typedef struct {
    int vlid;               // VLID
    int vl_handle;          // handle for this VLID
    int n_ports;            // number of ports attached
    int* port_id;           // port ID
    int* port_handle;       // handles to those ports 
} S664_HANDLES;

// Maps an output port's input port; assumes only 1 input port per output port.
typedef struct {
	// unique key pair 1
	int in_port_handle;
	int out_port_handle;
	// unique key pair 2
	pAR664_PORT_CFG output_port;
	pAR664_PORT_CFG input_port;
	// payload of 0-1518 bytes per packet
	char lastmsg[8192];		// content of message sent
	uint32 msglen;			// length of message sent
	LARGE_INTEGER sendtime;	// time message was sent (to compare with pAR664_VL_CFG.bag)
	// linked list and better keys will be needed if more than 1 input port 
	// per output port, e.g. struct S664_TEST_DATA* next;
} S664_TEST_DATA, *pS664_TEST_DATA;


int build_test_structure ( pAR664_PORT_CFG pInPort, int inPortSize,
                           pAR664_PORT_CFG pOutPort, int outPortSize,
						   S664_HANDLES* in_handles, S664_HANDLES* out_handles,
						   pS664_TEST_DATA* ppTestData, int* pTestdataSize);
void print_test_structure(int id, pS664_TEST_DATA pTestdata, int length);
int init_cube(char* ip, int* hd, pDQRDCFG* pDQRdCfg);
void* memset_alphabet(void *s, int c, unsigned n);
void* memset_arincdbg(void *s, uint8 sn, uint32 gsn, AR664_VL_CFG vl, AR664_PORT_CFG port);
uint32 ip_string_to_uint32(char* ip_addr);
int look_up_handle(S664_HANDLES* handle_table, int vlid, int portid);
char* handle_string(uint16 handle);
void print_common_structure( pAR664_VL_CFG pInVL, int InVLSize,
                             pAR664_VL_CFG pOutVL, int OutVLSize,
                             pAR664_PORT_CFG pInPort, int InPortSize,
                             pAR664_PORT_CFG pOutPort, int OutPortSize);
void print_bus_stats (int hd, int devn, int bus, int toScreen, int toLog, int clear, char* prelog);
void compare664_input(int vl_handle, int port_handle, 
					  S664_HANDLES* in_handles, pAR664_PORT_CFG pInPort, 
					  uint8* message, int rcvlen, pAR664_HEADERS headers, 
					  uint32 available, uint32 status, LARGE_INTEGER recvtime, 
					  pS664_TEST_DATA pTestdata, int TestdataSize,
					  int printHexBits);

#endif
