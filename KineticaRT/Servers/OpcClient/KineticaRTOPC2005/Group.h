// Group.h : Declaration of the CGroup

#ifndef __GROUP_H_
#define __GROUP_H_

#include "resource.h"       // main symbols

class CClient ;

/////////////////////////////////////////////////////////////////////////////
// CGroup
class ATL_NO_VTABLE CGroup : 
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CGroup, &CLSID_Group>,
	public IDispatchImpl<IGroup, &IID_IGroup, &LIBID_KineticaRT_OPC>
{
private:
	//----------------------------------------------------------------------------------------------------------
	// CTestAdviseSink - Sinks events from the OPC group.
	//----------------------------------------------------------------------------------------------------------
	class ATL_NO_VTABLE CTestAdviseSink : public CComObjectRoot, public IOPCDataCallback
	{
		private: 
			CGroup * parent ;

		public:
			BEGIN_COM_MAP(CTestAdviseSink)
				COM_INTERFACE_ENTRY(IOPCDataCallback)
			END_COM_MAP()

			STDMETHOD(OnDataChange)		(DWORD dwTransid, OPCHANDLE hGroup, HRESULT hrMasterquality, HRESULT hrMastererror, DWORD dwCount, OPCHANDLE *phClientItems, VARIANT *pvValues, WORD *pwQualities, FILETIME *pftTimeStamps, HRESULT *pErrors) ;

			STDMETHOD(OnReadComplete)	(DWORD /*dwTransid*/, OPCHANDLE /*hGroup*/, HRESULT /*hrMasterquality*/, HRESULT /*hrMastererror*/, DWORD /*dwCount*/, OPCHANDLE * /*phClientItems*/, VARIANT * /*pvValues*/, WORD * /*pwQualities*/, FILETIME * /*pftTimeStamps*/, HRESULT * /*pErrors*/) { return S_OK; }
			STDMETHOD(OnWriteComplete)	(DWORD /*dwTransid*/, OPCHANDLE /*hGroup*/, HRESULT /*hrMastererr*/, DWORD /*dwCount*/, OPCHANDLE * /*pClienthandles*/, HRESULT * /*pErrors*/) { return S_OK; }
			STDMETHOD(OnCancelComplete)	(DWORD /*dwTransid*/, OPCHANDLE /*hGroup*/) { return S_OK; }

			// Local
			void put_Parent (CGroup * new_parent) {parent = new_parent;}
	} ;

	typedef CComObject<CTestAdviseSink> CComCTestAdviseSink;

public:
	HRESULT	FinalConstruct	() ;
	void	FinalRelease	() ;

public:
	CGroup()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_GROUP)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CGroup)
	COM_INTERFACE_ENTRY(IGroup)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

// IGroup
public:
	STDMETHOD(UpdateItems)(/*[in]*/ long count, /*[in]*/ SAFEARRAY* items, /*[in]*/ SAFEARRAY* values);
	STDMETHOD(DeleteItem)(/*[in]*/ long pItem);
	STDMETHOD(DeleteItems)(/*[in]*/ long count, /*[in]*/ SAFEARRAY* items);
	STDMETHOD(get_Destination)(/*[out, retval]*/ IDispatch* *pVal);
	STDMETHOD(put_Destination)(/*[in]*/ IDispatch* newVal);
	STDMETHOD(get_Server)(/*[out, retval]*/ IUnknown* *pVal);
	STDMETHOD(put_Server)(/*[in]*/ IUnknown* newVal);
	STDMETHOD(get_Client)(/*[out, retval]*/ IClient * *pVal);
	STDMETHOD(put_Client)(/*[in]*/ IClient * newVal);
	STDMETHOD(CreateItem)(/*[in]*/ long id, /*[in]*/ BSTR itemID, /*[out]*/ VARIANT *type, /*[out]*/ long *pItem);
	STDMETHOD(get_DeadBand)(/*[out, retval]*/ float *pVal);
	STDMETHOD(put_DeadBand)(/*[in]*/ float newVal);
	STDMETHOD(get_UpdateRate)(/*[out, retval]*/ long *pVal);
	STDMETHOD(put_UpdateRate)(/*[in]*/ long newVal);
	STDMETHOD(get_Connected)(/*[out, retval]*/ BOOL *pVal);
	STDMETHOD(put_Connected)(/*[in]*/ BOOL newVal);

	CComQIPtr<IDispatch>	Destination ;

private:
	long					UpdateRate ;
	float					DeadBand ;
	BOOL					Connected ;

	CComPtr<IClient>		client ;
	CComQIPtr<IOPCServer>	Server ;
	CComPtr<IUnknown>		OpcGroup ;
	CComPtr<IUnknown>		advise_sink ;
	DWORD					advise_sink_cookie ;
	OPCHANDLE				server_group_handle	;
};

#endif //__GROUP_H_
