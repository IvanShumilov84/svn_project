/////////////////////////////////////////////////////////////////////////////
// CKRT_Frame

//--------------------------------------------------------------------------------
// Windows Messages - alphabetical
//------------------------------------------------------------------------

LRESULT CKRT_Frame::OnButtonUp(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	//...
			AppendMenu(menu, MF_STRING, 1, L"Enter Design Mode") ;
			//...
            AppendMenu(menu, MF_SEPARATOR, 0, NULL) ;
			AppendMenu(menu, MF_STRING, 2, L"Save to XML file") ;
	//...
}

//////////////////////////////////////////////////////////////////////////
//
//		Design Mode handling
//
//////////////////////////////////////////////////////////////////////////

// checks registry and file flag and returns 'true' if editable, or 'false' if not
bool CKRT_Frame::is_editable(void)
{
	bool	registry_editable		= false ;
	bool	editable ;
	
	//truth table of editablility: (both default to 'not editable' if not otherwise set)
	//		(reg)	0	1
	//(file)	0	0	1
	//			1	1	1

	if (!file_editable)
	{
		HKEY	key ;
		WCHAR *	path ;

		//only bother checking the registry if file hasn't already decreed editability
		path = L"Software\\RHS Harntec Ltd\\KineticaRT\\5\\Libraries\\KRTLib_Frame" ;

		//find the 'KRTLib_Frame' key
		if (RegOpenKey (HKEY_CURRENT_USER, path, &key) == ERROR_SUCCESS)
		{
			DWORD		data ;
			DWORD		bytes		= sizeof (data) ;

			if (RegQueryValueEx(key, L"Editable", NULL, NULL, (BYTE *)&data, &bytes) == ERROR_SUCCESS)
			{
				registry_editable = (data != 0) ;
			}

			RegCloseKey (key) ;
		}
	}

	editable = (file_editable || registry_editable) ? true : false ;

	return editable ;
}

STDMETHODIMP CKRT_Frame::SaveXML(IXMLDOMNode * pNode)
{
	//save own properties...
	CComQIPtr<IXMLDOMElement>		pElement(pNode) ;
	CKRT_Variant					var ;

	var = file_editable ;
	pElement->setAttribute(L"Editable", var) ;

	var = m_nAppearance ;
	pElement->setAttribute(L"Appearance", var); 
	var = m_nBorderStyle ;
	pElement->setAttribute(L"BorderStyle", var) ;
	pElement->setAttribute(L"BorderWidth", border_width) ;
	var = OleColorToHTMLColor(m_clrBackColor) ;
	pElement->setAttribute(L"BackColor", var) ;
	var = OleColorToHTMLColor(m_clrBorderColor) ;
	pElement->setAttribute(L"BorderColor", var) ;
//	var = m_pMouseIcon ;
//	pElement->setAttribute(L"MouseIcon", var) ;
	var = m_nMousePointer ;
	pElement->setAttribute(L"MousePointer", var) ;
	
	var = h_grid.enabled ;
	pElement->setAttribute(L"HorizontalGridEnabled", var) ;
	pElement->setAttribute(L"HorizontalGridSize", h_grid.var_size) ;
	var = h_grid.spacing ;
	pElement->setAttribute(L"HorizontalSpacing", var) ;
	var = v_grid.enabled ;
	pElement->setAttribute(L"VerticalGridEnabled", var) ;
	pElement->setAttribute(L"VerticalGridSize", v_grid.var_size) ;
	var = v_grid.spacing ;
	pElement->setAttribute(L"VerticalSpacing", var) ;

	pElement->setAttribute(L"ToolTipText", tool_tip) ;
	
	if (color_file.Length())	pElement->setAttribute(L"ColorFile", color_file) ;
	if (add_list_file.Length())	pElement->setAttribute(L"AddListFile", add_list_file) ;

	pElement->setAttribute(L"TitleEnabled", title_enabled) ;
	pElement->setAttribute(L"TitleUnderlined", title_underlined) ;
//	pElement->setAttribute(L"TitleMinimizeButton", title_minimize_btn) ;
	pElement->setAttribute(L"TitleText", title_text) ;
	var = OleColorToHTMLColor(title_left_clr) ;
	pElement->setAttribute(L"TitleLeftColor", var) ;
	var = OleColorToHTMLColor(title_right_clr) ;
	pElement->setAttribute(L"TitleRightColor", var) ;
	var = OleColorToHTMLColor(title_text_clr) ;
	pElement->setAttribute(L"TitleTextColor", var) ;
	var = OleColorToHTMLColor(title_underline_clr) ;
	pElement->setAttribute(L"TitleUnderlineColor", var) ;
	var = OleColorToHTMLColor(bezel_clr) ;
	pElement->setAttribute(L"BezelColor", var) ;

	//no longer saving filename automatically - only if used
//	pElement->setAttribute(L"XMLFilename", xml_filename) ;

	if (xml_filename.Length() > 0)
	{
		// 'saving' - set to true in ThreadSaveXMLFile just before
		// call to here and reset to false afterwards
		if (!saving && parsed && pDoc)
		{
			//save it in it's specified file, not in the current one
			pElement->setAttribute(L"XMLFilename", xml_filename) ;

			return SaveXMLFile() ;
		}
	}

	// only gets here if haven't got a filename which we previously loaded
	
	//save collections...
	CComQIPtr<IXMLDOMNode>		pControls ;
	CComQIPtr<IXMLDOMNode>		pColours ;
	HRESULT						hr ;

	hr = pNode->selectSingleNode(L"Controls", &pControls) ;
	hr = pNode->selectSingleNode(L"Colors", &pColours) ;

	if (!pControls)
	{
		//doesn't already exist - create it
		CComQIPtr<IXMLDOMElement>		pElement ;
		CComQIPtr<IXMLDOMDocument>		pDocument ;

		pNode->get_ownerDocument(&pDocument) ;
		hr = pDocument->createElement(L"Controls", &pElement);
		if (pElement)
		{
			hr = pNode->appendChild(pElement, &pControls);
		}
	}

	if (pControls)
	{
		CComQIPtr<_IKRT_XML>		xml(children[CID_Controls]) ;

		if (xml)				xml->SaveXML(pControls) ;
	}

	CComQIPtr<IXMLDOMNode>		pChildren ;
	hr = pNode->selectSingleNode(L"Children", &pChildren) ;

	if (!pChildren)
	{
		//doesn't already exist - create it
		CComQIPtr<IXMLDOMElement>		pElement ;
		CComQIPtr<IXMLDOMDocument>		pDocument ;

		pNode->get_ownerDocument(&pDocument) ;
		hr = pDocument->createElement(L"Children", &pElement);
		if (pElement)
		{
			hr = pNode->appendChild(pElement, &pChildren);
		}
	}

	if (pChildren)
	{
		CComQIPtr<IDispatch>	disp ;
		CComQIPtr<_IKRT_XML>	xml ;

		get_Children(&disp) ;
		xml = disp ;
		if (xml)				xml->SaveXML(pChildren) ;
	}

	if ( !color_file.Length() )
	{
		if (!pColours)
		{
			//doesn't already exist - create it
			CComQIPtr<IXMLDOMElement>		pElement ;
			CComQIPtr<IXMLDOMDocument>		pDocument ;

			pNode->get_ownerDocument(&pDocument) ;
			hr = pDocument->createElement(L"Colors", &pElement);
			if (pElement)
			{
				hr = pNode->appendChild(pElement, &pColours);
			}
		}

		if (pColours)
		{
			write_color_list(pColours) ;
		}
	}
	else
	{
		//clear any colors held in the frame...
		if (pColours)
		{
			clear_node(pColours) ;
		}

		//...and store the current list to the color file
		SaveColorFile() ;
	}

	//save font
	if (m_pFont)
	{
		XMLSaveFont(pNode, m_pFont) ;
	}

	//add list here???

	is_dirty = false ;

	return S_OK ;
}

STDMETHODIMP CKRT_Frame::LoadXML(IXMLDOMNode * pNode)
{
	CComQIPtr<IXMLDOMNode>			pChild ;
	CKRT_Ptr<IXMLDOMElement>		pElement(pNode) ;
	CKRT_Variant					val ;
//	CComQIPtr<_IControlsCollection>	_controls (children[CID_Controls]) ;
//	CComQIPtr<_IObjectsCollection>	_objects (children[CID_Objects]) ;

	DWORD		ticks		= GetTickCount() ;

//	//clear any previous addables lists
//	_controls->ClearAddables() ;
//	_objects->ClearAddables() ;

	read_only = false ;

	//read frames attributes
	if (pElement->getAttribute(L"Appearance", &val) == S_OK)
	{
		val.ChangeType(VT_I2) ;
		put_Appearance(val.iVal) ;
	}
	if (pElement->getAttribute(L"BorderStyle", &val) == S_OK)
	{
		put_BorderStyle(val) ;
	}
	if (pElement->getAttribute(L"BorderWidth", &val) == S_OK)
	{
		//if border width is saved as a double it is an old version,
		// and means a percentage, so convert it first
		CKRT_Variant		temp ;

		if (temp.Copy(val, VT_R8) == S_OK && (temp.dblVal - (int)(temp.dblVal)) != 0)
		{
			CKRT_Bstr		temp_str((long)(100 * temp.dblVal)) ;
			temp_str += L"%" ;
			val = temp_str ;
		}
		put_BorderWidth(val) ;
	}
	if (pElement->getAttribute(L"BackColor", &val) == S_OK)
	{
		put_BackColor(HTMLColorToOleColor(val)) ;
	}
	if (pElement->getAttribute(L"BorderColor", &val) == S_OK)
	{
		put_BorderColor(HTMLColorToOleColor(val)) ;
	}
	if (pElement->getAttribute(L"MousePointer", &val) == S_OK)
	{
		put_MousePointer(val) ;
	}
	if (pElement->getAttribute(L"ToolTipText", &val) == S_OK)
	{
		put_ToolTipText(val) ;
	}

	if (pElement->getAttribute(L"XMLFilename", &val) == S_OK)
	{
		//only set it if non blank
		if (lstrlen(val.bstrVal))
		{
			put_XMLFilename(val) ;
		}
	}
	if (pElement->getAttribute(L"ColorFile", &val) == S_OK)
	{
		//only set it if non blank
		if (lstrlen(val.bstrVal))
		{
			put_ColorFile(val) ;
			ParseColorFile() ;
		}
	}

	//grid: old versions have a single grid: make this both directions now
	if (pElement->getAttribute(L"GridEnabled", &val) == S_OK)
	{
		put_HorizontalGridEnabled(val) ;
		put_VerticalGridEnabled(val) ;
	}
	if (pElement->getAttribute(L"GridSize", &val) == S_OK)
	{
		put_HorizontalGridSize(val) ;
		put_VerticalGridSize(val) ;
	}

	if (pElement->getAttribute(L"HorizontalGridEnabled", &val) == S_OK)
	{
		put_HorizontalGridEnabled(val) ;
	}
	if (pElement->getAttribute(L"HorizontalGridSize", &val) == S_OK)
	{
		put_HorizontalGridSize(val) ;
	}
	if (pElement->getAttribute(L"VerticalGridEnabled", &val) == S_OK)
	{
		put_VerticalGridEnabled(val) ;
	}
	if (pElement->getAttribute(L"VerticalGridSize", &val) == S_OK)
	{
		put_VerticalGridSize(val) ;
	}
	if (pElement->getAttribute(L"HorizontalSpacing", &val) == S_OK)
	{
		put_HorizontalSpacing(val) ;
	}
	if (pElement->getAttribute(L"VerticalSpacing", &val) == S_OK)
	{
		put_VerticalSpacing(val) ;
	}

	if (pElement->getAttribute(L"Editable", &val) == S_OK)
	{
		val.ChangeType(VT_BOOL) ;
		file_editable = (val.boolVal != false) ;
	}

	if (pElement->getAttribute(L"TitleEnabled", &val) == S_OK)
	{
		put_TitleEnabled(val) ;
	}
	if (pElement->getAttribute(L"TitleUnderlined", &val) == S_OK)
	{
		put_TitleUnderlined(val) ;
	}
//	if (pElement->getAttribute(L"TitleMinimizeButton", &val) == S_OK)
//	{
//		put_TitleMinimizeButton(val) ;
//	}
	if (pElement->getAttribute(L"TitleText", &val) == S_OK)
	{
		put_TitleText(val) ;
	}
	if (pElement->getAttribute(L"TitleLeftColor", &val) == S_OK)
	{
		put_TitleLeftColor(HTMLColorToOleColor(val)) ;
	}
	if (pElement->getAttribute(L"TitleRightColor", &val) == S_OK)
	{
		put_TitleRightColor(HTMLColorToOleColor(val)) ;
	}
	if (pElement->getAttribute(L"TitleTextColor", &val) == S_OK)
	{
		put_TitleTextColor(HTMLColorToOleColor(val)) ;
	}
	if (pElement->getAttribute(L"TitleUnderlineColor", &val) == S_OK)
	{
		put_TitleUnderlineColor(HTMLColorToOleColor(val)) ;
	}
	if (pElement->getAttribute(L"BezelColor", &val) == S_OK)
	{
		put_BezelColor(HTMLColorToOleColor(val)) ;
	}

	if (pElement->getAttribute(L"AddListFile", &val) == S_OK)
	{
		//only set it if non blank
		if (lstrlen(val.bstrVal))
		{
			put_AddListFile(val) ;
			ParseAddListFile() ;
		}
	}

	pNode->selectSingleNode(L"KRT_Colors", &pChild) ;
	if (pChild)
	{
		read_color_list(pChild) ;
		pChild.Release() ;
	}
	pNode->selectSingleNode(L"Controls", &pChild) ;
	if (pChild)
	{
		CComQIPtr<_IKRT_XML>	xml(children[CID_Controls]) ;
		xml->LoadXML(pChild) ;
		pChild.Release() ;
	}

	pNode->selectSingleNode(L"Children", &pChild) ;
	if (pChild)
	{
		CComQIPtr<IDispatch>	disp ;
		CComQIPtr<_IKRT_XML>	xml ;

		get_Children(&disp) ;
		xml = disp ;
		if (xml)		xml->LoadXML(pChild) ;
		pChild.Release() ;
	}

	//load font
	if (!m_pFont)
	{
		FONTDESC			font_desc = {sizeof(FONTDESC), L"MS Sans Serif", 82500, FW_NORMAL, DEFAULT_CHARSET, FALSE, FALSE, FALSE} ;
		CComPtr<IFontDisp>	new_font;

		OleCreateFontIndirect(&font_desc, IID_IFontDisp, (void**)&new_font) ;
		put_Font(new_font) ;
	}

	CComQIPtr<IFont>		new_font(m_pFont) ;

	if ( XMLLoadFont(pNode, &new_font.p) == S_OK )
	{
		CComPtr<IFontDisp>		font_disp ;

		//set the font property
		new_font->QueryInterface(IID_IFontDisp, (void**)&font_disp) ;
		put_Font(font_disp) ;
	}

	ticks = GetTickCount() - ticks ;
//	ATLTRACE2(atlTraceUser, 0, L"Frame LoadXML took %d ms\n", ticks) ;

	is_dirty = false ;

	return S_OK ;
}

//---------------------------------------------------------------------------------
//	IKRT_Colors interface
//---------------------------------------------------------------------------------

//...
