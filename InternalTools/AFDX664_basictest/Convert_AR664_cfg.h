#define NUM_TBL 1
//#define AFDXFILEPREFIX    "afdx_cd"

#define ENABLE_PERIODIC 1
#define ENABLE_SAMPLING 1
#define ENABLE_QUEUEING 1
#define ENABLE_SAP      1

#define ENABLE_OUTPUT   1
#define ENABLE_INPUT    1


#define MAX_MSG_SZ      8192

int DqAdv664CreateStructures(char* file_in, char* eq_name,
                             pAR664_VL_CFG* pVL, int* VLSize, 
                             pAR664_PORT_CFG* pPort, int* PortSize);

#define DQAR664_COMBINESTR_FREE_TABLES  (1L<<0)     // free separate tables after processing
#define DQAR664_COMBINESTR_INVERSE_SIM  (1L<<1)     // inverse port direction for simulation purposes

int DqAdv664CombineStructures(int num_tables, int flags,                     // number of VL and port tables, use (1L<<0) to free() original tables
                              pAR664_VL_CFG parVL[], int VLSize[],           // Array of pointers to VL tables and size
                              pAR664_PORT_CFG parPort[], int PortSize[],     // Array of pointers to Port tables and size
                              pAR664_VL_CFG* ppInVL, int* InVLSize,          // Combined input VL and its size (allocate here)
                              pAR664_VL_CFG* ppOutVL, int* OutVLSize,        // Combined input VL and its size (allocate here)
                              pAR664_PORT_CFG* ppInPort, int* InPortSize,    // Combined input ports and size (allocate here)
                              pAR664_PORT_CFG* ppOutPort, int* OutPortSize   // Combined output ports and size (allocate here)
                             );
