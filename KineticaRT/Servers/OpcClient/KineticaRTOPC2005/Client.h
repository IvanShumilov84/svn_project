// Client.h : Declaration of the CClient

#ifndef __CLIENT_H_
#define __CLIENT_H_

#include "resource.h"       // main symbols
#include "opcda.h"
#include "group.h"

/////////////////////////////////////////////////////////////////////////////
// CClient
class ATL_NO_VTABLE CClient : 
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CClient, &CLSID_Client>,
	public IDispatchImpl<IClient, &IID_IClient, &LIBID_KineticaRT_OPC>
{
public:
	CClient()
	{
		Machine = NULL ;
		Server = NULL ;
		VendorInfo = SysAllocString (L"Initialised") ;
		OpcServer = NULL ;
		Connected = FALSE ;
	}

	~CClient()
	{
		SysFreeString(Machine) ;
		SysFreeString(Server) ;
		SysFreeString(VendorInfo) ;
		if (OpcServer != NULL) OpcServer->Release () ;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_CLIENT)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CClient)
	COM_INTERFACE_ENTRY(IClient)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

// IClient
public:
	STDMETHOD(CreateGroup)(/*[out]*/ IGroup ** pGroup);
	STDMETHOD(get_VendorInfo)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(get_BrowseInfo)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(get_Connected)(/*[out, retval]*/ BOOL *pVal);
	STDMETHOD(put_Connected)(/*[in]*/ BOOL newVal);
	STDMETHOD(get_Server)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Server)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Machine)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Machine)(/*[in]*/ BSTR newVal);

private:
	BSTR			Machine ;
	BSTR			Server ;
	BSTR			VendorInfo ;
	BOOL			Connected ;
	IOPCServer *	OpcServer ;

	//void add_all (IOPCBrowseServerAddressSpace* opc_browser, CComBSTR & all) ;
	//void add_flat (IOPCBrowseServerAddressSpace* opc_browser, CComBSTR & all) ;
	void add_all (IOPCBrowseServerAddressSpace* opc_browser, WCHAR* & all, WCHAR* & all_ptr, int & all_length) ;
	void add_flat (IOPCBrowseServerAddressSpace* opc_browser, WCHAR* & all, WCHAR* & all_ptr, int & all_length) ;
};

#endif //__CLIENT_H_
