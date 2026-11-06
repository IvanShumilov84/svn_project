/////////////////////////////////////////////////////////////////////////////
// CControlsCollection

//---------------------------------------------------------------------------
//	_IKRT_XML interface: Load and Save XML
//---------------------------------------------------------------------------

STDMETHODIMP CControlsCollection::LoadXML(IXMLDOMNode * pNode)
{
	HRESULT						hr ;
	CComQIPtr<IXMLDOMNode>		pChildNode ;
	CComQIPtr<IXMLDOMNode>		pSibling ;
	CComQIPtr<IXMLDOMElement>	pElement ;
	HCURSOR						old_cursor ;
	HCURSOR						wait_cursor ;

	wait_cursor = LoadCursor(NULL, IDC_WAIT) ;
	old_cursor = SetCursor(wait_cursor) ;

	//process children
	hr = pNode->get_firstChild(&pChildNode) ;
	while (pChildNode)
	{
		pElement = pChildNode ;
		if (pElement)
		{
			//process any children of this node
			SetCursor(wait_cursor) ;
			read_node(pChildNode) ;
		}
		//else not an element - ignore it

		//go on to next sibling
		pChildNode->get_nextSibling(&pSibling) ;
		pChildNode = pSibling ;
		pSibling.Release() ;
	}

	Refresh() ;
	clear_undos() ;

	Connect(krt_connect_mode_connect, true) ;

	SetCursor(old_cursor) ;

	is_dirty = false ;

	return hr ;
}

STDMETHODIMP CControlsCollection::SaveXML(IXMLDOMNode * pNode)
{
	HRESULT				hr ;

	//remove all children of 'controls' element
	{
		CComQIPtr<IXMLDOMNodeList>		pChildList ;
		CComQIPtr<IXMLDOMNode>			pChild ;

		hr = pNode->get_childNodes(&pChildList) ;

		pChildList->get_item(0, &pChild) ;
		while (pChild)
		{
			hr = pNode->removeChild(pChild, NULL) ;
			pChild.Release() ;

			pChildList->get_item(0, &pChild) ;
		}
	}

	//ensure no deleted controls are saved
	clear_undos() ;

	CComQIPtr<IXMLDOMDocument>		pDoc ;
	pNode->get_ownerDocument(&pDoc) ;

	//go through and add the controls...
	for (int index = 0; index < no_of_items; index++)
	{
		//did this control exist when file was loaded?
		CComQIPtr<IXMLDOMNode>		pExistingNode ;
		CComQIPtr<IXMLDOMElement>	pElement ;

		if (list[index].pNode)
		{
			CComQIPtr<IXMLDOMNode>		pNodeClone ;

			hr = list[index].pNode->cloneNode(-1, &pNodeClone) ;
			pElement = pNodeClone ;
			hr = pNode->appendChild(pNodeClone, NULL);
			//update node pointer in list
			list[index].pNode = pNodeClone ;
		}
		else		//new object
		{
			CComQIPtr<IXMLDOMNode>			pNewNode ;

			//create a new node and add it as child of the controls node
			//Type
			if (list[index].ctlType.Length() == 0)
			{
				list[index].ctlType = prog_id_to_object_type(list[index].ctlProgId) ;
			}

			hr = pDoc->createElement(list[index].ctlType, &pElement);
			if (FAILED(hr))
			{
				WCHAR		text[0x100] ;
				wsprintf(text, L"create element named %s, type %s failed (hr = 0x%0x) - ignoring control", list[index].name, list[index].ctlType, hr) ;
				::MessageBox(frame_window, text, L"SaveXMLFile", MB_OK) ;
				continue;
			}

			hr = pNode->appendChild(pElement, &pNewNode);
			if (FAILED(hr))
			{
				WCHAR		text[0x100] ;
				wsprintf(text, L"append element named %s, type %s failed (hr = 0x%0x) - ignoring control", list[index].name, list[index].ctlType, hr) ;
				::MessageBox(frame_window, text, L"SaveXMLFile", MB_OK) ;
				continue;
			}

			//store the node pointer for next time
			list[index].pNode = pNewNode ;
		}

		if (pElement)
		{
			//update the element's name and position attributes
			CComQIPtr<_IKRT_XML>			xml(list[index].item) ;

			//save prog id
			hr = pElement->setAttribute(L"Object", list[index].ctlProgId) ;

			//always set the name and position
			if (!list[index].position.left.formula.Length())	create_formula(&list[index].position.left, x_offset) ;
			if (!list[index].position.top.formula.Length())		create_formula(&list[index].position.top, y_offset) ;
			if (!list[index].position.width.formula.Length())	create_formula(&list[index].position.width) ;
			if (!list[index].position.height.formula.Length())	create_formula(&list[index].position.height) ;

			hr = pElement->setAttribute(L"Name", CKRT_Variant(list[index].name)) ;
			hr = pElement->setAttribute(L"Left", list[index].position.left.formula) ;
			hr = pElement->setAttribute(L"Top", list[index].position.top.formula) ;
			hr = pElement->setAttribute(L"Width", list[index].position.width.formula) ;
			hr = pElement->setAttribute(L"Height", list[index].position.height.formula) ;
			hr = pElement->setAttribute(L"Visible", CKRT_Bstr(list[index].visible)) ;
			hr = pElement->setAttribute(L"LockPosition", CKRT_Bstr(list[index].lock_pos)) ;
			hr = pElement->setAttribute(L"LockSize", CKRT_Bstr(list[index].lock_size)) ;

			if (xml)
			{
				//tell control to save itself and its children
				xml->SaveXML(pElement) ;
			}
			else
			{
				//does it have a property bag?
				CComQIPtr<IPersistPropertyBag>		persist(list[index].item) ;
				CComQIPtr<IPropertyBag>				bag(this) ;
				
				if (persist)
				{
					bag_object_element = pElement ;
					hr = persist->Save(bag, TRUE, TRUE) ;
					bag_object_element.Release() ;
				}
				else 
				{
					//cannot save it
					WCHAR		text[0x100] ;
					wsprintf(text, L"Cannot save control named %s - no xml or property bag interface", list[index].name) ;
					::MessageBox(frame_window, text, L"SaveXMLFile", MB_OK) ;
				}
			}
		}
		else
		{
			//should never get here
			WCHAR		text[0x100] ;
			wsprintf(text, L"no xml element for control named %s - ignoring control", list[index].name) ;
			::MessageBox(frame_window, text, L"SaveXMLFile", MB_OK) ;
		}
	}
	
	is_dirty = false ;

	return S_OK ;
}

//---------------------------------------------------------------------------
//	IControlsCollection interface: properties (alphabetical order)
//---------------------------------------------------------------------------

STDMETHODIMP CControlsCollection::get_LockSize(VARIANT Name, VARIANT *pVal)
//...

STDMETHODIMP CControlsCollection::put_LockSize(VARIANT Name, VARIANT newVal)
//...

STDMETHODIMP CControlsCollection::get_LockPosition(VARIANT Name, VARIANT *pVal)
//...

STDMETHODIMP CControlsCollection::put_LockPosition(VARIANT Name, VARIANT newVal)
//...

//---------------------------------------------------------------------------
//	IControlsCollection interface: methods (alphabetical order)
//---------------------------------------------------------------------------

// Renames the given control
STDMETHODIMP CControlsCollection::Rename(VARIANT OldName, VARIANT NewName)
{
	int		index		= get_control_index(OldName) ;

	if (index == -1)		return Error (L"Name does not exist", GUID_NULL, E_INVALIDARG) ;

	if (get_control_index(NewName) != -1)
	{
		return Error (L"New name already exists", GUID_NULL, E_INVALIDARG) ;
	}

	CKRT_Bstr(NewName).CopyTo(list[index].name) ;
	tip_update_text(index) ;

	return S_OK ;
}

//---------------------------------------------------------------------------
//	_IControlsCollection interface: methods (alphabetical order)
//---------------------------------------------------------------------------

// Adds a control specified by an index into the 'addables' list
STDMETHODIMP CControlsCollection::AddFromAddablesList(int Index, POINT MousePos)
{
	if (Index < 0 || Index >= no_of_addables)		return S_FALSE ;

	HRESULT				hr				= S_FALSE ;
	CKRT_Variant		new_name ;
	int					index			= 0 ;
	WCHAR				temp[100] ;

	//name the object according to its type
	do
	{
		index++ ;
		wsprintf(temp, L"%s%d", addables[Index].type, index) ;
		new_name = temp ;
	}
	while (get_control_index(new_name) != -1) ;

	//...
}

// Puts a copy of the selected controls on the clipboard
STDMETHODIMP CControlsCollection::Copy(void)
{
	if (current_selection == -1)		return S_FALSE ;

	//store info about selected controls
	CComQIPtr<IStorage>		storage ;
	CComQIPtr<IStream>		stream ;
	HRESULT					hr ;
	int						index ;
	int						sel_count		= no_of_selected_controls() ;
	int						control_index ;
	int						first_control	= -1 ;
	CComQIPtr<ILockBytes>	lock_bytes ;
	HGLOBAL					hglobal ;

	if (!::OpenClipboard(frame_window))			return S_FALSE ;
	
	if (!EmptyClipboard())
	{
		CloseClipboard() ;
		return S_FALSE ;
	}

	CreateILockBytesOnHGlobal(NULL, FALSE, &lock_bytes) ;
	hr = StgCreateDocfileOnILockBytes(lock_bytes, STGM_DIRECT | STGM_READWRITE | STGM_CREATE | STGM_SHARE_EXCLUSIVE, 0 , &storage) ;
	if (FAILED (hr))
	{
		CloseClipboard() ;
		return S_FALSE ;
	}

	// Save number, names and positions in a stream.
	hr = storage->CreateStream (L"Properties", STGM_WRITE | STGM_SHARE_EXCLUSIVE | STGM_CREATE, 0, 0, &stream) ;
	if (FAILED (hr))
	{
		CloseClipboard() ;
		return S_FALSE ;
	}

	hr = stream->Write(&sel_count, sizeof(int), NULL);
	if (FAILED (hr))
	{
		CloseClipboard() ;
		return S_FALSE ;
	}

	reprocess_selected(false) ;

	//save object info
	for (index = 0, control_index = 0; index < no_of_items; index++)
	{
		if (list[index].selected && !list[index].deleted)
		{
			CComQIPtr<IStorage>			ctl_storage ;
			CComQIPtr<IPersistStorage>	persist ;
			WCHAR						name [50] ;
			long						left ;
			long						top ;
			
			wsprintf (name, L"Control%d", control_index) ;

			hr = storage->CreateStorage (name, STGM_READWRITE | STGM_SHARE_EXCLUSIVE | STGM_CREATE, 0, 0, &ctl_storage) ;
			if (FAILED (hr))
			{
				CloseClipboard() ;
				return S_FALSE ;
			}

			hr = list[index].item->QueryInterface (IID_IPersistStorage, (void **)&persist) ;
			if (SUCCEEDED(hr))
			{
				CLSID clsid ;
				persist->GetClassID (&clsid) ;
				hr = persist->Save (ctl_storage, FALSE) ;
			}

			//save name and prog_id to stream
			list[index].name.WriteToStream(stream) ;		
			list[index].ctlProgId.WriteToStream(stream) ;		

			//also save the size (formulae) and position relative to first selected control
			if (!list[index].position.width.formula.Length())	create_formula(&list[index].position.width) ;
			if (!list[index].position.height.formula.Length())	create_formula(&list[index].position.height) ;

			hr = list[index].position.width.formula.WriteToStream(stream) ;
			hr = list[index].position.height.formula.WriteToStream(stream) ;

			if (first_control == -1)
			{
				first_control = index ;
				left = 0 ;
				top = 0 ;
			}
			else
			{
				left = list[index].position.left.value - list[first_control].position.left.value ;
				top = list[index].position.top.value - list[first_control].position.top.value ;
			}

			hr = stream->Write(&left, sizeof(long), NULL) ;
			hr = stream->Write(&top, sizeof(long), NULL) ;

			//...and visible
			hr = stream->Write(&list[index].visible, sizeof(bool), NULL) ;

			//...and lock status (size only for copy/paste)
			hr = stream->Write(&list[index].lock_size, sizeof(bool), NULL) ;

			control_index++ ;
		}
	}

	GetHGlobalFromILockBytes(lock_bytes, &hglobal) ;
	lock_bytes.Release() ;
	stream.Release() ;
	storage.Release() ;

	SetClipboardData(clipboard_format, hglobal) ;
	CloseClipboard() ;

	return S_OK ;
}

// Frame has been given a new addables list, which it has dealt with
// - pass the list on to any child frames
STDMETHODIMP CControlsCollection::NewAddList(VARIANT newList)
{
	applog.Write(CKRT_Bstr(L"passing add list node to child frames")) ;

	for (int index = 0; index < no_of_items; index++)
	{
		if (!list[index].deleted)
		{
			//is it a frame?
			if (!lstrcmp(list[index].ctlProgId, L"KRTLib_Frame.KRT_Frame"))
			{
				//yes!
				CKRT_Ptr<_IKRT_Frame>		frame(list[index].item) ;
				
				if (frame)
				{
					frame->put_AddListNode(newList) ;
//					//parse the new file
//					frame->put_AddListFile(newFile) ;
//					frame->ParseAddListFile() ;
				}
			}
		}
	}

	return S_OK;
}

// Pastes the controls in the clipboard to the cursor position
// Uses grid size or '1' if grid disabled
STDMETHODIMP CControlsCollection::Paste(POINT mouse_pos)
{
//	DWORD total_ticks = GetTickCount() ;
//	DWORD ticks = total_ticks ;
//	DWORD ticks2 ;

	HRESULT					hr ;
	CComQIPtr<IStorage>		storage ;
	CComQIPtr<IStream>		stream ;
	int						count			= 0 ;
	int						index ;
	bool					first_object	= true ;
	int						no_added		= 0 ;		//no of controls successfully added
	CComQIPtr<ILockBytes>	lock_bytes ;
	HGLOBAL					hglobal ;

	if (!::OpenClipboard(frame_window))		return false ;
	if (!IsClipboardFormatAvailable(clipboard_format))
	{
		CloseClipboard() ;
		return false ;
	}

	hglobal = ::GetClipboardData(clipboard_format) ;
	if (!hglobal)
	{
		CloseClipboard() ;
		return false ;
	}

	CreateILockBytesOnHGlobal(hglobal, FALSE, &lock_bytes) ;
	hr = StgOpenStorageOnILockBytes(lock_bytes, NULL, STGM_DIRECT | STGM_READWRITE | STGM_SHARE_EXCLUSIVE, NULL, 0 , &storage) ;
	if (FAILED(hr))
	{ 
		CloseClipboard() ;
		return false ;
	} 

	// number, names and positions
	hr = storage->OpenStream (L"Properties", NULL, STGM_DIRECT | STGM_READ | STGM_SHARE_EXCLUSIVE, 0, &stream) ;
	if (FAILED(hr))
	{ 
		CloseClipboard() ;
		return false ;
	} 

	//load controls
	//get count
	hr = stream->Read(&count, sizeof(int), NULL);
	if (FAILED(hr))
	{ 
		CloseClipboard() ;
		return false ;
	} 

	ClearSelections() ;

//	ticks2 = GetTickCount() ;
//	ATLTRACE2(atlTraceUser, 0, L"pre-loop time %d\n", ticks2 - ticks) ;
//	ticks = ticks2 ;

	for (index = 0; index < count; index++)
	{
		CComQIPtr<IStorage>	ctl_storage ;
		WCHAR				name [50] ;
		
		wsprintf (name, L"Control%d", index) ;

		hr = storage->OpenStorage(name, NULL, STGM_DIRECT | STGM_READ | STGM_SHARE_EXCLUSIVE, 0, 0, &ctl_storage) ;
		if (SUCCEEDED(hr))
		{
			CKRT_Bstr					control_name ;
			CKRT_Bstr					temp_name ;
			CKRT_Variant				varName ;
			CKRT_Bstr					control_prog_id ;
			CComQIPtr<IPersistStorage>	persist ;
			int							control_index ;
//			WCHAR						temp_left[0x100] ;
//			WCHAR						temp_top[0x100] ;

			control_name.ReadFromStream(stream) ;
			control_name.CopyTo(varName) ;

			control_prog_id.ReadFromStream(stream) ;

			hr = add(varName, control_prog_id, 0, 0, 10, 10, first_object, false, &control_index) ;
			while (hr == KRT_E_Duplicate)
			{
				increment_name(varName, temp_name) ;

//				temp_name.CopyTo(&varName.bstrVal) ; 
				temp_name.CopyTo(varName) ; 

				hr = add(varName, control_prog_id, 0, 0, 10, 10, first_object, false, &control_index) ;
			}

			if (FAILED(hr))			continue ;

			persist = list[control_index].item ;

//			ticks2 = GetTickCount() ;
//			ATLTRACE2(atlTraceUser, 0, L"add ctl time %d\n", ticks2 - ticks) ;
//			ticks = ticks2 ;

			if (persist)		hr = persist->Load (ctl_storage) ;

//			ticks2 = GetTickCount() ;
//			ATLTRACE2(atlTraceUser, 0, L"persist load time %d\n", ticks2 - ticks) ;
//			ticks = ticks2 ;

			//size as formulae
			hr = list[control_index].position.width.formula.ReadFromStream(stream) ;
			hr = list[control_index].position.height.formula.ReadFromStream(stream) ;

			process_position(&list[control_index].position.width, 0, x_scale) ;
			process_position(&list[control_index].position.height, 0, y_scale) ;

			//position relative to first object
			hr = stream->Read(&list[control_index].position.left.value, sizeof(long), NULL);
			hr = stream->Read(&list[control_index].position.top.value, sizeof(long), NULL);

			if (first_object)
			{
				long		left ;
				long		top ;

				first_object = false ;
				current_selection = control_index ;

				//move it to (a grid position) underneath the cursor
				::ScreenToClient(frame_window, &mouse_pos) ;

				left = mouse_pos.x - list[control_index].position.width.value / 2 ;
				if (left < x_offset )
				{
					left = x_offset ;
				}
				else if ( (left + list[control_index].position.width.value) > scale_x(1))
				{
					left = scale_x(1) - list[control_index].position.width.value ;
				}

				top = mouse_pos.y - list[control_index].position.height.value / 2 ;
				if (top < y_offset )
				{
					top = y_offset ;
				}
				else if ( (top + list[control_index].position.height.value) > scale_y(1))
				{
					top = scale_y(1) - list[control_index].position.height.value ;
				}

				if (h_grid.enabled && h_grid.pixels > 1)
				{
					move_to_grid_position (&left, list[control_index].position.width.value, true) ;
				}
				if (v_grid.enabled && v_grid.pixels > 1)
				{
					move_to_grid_position (&top, list[control_index].position.height.value, false) ;
				}

				list[control_index].position.left.value = left ;
				list[control_index].position.top.value = top ;

				list[control_index].position.left.formula = L"" ;
				list[control_index].position.top.formula = L"" ;
//				create_formula(&list[control_index].position.left, x_offset) ;
//				create_formula(&list[control_index].position.top, y_offset) ;
			}
			else
			{
//				if (list[control_index].position.left.value >= 0)
//				{
//					wsprintf(temp_left, L"Value(%s.Left) + %d", list[current_selection].name, list[control_index].position.left.value) ;
//				}
//				else
//				{
//					wsprintf(temp_left, L"Value(%s.Left) - %d", list[current_selection].name, -list[control_index].position.left.value) ;
//				}
//				list[control_index].position.left.formula = temp_left ;
//
//				if (list[control_index].position.top.value >= 0)
//				{
//					wsprintf(temp_top, L"Value(%s.Top) + %d", list[current_selection].name, list[control_index].position.top.value) ;
//				}
//				else
//				{
//					wsprintf(temp_top, L"Value(%s.Top) - %d", list[current_selection].name, -list[control_index].position.top.value) ;
//				}
//				list[control_index].position.top.formula = temp_top ;

				list[control_index].position.left.value += list[current_selection].position.left.value ;
				list[control_index].position.top.value += list[current_selection].position.top.value ;

				list[control_index].position.left.formula = L"" ;
				list[control_index].position.top.formula = L"" ;
//				create_formula(&list[control_index].position.left, x_offset) ;
//				create_formula(&list[control_index].position.top, y_offset) ;
			}

			//visible (objects are created hidden)
			hr = stream->Read(&list[control_index].visible, sizeof(bool), NULL);
			if (design_mode)
			{
				list[control_index].ctlHwnd.ShowWindow(SW_SHOW) ;
				if (!list[control_index].visible)
				{
					tip_update_text(control_index) ;
				}
			}
			else if (list[control_index].visible && !list[control_index].invisible_at_runtime)
			{
				list[control_index].ctlHwnd.ShowWindow(SW_SHOW) ;
			}
//			if (!list[control_index].visible)
//			{
//				if (design_mode)
//				{
//					list[control_index].ctlHwnd.ShowWindow(SW_SHOW) ;
//					tip_update_text(control_index) ;
//				}
//			}
//			else
//			{
//				list[control_index].ctlHwnd.ShowWindow(SW_SHOW) ;
//			}

			//...and lock status (size only for copy/paste)
			hr = stream->Read(&list[control_index].lock_size, sizeof(bool), NULL) ;

			list[control_index].selected = true ;

			//connect...
			CKRT_Ptr<IKRT_Object>		obj(list[control_index].item) ;
			if (obj)		obj->Connect(krt_connect_mode_connect, true) ;

			no_added ++ ;
		}

//		ticks2 = GetTickCount() ;
//		ATLTRACE2(atlTraceUser, 0, L"end loop %d time %d\n", index, ticks2 - ticks) ;
//		ticks = ticks2 ;
	}

	reprocess_selected(true) ;

//	ticks2 = GetTickCount() ;
//	ATLTRACE2(atlTraceUser, 0, L"reprocessed selected time %d\n", ticks2 - ticks) ;
//	ticks = ticks2 ;

	//update undo event with other indices
	if (no_added > 1)
	{
		newest_undo->sel_count = no_added - 1 ;
		newest_undo->selections = new int [no_added - 1] ;

		for (index = 0, count = 0; index < no_of_items; index++)
		{
			if (list[index].selected && index != current_selection)
			{
				newest_undo->selections[count] = index ;
				count++ ;
			}
		}
	}

	CalculateHandles(handles_window) ;

	CloseClipboard() ;

//	total_ticks = GetTickCount() - total_ticks ;
//	ATLTRACE2(atlTraceUser, 0, L"paste time %d, no_of_items %d\n", total_ticks, no_of_items) ;

	return S_OK ;
}

// Undoes most recent undo
STDMETHODIMP CControlsCollection::Undo(void)
{
	//...
			case undo_type_delete:
			{
				//'undelete' - ie show and remove deleted flag
				if (get_control_index(CKRT_Variant(list[newest_undo->index].name)) == -1)
				{
					list[newest_undo->index].deleted = false ;
					list[newest_undo->index].ctlHwnd.ShowWindow(SW_SHOW) ;
					list[newest_undo->index].selected = true ;
					current_selection = newest_undo->index ;
					tip_add(newest_undo->index) ;

					//reconnect the newly recreated object
					CKRT_Ptr<IKRT_Object>	obj(list[newest_undo->index].item) ;
					if (obj)
					{
						obj->Connect (krt_connect_mode_connect, true) ;
					}
					else
					{
						CKRT_Ptr<IKRT_Object2>	obj2(list[newest_undo->index].item) ;
						if (obj2)
						{
							obj2->Connect (krt_connect_mode_connect, true) ;
						}
					}
				}
				else
				{
					//something else now has this name - throw this one away
					remove(newest_undo->index) ;
				}

	//...
}

//---------------------------------------------------------------------------
//	private functions
//---------------------------------------------------------------------------

// Sets up and handles a control's edit menu
void CControlsCollection::control_edit_menu(POINT mouse_pos)
{
	//...

	AppendMenu(menu, MF_STRING, 1, L"Cut") ;
	AppendMenu(menu, MF_STRING, 2, L"Copy") ;
	AppendMenu(menu, MF_STRING, 4, L"Delete") ;
	AppendMenu(menu, MF_SEPARATOR, 0, NULL) ;
	AppendMenu(menu, MF_STRING, 5, list[current_selection].visible ? L"Hide" : L"Show") ;
	AppendMenu(menu, MF_SEPARATOR, 0, NULL) ;
	AppendMenu(menu, MF_STRING | MF_POPUP, (UINT)format_menu, L"Format") ;
	AppendMenu(menu, MF_SEPARATOR, 0, NULL) ;
	AppendMenu(menu, MF_STRING, 10, L"Properties") ;

	//format options...
	fill_format_menu(format_menu, sel_count) ;
	//...
}

// fills in the formatting menu
void CControlsCollection::fill_format_menu(HMENU menu, int sel_count)
{
	//...

	if (!sel_count)					//no selections
	{
		EnableMenuItem(menu, format_align_to_grid, MF_GRAYED) ;
		EnableMenuItem(menu, format_size_to_grid, MF_GRAYED) ;
		EnableMenuItem(menu, format_snap_to_grid, MF_GRAYED) ;
		EnableMenuItem(menu, format_send_to_back, MF_GRAYED) ;
		EnableMenuItem(menu, format_bring_to_front, MF_GRAYED) ;
	}
	
	if (sel_count <= 1)		//no selections or only current_selection is selected
	{
		EnableMenuItem(align_menu, format_align_lefts, MF_GRAYED) ;
		EnableMenuItem(align_menu, format_align_rights, MF_GRAYED) ;
		EnableMenuItem(align_menu, format_align_tops, MF_GRAYED) ;
		EnableMenuItem(align_menu, format_align_bottoms, MF_GRAYED) ;
		EnableMenuItem(align_menu, format_align_h_centres, MF_GRAYED) ;
		EnableMenuItem(align_menu, format_align_v_centres, MF_GRAYED) ;
		EnableMenuItem(size_menu, format_same_size_width, MF_GRAYED) ;
		EnableMenuItem(size_menu, format_same_size_height, MF_GRAYED) ;
		EnableMenuItem(size_menu, format_same_size_both, MF_GRAYED) ;
		EnableMenuItem(space_menu, format_space_h, MF_GRAYED) ;
		EnableMenuItem(space_menu, format_space_v, MF_GRAYED) ;
		EnableMenuItem(stack_menu, format_stack_h, MF_GRAYED) ;
		EnableMenuItem(stack_menu, format_stack_v, MF_GRAYED) ;
	}
}

//takes a control name and increments the index of it
void CControlsCollection::increment_name(BSTR in_name, BSTR * out_name)
{
	CKRT_Bstr			temp_name ;
	WCHAR				name[0x100] ;
	WCHAR *				name_ptr ;
	int					index			= 1 ;	
	int					number			= 1 ;

	//find the number at end of name (if any)
	lstrcpy(name, in_name) ;
	name_ptr = name ;

	while (*name_ptr && !isdigit(*name_ptr))
	{
		name_ptr ++ ;
		index++ ;
	}

	if (*name_ptr)
	{
		number = _wtol(name_ptr) ;
		number ++ ;
	}

	//remove the original number...
	lstrcpyn(name, in_name, index) ;
	//...and append the new one
	wsprintf(name, L"%s%d", name, number) ;

	temp_name = name ;
	temp_name.CopyTo(out_name) ; 
}

// Returns how many controls are selected - includes current_selection
int	CControlsCollection::no_of_selected_controls(void)
{
	int		index ;
	int		sel_count		= 0 ;

	for (index = 0; index < no_of_items; index++)
	{
		if (!list[index].deleted && list[index].selected)
		{
			sel_count++ ;
		}
	}

	return sel_count ;
}

// Processes an XML Node's attributes and passes it on for reading children (if valid)
bool CControlsCollection::read_node(IXMLDOMNode * Node)
{
	CComQIPtr<IDispatch>			object ;
	HRESULT							hr ;
	CKRT_Bstr						prog_id ;
	CKRT_Variant					object_name ;
	CKRT_Variant					value ;
	CComQIPtr<IXMLDOMElement>		pElement(Node);

	//find object name and prog_id
	if ( pElement->getAttribute(L"Name", object_name) == S_OK	&&
		 pElement->getAttribute(L"Object", value) == S_OK			)
	{
		value.ChangeType(VT_BSTR) ;
		prog_id = value.bstrVal ;
	
//		applog.Write(object_name) ;
		//add object to frame - at default position in case nothing else specified
		hr = add(object_name, prog_id, 0, 0, 100, 100, false, false, NULL) ;
		if (SUCCEEDED(hr))
		{
			applog.Write(object_name, L"Added") ;

			CKRT_Bstr			type ;

			list[no_of_items - 1].pNode = Node ;

			//store the type
			Node->get_baseName(type) ;
			list[no_of_items - 1].ctlType = type ;

			//get a pointer to the object
			hr = get_Item(object_name, &object) ;
			if (SUCCEEDED(hr))
			{
//				long					index		= 0 ;
				CKRT_Bstr				name ;
				bool					move		= false ;
				CComQIPtr<_IKRT_XML>	xml(object) ;
				CKRT_Variant			value ;

				//process position and visible...
				if (pElement->getAttribute(L"Left", value) == S_OK)
				{
					list[no_of_items - 1].position.left.formula = value ;
					move = process_position (&list[no_of_items - 1].position.left, x_offset, scale_x(1)) ;
				}

				value.Empty() ;
				if (pElement->getAttribute(L"Top", value) == S_OK)
				{
					list[no_of_items - 1].position.top.formula = value ;
					move = process_position (&list[no_of_items - 1].position.top, y_offset, scale_y(1)) || move ;
				}

				value.Empty() ;
				if (pElement->getAttribute(L"Width", value) == S_OK)
				{
					list[no_of_items - 1].position.width.formula = value ;
					move = process_position (&list[no_of_items - 1].position.width, 0, x_scale) || move ;
				}
				
				value.Empty() ;
				if (pElement->getAttribute(L"Height", value) == S_OK)
				{
					list[no_of_items - 1].position.height.formula = value ;
					move = process_position (&list[no_of_items - 1].position.height, 0, y_scale) || move ;
				}

				value.Empty() ;
				if (pElement->getAttribute(L"Visible", value) == S_OK)
				{
					value.ChangeType(VT_BOOL) ;
					list[no_of_items - 1].visible = (value.boolVal != 0) ;
				}

				value.Empty() ;
				if (pElement->getAttribute(L"LockPosition", value) == S_OK)
				{
					value.ChangeType(VT_BOOL) ;
					list[no_of_items - 1].lock_pos = (value.boolVal != 0) ;
				}

				value.Empty() ;
				if (pElement->getAttribute(L"LockSize", value) == S_OK)
				{
					value.ChangeType(VT_BOOL) ;
					list[no_of_items - 1].lock_size = (value.boolVal != 0) ;
				}

				//load other control properties
				if (xml)
				{
					//get the object to load itself
//					applog.Write(object_name, L"xml load") ;
					xml->LoadXML(Node) ;
//					applog.Write(object_name, L"xml load complete") ;
				}
				else
				{
					//does it have a property bag?
					CComQIPtr<IPersistPropertyBag>		persist(list[no_of_items - 1].item) ;
					CComQIPtr<IPropertyBag>				bag(this) ;
					
					if (persist)
					{
//						applog.Write(object_name, L"property bag load") ;
						bag_object_element = pElement ;
						hr = persist->Load(bag, NULL) ;
						bag_object_element.Release() ;
//						applog.Write(object_name, L"property bag load complete") ;
					}
					//else cannot load its properties
				}
				applog.Write(object_name, L"loaded") ;

				if (move)
				{
					//update position of control
					move_window(no_of_items - 1, false) ;
				}

				//set visiblity now all other configuration of this object is done
				//(object is created hidden)
				if (design_mode)
				{
					list[no_of_items - 1].ctlHwnd.ShowWindow(SW_SHOW) ;
					if (!list[no_of_items - 1].visible)
					{
						tip_update_text(no_of_items - 1) ;
					}
				}
				else if (list[no_of_items - 1].visible && !list[no_of_items - 1].invisible_at_runtime)
				{
					list[no_of_items - 1].ctlHwnd.ShowWindow(SW_SHOW) ;
				}
//				if (!list[no_of_items - 1].visible)
//				{
//					if (design_mode)
//					{
//						tip_update_text(no_of_items - 1) ;
//					}
//				}
//				else
//				{
//					list[no_of_items - 1].ctlHwnd.ShowWindow(SW_SHOW) ;
//				}

//				applog.Write(object_name, L"Complete") ;

				return S_OK ;
			}
			//else failed to get object pointer
		}
		//else failed to add
	}
	//else no name / progid

	return S_FALSE ;
}

// Removes oldest undo (end of list)
void CControlsCollection::remove_oldest_undo(void)
{
	//...
			case undo_type_delete:
			{
				//actually remove the designated control, not just hide it
				remove (oldest_undo->index) ;

				//remove controls removed at the same time
				for (int index = 0; index < oldest_undo->sel_count; index++)
				{
					remove (oldest_undo->selections[index]) ;
				}
			}
			break ;
	//...
}

// Adds a tooltip
void CControlsCollection::tip_add(int Index)
{
	if (design_mode)
	{
		TOOLINFO			info ;
		CKRT_Bstr			text ;

		text = list[Index].name ;
		if (!list[Index].visible)
		{
			text += L" (invisible)" ;
		}
//		text.CopyTo(info.lpszText) ;
		info.lpszText = text ;

		info.cbSize		= sizeof (TOOLINFO) ;
		info.hinst		= _Module.GetModuleInstance() ;
		info.lParam		= 0 ;
		info.hwnd		= clicks_window ;
		info.uId		= Index ;
		info.uFlags		= TTF_SUBCLASS ;
		info.rect.left	= list[Index].position.left.value ;
		info.rect.top	= list[Index].position.top.value ;
		info.rect.right	= list[Index].position.left.value + list[Index].position.width.value ;
		info.rect.bottom= list[Index].position.top.value + list[Index].position.height.value ;

		::SendMessage (tip_window, TTM_ADDTOOL, 0, (LPARAM) &info) ;
	}
}

// Removes a tooltip
void CControlsCollection::tip_clear(int Index)
{
	if (design_mode)
	{
		TOOLINFO	info ;
		
		info.cbSize		= sizeof (TOOLINFO) ;
		info.hwnd		= clicks_window ;
		info.uId		= Index ;

		::SendMessage (tip_window, TTM_DELTOOL, 0, (LPARAM)&info) ;
	}
}

// Updates a tooltip's rectangle
void CControlsCollection::tip_update_rect(int Index)
{
	if (design_mode)
	{
		TOOLINFO	info ;
		
		info.cbSize		= sizeof (TOOLINFO) ;
		info.rect.left	= list[Index].position.left.value ;
		info.rect.top	= list[Index].position.top.value ;
		info.rect.right	= list[Index].position.left.value + list[Index].position.width.value ;
		info.rect.bottom= list[Index].position.top.value + list[Index].position.height.value ;
		info.hwnd		= clicks_window ;
		info.uId		= Index ;

		::SendMessage (tip_window, TTM_NEWTOOLRECT, 0, (LPARAM)&info) ;
	}
}

// Updates a tooltip's text
void CControlsCollection::tip_update_text(int Index)
{
	if (design_mode)
	{
		TOOLINFO	info ;
		CKRT_Bstr			text ;

		text = list[Index].name ;
		if (!list[Index].visible && !list[Index].invisible_at_runtime)
		{
			text += L" (invisible)" ;
		}
		text.CopyTo(info.lpszText) ;
		
		info.cbSize		= sizeof (TOOLINFO) ;
		info.hinst		= _Module.GetModuleInstance() ;
		info.hwnd		= clicks_window ;
		info.uId		= Index ;

		::SendMessage (tip_window, TTM_UPDATETIPTEXT, 0, (LPARAM)&info) ;

		SysFreeString (info.lpszText) ;
	}
}

