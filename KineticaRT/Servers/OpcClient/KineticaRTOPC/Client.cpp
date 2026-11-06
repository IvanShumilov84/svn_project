// Client.cpp : Implementation of CClient
#include "stdafx.h"
#include "KineticaRTOPC.h"
#include "Client.h"

/////////////////////////////////////////////////////////////////////////////
// CClient


STDMETHODIMP CClient::get_Machine(BSTR *pVal)
{
	*pVal = SysAllocString (Machine) ;

	return S_OK;
}

STDMETHODIMP CClient::put_Machine(BSTR newVal)
{
	if (VarBstrCmp(Machine, newVal, NULL, 0) == VARCMP_EQ) return S_OK;

	SysFreeString (Machine) ;
	Machine = SysAllocString (newVal) ;

	return S_OK;
}

STDMETHODIMP CClient::get_Server(BSTR *pVal)
{
	*pVal = SysAllocString (Server) ;

	return S_OK;
}

STDMETHODIMP CClient::put_Server(BSTR newVal)
{
	if (VarBstrCmp(Server, newVal, NULL, 0) == VARCMP_EQ) return S_OK;

	SysFreeString (Server) ;
	Server = SysAllocString (newVal) ;

	return S_OK;
}

STDMETHODIMP CClient::get_Connected(BOOL *pVal)
{
	*pVal = Connected ;

	return S_OK;
}

STDMETHODIMP CClient::put_Connected(BOOL newVal)
{
	HRESULT hr ;

	if (Connected == newVal) return S_OK;

	Connected = newVal ;

	if (Connected)
	{
		// Delete old server object
		if (OpcServer != NULL)
		{
			OpcServer->Release () ; 
			OpcServer = NULL ;
		}

		// Create the server object
		CLSID	clsid;

		hr = CLSIDFromProgID (Server, &clsid);
		if(FAILED(hr))
		{
			WCHAR message [400] ;
			wsprintf (message, L"OPC Server not registered \"%s\" (%x)", Server, hr) ;
			SysFreeString (VendorInfo) ;
			VendorInfo = SysAllocString (message) ;
			return S_OK ;
		}

		MULTI_QI mq ;
		COSERVERINFO server_info ;

		mq.pIID = &IID_IOPCServer ;
		mq.hr = 0 ;
		mq.pItf = NULL ;
		server_info.dwReserved1 = 0 ;
		server_info.dwReserved2 = 0 ;
		server_info.pAuthInfo = NULL ;

		if (lstrlen (Machine) == 0)
		{
			server_info.pwszName = NULL ;
		}
		else 
		{
			server_info.pwszName = Machine ;
		}
		hr = CoCreateInstanceEx (clsid,NULL, CLSCTX_SERVER, &server_info, 1, &mq) ;
		OpcServer = (IOPCServer*) mq.pItf ;
		
		if(FAILED(hr) || (OpcServer == NULL)) 
		{
			WCHAR message [400] ;
			wsprintf (message, L"OPC Server creation failed (%x)", hr) ;
			SysFreeString (VendorInfo) ;
			VendorInfo = SysAllocString (message) ;
			return S_OK ;
		}

		// Check status.
		OPCSERVERSTATUS * status ;

		HRESULT hr = OpcServer->GetStatus (&status) ;
		if(FAILED(hr)) 
		{
			SysFreeString (VendorInfo) ;
			VendorInfo = SysAllocString (L"OPC Server status unavailable") ;
			return S_OK ;
		}

		SysFreeString (VendorInfo) ;
		VendorInfo = SysAllocString (status->szVendorInfo) ;

		CoTaskMemFree(status->szVendorInfo);
		CoTaskMemFree(status);

		// Notify groups about new server.
//!		KRT_Fire (CID_Groups, CComVariant(0)) ;

	}
	else
	{
		// Disconnect.
		if (OpcServer != NULL)
		{
			SysFreeString (VendorInfo) ;
			VendorInfo = SysAllocString (L"Disconnected") ;
			OpcServer->Release () ; 
			OpcServer = NULL ;
		}
	}

	return S_OK;
}

STDMETHODIMP CClient::get_BrowseInfo(BSTR *pVal)
{
	if (!OpcServer)
	{
		CComBSTR(L"<Error Text='Server not connected' />").CopyTo(pVal) ;
		return S_OK ;
	}

	CComQIPtr<IOPCBrowseServerAddressSpace>	opc_browser ;

	opc_browser = OpcServer ;

	if (!opc_browser)
	{
		CComBSTR(L"<Error Text='Server does not support browsing' />").CopyTo(pVal) ;
		return S_OK ;
	}

	OPCNAMESPACETYPE nstype ;

	opc_browser->QueryOrganization (&nstype) ;
	opc_browser->ChangeBrowsePosition (OPC_BROWSE_TO, NULL) ;

//	CComBSTR all ;
	WCHAR * all ;
	WCHAR * all_ptr ;
	int		all_length = 10000 ;
	all = new WCHAR [all_length] ;
	all_ptr = all ;

	//all = L"<List>";
	lstrcpy (all_ptr, L"<List>") ; 
	all_ptr += 6 ;

	if (nstype == OPC_NS_HIERARCHIAL)
	{
		add_all (opc_browser, all, all_ptr, all_length) ;
	}
	else
	{
		add_flat (opc_browser, all, all_ptr, all_length) ;
	}

	//all += L"</List>";
	lstrcpy (all_ptr, L"</List>") ; 
	all_ptr += 7 ;

	//all.CopyTo(pVal) ;
	*pVal = SysAllocString (all) ;

	return S_OK;
}

//void CClient::add_flat (IOPCBrowseServerAddressSpace* opc_browser, CComBSTR & all)
void CClient::add_flat (IOPCBrowseServerAddressSpace* opc_browser, WCHAR* & all, WCHAR* & all_ptr, int & all_length)
{
	BSTR name [1] ;
	ULONG retrieved ;
	CComQIPtr<IEnumString> enum_leaves ;

	opc_browser->BrowseOPCItemIDs (OPC_FLAT, L"", VT_EMPTY, 0, &enum_leaves) ;

	// Add Leaves.
	enum_leaves->Next (1, name, &retrieved) ;

	while (retrieved)
	{
		BSTR itemid ;
		opc_browser->GetItemID (name[0], &itemid) ;

		//all += L"<Leaf Name='";
		//all += name [0] ;
		//all += L"' ItemID='";
		//all += itemid ;
		//all += L"' />";

		if (all_ptr > all + all_length - 1000)
		{
			all_length *= 2 ;
			WCHAR* new_all = new WCHAR [all_length] ;
			memcpy (new_all, all, all_length) ;
			all_ptr = new_all + (all_ptr - all) ;
			delete [] all ;
			all = new_all ;
		}

		all_ptr += wsprintf(all_ptr, L"<Leaf Name='%s' ItemID='%s'/>", name [0], itemid) ;

		enum_leaves->Next (1, name, &retrieved) ;
	}
}

//void CClient::add_all (IOPCBrowseServerAddressSpace* opc_browser, CComBSTR & all)
void CClient::add_all (IOPCBrowseServerAddressSpace* opc_browser, WCHAR* & all, WCHAR* & all_ptr, int & all_length)
{
	BSTR name [1] ;
	ULONG retrieved ;
	CComQIPtr<IEnumString> enum_branch ;
	CComQIPtr<IEnumString> enum_leaves ;

	opc_browser->BrowseOPCItemIDs (OPC_BRANCH, L"", VT_EMPTY, 0, &enum_branch) ;
	opc_browser->BrowseOPCItemIDs (OPC_LEAF, L"", VT_EMPTY, 0, &enum_leaves) ;

	// Add Branches.
	enum_branch->Next (1, name, &retrieved) ;

	while (retrieved)
	{
		//all += L"<Branch Name='";
		//all += name [0] ;
		//all += L"'>";
		if (all_ptr > all + all_length - 1000)
		{
			all_length *= 2 ;
			WCHAR* new_all = new WCHAR [all_length] ;
			memcpy (new_all, all, all_length) ;
			all_ptr = new_all + (all_ptr - all) ;
			delete [] all ;
			all = new_all ;
		}

		all_ptr += wsprintf(all_ptr, L"<Branch Name='%s'>", name [0]) ;

		opc_browser->ChangeBrowsePosition (OPC_BROWSE_DOWN, name[0]) ;

		add_all (opc_browser, all, all_ptr, all_length) ;

		opc_browser->ChangeBrowsePosition (OPC_BROWSE_UP, name[0]) ;

		enum_branch->Next (1, name, &retrieved) ;
		all_ptr += wsprintf(all_ptr, L"</Branch>") ;
		//all += L"</Branch>" ;
	}

	
	// Add Leaves.
	enum_leaves->Next (1, name, &retrieved) ;

	while (retrieved)
	{
		BSTR itemid ;
		opc_browser->GetItemID (name[0], &itemid) ;

		//all += L"<Leaf Name='";
		//all += name [0] ;
		//all += L"' ItemID='";
		//all += itemid ;
		//all += L"' />";

		if (all_ptr > all + all_length - 1000)
		{
			all_length *= 2 ;
			WCHAR* new_all = new WCHAR [all_length] ;
			memcpy (new_all, all, all_length) ;
			all_ptr = new_all + (all_ptr - all) ;
			delete [] all ;
			all = new_all ;
		}


		all_ptr += wsprintf(all_ptr, L"<Leaf Name='%s' ItemID='%s'/>", name [0], itemid) ;

		enum_leaves->Next (1, name, &retrieved) ;
	}
}

STDMETHODIMP CClient::get_VendorInfo(BSTR *pVal)
{
	if (OpcServer != NULL)
	{
		// Check status.
		OPCSERVERSTATUS * status ;

		HRESULT hr = OpcServer->GetStatus (&status) ;
		if(FAILED(hr)) 
		{
			SysFreeString (VendorInfo) ;
			VendorInfo = SysAllocString (L"OPC Server status unavailable") ;
			return S_OK ;
		}

		SysFreeString (VendorInfo) ;
		VendorInfo = SysAllocString (status->szVendorInfo) ;

		CoTaskMemFree(status->szVendorInfo);
		CoTaskMemFree(status);
	}

	*pVal = SysAllocString (VendorInfo) ;

	return S_OK;
}

STDMETHODIMP CClient::CreateGroup(IGroup ** pGroup)
{
	CComQIPtr <IGroup> group ;
	group.CoCreateInstance (__uuidof(Group), NULL, CLSCTX_ALL) ;
	group->put_Client (this) ;
	group->put_Server (OpcServer) ;

	return group->QueryInterface (__uuidof(IGroup), (void**)pGroup) ;
}
