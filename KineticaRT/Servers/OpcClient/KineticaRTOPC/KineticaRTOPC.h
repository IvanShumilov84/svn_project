/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Wed Oct 01 12:06:33 2008
 */
/* Compiler settings for E:\Nigel\KineticaRTOPC\KineticaRTOPC.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __KineticaRTOPC_h__
#define __KineticaRTOPC_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IKRT_GroupUpdate_FWD_DEFINED__
#define __IKRT_GroupUpdate_FWD_DEFINED__
typedef interface IKRT_GroupUpdate IKRT_GroupUpdate;
#endif 	/* __IKRT_GroupUpdate_FWD_DEFINED__ */


#ifndef __IGroup_FWD_DEFINED__
#define __IGroup_FWD_DEFINED__
typedef interface IGroup IGroup;
#endif 	/* __IGroup_FWD_DEFINED__ */


#ifndef __IClient_FWD_DEFINED__
#define __IClient_FWD_DEFINED__
typedef interface IClient IClient;
#endif 	/* __IClient_FWD_DEFINED__ */


#ifndef __Client_FWD_DEFINED__
#define __Client_FWD_DEFINED__

#ifdef __cplusplus
typedef class Client Client;
#else
typedef struct Client Client;
#endif /* __cplusplus */

#endif 	/* __Client_FWD_DEFINED__ */


#ifndef __Group_FWD_DEFINED__
#define __Group_FWD_DEFINED__

#ifdef __cplusplus
typedef class Group Group;
#else
typedef struct Group Group;
#endif /* __cplusplus */

#endif 	/* __Group_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "opcda.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

/* interface __MIDL_itf_KineticaRTOPC_0000 */
/* [local] */ 




extern RPC_IF_HANDLE __MIDL_itf_KineticaRTOPC_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_KineticaRTOPC_0000_v0_0_s_ifspec;

/* interface __MIDL_itf_KineticaRTOPC_0262 */
/* [local] */ 




extern RPC_IF_HANDLE __MIDL_itf_KineticaRTOPC_0262_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_KineticaRTOPC_0262_v0_0_s_ifspec;

#ifndef __IGroup_INTERFACE_DEFINED__
#define __IGroup_INTERFACE_DEFINED__

/* interface IGroup */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IGroup;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("E26B69FF-32EB-4683-82AD-728D0B0BB108")
    IGroup : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_UpdateRate( 
            /* [retval][out] */ long __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_UpdateRate( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_DeadBand( 
            /* [retval][out] */ float __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_DeadBand( 
            /* [in] */ float newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Connected( 
            /* [retval][out] */ BOOL __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Connected( 
            /* [in] */ BOOL newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateItem( 
            /* [in] */ long id,
            /* [in] */ BSTR itemID,
            /* [out] */ VARIANT __RPC_FAR *type,
            /* [retval][out] */ long __RPC_FAR *pItem) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Client( 
            /* [retval][out] */ IClient __RPC_FAR *__RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Client( 
            /* [in] */ IClient __RPC_FAR *newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Server( 
            /* [retval][out] */ IUnknown __RPC_FAR *__RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Server( 
            /* [in] */ IUnknown __RPC_FAR *newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Destination( 
            /* [retval][out] */ IDispatch __RPC_FAR *__RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Destination( 
            /* [in] */ IDispatch __RPC_FAR *newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE DeleteItem( 
            /* [in] */ long pItem) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE UpdateItems( 
            /* [in] */ long count,
            /* [in] */ SAFEARRAY __RPC_FAR * items,
            /* [in] */ SAFEARRAY __RPC_FAR * values) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE DeleteItems( 
            /* [in] */ long count,
            /* [in] */ SAFEARRAY __RPC_FAR * items) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IGroupVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IGroup __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IGroup __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IGroup __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IGroup __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IGroup __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IGroup __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IGroup __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_UpdateRate )( 
            IGroup __RPC_FAR * This,
            /* [retval][out] */ long __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_UpdateRate )( 
            IGroup __RPC_FAR * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_DeadBand )( 
            IGroup __RPC_FAR * This,
            /* [retval][out] */ float __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_DeadBand )( 
            IGroup __RPC_FAR * This,
            /* [in] */ float newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Connected )( 
            IGroup __RPC_FAR * This,
            /* [retval][out] */ BOOL __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Connected )( 
            IGroup __RPC_FAR * This,
            /* [in] */ BOOL newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CreateItem )( 
            IGroup __RPC_FAR * This,
            /* [in] */ long id,
            /* [in] */ BSTR itemID,
            /* [out] */ VARIANT __RPC_FAR *type,
            /* [retval][out] */ long __RPC_FAR *pItem);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Client )( 
            IGroup __RPC_FAR * This,
            /* [retval][out] */ IClient __RPC_FAR *__RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Client )( 
            IGroup __RPC_FAR * This,
            /* [in] */ IClient __RPC_FAR *newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Server )( 
            IGroup __RPC_FAR * This,
            /* [retval][out] */ IUnknown __RPC_FAR *__RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Server )( 
            IGroup __RPC_FAR * This,
            /* [in] */ IUnknown __RPC_FAR *newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Destination )( 
            IGroup __RPC_FAR * This,
            /* [retval][out] */ IDispatch __RPC_FAR *__RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Destination )( 
            IGroup __RPC_FAR * This,
            /* [in] */ IDispatch __RPC_FAR *newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DeleteItem )( 
            IGroup __RPC_FAR * This,
            /* [in] */ long pItem);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *UpdateItems )( 
            IGroup __RPC_FAR * This,
            /* [in] */ long count,
            /* [in] */ SAFEARRAY __RPC_FAR * items,
            /* [in] */ SAFEARRAY __RPC_FAR * values);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DeleteItems )( 
            IGroup __RPC_FAR * This,
            /* [in] */ long count,
            /* [in] */ SAFEARRAY __RPC_FAR * items);
        
        END_INTERFACE
    } IGroupVtbl;

    interface IGroup
    {
        CONST_VTBL struct IGroupVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IGroup_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IGroup_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IGroup_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IGroup_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IGroup_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IGroup_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IGroup_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IGroup_get_UpdateRate(This,pVal)	\
    (This)->lpVtbl -> get_UpdateRate(This,pVal)

#define IGroup_put_UpdateRate(This,newVal)	\
    (This)->lpVtbl -> put_UpdateRate(This,newVal)

#define IGroup_get_DeadBand(This,pVal)	\
    (This)->lpVtbl -> get_DeadBand(This,pVal)

#define IGroup_put_DeadBand(This,newVal)	\
    (This)->lpVtbl -> put_DeadBand(This,newVal)

#define IGroup_get_Connected(This,pVal)	\
    (This)->lpVtbl -> get_Connected(This,pVal)

#define IGroup_put_Connected(This,newVal)	\
    (This)->lpVtbl -> put_Connected(This,newVal)

#define IGroup_CreateItem(This,id,itemID,type,pItem)	\
    (This)->lpVtbl -> CreateItem(This,id,itemID,type,pItem)

#define IGroup_get_Client(This,pVal)	\
    (This)->lpVtbl -> get_Client(This,pVal)

#define IGroup_put_Client(This,newVal)	\
    (This)->lpVtbl -> put_Client(This,newVal)

#define IGroup_get_Server(This,pVal)	\
    (This)->lpVtbl -> get_Server(This,pVal)

#define IGroup_put_Server(This,newVal)	\
    (This)->lpVtbl -> put_Server(This,newVal)

#define IGroup_get_Destination(This,pVal)	\
    (This)->lpVtbl -> get_Destination(This,pVal)

#define IGroup_put_Destination(This,newVal)	\
    (This)->lpVtbl -> put_Destination(This,newVal)

#define IGroup_DeleteItem(This,pItem)	\
    (This)->lpVtbl -> DeleteItem(This,pItem)

#define IGroup_UpdateItems(This,count,items,values)	\
    (This)->lpVtbl -> UpdateItems(This,count,items,values)

#define IGroup_DeleteItems(This,count,items)	\
    (This)->lpVtbl -> DeleteItems(This,count,items)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGroup_get_UpdateRate_Proxy( 
    IGroup __RPC_FAR * This,
    /* [retval][out] */ long __RPC_FAR *pVal);


void __RPC_STUB IGroup_get_UpdateRate_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGroup_put_UpdateRate_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ long newVal);


void __RPC_STUB IGroup_put_UpdateRate_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGroup_get_DeadBand_Proxy( 
    IGroup __RPC_FAR * This,
    /* [retval][out] */ float __RPC_FAR *pVal);


void __RPC_STUB IGroup_get_DeadBand_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGroup_put_DeadBand_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ float newVal);


void __RPC_STUB IGroup_put_DeadBand_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGroup_get_Connected_Proxy( 
    IGroup __RPC_FAR * This,
    /* [retval][out] */ BOOL __RPC_FAR *pVal);


void __RPC_STUB IGroup_get_Connected_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGroup_put_Connected_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ BOOL newVal);


void __RPC_STUB IGroup_put_Connected_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IGroup_CreateItem_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ long id,
    /* [in] */ BSTR itemID,
    /* [out] */ VARIANT __RPC_FAR *type,
    /* [retval][out] */ long __RPC_FAR *pItem);


void __RPC_STUB IGroup_CreateItem_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGroup_get_Client_Proxy( 
    IGroup __RPC_FAR * This,
    /* [retval][out] */ IClient __RPC_FAR *__RPC_FAR *pVal);


void __RPC_STUB IGroup_get_Client_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGroup_put_Client_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ IClient __RPC_FAR *newVal);


void __RPC_STUB IGroup_put_Client_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGroup_get_Server_Proxy( 
    IGroup __RPC_FAR * This,
    /* [retval][out] */ IUnknown __RPC_FAR *__RPC_FAR *pVal);


void __RPC_STUB IGroup_get_Server_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGroup_put_Server_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ IUnknown __RPC_FAR *newVal);


void __RPC_STUB IGroup_put_Server_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGroup_get_Destination_Proxy( 
    IGroup __RPC_FAR * This,
    /* [retval][out] */ IDispatch __RPC_FAR *__RPC_FAR *pVal);


void __RPC_STUB IGroup_get_Destination_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGroup_put_Destination_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ IDispatch __RPC_FAR *newVal);


void __RPC_STUB IGroup_put_Destination_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IGroup_DeleteItem_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ long pItem);


void __RPC_STUB IGroup_DeleteItem_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IGroup_UpdateItems_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ long count,
    /* [in] */ SAFEARRAY __RPC_FAR * items,
    /* [in] */ SAFEARRAY __RPC_FAR * values);


void __RPC_STUB IGroup_UpdateItems_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IGroup_DeleteItems_Proxy( 
    IGroup __RPC_FAR * This,
    /* [in] */ long count,
    /* [in] */ SAFEARRAY __RPC_FAR * items);


void __RPC_STUB IGroup_DeleteItems_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IGroup_INTERFACE_DEFINED__ */


#ifndef __IClient_INTERFACE_DEFINED__
#define __IClient_INTERFACE_DEFINED__

/* interface IClient */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IClient;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("6B1EB334-5DF5-4B55-939F-C2453810D90C")
    IClient : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Machine( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Machine( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Server( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Server( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Connected( 
            /* [retval][out] */ BOOL __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Connected( 
            /* [in] */ BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_BrowseInfo( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_VendorInfo( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateGroup( 
            /* [retval][out] */ IGroup __RPC_FAR *__RPC_FAR *pGroup) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IClientVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IClient __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IClient __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IClient __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IClient __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IClient __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IClient __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IClient __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Machine )( 
            IClient __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Machine )( 
            IClient __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Server )( 
            IClient __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Server )( 
            IClient __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Connected )( 
            IClient __RPC_FAR * This,
            /* [retval][out] */ BOOL __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Connected )( 
            IClient __RPC_FAR * This,
            /* [in] */ BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_BrowseInfo )( 
            IClient __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_VendorInfo )( 
            IClient __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CreateGroup )( 
            IClient __RPC_FAR * This,
            /* [retval][out] */ IGroup __RPC_FAR *__RPC_FAR *pGroup);
        
        END_INTERFACE
    } IClientVtbl;

    interface IClient
    {
        CONST_VTBL struct IClientVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IClient_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IClient_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IClient_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IClient_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IClient_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IClient_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IClient_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IClient_get_Machine(This,pVal)	\
    (This)->lpVtbl -> get_Machine(This,pVal)

#define IClient_put_Machine(This,newVal)	\
    (This)->lpVtbl -> put_Machine(This,newVal)

#define IClient_get_Server(This,pVal)	\
    (This)->lpVtbl -> get_Server(This,pVal)

#define IClient_put_Server(This,newVal)	\
    (This)->lpVtbl -> put_Server(This,newVal)

#define IClient_get_Connected(This,pVal)	\
    (This)->lpVtbl -> get_Connected(This,pVal)

#define IClient_put_Connected(This,newVal)	\
    (This)->lpVtbl -> put_Connected(This,newVal)

#define IClient_get_BrowseInfo(This,pVal)	\
    (This)->lpVtbl -> get_BrowseInfo(This,pVal)

#define IClient_get_VendorInfo(This,pVal)	\
    (This)->lpVtbl -> get_VendorInfo(This,pVal)

#define IClient_CreateGroup(This,pGroup)	\
    (This)->lpVtbl -> CreateGroup(This,pGroup)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IClient_get_Machine_Proxy( 
    IClient __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IClient_get_Machine_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IClient_put_Machine_Proxy( 
    IClient __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IClient_put_Machine_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IClient_get_Server_Proxy( 
    IClient __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IClient_get_Server_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IClient_put_Server_Proxy( 
    IClient __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IClient_put_Server_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IClient_get_Connected_Proxy( 
    IClient __RPC_FAR * This,
    /* [retval][out] */ BOOL __RPC_FAR *pVal);


void __RPC_STUB IClient_get_Connected_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IClient_put_Connected_Proxy( 
    IClient __RPC_FAR * This,
    /* [in] */ BOOL newVal);


void __RPC_STUB IClient_put_Connected_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IClient_get_BrowseInfo_Proxy( 
    IClient __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IClient_get_BrowseInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IClient_get_VendorInfo_Proxy( 
    IClient __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IClient_get_VendorInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IClient_CreateGroup_Proxy( 
    IClient __RPC_FAR * This,
    /* [retval][out] */ IGroup __RPC_FAR *__RPC_FAR *pGroup);


void __RPC_STUB IClient_CreateGroup_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IClient_INTERFACE_DEFINED__ */



#ifndef __KineticaRT_OPC_LIBRARY_DEFINED__
#define __KineticaRT_OPC_LIBRARY_DEFINED__

/* library KineticaRT_OPC */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_KineticaRT_OPC;

EXTERN_C const CLSID CLSID_Client;

#ifdef __cplusplus

class DECLSPEC_UUID("0225BC86-34E7-4D03-BA27-87D6A87FB59A")
Client;
#endif

EXTERN_C const CLSID CLSID_Group;

#ifdef __cplusplus

class DECLSPEC_UUID("C68958AC-AE95-4DCF-8DAF-EF0DF29B3E11")
Group;
#endif
#endif /* __KineticaRT_OPC_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long __RPC_FAR *, unsigned long            , BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long __RPC_FAR *, BSTR __RPC_FAR * ); 

unsigned long             __RPC_USER  LPSAFEARRAY_UserSize(     unsigned long __RPC_FAR *, unsigned long            , LPSAFEARRAY __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  LPSAFEARRAY_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, LPSAFEARRAY __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  LPSAFEARRAY_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, LPSAFEARRAY __RPC_FAR * ); 
void                      __RPC_USER  LPSAFEARRAY_UserFree(     unsigned long __RPC_FAR *, LPSAFEARRAY __RPC_FAR * ); 

unsigned long             __RPC_USER  VARIANT_UserSize(     unsigned long __RPC_FAR *, unsigned long            , VARIANT __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  VARIANT_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, VARIANT __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  VARIANT_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, VARIANT __RPC_FAR * ); 
void                      __RPC_USER  VARIANT_UserFree(     unsigned long __RPC_FAR *, VARIANT __RPC_FAR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
