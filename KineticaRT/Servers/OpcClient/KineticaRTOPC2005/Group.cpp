// Group.cpp : Implementation of CGroup
#include "stdafx.h"
#include "KineticaRTOPC.h"
#include "Group.h"

//CRITICAL_SECTION		critical ;

//----------------------------------------------------------------------------------------------------------
// CTestAdviseSink - Sinks events from the OPC group.
//----------------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------------
// CTestAdviseSink::OnDataChange
//		Called on startup and whenever a channel changes by more than
//		the deadband.  Including when we change it by a write.
//		All in OpcFormatDatatime format.
//----------------------------------------------------------------------------------------------------------

STDMETHODIMP CGroup::CTestAdviseSink::OnDataChange (DWORD /*dwTransid*/, OPCHANDLE /*hGroup*/, HRESULT /*hrMasterquality*/, HRESULT /*hrMastererror*/, DWORD dwCount, OPCHANDLE *phClientItems, VARIANT *pvValues, WORD * pwQualities, FILETIME * /*pftTimeStamps*/, HRESULT * /*pErrors*/)
{
	HRESULT			hr ;

//	EnterCriticalSection (&critical) ;

	CComDispatchDriver dest (parent->Destination) ;

	CComVariant update_rate ;

	DISPID update_dispid ;


	hr = dest.GetIDOfName(L"Update", &update_dispid) ;

	if (hr == S_OK)
	{
		VARIANT id_array ;
		VARIANT value_array ;

		{
			//create a safearray of the expected size
			SAFEARRAY *			pArray ;
			SAFEARRAYBOUND		rgsabound[2] ;
			
			rgsabound[0].lLbound	= rgsabound[1].lLbound = 1 ;	//1-based
			rgsabound[0].cElements	= dwCount;
			rgsabound[1].cElements	= 1 ;

			pArray = SafeArrayCreate(VT_UI4, 2, rgsabound);
			if (pArray)
			{
				unsigned long * element ;
				unsigned long *	data = phClientItems ;

				SafeArrayAccessData (pArray, (void**)&element) ;

				// Have to convert from float to double.
				for (unsigned long count = 0 ; count < dwCount; count ++)
				{
					*element++ = *data++ ;
				}

				SafeArrayUnaccessData (pArray) ;

				id_array.vt = VT_UI4 | VT_ARRAY ;
				id_array.parray = pArray ;
			}
		}
			
		{
			//create a safearray of the expected size
			SAFEARRAY *			pArray ;
			SAFEARRAYBOUND		rgsabound[2] ;
			
			rgsabound[0].lLbound	= rgsabound[1].lLbound = 1 ;	//1-based
			rgsabound[0].cElements	= dwCount;
			rgsabound[1].cElements	= 1 ;

			pArray = SafeArrayCreate(VT_VARIANT, 2, rgsabound);
			if (pArray)
			{
				VARIANT *	element ;
				VARIANT *	data = pvValues ;
				WORD *		quality = pwQualities ;

				SafeArrayAccessData (pArray, (void**)&element) ;

				// Have to convert from float to double.
				for (unsigned long count = 0 ; count < dwCount; count ++)
				{
					if (*quality != OPC_QUALITY_BAD)
					{
						*element = *data ;
					}
					else
					{
						element->vt = VT_EMPTY ;
					}
//					VariantInit (element) ;
//					VariantCopy (element++, data++) ;
					
					quality ++;
					element++ ;
					data++ ;
				}

				SafeArrayUnaccessData (pArray) ;

				value_array.vt = VT_VARIANT | VT_ARRAY ;
				value_array.parray = pArray ;
			}
		}
			
//		LeaveCriticalSection (&critical) ;
		dest.Invoke2 (update_dispid, &id_array, &value_array, NULL) ;

		// clear value array.  Since it is full of the servers data, the data should not be deleted.
		{
			if (value_array.parray)
			{
				VARIANT *	element ;

				SafeArrayAccessData (value_array.parray, (void**)&element) ;

				for (unsigned long count = 0 ; count < dwCount; count ++)
				{
					(element++)->vt = VT_EMPTY ;
				}

				SafeArrayUnaccessData (value_array.parray) ;
				SafeArrayDestroy (value_array.parray) ;
			}
		}

		VariantClear(&id_array);
	}
	else
	{
//		LeaveCriticalSection (&critical) ;
	}

	return S_OK ;
}

/////////////////////////////////////////////////////////////////////////////
// CGroup

HRESULT CGroup::FinalConstruct () 
{
//	InitializeCriticalSection (&critical) ;
	advise_sink_cookie		= 0 ;

	CComCTestAdviseSink *sink = new CComCTestAdviseSink ;
	sink->QueryInterface (&advise_sink) ;
	sink->put_Parent (this) ;

	return CComObjectRootEx<CComMultiThreadModel>::FinalConstruct () ;
}

//----------------------------------------------------------------------------------------------------------

void CGroup::FinalRelease () 
{
	if (advise_sink_cookie)
	{
		AtlUnadvise(OpcGroup, __uuidof(IOPCDataCallback), advise_sink_cookie);
		advise_sink_cookie = 0 ;
	}

//	DeleteCriticalSection (&critical) ;

	CComObjectRootEx<CComMultiThreadModel>::FinalRelease () ;
}

STDMETHODIMP CGroup::get_UpdateRate(long *pVal)
{
	*pVal = UpdateRate ;

	return S_OK;
}

STDMETHODIMP CGroup::put_UpdateRate(long newVal)
{
	if (UpdateRate == newVal) return S_OK;

	UpdateRate = newVal ;

	unsigned long new_update_rate = newVal ;

	DWORD actual_update_rate = 0 ;
	CComQIPtr <IOPCGroupStateMgt> group (OpcGroup) ;
	if (group) group->SetState (&new_update_rate, &actual_update_rate, NULL, NULL, NULL, NULL, NULL) ;

	return S_OK;
}

STDMETHODIMP CGroup::get_DeadBand(float *pVal)
{
	*pVal = DeadBand ;

	return S_OK;
}

STDMETHODIMP CGroup::put_DeadBand(float newVal)
{
	if (DeadBand == newVal) return S_OK;

	DeadBand = newVal ;

	DWORD actual_update_rate = 0 ;
	CComQIPtr <IOPCGroupStateMgt> group (OpcGroup) ;
	if (group) group->SetState (NULL, &actual_update_rate, NULL, NULL, &DeadBand, NULL, NULL) ;

	return S_OK;
}


STDMETHODIMP CGroup::get_Connected(BOOL *pVal)
{
	*pVal = Connected ;

	return S_OK;
}

STDMETHODIMP CGroup::put_Connected(BOOL newVal)
{
	HRESULT hr ;

	if (Connected == newVal) return S_OK;

	Connected = newVal ;

	if (Connected)
	{
		if (advise_sink_cookie)
		{
			AtlUnadvise(OpcGroup, __uuidof(IOPCDataCallback), advise_sink_cookie);
			advise_sink_cookie = 0 ;
		}
		if (OpcGroup) OpcGroup.Release() ; 

	#pragma message ("Should do a Remove Group") 

		if (Server)
		{
			long		time_bias = 0 ;
			DWORD		actual_update_rate = 0 ;

			hr = Server->AddGroup
			(
				L"",								// [in] Server name, if NULL OPC Server will generate a unique name
				TRUE		,						// [in] State of group to add
				UpdateRate,							// [in] Requested update rate for group (ms)
				1234,								// [in] Client handle to OPC Group
				&time_bias,							// [in] Time 
				&DeadBand,							// [in] Percent Deadband
				0,									// [in] Localization ID
				&server_group_handle,				// [out] Server Handle to group
				&actual_update_rate,				// [out] Revised update rate
				IID_IUnknown,						// [in] Type of interface desired
				&OpcGroup							// [out] where to store the interface pointer
			);	


			if (SUCCEEDED(hr))
			{
				// Connect advise sink.
				hr = AtlAdvise(OpcGroup, advise_sink, __uuidof(IOPCDataCallback), &advise_sink_cookie);
			}
		}
	}
	else
	{
		// Disconnect.
		if (OpcGroup != NULL)
		{
			if (advise_sink_cookie)
			{
				AtlUnadvise(OpcGroup, __uuidof(IOPCDataCallback), advise_sink_cookie);
				advise_sink_cookie = 0 ;
			}
			OpcGroup.Release () ; 
			OpcGroup = NULL ;
			hr = Server->RemoveGroup (server_group_handle, TRUE) ;
		}
	}

	return S_OK;
}


STDMETHODIMP CGroup::CreateItem(long id, /*[in]*/ BSTR itemID, /*[out]*/ VARIANT *type, long * pItem)
{
	HRESULT hr = S_OK ;
	*pItem = -1 ;

	OPCITEMDEF		item_def ;
	OPCITEMRESULT *	item_result		= NULL;
	HRESULT *		item_error		= NULL;

	item_def.szAccessPath			= NULL;
	item_def.szItemID				= itemID ;
	item_def.bActive				= TRUE;
	item_def.hClient				= id ;
	item_def.dwBlobSize				= 0;
	item_def.pBlob					= NULL;
	// item_def.vtRequestedDataType	= VT_R8 ;
	item_def.vtRequestedDataType	= VT_EMPTY ;	// Changed 10 Jan 2001 by N.Savidge.

	CComQIPtr<IOPCItemMgt> item_mgt (OpcGroup) ;
	if (item_mgt)
	{
		VariantInit (type) ;
		hr = item_mgt->AddItems (1, &item_def, &item_result, &item_error);

		if (SUCCEEDED (hr))
		{
			// Get the server handle.
			if (SUCCEEDED (*item_error))
			{
				*pItem = item_result->hServer ;
				if 
				(
					(item_result->vtCanonicalDataType <= VT_HRESULT) &&
					(item_result->vtCanonicalDataType != VT_BSTR) &&
					(item_result->vtCanonicalDataType != VT_UNKNOWN) &&
					(item_result->vtCanonicalDataType != VT_VARIANT) &&
					(item_result->vtCanonicalDataType != VT_DISPATCH)
				)
				{
					type->vt = item_result->vtCanonicalDataType ;
				}
				else
				{
					type->vt = VT_BSTR ;
					type->bstrVal = SysAllocString (L"") ;
				}
			}
			else
			{
				hr = *item_error ;
			}

			CoTaskMemFree(item_result);
			CoTaskMemFree(item_error);
		}
	}
	
	return S_OK ;
}

STDMETHODIMP CGroup::get_Client(IClient **pVal)
{
	client.QueryInterface (pVal) ;

	return S_OK;
}

STDMETHODIMP CGroup::put_Client(IClient *newVal)
{
	client = newVal ;

	return S_OK;
}

STDMETHODIMP CGroup::get_Server(IUnknown **pVal)
{
	Server.QueryInterface (pVal) ;

	return S_OK;
}

STDMETHODIMP CGroup::put_Server(IUnknown *newVal)
{
	Server = newVal ;

	return S_OK;
}

STDMETHODIMP CGroup::get_Destination(IDispatch **pVal)
{
	Destination.QueryInterface (pVal) ;

	return S_OK;
}

STDMETHODIMP CGroup::put_Destination(IDispatch *newVal)
{
	if (Destination == newVal) return S_OK;

	Destination = newVal ;

//	CComDispatchDriver dest (Destination) ;

//	CComVariant update_rate ;

//	dest.GetPropertyByName(L"UpdateRate", &update_rate) ;

	return S_OK;
}

STDMETHODIMP CGroup::DeleteItems(/*[in]*/ long dwCount, /*[in]*/ SAFEARRAY* items)
{
	HRESULT hr = S_OK ;

	HRESULT *		item_error		= NULL;
	OPCHANDLE *		server_handle	= new OPCHANDLE [dwCount] ;

	// Copy server handle array.
	{
		unsigned long *	data = server_handle ;
		VARIANT *	element ;

		SafeArrayAccessData (items, (void**)&element) ;

		// Have to convert from float to double.
		for (long count = 0 ; count < dwCount; count ++)
		{
			*data++ = (*element++).ulVal ;
		}

		SafeArrayUnaccessData (items) ;
	}

	CComQIPtr<IOPCItemMgt> item_mgt (OpcGroup) ;
	if (item_mgt)
	{
		hr = item_mgt->RemoveItems (dwCount, server_handle, &item_error);

		if (SUCCEEDED (hr))
		{
			// Get the server handle.
			if (SUCCEEDED (*item_error))
			{
			}
			else
			{
				hr = *item_error ;
			}

			CoTaskMemFree(item_error);
		}
	}

	return S_OK ;
}

STDMETHODIMP CGroup::DeleteItem(long pItem)
{
	HRESULT hr = S_OK ;

	if (pItem == -1) return hr ;

	HRESULT *		item_error		= NULL;
	OPCHANDLE		item = pItem ;

	CComQIPtr<IOPCItemMgt> item_mgt (OpcGroup) ;
	if (item_mgt)
	{
		hr = item_mgt->RemoveItems (1, &item, &item_error);

		if (SUCCEEDED (hr))
		{
			// Get the server handle.
			if (SUCCEEDED (*item_error))
			{
			}
			else
			{
				hr = *item_error ;
			}

			CoTaskMemFree(item_error);
		}
	}

	return S_OK ;
}

STDMETHODIMP CGroup::UpdateItems(/*[in]*/ long dwCount, /*[in]*/ SAFEARRAY* items, /*[in]*/ SAFEARRAY* values)
{
	HRESULT		hr = S_OK ;
	CComQIPtr<IOPCSyncIO>	ogi (OpcGroup) ;

	if (ogi)
	{
		HRESULT*	errors ;

		OPCHANDLE *	server_handle	= new OPCHANDLE [dwCount] ;
		VARIANT *	newVal			;//= new VARIANT [dwCount] ;


		{
			unsigned long *	data = server_handle ;
			VARIANT *	element ;

			SafeArrayAccessData (items, (void**)&element) ;

			// Have to convert from float to double.
			for (long count = 0 ; count < dwCount; count ++)
			{
				*data++ = (*element++).ulVal ;
			}

			SafeArrayUnaccessData (items) ;
		}
			

//		{
//			VARIANT *	element ;
//			VARIANT *	data = newVal ;
//
//			SafeArrayAccessData (values, (void**)&element) ;
//
//			// Have to convert from float to double.
//			for (long count = 0 ; count < dwCount; count ++)
//			{
//				VariantInit (data) ;
//				VariantCopy (data++, element++) ;
//			}
//
//			SafeArrayUnaccessData (values) ;
//		}
//
		SafeArrayAccessData (values, (void**)&newVal) ;

		hr = ogi->Write (dwCount, server_handle, newVal, &errors) ;
		
		SafeArrayUnaccessData (values) ;

		delete [] server_handle ;

		if (SUCCEEDED (hr))
		{
//			if (*errors)
//			{
//				// Just in case we are talking to a Rockwell based server!
//				CoTaskMemFree(errors);
//				CKRT_Variant iVal ;
//				iVal.Copy(newVal, VT_I4) ;
//				hr = ogi->Write (1, &server_handle, &iVal, &errors) ;
//			}
//
//			if (SUCCEEDED (hr))
//			{
			hr = *errors ;
			CoTaskMemFree(errors);
//			}
		}

//		// Free the memory.
//		{
//			VARIANT *	data = newVal ;
//			for (long count = 0 ; count < dwCount; count ++)
//			{
//				VariantClear (data++) ;
//			}
//		}
	}

	return S_OK ;
}
