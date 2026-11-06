Imports FileHelpers
Imports System.IO
Imports CEL.HMI
Imports CEL.Data.BusinessObjects
Imports CEL.Data.BusinessObjects.Dacn1Afdx



Public Class AfdxDefinitionPw980A

    Private CELProject As CELDAS
    Private WithEvents engine As MultiRecordEngine


    Private PortId As Integer = 0
    Public Sub New(ByRef _CELProject As CELDAS)
        CELProject = _CELProject
        CELProject.EngineModel = "PW980A"
    End Sub

    Public Sub ImportAfdxDefiniton(ByVal csvFullFileName As String, ByVal defaultMTU As Integer)


        PortId = 0

        If csvFullFileName <> "" AndAlso File.Exists(csvFullFileName) Then

            engine = New MultiRecordEngine(GetType(IcdVersionNumber), _
                                           GetType(OutputVL), _
                                           GetType(OutputMessage), _
                                           GetType(InputVL), _
                                           GetType(InputMessage), _
                                           GetType(DiscreteOutputLine), _
                                           GetType(DiscreteOutputSignal), _
                                           GetType(DiscreteInputLine), _
                                           GetType(DiscreteInputSignal))

            engine.RecordSelector = New RecordTypeSelector(AddressOf CustomSelector)

            engine.ErrorManager.ErrorMode = ErrorMode.ThrowException


            'engine.ReadFile(txtFileName.Text)

            'If (engine.ErrorManager.HasErrors) Then
            '    For Each err As ErrorInfo In engine.ErrorManager.Errors

            '        Console.WriteLine(err.LineNumber)
            '        Console.WriteLine(err.RecordString)
            '        Console.WriteLine(err.ExceptionInfo.ToString())
            '    Next
            '    If (engine.ErrorManager.HasErrors) Then
            '        engine.ErrorManager.SaveErrors("errors.txt")
            '    End If
            '    MessageBox.Show("Error(s) found")
            'Else
            engine.BeginReadFile(csvFullFileName)

            For Each record As Object In engine
                If TypeOf record Is IcdVersionNumber Then
                    Debug.Assert(record.VersionNumber.ToString.StartsWith("#ICD Format Version Number :   LOOD15151941-02.REV.2"), "Wrong File Version")
                ElseIf TypeOf record Is OutputVL Then

                    ''Console.WriteLine("ImportInputPorts")
                    ImporstInputVL(record, defaultMTU)
                    ImportsInputPorts(record)


                ElseIf TypeOf record Is OutputMessage Then
                    ''Console.WriteLine("ImportInputMessages")
                    ImportsInputMessages(record)

                ElseIf TypeOf record Is InputVL Then

                    Console.WriteLine("ImportOutputPorts")
                    ImporstOutputVL(record, defaultMTU)
                    ImportsOutputPorts(record)


                ElseIf TypeOf record Is InputMessage Then

                    Console.WriteLine("ImportOutputMessages")
                    ImportsOutputMessages(record)

                ElseIf TypeOf record Is DiscreteOutputLine Then
                ElseIf TypeOf record Is DiscreteOutputSignal Then
                ElseIf TypeOf record Is DiscreteInputLine Then
                ElseIf TypeOf record Is DiscreteInputSignal Then
                End If

                If (engine.ErrorManager.HasErrors) Then
                    For Each err As ErrorInfo In engine.ErrorManager.Errors

                        Console.WriteLine(err.LineNumber)
                        Console.WriteLine(err.RecordString)
                        Console.WriteLine(err.ExceptionInfo.ToString())
                    Next
                    If (engine.ErrorManager.HasErrors) Then
                        engine.ErrorManager.SaveErrors("errors.txt")
                    End If
                    System.Windows.Forms.MessageBox.Show("Error(s) found. See file errors.txt for more details")
                End If

            Next

            engine.Close()

        End If
    End Sub

    Public Function ExportAfdxDefinitonToPowerDnaFormat(ByVal outputFilename As String) As Boolean


        If File.Exists(outputFilename) Then
            File.Delete(outputFilename)
        End If


        Dim writer As New StreamWriter(outputFilename)
        Dim vlCollection As New Dacn1AfdxVirtualLinkCollection()
        vlCollection.es.Connection.Name = CELProject.DefaultConnectionName
        vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                             vlCollection.Query.TxRx = 0) '0 =Rx
        'vlCollection.Query.OrderBy(vlCollection.Query.Vlid.Ascending)
        vlCollection.Query.Load()

        'INPUT_VL
        writer.WriteLine("#Begin")
        writer.WriteLine("#FORMAT;Producer;Revision;Author;Equipment name;")
        writer.WriteLine("FORMAT;UEI;V1.0;UEI ISCD Converter;DU_ONE;")
        writer.WriteLine("#End")
        writer.WriteLine("")
        writer.WriteLine("")
        writer.WriteLine("#Begin")
        writer.WriteLine("#INPUT_VL;VLID;frag_en;ICMP_en;mode;bag;mtu;max_jitter;skew_max;skew_delay;prev_sn_range;ede_en;ede_sid;ede_skew_max;ede_age_max;ede_lag_max;")

        For Each record As Dacn1AfdxVirtualLink In vlCollection
            Dim str As String
            Dim mode As String = GetVLMode(True, _
                                   IIf(record.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Unicast, 0, 1), _
                                    record.FragmentationEnabled, _
                                    record.ICMPEnabled, _
                                    record.EDEEnabled, _
                                    record.ArincChannel, _
                                    record.IntegrityCheck, _
                                    record.RedondancyEnabled)

            str = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};", _
                                "INPUT_VL", _
                                record.Vlid, _
                                record.FragmentationEnabled, _
                                record.ICMPEnabled, _
                                mode, _
                                record.Bag, _
                                record.Mtu, _
                                record.MaxJitter, _
                                record.SkewMax, _
                                record.SkewDelay, _
                                IIf(IsNothing(record.PrevSnRange), 0, record.PrevSnRange), _
                                record.EDEEnabled, _
                                record.EDESid, _
                                record.EDESkewMax, _
                                record.EDEAgeMax, _
                                record.EDELagMax)
            writer.WriteLine(str)
        Next
        writer.WriteLine("#End")
        writer.WriteLine("")


        'INPUT_PORT
        writer.WriteLine("")
        writer.WriteLine("#Begin")
        writer.WriteLine("#INPUT_PORT;mode;portID;VLID;period;period_skew;endsys_src;part_src;endsys_dst;part_dst;subvl_id;udp_src_port;udp_dst_port;port_mode;d_size;depth;")

        Dim portCollection As New Dacn1AfdxPortCollection()
        portCollection.es.Connection.Name = CELProject.DefaultConnectionName
        portCollection.Query.Where(portCollection.Query.CollectionName = CELProject.EngineModel And _
                             portCollection.Query.TxRx = 0) '0 =Rx
        portCollection.Query.OrderBy(portCollection.Query.PortID.Ascending)
        portCollection.Query.Load()
        For Each record As Dacn1AfdxPort In portCollection
            Dim str As String = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};", _
                              "INPUT_PORT", _
                              record.PortMode, _
                              record.PortID, _
                              record.Vlid, _
                              record.Period, _
                              record.PeriodSkew, _
                              record.EndSystemSource, _
                              record.PartitionSource, _
                              record.EndSystemDestination, _
                              record.PartitionDestination, _
                              record.SubVLID, _
                              record.UdpPortSource, _
                              record.UdpPortDestination, _
                              Convert.ToInt32(record.PortTypeEnum), _
                              record.MessageLength, _
                              record.Depth)
            writer.WriteLine(str)

        Next
        writer.WriteLine("#End")
        writer.WriteLine("")









        vlCollection = New Dacn1AfdxVirtualLinkCollection()
        vlCollection.es.Connection.Name = CELProject.DefaultConnectionName
        vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                             vlCollection.Query.TxRxEnum = eTransmissionMode.Tx) '1 = Tx
        'vlCollection.Query.OrderBy(vlCollection.Query.Vlid.Ascending)
        vlCollection.Query.Load()



        writer.WriteLine("")
        writer.WriteLine("#Begin")
        writer.WriteLine("#OUTPUT_VL;VLID;n_subVL;frag_en;ICMP_en;mode;bag;mtu;max_jitter;skew_max;skew_delay;prev_sn_range;ede_en;ede_sid;ede_skew_max;ede_age_max;ede_lag_max;")

        For Each record As Dacn1AfdxVirtualLink In vlCollection
            Dim str As String
            'Dim mode As String = GetVLMode(True, _
            '                       IIf(record.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Unicast, 0, 1), _
            '                        record.FragmentationEnabled, _
            '                        record.ICMPEnabled, _
            '                        record.EDEEnabled, _
            '                        record.ArincChannel, _
            '                        record.IntegrityCheck, _
            '                        record.RedondancyEnabled)

            str = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};", _
                                "OUTPUT_VL", _
                                record.Vlid, _
                                IIf(IsNothing(record.NumberOfSubVL), 0, record.NumberOfSubVL), _
                                record.FragmentationEnabled, _
                                record.ICMPEnabled, _
                                record.VLMode, _
                                record.Bag, _
                                record.Mtu, _
                                record.MaxJitter, _
                                record.SkewMax, _
                                record.SkewDelay, _
                                IIf(IsNothing(record.PrevSnRange), 0, record.PrevSnRange), _
                                record.EDEEnabled, _
                                record.EDESid, _
                                record.EDESkewMax, _
                                record.EDEAgeMax, _
                                record.EDELagMax)
            writer.WriteLine(str)
        Next
        writer.WriteLine("#End")
        writer.WriteLine("")

        'OUTPUT_PORT
        writer.WriteLine("")
        writer.WriteLine("#Begin")
        writer.WriteLine("#OUTPUT_PORT;mode;portID;VLID;period;period_skew;endsys_src;part_src;endsys_dst;part_dst;subvl_id;udp_src_port;udp_dst_port;port_mode;d_size;depth;")

        portCollection = New Dacn1AfdxPortCollection()
        portCollection.es.Connection.Name = CELProject.DefaultConnectionName
        portCollection.Query.Where(portCollection.Query.CollectionName = CELProject.EngineModel And _
                             portCollection.Query.TxRx = 1) '1 =Tx
        portCollection.Query.OrderBy(portCollection.Query.Vlid.Ascending)
        portCollection.Query.OrderBy(portCollection.Query.PortID.Ascending)
        portCollection.Query.Load()
        For Each record As Dacn1AfdxPort In portCollection
            Dim str As String = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};", _
                              "OUTPUT_PORT", _
                              record.PortMode, _
                              record.PortID, _
                              record.Vlid, _
                              record.Period, _
                              record.PeriodSkew, _
                              IIf(IsNothing(record.EndSystemSource), 0, record.EndSystemSource), _
                              IIf(IsNothing(record.PartitionSource), 0, record.PartitionSource), _
                              IIf(IsNothing(record.EndSystemDestination), 0, record.EndSystemDestination), _
                              IIf(IsNothing(record.PartitionDestination), 0, record.PartitionDestination), _
                              IIf(IsNothing(record.SubVLID), 0, record.SubVLID), _
                              record.UdpPortSource, _
                              record.UdpPortDestination, _
                               Convert.ToInt32(record.PortTypeEnum), _
                              record.MessageLength, _
                              record.Depth)
            writer.WriteLine(str)

        Next
        writer.WriteLine("#End")
        writer.WriteLine("")

        writer.Close()

        Return True
    End Function


    Public Function ImportsInputPorts(ByRef record As OutputVL) As Boolean

        ImportsInputPorts = False



        'If record.PortCharacteristic.ToUpper = "SAP" Or record.PortCharacteristic.ToUpper = "QUEUING" Then
        '    Exit Function
        'End If
        If record.PortCharacteristic.ToUpper = "SAP" Then
            Exit Function
        End If



        Dim portCollection As New Dacn1AfdxPortCollection()
        portCollection.es.Connection.Name = CELProject.DefaultConnectionName

        portCollection.Query.Where(portCollection.Query.CollectionName = CELProject.EngineModel And _
                                 portCollection.Query.Name = record.AfdxPortMasterName And _
                                 portCollection.Query.Vlid = record.VlIdentifier And _
                                 portCollection.Query.UdpPortSource = record.DestinationUdpPortAddress)


        portCollection.Query.Load()

        If portCollection.Count = 0 Then

            PortId += 1

            Dim port As Dacn1AfdxPort = portCollection.AddNew()
            port.CollectionName = CELProject.EngineModel
            port.TxRxEnum = eTransmissionMode.Rx 'Ouin, ici faire attention un Output est en fait un input du point de vue du PowerDNA
            port.Name = record.AfdxPortMasterName
            'port.Description = record.PhysicalPortId
            port.ExportToCsv = True
            port.PortMode = GetPortMode(True)
            port.Vlid = record.VlIdentifier
            port.PortID = record.TxAfdxportIdentifier 'port.PortID = PortId 'record.TxAfdxportIdentifier ' Right(record.PhysicalPortId, 1)
            'Deplace dans VL
            'If record.NetworkSelect.ToUpper = "A&B" Then
            '    port.TransmissionChannelAB = 2 '2 =A&B
            'Else
            '    port.TransmissionChannelAB = IIf(record.NetworkId = "A", 0, 1)
            'End If
            port.Period = record.Bag * 1000 'en microsec et non en ms
            port.PeriodSkew = 0 '
            port.EndSystemSource = GetEndSystemSourceFromIp(record.SourceIpAddress)
            port.PartitionSource = GetPartitionSourceFromIp(record.SourceIpAddress)
            port.EndSystemDestination = GetEndSystemDestionationFromIp(record.DestinationIpAddress) ', record.AfdxPortTransmissionType)
            port.PartitionDestination = GetPartitionDestinationFromIp(record.DestinationIpAddress) ', record.AfdxPortTransmissionType)
            If IsNothing(record.SubVlIdentifier) Then
                port.SubVLID = record.SubVlIdentifier
            Else
                port.SubVLID = record.SubVlIdentifier
            End If

            'ft pas vrai port.SubVLID = 0 'Un probleme lors de l'installation a Revima,certaines n'etaient pas transmis et les gens de UEI ont dit de mettre cechamps a 0





            port.UdpPortSource = record.SourceUdpPortAddress
            port.UdpPortDestination = IIf(IsNothing(record.DestinationUdpPortAddress), 0, record.DestinationUdpPortAddress)
            port.PortTypeEnum = GetPortType(record.PortCharacteristic) 'PortType 
            port.MessageLength = IIf(record.BufferSize = 120120, 120, record.BufferSize)
            If record.PortCharacteristic.ToUpper = "SAP" Or record.PortCharacteristic.ToUpper = "QUEUING" Then
                port.Depth = 16 '' Peu importe si Queuing, le max doit être 16 'record.BufferSize '16 'Applicable when Queuing Or SAP only'record.BufferSize '16 'Applicable when Queuing Or SAP only

            Else
                port.Depth = 0
            End If
            'ft pas vrai port.Depth = port.MessageLength 'Un probleme lors de l'installation a Revima,certaines n'estaient pas transmis et les gens de UEI ont dit de mettre depth = au message length
            port.BufferSize = -1
            portCollection.Save()

            'ElseIf portCollection.Count = 1 Then
            '    Dim port As Dacn1AfdxPort = portCollection.Item(0)
            '    If (port.TransmissionChannelAB = Dacn1AfdxPort.eArincChannel.CHA _
            '        OrElse port.TransmissionChannelAB = Dacn1AfdxPort.eArincChannel.CHB) _
            '        AndAlso record.NetworkSelect.ToUpper = "A&B" Then

            '        port.MarkAllColumnsAsDirty(EntitySpaces.Interfaces.esDataRowState.Added)
            '        port.TransmissionChannelAB = Dacn1AfdxPort.eArincChannel.CHAB
            '        portCollection.Save() 'Make a copy of the record, change a value in primary key, and Insert it

            '        portCollection.Query.Load() 'Reload the the original channel and delete it
            '        If portCollection.Count = 1 Then
            '            portCollection.MarkAllAsDeleted()
            '            portCollection.Save()
            '        Else
            '            Throw New ArgumentException("Exception Occured")
            '        End If


            '    End If
            'Else
        End If

        Return True
    End Function

    Public Function ImportsOutputPorts(ByRef record As InputVL) As Boolean 'Attention un Input est en fait un ouput du point de vue du PowerDNA

        ImportsOutputPorts = False

        'If record.PortCharacteristic.ToUpper = "SAP" Or record.PortCharacteristic.ToUpper = "QUEUING" Then
        '    Exit Function
        'End If
        If record.PortCharacteristic.ToUpper = "SAP" Then
            Exit Function
        End If
       

        Dim portCollection As New Dacn1AfdxPortCollection()
        portCollection.es.Connection.Name = CELProject.DefaultConnectionName

        portCollection.Query.Where(portCollection.Query.CollectionName = CELProject.EngineModel And _
                                  portCollection.Query.Name = record.AfdxPortMasterName And _
                                  portCollection.Query.Vlid = record.VLIdentifier And _
                                  portCollection.Query.UdpPortDestination = record.DestinationUdpPortAddress)

        portCollection.Query.Load()
        If portCollection.Count = 0 Then


            PortId += 1

            Dim port As Dacn1AfdxPort = portCollection.AddNew()
            port.CollectionName = "PW980A"
            port.TxRxEnum = eTransmissionMode.Tx 'Attention un Input est en fait un ouput du point de vue du PowerDNA
            port.Name = record.AfdxPortMasterName
            'port.Description = record.PhysicalPortId
            port.ExportToCsv = True
            port.PortMode = GetPortMode(False).ToUpper
            port.Vlid = record.VLIdentifier
            port.PortID = record.RxAfdxPortIdentifier 'PortId 'record.RxAfdxPortIdentifier 'Right(record.PhysicalPortId, 1)
            port.SubVLID = 0 'Default
            If port.Vlid = 8755 Or port.Vlid = 8955 Or port.Vlid = 9555 Or port.Vlid = 9755 Or port.Vlid = 15058 Then
                'ft but lors de l'ionstallation a Revima, certaines vl n'etaient pas transmises, Une meme VL sur 2 ports differetns, uniquement la premiere etaient transmises
                'UEI a donc dit qu'il faut setter le number de sous Vl a 2, et de changer dans les ports, sub vl id a 0, 1, ou autres dans les cas ou il y en a plus
                If port.PortID = 12338 Or port.PortID = 12578 Or port.PortID = 13298 Or port.PortID = 13538 Or port.PortID = 50250 Then
                    port.SubVLID = 0
                ElseIf port.PortID = 12344 Or port.PortID = 12584 Or port.PortID = 13304 Or port.PortID = 13544 Or port.PortID = 50251 Then
                    port.SubVLID = 1
                End If
            ElseIf port.Vlid = 15068 Then
                'cette VL sera transmises sur 4 ports differents, aussi faut updater les ports avec subvl id correspondant i.e. 0,1,2,3 pour les ports de cette VL
                If port.PortID = 20015 Then
                    port.SubVLID = 0
                ElseIf port.PortID = 50210 Then
                    port.SubVLID = 1
                ElseIf port.PortID = 50400 Then
                    port.SubVLID = 2
                ElseIf port.PortID = 50401 Then
                    port.SubVLID = 3
                End If

            End If
            'ft bug chez revima    port.SubVLID = 0 'record.SubVlIdentifier
            'Deplace dans VL
            'If record.Networkselect.ToUpper = "A&B" Then
            '    port.TransmissionChannelAB = 2 '2 =A&B
            'Else
            '    port.TransmissionChannelAB = IIf(record.NetworkId = "A", 0, 1)
            'End If
            port.Period = record.BAG * 1000
            port.PeriodSkew = 0 'record.BAG * 1000
            port.EndSystemSource = 0 'GetEndSystemSourceFromIp(record.SourceIpAddress)
            port.PartitionSource = 0 'GetPartitionSourceFromIp(record.SourceIpAddress)
            port.EndSystemDestination = GetEndSystemDestionationFromIp(record.DestinationIpAddress) ', "unicast")
            port.PartitionDestination = GetPartitionDestinationFromIp(record.DestinationIpAddress) ', "unicast")

            'port.UdpPortSource = record.SourceUdpPortAddress
            port.UdpPortSource = record.DestinationUdpPortAddress 'Source n'est pas definie dans le CSV, mais c'est une clé primaire et doit contenir une valeur
            port.UdpPortDestination = record.DestinationUdpPortAddress
            port.PortTypeEnum = GetPortType(record.PortCharacteristic) 'PortType 
            Try
                port.MessageLength = IIf(record.BufferSize.Replace(",", "") = "120120", 120, record.BufferSize.Replace(",", ""))
            Catch
                port.MessageLength = record.BufferSize.Replace(",", "")
            End Try
            If record.PortCharacteristic.ToUpper = "SAP" Or record.PortCharacteristic.ToUpper = "QUEUING" Then
                port.Depth = 16 ' Peu importe si Queuing, le max doit être 16 'record.BufferSize '16 'Applicable when Queuing Or SAP only
            Else
                port.Depth = 0
            End If
            'ft pas vrai port.Depth = port.MessageLength 'Un probleme lors de l'installation a Revima,certaines n'estaient pas transmis et les gens de UEI ont dit de mettre depth = au message length
            port.BufferSize = -1
            portCollection.Save()
        End If

        Return True

    End Function

    Private Function ImporstInputVL(record As OutputVL, ByVal defaultMTU As Integer) As Boolean

        ImporstInputVL = False

        'If record.PortCharacteristic.ToUpper = "SAP" Or record.PortCharacteristic.ToUpper = "QUEUING" Then
        '    Exit Function
        'End If
        If record.PortCharacteristic.ToUpper = "SAP" Then
            Exit Function
        End If

        If record.NetworkId.ToUpper = "A" Then 'Conserve uniquement les pour Channel A (identique a B)

            Dim vlCollection As New Dacn1AfdxVirtualLinkCollection()

            If record.NetworkSelect.ToUpper = "A&B" Then
                vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                                         vlCollection.Query.Vlid = record.VlIdentifier And _
                                         vlCollection.Query.ArincChannelEnum = Dacn1AfdxVirtualLink.eArincChannel.ChAB)
                'Collection.Query.ArincChannel = 2) '2 =A&B
            Else
                vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                                         vlCollection.Query.Vlid = record.VlIdentifier And _
                                         vlCollection.Query.ArincChannelEnum = IIf(record.NetworkId = "A", Dacn1AfdxVirtualLink.eArincChannel.ChA, Dacn1AfdxVirtualLink.eArincChannel.ChB))
            End If
            vlCollection.Query.Load()
            If vlCollection.Count = 0 Then

                Dim vl As Dacn1AfdxVirtualLink = vlCollection.AddNew()
                vl.ExportToCsv = True
                vl.CollectionName = CELProject.EngineModel
                vl.Vlid = record.VlIdentifier
                vl.VLName = record.VlName

                'If IsNothing(record.NumberOfSubVl) Then
                '    vl.NumberOfSubVL = 0
                'Else
                '    vl.NumberOfSubVL = record.NumberOfSubVl
                'End If
                vl.NumberOfSubVL = 0 'Un probleme lors de l'installation a Revima,certaines VL n'etaient pas transmis et les gens de UEI ont dit de mettre cechamps a 0

                'If IsNothing(record.SubVlIdentifier) Then
                '    vl.SubVLID = 0
                'Else
                '    vl.SubVLID = record.SubVlIdentifier
                'End If
                vl.SubVLID = 0 'Un probleme lors de l'installation a Revima,certaines VL n'etaient pas transmis et les gens de UEI ont dit de mettre cechamps a 0

                vl.Description = record.AfdxPortMasterName
                vl.BroadcastTypeEnum = IIf(record.AfdxPortTransmissionType.ToUpper = "UNICAST", _
                                        Dacn1AfdxVirtualLink.eBroadcastType.Unicast, _
                                        Dacn1AfdxVirtualLink.eBroadcastType.Multicast)
                vl.IntegrityCheck = 1 'True ATTENTION vb traite le True = a 255  'IC Active pas specifie pour les input vl??
                If record.NetworkSelect.ToUpper = "A&B" Then
                    vl.ArincChannelEnum = 2 '2 =A&B
                    vl.RedondancyEnabled = 1 'True ATTENTION vb traite le True = a 255
                Else
                    vl.ArincChannelEnum = IIf(record.NetworkId = "A", Dacn1AfdxVirtualLink.eArincChannel.ChA, Dacn1AfdxVirtualLink.eArincChannel.ChB) 'A ou B
                    vl.RedondancyEnabled = 0 'Ne pas utiliser False a cause de conversion implicite a byte de VB
                End If
                vl.TxRxEnum = eTransmissionMode.Rx
                vl.VirtualLinkEnabled = 1 'True
                vl.FragmentationEnabled = record.IpFragmentationAllowed
                vl.ICMPEnabled = GetPortType(record.PortCharacteristic) = ePortType.ICMP
                vl.Bag = record.Bag * 1000 'Bag en microsec et non en ms
                vl.Mtu = defaultMTU + 18 '  doit etre >= record.MaxFrameSize + header UDP + header IP
                vl.MaxJitter = 0 '??
                vl.SkewMax = 65500000 '65535 * 1000 '?? record.SkewMax
                vl.SkewDelay = 0 '??
                vl.PrevSnRange = 0
                vl.EDEEnabled = 0 'False
                vl.EDESid = 0
                vl.EDESkewMax = 0
                vl.EDEAgeMax = 0
                vl.EDELagMax = 0
                vl.VLMode = GetVLMode(True, _
                                   IIf(vl.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Unicast, 0, 1), _
                                    vl.FragmentationEnabled, _
                                    vl.ICMPEnabled, _
                                    vl.EDEEnabled, _
                                    vl.ArincChannel, _
                                    vl.IntegrityCheck, _
                                    vl.RedondancyEnabled).ToUpper

                vlCollection.Save()
            End If
        End If
    End Function
    Private Sub ImporstOutputVL(record As InputVL, ByVal defaultMTU As Integer)

        'If record.NetworkId.ToUpper = "A" Then 'Conserve uniquement les pour Channel A (identique a B)

        Dim vlCollection As New Dacn1AfdxVirtualLinkCollection()

        If record.Networkselect.ToUpper = "A&B" Then
            vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                                     vlCollection.Query.Vlid = record.VLIdentifier And _
                                     vlCollection.Query.ArincChannelEnum = Dacn1AfdxVirtualLink.eArincChannel.ChAB) '2 =A&B



        Else
            vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                                     vlCollection.Query.Vlid = record.VLIdentifier And _
                                     vlCollection.Query.ArincChannel = IIf(record.NetworkId = "A", 0, 1))
        End If
        vlCollection.Query.Load()

        If vlCollection.Count = 0 Then

            Dim vl As Dacn1AfdxVirtualLink = vlCollection.AddNew()
            vl.ExportToCsv = True
            vl.CollectionName = CELProject.EngineModel
            vl.Vlid = record.VLIdentifier
            vl.VLName = record.VLName
            vl.Description = record.AfdxPortMasterName
            'TODO dans le document de pratt il y a pas de colonne Pour Unicast ou Multicast, il faut le deviner a partir de l'Adresse Ip de destination

            If vl.Vlid = 8755 Or vl.Vlid = 8955 Or vl.Vlid = 9555 Or vl.Vlid = 9755 Or vl.Vlid = 15058 Then
                'ft but lors de l'ionstallation a Revima, certaines vl n'etaient pas transmises, Une meme VL sur 2 ports differetns, uniquement la premiere etaient transmises
                'UEI a donc dit qu'il faut setter le number de sous Vl a 2, et de changer dans les ports, sub vl id a 0, 1, ou autres dans les cas ou il y en a plus
                vl.NumberOfSubVL = 2
            ElseIf vl.Vlid = 15068 Then
                vl.NumberOfSubVL = 4 'cette VL sera transmises sur 4 ports differents, aussi faut updater les ports avec subvl id correspondant i.e. 0,1,2,3 pour les ports de cette VL
            Else
                vl.NumberOfSubVL = 0
            End If

            'vl.NumberOfSubVL = 0

            vl.SubVLID = 0

            'vl.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Unicast ' IIf(record.AfdxPortTransmissionType.ToUpper = "UNICAST", _
            If record.DestinationIpAddress.StartsWith("224.224") Then
                vl.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Multicast
            ElseIf record.DestinationIpAddress.StartsWith("10.") Then
                vl.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Unicast
            End If
            'Dacn1AfdxVirtualLink.eBroadCast.Unicast, _
            'Dacn1AfdxVirtualLink.eBroadCast.Multicast)
            If record.ICActive.ToLower = "yes" Then
                vl.IntegrityCheck = 1
            Else
                vl.IntegrityCheck = 0
            End If

            If record.RMA.ToLower = "active" Then
                vl.RedondancyEnabled = 1
            Else
                vl.RedondancyEnabled = 0
            End If

            If record.Networkselect.ToUpper = "A&B" Then
                vl.ArincChannelEnum = Dacn1AfdxVirtualLink.eArincChannel.ChAB '2 =A&B
            Else
                vl.ArincChannelEnum = IIf(record.NetworkId = "A", Dacn1AfdxVirtualLink.eArincChannel.ChA, Dacn1AfdxVirtualLink.eArincChannel.ChB) 'A ou B
            End If
            vl.TxRxEnum = eTransmissionMode.Tx
            vl.VirtualLinkEnabled = 1 'True
            vl.FragmentationEnabled = record.IpFragmentationAllowed
            vl.ICMPEnabled = GetPortType(record.PortCharacteristic) = ePortType.ICMP
            vl.Bag = record.BAG * 1000
            vl.Mtu = defaultMTU + 18 ' doit etre >= record.MaxFrameSize + header UDP + header IP
            vl.MaxJitter = 0 '??
            vl.SkewMax = 65500000 '65535 * 1000 '?? skewmax en microsec et non en ms ==> *1000 record.SkewMax
            vl.SkewDelay = 0 '??
            vl.PrevSnRange = 0
            vl.EDEEnabled = 0 'False
            vl.EDESid = 0
            vl.EDESkewMax = 0
            vl.EDEAgeMax = 0
            vl.EDELagMax = 0

            vl.VLMode = GetVLMode(False, _
                                IIf(vl.BroadcastTypeEnum = Dacn1AfdxVirtualLink.eBroadcastType.Unicast, 0, 1), _
                                 vl.FragmentationEnabled, _
                                 vl.ICMPEnabled, _
                                 vl.EDEEnabled, _
                                 vl.ArincChannel, _
                                 vl.IntegrityCheck, _
                                 vl.RedondancyEnabled).ToUpper

            vlCollection.Save()
        End If
        ' End If

    End Sub



    Const AR664_PORT_EN = (1L << 31)   '// port is enabled
    '// <mode>
    Const AR664_VL_EN = (1L << 31)      '// VL enabled
    Const AR664_VL_RX = (0L << 30)      '// this is RX VL
    Const AR664_VL_TX = (1L << 30)      '// this is TX VL
    Const AR664_VL_MULTI = (1L << 8)    '// this VL has multiple sinks
    Const AR664_VL_FRAG_EN = (1L << 7)  '// fragmentation is enabled
    Const AR664_VL_ICMP_EN = (1L << 6)  '// ES needs to respond on ICMP requests on this VL
    Const AR664_VL_EDE_EN = (1L << 5)   '// enable EDE framing

    Private Function GetPortMode(ByVal Rx As Boolean) As String

        Dim portMode As Long = AR664_PORT_EN  'Enable Port

        'Rx Tx sont configure sur la VL et non sur le port
        'If Not Rx Then
        '    portMode = portMode Or AR664_VL_TX
        'End If

        'If Rx Then
        '    'vlMode = vlMode Or AR664_VL_RX
        '    portMode = portMode And (Not (AR664_VL_TX))
        'Else
        '    portMode = portMode Or AR664_VL_TX
        'End If


        Return Convert.ToString(portMode, 16)

    End Function
    Const AR664_VL_USE_A = (1L << 0)
    Const AR664_VL_USE_B = (2L << 0)
    Const AR664_VL_DUAL = (3L << 0)
    Const AR664_VL_RED_MGM = (1L << 2)
    Const AR664_VL_INTGR_EN = (1L << 3)

    Private Function GetVLMode(ByVal Rx As Boolean, _
                                ByVal multipleSink As Boolean, _
                                ByVal fragmentationEnable As Boolean, _
                                ByVal icmpEnable As Boolean, _
                                ByVal edeEnable As Boolean, _
                                ByVal transmissionChannel As Byte, _
                                ByVal integrityCheck As Boolean, _
                                ByVal redondancyManager As Boolean) As String


        Dim vlMode As Long = AR664_VL_EN 'VL Enabled by default
        'Dim vlMode As Long ' dans la config de la V6 du sample test, = AR664_VL_EN Michael a retire ce flag?? 'VL Enabled by default

        If transmissionChannel = 0 Then
            vlMode = vlMode Or AR664_VL_USE_A
        ElseIf transmissionChannel = 1 Then
            vlMode = vlMode Or AR664_VL_USE_B
        ElseIf transmissionChannel = 2 Then
            vlMode = vlMode Or AR664_VL_DUAL

            ' dans la config de la V6 du sample test, = AR664_VL_EN Michael a retire ces flags de IC et Redondancy?
            'If integrityCheck = 1 Then
            '    vlMode = vlMode Or AR664_VL_INTGR_EN
            'End If
            'If redondancyManager = 1 Then
            '    vlMode = vlMode Or AR664_VL_RED_MGM
            'End If
        End If

        'ft test override uniquement pour le channel A
        'vlMode = AR664_VL_EN Or AR664_VL_USE_A

        If Rx Then
            'vlMode = vlMode Or AR664_VL_RX
            vlMode = vlMode And (Not (AR664_VL_TX))
        Else
            vlMode = vlMode Or AR664_VL_TX
        End If

        If multipleSink Then vlMode = vlMode Or AR664_VL_MULTI
        If fragmentationEnable Then vlMode = vlMode Or AR664_VL_FRAG_EN
        If icmpEnable Then vlMode = vlMode Or AR664_VL_ICMP_EN
        If edeEnable Then vlMode = vlMode Or AR664_VL_EDE_EN

        Return Convert.ToString(vlMode, 16)

    End Function

    'Const AR664_PORT_SMPL = 1    '// sampling port
    'Const AR664_PORT_QUEUE = 2   '// queuing port
    'Const AR664_PORT_SAP = 4     '// SAP port
    'Const AR664_PORT_ICMP = 5    '// ICMP port (only one allowed per VL)
    Private Function GetPortType(ByVal portType As String) As ePortType

        Select Case portType.ToUpper
            Case "SAMPLING"
                Return ePortType.Sampling
            Case "QUEUING"
                Return ePortType.Queuing
            Case "SAP"
                Return ePortType.SAP
            Case "ICMP"
                Return ePortType.ICMP
            Case Else
                Throw New Exception("PortType")
        End Select
        Return Nothing

    End Function
    Private Function GetEndSystemSourceFromIp(ByVal ipAddress As String) As Int32

        Dim systemSourceHexa As String = "0"
        If ipAddress & "" <> "" Then
            Dim bytes() As String = ipAddress.Split(".")
            If bytes.GetUpperBound(0) = 3 Then
                systemSourceHexa = ((Convert.ToInt32(bytes(1))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                systemSourceHexa += ((Convert.ToInt32(bytes(2))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
            Else
                Throw New Exception("Exception Occured, invalid Ip Address Source")
            End If
        End If

        Return Convert.ToInt32(systemSourceHexa, 16)

    End Function
    Private Function GetPartitionSourceFromIp(ByVal ipAddress As String) As Int32

        Dim partitionSourceHexa As String = "0"
        If ipAddress & "" <> "" Then
            Dim bytes() As String = ipAddress.Split(".")
            If bytes.GetUpperBound(0) = 3 Then
                partitionSourceHexa = ((Convert.ToInt32(bytes(3))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
            Else
                Throw New Exception("Exception Occured, invalid Ip Address Source")
            End If
        End If

        Return Convert.ToInt32(partitionSourceHexa, 16)

    End Function
    Private Function GetEndSystemDestionationFromIp(ByVal ipAddress As String) As Int32

        Dim systemDestinationHexa As String = "0"
        If ipAddress & "" <> "" Then
            Dim bytes() As String = ipAddress.Split(".")
            If bytes.GetUpperBound(0) = 3 Then
                'If transmissionType.ToUpper = "UNICAST" Or ipAddress.StartsWith("10.") Then
                If ipAddress.StartsWith("10.") Then
                    systemDestinationHexa = ((Convert.ToInt32(bytes(1))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                    systemDestinationHexa += ((Convert.ToInt32(bytes(2))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                    'ElseIf transmissionType.ToUpper = "MULTICAST" Or ipAddress.StartsWith("224.224") Then
                ElseIf ipAddress.StartsWith("224.224") Then
                    'systemDestinationHexa = 0
                    'systemDestinationHexa = ((Convert.ToInt32(bytes(1))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                    'systemDestinationHexa += ((Convert.ToInt32(bytes(2))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                    'systemDestinationHexa += ((Convert.ToInt32(bytes(3))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                    systemDestinationHexa = ((Convert.ToInt32(bytes(2))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa

                Else
                    Throw New Exception("transmission type not supported")
                End If
            Else
                Throw New Exception("Exception Occured, invalid Ip Address Source")
            End If
        End If
        Dim test As Int32 = Convert.ToInt32(systemDestinationHexa, 16)
        Return Convert.ToInt32(systemDestinationHexa, 16)

    End Function
    Private Function GetPartitionDestinationFromIp(ByVal ipAddress As String) ', ByVal transmissionType As String) As Int32

        Dim partitionDestinationHexa As String = "0"
        If ipAddress & "" <> "" Then
            Dim bytes() As String = ipAddress.Split(".")
            If bytes.GetUpperBound(0) = 3 Then
                If ipAddress.StartsWith("10.") Then ' transmissionType = "unicast" Then
                    partitionDestinationHexa = ((Convert.ToInt32(bytes(3))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                ElseIf ipAddress.StartsWith("224.224") Then ' transmissionType = "multicast" Then
                    'partitionDestinationHexa = 0
                    'partitionDestinationHexa = ((Convert.ToInt32(bytes(3))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                    partitionDestinationHexa = ((Convert.ToInt32(bytes(3))).ToString("X")).PadLeft(2, "0") 'Convert to Hexa
                Else
                    Throw New Exception("transmission type not supported")
                End If

            Else
                Throw New Exception("Exception Occured, invalid Ip Address Source")
            End If
        End If
        Dim test As Int32 = Convert.ToInt32(partitionDestinationHexa, 16)
        Return Convert.ToInt32(partitionDestinationHexa, 16)

    End Function


    Private Function ImportsOutputMessages(record As InputMessage) As Boolean

        If record.MessageProtocolType = "SNMP" Or record.MessageProtocolType = "BITE" Then
            Return False
        End If

        Dim udpPortDestionation As Integer


        Dim afdxPortCollection As New Dacn1AfdxPortCollection()
        afdxPortCollection.es.Connection.Name = CELProject.DefaultConnectionName
        afdxPortCollection.Query.Where(afdxPortCollection.Query.CollectionName = CELProject.EngineModel)
        afdxPortCollection.Query.Where(afdxPortCollection.Query.PortID = record.AssociatedRxAFDXPort) 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
        afdxPortCollection.Query.Load()
        If (afdxPortCollection.Count = 1) Then
            udpPortDestionation = afdxPortCollection.Item(0).UdpPortDestination

            Dim afdxMessageCollection As New Dacn1AfdxMessageCollection()
            afdxMessageCollection.es.Connection.Name = CELProject.DefaultConnectionName
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.CollectionName = CELProject.EngineModel)
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.FdsName = record.FDSName)
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.UdpPortDestination = udpPortDestionation) 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
            afdxMessageCollection.Query.Load()

            If afdxMessageCollection.Count = 0 And record.FDSName & "" <> "" Then


                Dim tempVlid As Integer
                Dim vlCollection As New Dacn1AfdxVirtualLinkCollection()
                If record.AssociatedVL <> "" Then
                    vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                                             vlCollection.Query.VLName = record.AssociatedVL)
                    vlCollection.Query.Load()
                    If vlCollection.Count Then
                        tempVlid = vlCollection.Item(0).Vlid
                    End If

                Else
                    Return False 'Ne pas importer une definition de message si la VL relie n as pas ete definie
                    Exit Function
                End If

                If record.FDSName = "ENGINE_STATUS" Then 'Fix un bug de definition dans le ICD de Pratt, le FDSNAME ENGINE_STATUS est en conflit avec le ENGINE_STATUS_1A 1B 2A/B 3A/B 4A/B qui sont respectivement sur le meme VL,meme port, meme FdsAddess mais avec des fdsName differents
                    If tempVlid = 6854 Then
                        record.FDSName = "ENGINE_STATUS_1A"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 7054 Then
                        record.FDSName = "ENGINE_STATUS_1B"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 7254 Then
                        record.FDSName = "ENGINE_STATUS_2A"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 7454 Then
                        record.FDSName = "ENGINE_STATUS_2B"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 7654 Then
                        record.FDSName = "ENGINE_STATUS_3A"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 7854 Then
                        record.FDSName = "ENGINE_STATUS_3B"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 8054 Then
                        record.FDSName = "ENGINE_STATUS_4A"
                        record.FSName = record.FDSName + "_FS"
                    ElseIf tempVlid = 8254 Then
                        record.FDSName = "ENGINE_STATUS_4B"
                        record.FSName = record.FDSName + "_FS"
                    End If
                End If

                Dim message As Dacn1AfdxMessage = afdxMessageCollection.AddNew()
                message.TxRxEnum = eTransmissionMode.Tx '  1 'Tx
                message.CollectionName = CELProject.EngineModel
                message.UdpPortSource = udpPortDestionation
                message.UdpPortDestination = udpPortDestionation

                'message.Name = record.MessageName
                message.FdsDescription = record.MessageDescription

                message.Vlid = tempVlid 'vlCollection.Item(0).Vlid
                message.ExportToCsv = True


                message.FdsName = record.FDSName
                message.FdsDescription = record.FDSDescription
                message.FdsLength = record.FDSLength
                message.FdsAddress = record.FDSAddressInTheMessage
                'message.SignalAddress = record.SignalAddress
                'message.SignalNumberOfBit = record.SignalNbOfBit
                message.FsName = record.FSName
                message.FsDescription = record.FSDescription
                message.FsAddress = record.FSAddressInTheMessage
                'message.ParameterName = record.ParameterName
                'message.DataTypeEnum = GetDataType(record.SignalType) 'ParameterType Or SignalType is the same
                'message.CodingTypeEnum = GetCodingType(record, message.DataTypeEnum)
                'message.MinValue = GetMinValue(record, message.DataTypeEnum)
                'message.MaxValue = GetMaxValue(record, message.DataTypeEnum)
                'message.Length = GetLength(record, message.DataTypeEnum) 'String Length, Opaque Length
                'message.Accuracy = GetAccuracy(record, message.DataTypeEnum)
                'message.EngUnitID = GetEngUnitId(record, message.DataTypeEnum)
                'message.Format = GetFormat(message.DataTypeEnum)
                'message.EnumerateState = record.EnumerateState
                'message.EnumateAssociatedValue = record.EnumerateAssociatedValue
                'message.DisplayFormat = "F0"
                'message.DecimalDigits = 1

                Try
                    afdxMessageCollection.Save()
                Catch ex As Exception
                    If record.FDSName = "ENGINE_STATUS_1A" Or _
                        record.FDSName = "ENGINE_STATUS_1B" Or _
                        record.FDSName = "ENGINE_STATUS_2A" Or _
                        record.FDSName = "ENGINE_STATUS_2B" Or _
                        record.FDSName = "ENGINE_STATUS_3A" Or _
                        record.FDSName = "ENGINE_STATUS_3B" Or _
                        record.FDSName = "ENGINE_STATUS_4A" Or _
                        record.FDSName = "ENGINE_STATUS_4B" Then
                        'La definition de FDS doit etre unique pour 1 VLID-PORT-FDSAddress, alors on tombera 16 fois dans le catch pour resoudre ce problème dans le ICD Definition du PW980A
                        Console.WriteLine("WARNING: Fix Bug dans la definition ICD de Pratt, Substuitution du nom de FDS ENGINE_STATUS pour Fds : " + record.FDSName)
                    Else
                        System.Windows.Forms.MessageBox.Show("Error Saving Output Message FdsName : " + record.FDSName)
                    End If
                End Try



            Else
                Debug.Assert(True)
            End If
            ImportsOutputSignals(record)
        Else
            Debug.Assert(True)
        End If
        Return True

    End Function
    Private Sub ImportsOutputSignals(ByRef record As InputMessage)
        'If GetDataType(record.SignalType) = eDataType.Boolean Then


        Dim udpPortDestionation As Integer


        Dim afdxPortCollection As New Dacn1AfdxPortCollection()
        afdxPortCollection.es.Connection.Name = CELProject.DefaultConnectionName
        afdxPortCollection.Query.Where(afdxPortCollection.Query.CollectionName = CELProject.EngineModel)
        afdxPortCollection.Query.Where(afdxPortCollection.Query.PortID = record.AssociatedRxAFDXPort) 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
        afdxPortCollection.Query.Load()

        If (afdxPortCollection.Count = 1) Then

            udpPortDestionation = afdxPortCollection.Item(0).UdpPortDestination

            Dim afdxMessageCollection As New Dacn1AfdxMessageCollection()
            afdxMessageCollection.es.Connection.Name = CELProject.DefaultConnectionName
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.CollectionName = CELProject.EngineModel)
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.FdsName = record.FDSName)
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.UdpPortDestination = udpPortDestionation) 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
            afdxMessageCollection.Query.Load()

            If afdxMessageCollection.Count > 0 Then

                Dim signalCollection As New Dacn1AfdxSignalCollection
                signalCollection.es.Connection.Name = CELProject.DefaultConnectionName
                signalCollection.Query.Where(signalCollection.Query.CollectionName = CELProject.EngineModel)
                signalCollection.Query.Where(signalCollection.Query.FdsName = record.FDSName)
                signalCollection.Query.Where(signalCollection.Query.UdpPortDestination = udpPortDestionation)
                If (record.SignalPosition <> "") Then
                    signalCollection.Query.Where(signalCollection.Query.StartBit = record.SignalPosition)
                    signalCollection.Query.Where(signalCollection.Query.StopBit = record.SignalPosition)
                End If
                signalCollection.Query.Load()

                'If signalCollection.Count = 0 Then

                Dim signal As Dacn1AfdxSignal = signalCollection.AddNew()
                signal.ExportToCsv = True
                signal.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                signal.CollectionName = CELProject.EngineModel
                signal.Vlid = afdxMessageCollection.Item(0).Vlid
                signal.UdpPortSource = udpPortDestionation '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signal.UdpPortDestination = udpPortDestionation ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signal.FdsName = record.FDSName
                signal.ParameterName = record.ParameterName
                signal.SignalAddress = record.SignalAddress
                signal.SignalNumberOfBit = record.SignalNbOfBit
                'Default value pour ne pas avoir de CSV avec des vides
                signal.StartBit = -1
                signal.StopBit = -1
                If GetDataType(record.SignalType) = eDataType.Boolean Then
                    If (record.SignalPosition.Trim <> "") Then
                        signal.StartBit = record.SignalPosition
                        signal.StopBit = record.SignalPosition
                    End If
                End If

                signal.DataTypeEnum = GetDataType(record.SignalType) 'ParameterType Or SignalType is the same
                signal.CodingTypeEnum = GetCodingType(record, signal.DataTypeEnum)

                If signal.DataTypeEnum = eDataType.Boolean Then
                    If signal.ParameterName.ToUpper.Contains("FLT") Or signal.ParameterName.ToUpper.Contains("FAULT") Or signal.ParameterName.ToUpper.Contains("FAIL") Then
                        signal.CodingTypeEnum = eCodingType.Fault
                    ElseIf signal.FdsName.ToUpper.StartsWith("FWD") Then
                        signal.CodingTypeEnum = eCodingType.Status
                    Else
                        signal.CodingTypeEnum = eCodingType.Status
                    End If
                End If


                signal.MinValue = GetMinValue(record, signal.DataTypeEnum)
                signal.MaxValue = GetMaxValue(record, signal.DataTypeEnum)
                signal.Length = GetLength(record, signal.DataTypeEnum) 'String Length, Opaque Length
                signal.Accuracy = GetAccuracy(record, signal.DataTypeEnum)
                signal.EngUnitID = GetEngUnitId(record, signal.DataTypeEnum)
                signal.Format = GetFormat(signal.DataTypeEnum)
                'signal.EnumerateState = record.EnumerateState
                'signal.EnumateAssociatedValue = record.EnumerateAssociatedValue
                signal.DisplayFormat = GetFormat(signal.DataTypeEnum)
                signal.DecimalDigits = 1

                signalCollection.Save()

                ImportsOutputSignalsEnumeration(signal, record)

                'Else
                '    Debug.Assert(True)
                'End If
            Else
                Debug.Assert(True)
            End If
        Else
            Debug.Assert(True)
        End If
        'End If
    End Sub
    Private Sub ImportsOutputSignalsEnumeration(ByRef signal As Dacn1AfdxSignal, ByRef record As InputMessage)

        If GetDataType(record.SignalType) = eDataType.Boolean Or GetDataType(record.SignalType) = eDataType.Enumeration Then


            Dim signalEnumCollection As New Dacn1AfdxSignalEnumerationCollection
            signalEnumCollection.es.Connection.Name = CELProject.DefaultConnectionName
            signalEnumCollection.Query.Where(signalEnumCollection.Query.CollectionName = CELProject.EngineModel)
            signalEnumCollection.Query.Where(signalEnumCollection.Query.FdsName = record.FDSName)
            signalEnumCollection.Query.Where(signalEnumCollection.Query.UdpPortDestination = signal.UdpPortDestination)
            signalEnumCollection.Query.Load()

            If GetDataType(record.SignalType) = eDataType.Boolean Then


                Dim signalEnum As Dacn1AfdxSignalEnumeration = signalEnumCollection.AddNew()
                signalEnum.ExportToCsv = True
                'signalEnum.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                signalEnum.CollectionName = CELProject.EngineModel
                signalEnum.Vlid = signal.Vlid
                signalEnum.UdpPortSource = signal.UdpPortSource '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.UdpPortDestination = signal.UdpPortDestination ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.FdsName = signal.FdsName
                signalEnum.ParameterName = record.ParameterName


                'False State
                signalEnum.AssociatedStateValue = record.BoolFalseState

                If record.BoolFalseDefinition <> record.BoolTrueDefinition Then
                    signalEnum.AssociatedStateDefinition = record.BoolFalseDefinition.Replace("_", " ")
                Else
                    signalEnum.AssociatedStateDefinition = record.BoolFalseDefinition.Replace("_", " ") + " false"
                End If
                'True State
                signalEnum = signalEnumCollection.AddNew()

                signalEnum.ExportToCsv = True
                'signalEnum.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                signalEnum.CollectionName = CELProject.EngineModel
                signalEnum.Vlid = signal.Vlid
                signalEnum.UdpPortSource = signal.UdpPortSource '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.UdpPortDestination = signal.UdpPortDestination ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.FdsName = signal.FdsName
                signalEnum.ParameterName = record.ParameterName
                signalEnum.AssociatedStateValue = record.BoolTrueState
                signalEnum.AssociatedStateDefinition = record.BoolTrueDefinition.Replace("_", " ")
                signalEnum.DisplayOn = True 'Permettera d'afficher uniquement lorsque le flag est true, si false affiche rien
                signalEnumCollection.Save()


            Else 'Enumaration

                'Dim strEnumerateStates As String = record.EnumerateState.Replace("[", "").Replace("]", "")
                Dim strEnumerateValues As String = record.EnumerateAssociatedValue.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "")
                'Dim strArrEnumerateStates() As String
                Dim strArrEnumerateValues() As String
                Dim count As Integer

                'strArrEnumerateStates = strEnumerateStates.Split("&")
                strArrEnumerateValues = strEnumerateValues.Split("&")

                For count = 0 To strArrEnumerateValues.Length - 1


                    Dim signalEnum As Dacn1AfdxSignalEnumeration = signalEnumCollection.AddNew()
                    signalEnum.ExportToCsv = True
                    'signalEnum.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                    signalEnum.CollectionName = CELProject.EngineModel
                    signalEnum.Vlid = signal.Vlid
                    signalEnum.UdpPortSource = signal.UdpPortSource '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                    signalEnum.UdpPortDestination = signal.UdpPortDestination ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                    signalEnum.FdsName = signal.FdsName
                    signalEnum.ParameterName = record.ParameterName

                    signalEnum.AssociatedStateValue = strArrEnumerateValues(count).Split(":")(0)
                    signalEnum.AssociatedStateDefinition = strArrEnumerateValues(count).Split(":")(1)

                    signalEnumCollection.Save()

                Next

            End If
        End If
    End Sub
    'Private Function ImportsOutputDiscretes(ByRef record As InputMessage)
    '    If GetDataType(record.SignalType) = eDataType.Boolean Then

    '        Dim afdxMessageCollection As New Dacn1AfdxMessageCollection()
    '        afdxMessageCollection.es.Connection.Name = CELProject.DefaultConnectionName
    '        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.CollectionName = CELProject.EngineModel)
    '        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.FdsName = record.FDSName)
    '        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.UdpPortDestination = record.AssociatedRxAFDXPort)
    '        afdxMessageCollection.Query.Load()

    '        If afdxMessageCollection.Count = 1 Then

    '            Dim discreteCollection As New Dacn1AfdxDiscreteCollection
    '            discreteCollection.es.Connection.Name = CELProject.DefaultConnectionName
    '            discreteCollection.Query.Where(discreteCollection.Query.CollectionName = CELProject.EngineModel)
    '            discreteCollection.Query.Where(discreteCollection.Query.FdsName = record.FDSName)
    '            discreteCollection.Query.Where(discreteCollection.Query.UdpPortDestination = record.AssociatedRxAFDXPort)
    '            discreteCollection.Query.Where(discreteCollection.Query.StartBit = record.SignalPosition)
    '            discreteCollection.Query.Where(discreteCollection.Query.StopBit = record.SignalPosition)
    '            discreteCollection.Query.Load()

    '            If discreteCollection.Count = 0 Then

    '                Dim discrete As Dacn1AfdxDiscrete = discreteCollection.AddNew()
    '                discrete.ExportToCsv = True
    '                discrete.TxRxEnum = eTransmissionMode.Tx '1 'Tx
    '                discrete.CollectionName = CELProject.EngineModel
    '                discrete.Vlid = afdxMessageCollection.Item(0).Vlid
    '                discrete.UdpPortSource = record.AssociatedRxAFDXPort
    '                discrete.UdpPortDestination = record.AssociatedRxAFDXPort
    '                discrete.FdsName = record.FDSName
    '                discrete.ParameterName = record.ParameterName
    '                discrete.StartBit = record.SignalPosition
    '                discrete.StopBit = record.SignalPosition
    '                discrete.BoolFalseDefinition = record.BoolFalseDefinition
    '                discrete.BoolTrueDefinition = record.BoolTrueDefinition
    '                discrete.BoolFalseState = record.BoolFalseState
    '                discrete.BoolTrueState = record.BoolTrueState
    '                'discrete.BoolLogic = GetBoolLogic(record.BoolLogic)

    '                discreteCollection.Save()

    '            Else
    '                ' Debug.Assert(False)
    '            End If
    '        Else
    '            'Debug.Assert(False)
    '        End If
    '    End If
    'End Function

    Private Function ImportsInputMessages(record As OutputMessage) As Boolean

        If record.MessageProtocolType = "SNMP" Or record.MessageProtocolType = "BITE" Then
            Return False
        End If

        Dim udpPortSource As Integer


        Dim afdxPortCollection As New Dacn1AfdxPortCollection()
        afdxPortCollection.es.Connection.Name = CELProject.DefaultConnectionName
        afdxPortCollection.Query.Where(afdxPortCollection.Query.CollectionName = CELProject.EngineModel)
        afdxPortCollection.Query.Where(afdxPortCollection.Query.PortID = record.AssociatedTxAfdxPort) 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
        afdxPortCollection.Query.Load()

        If (afdxPortCollection.Count = 1) Then

            udpPortSource = afdxPortCollection.Item(0).UdpPortSource

            Dim afdxMessageCollection As New Dacn1AfdxMessageCollection()
            afdxMessageCollection.es.Connection.Name = CELProject.DefaultConnectionName
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.CollectionName = CELProject.EngineModel)
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.FdsName = record.FdsName)
            afdxMessageCollection.Query.Where(afdxMessageCollection.Query.UdpPortSource = udpPortSource)
            afdxMessageCollection.Query.Load()

            If afdxMessageCollection.Count = 0 And record.FdsName & "" <> "" Then

                Dim message As Dacn1AfdxMessage = afdxMessageCollection.AddNew()
                message.ExportToCsv = True
                message.TxRxEnum = eTransmissionMode.Rx ' 0 'Rx
                message.CollectionName = CELProject.EngineModel
                message.UdpPortSource = udpPortSource
                message.UdpPortDestination = udpPortSource
                'message.Name = record.MessageName
                message.FdsDescription = record.MessageDescription

                Dim vlCollection As New Dacn1AfdxVirtualLinkCollection()
                If record.AssociatedVl <> "" Then
                    vlCollection.Query.Where(vlCollection.Query.CollectionName = CELProject.EngineModel And _
                                             vlCollection.Query.VLName = record.AssociatedVl)
                    vlCollection.Query.Load()
                    If vlCollection.Count Then
                        message.Vlid = vlCollection.Item(0).Vlid
                    End If

                Else
                    Return False 'Ne pas importer une definition de message si la VL relie n as pas ete definie
                    Exit Function
                End If

                message.FdsName = record.FdsName
                message.FdsDescription = record.FdsDescription
                message.FdsLength = record.FdsLength
                message.FdsAddress = record.FdsAddressInTheMessage
                'message.SignalAddress = record.SignalAddress
                'message.SignalNumberOfBit = record.SignalNbOfBit
                message.FsName = record.FsName
                message.FsDescription = record.FsDescription
                message.FsAddress = record.FsAddressInTheMessage
                'message.ParameterName = record.ParameterName
                'message.DataTypeEnum = GetDataType(record.SignalType) 'ParameterType Or SignalType is the same
                'message.CodingTypeEnum = GetCodingType(record, message.DataTypeEnum)
                'message.MinValue = GetMinValue(record, message.DataTypeEnum)
                'message.MaxValue = GetMaxValue(record, message.DataTypeEnum)
                'message.Length = GetLength(record, message.DataTypeEnum) 'String Length, Opaque Length
                'message.Accuracy = GetAccuracy(record, message.DataTypeEnum)
                'message.EngUnitID = GetEngUnitId(record, message.DataTypeEnum)
                'message.Format = GetFormat(message.DataTypeEnum)
                'message.EnumerateState = record.EnumerateState
                'message.EnumateAssociatedValue = record.EnumerateAssociatedValue
                'message.DisplayFormat = "F0"
                'message.DecimalDigits = 1

                afdxMessageCollection.Save()

            Else
                Debug.Assert(True)
            End If
            ImportsInputSignals(record)
        End If
        Return True

    End Function
    Private Sub ImportsInputSignals(ByRef record As OutputMessage)
        'If GetDataType(record.SignalType) = eDataType.Boolean Then

        Dim afdxMessageCollection As New Dacn1AfdxMessageCollection()
        afdxMessageCollection.es.Connection.Name = CELProject.DefaultConnectionName
        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.CollectionName = CELProject.EngineModel)
        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.FdsName = record.FdsName)
        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.UdpPortSource = record.AssociatedTxAfdxPort)
        afdxMessageCollection.Query.Load()

        If afdxMessageCollection.Count = 1 Then

            Dim signalCollection As New Dacn1AfdxSignalCollection
            signalCollection.es.Connection.Name = CELProject.DefaultConnectionName
            signalCollection.Query.Where(signalCollection.Query.CollectionName = CELProject.EngineModel)
            signalCollection.Query.Where(signalCollection.Query.FdsName = record.FdsName)
            signalCollection.Query.Where(signalCollection.Query.UdpPortSource = record.AssociatedTxAfdxPort)
            If (record.SignalPosition <> "") Then
                signalCollection.Query.Where(signalCollection.Query.StartBit = record.SignalPosition)
                signalCollection.Query.Where(signalCollection.Query.StopBit = record.SignalPosition)
            End If
            signalCollection.Query.Load()

            If signalCollection.Count = 0 Then
                Dim signal As Dacn1AfdxSignal = signalCollection.AddNew()
                signal.ExportToCsv = True
                signal.Vlid = afdxMessageCollection.Item(0).Vlid
                signal.UdpPortSource = record.AssociatedTxAfdxPort
                signal.UdpPortDestination = IIf(IsNothing(record.AssociatedTxAfdxPort), 0, Convert.ToInt32(record.AssociatedTxAfdxPort))
                signal.TxRxEnum = eTransmissionMode.Rx '0 'Rx
                signal.CollectionName = CELProject.EngineModel
                signal.FdsName = record.FdsName
                signal.ParameterName = record.ParameterName
                signal.SignalAddress = record.SignalAddress
                signal.SignalNumberOfBit = record.SignalNbOfBit

                'Default value pour ne pas avoir de CSV avec des vides
                signal.StartBit = -1
                signal.StopBit = -1
                If GetDataType(record.SignalType) = eDataType.Boolean Then
                    If (record.SignalPosition.Trim <> "") Then
                        signal.StartBit = record.SignalPosition
                        signal.StopBit = record.SignalPosition
                    End If
                End If
                signal.DataTypeEnum = GetDataType(record.SignalType) 'ParameterType Or SignalType is the same
                signal.CodingTypeEnum = GetCodingType(record, signal.DataTypeEnum)

                If signal.DataTypeEnum = eDataType.Boolean Then
                    If signal.ParameterName.ToUpper.Contains("FLT") Or signal.ParameterName.ToUpper.Contains("FAULT") Or signal.ParameterName.ToUpper.Contains("FAIL") Then
                        signal.CodingTypeEnum = eCodingType.Fault
                    ElseIf signal.FdsName.ToUpper.StartsWith("FWD") Then
                        signal.CodingTypeEnum = eCodingType.Status
                    Else
                        signal.CodingTypeEnum = eCodingType.Status
                    End If
                End If

                signal.MinValue = GetMinValue(record, signal.DataTypeEnum)
                signal.MaxValue = GetMaxValue(record, signal.DataTypeEnum)
                signal.Length = GetLength(record, signal.DataTypeEnum) 'String Length, Opaque Length
                signal.Accuracy = GetAccuracy(record, signal.DataTypeEnum)
                signal.EngUnitID = GetEngUnitId(record, signal.DataTypeEnum)
                signal.Format = GetFormat(signal.DataTypeEnum)
                'signal.EnumerateState = record.EnumerateState
                'signal.EnumateAssociatedValue = record.EnumerateAssociatedValue

                signal.DisplayFormat = GetFormat(signal.DataTypeEnum)

                signal.DecimalDigits = 1


                signalCollection.Save()

                ImportsIntputSignalsEnumeration(signal, record)

            Else
                Debug.Assert(True)
            End If
        Else
            Debug.Assert(True)
        End If
        'End If
    End Sub
    Private Sub ImportsIntputSignalsEnumeration(ByRef signal As Dacn1AfdxSignal, ByRef record As OutputMessage)

        If GetDataType(record.SignalType) = eDataType.Boolean Or GetDataType(record.SignalType) = eDataType.Enumeration Then


            Dim signalEnumCollection As New Dacn1AfdxSignalEnumerationCollection
            signalEnumCollection.es.Connection.Name = CELProject.DefaultConnectionName
            signalEnumCollection.Query.Where(signalEnumCollection.Query.CollectionName = CELProject.EngineModel)
            signalEnumCollection.Query.Where(signalEnumCollection.Query.FdsName = record.FdsName)
            signalEnumCollection.Query.Where(signalEnumCollection.Query.UdpPortDestination = signal.UdpPortDestination)
            signalEnumCollection.Query.Load()

            If GetDataType(record.SignalType) = eDataType.Boolean Then


                Dim signalEnum As Dacn1AfdxSignalEnumeration = signalEnumCollection.AddNew()
                signalEnum.ExportToCsv = True
                'signalEnum.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                signalEnum.CollectionName = CELProject.EngineModel
                signalEnum.Vlid = signal.Vlid
                signalEnum.UdpPortSource = signal.UdpPortSource '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.UdpPortDestination = signal.UdpPortDestination ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.FdsName = signal.FdsName
                signalEnum.ParameterName = record.ParameterName


                'False State
                signalEnum.AssociatedStateValue = record.BoolFalseState
                If record.BoolFalseDefinition <> record.BoolTrueDefinition Then
                    signalEnum.AssociatedStateDefinition = record.BoolFalseDefinition.Replace("_", " ")
                Else
                    signalEnum.AssociatedStateDefinition = record.BoolFalseDefinition.Replace("_", " ") + " false"
                End If

                'True State
                signalEnum = signalEnumCollection.AddNew()

                signalEnum.ExportToCsv = True
                'signalEnum.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                signalEnum.CollectionName = CELProject.EngineModel
                signalEnum.Vlid = signal.Vlid
                signalEnum.UdpPortSource = signal.UdpPortSource '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.UdpPortDestination = signal.UdpPortDestination ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                signalEnum.FdsName = signal.FdsName
                signalEnum.ParameterName = record.ParameterName
                signalEnum.AssociatedStateValue = record.BoolTrueState
                signalEnum.AssociatedStateDefinition = record.BoolTrueDefinition.Replace("_", " ")
                signalEnum.DisplayOn = True 'Permettera d'afficher uniquement lorsque le flag est true, si false affiche rien

                signalEnumCollection.Save()


            Else 'Enumaration

                'Dim strEnumerateStates As String = record.EnumerateState.Replace("[", "").Replace("]", "")
                Dim strEnumerateValues As String = record.EnumerateAssociatedValue.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "")
                'Dim strArrEnumerateStates() As String
                Dim strArrEnumerateValues() As String
                Dim count As Integer

                'strArrEnumerateStates = strEnumerateStates.Split("&")
                strArrEnumerateValues = strEnumerateValues.Split("&")

                For count = 0 To strArrEnumerateValues.Length - 1


                    Dim signalEnum As Dacn1AfdxSignalEnumeration = signalEnumCollection.AddNew()
                    signalEnum.ExportToCsv = True
                    'signalEnum.TxRxEnum = eTransmissionMode.Tx '1 'Tx
                    signalEnum.CollectionName = CELProject.EngineModel
                    signalEnum.Vlid = signal.Vlid
                    signalEnum.UdpPortSource = signal.UdpPortSource '   record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                    signalEnum.UdpPortDestination = signal.UdpPortDestination ' record.AssociatedRxAFDXPort 'Attention ICI le AssociatedRxAfdxPort n'est pas le port UDP!!!!
                    signalEnum.FdsName = signal.FdsName
                    signalEnum.ParameterName = record.ParameterName
                    signalEnum.AssociatedStateValue = strArrEnumerateValues(count).Split(":")(0)
                    signalEnum.AssociatedStateDefinition = strArrEnumerateValues(count).Split(":")(1)

                    signalEnumCollection.Save()

                Next

            End If
        End If
    End Sub
    'Private Function ImportsInputDiscretes(ByRef record As OutputMessage)
    '    If GetDataType(record.SignalType) = eDataType.Boolean Then

    '        Dim afdxMessageCollection As New Dacn1AfdxMessageCollection()
    '        afdxMessageCollection.es.Connection.Name = CELProject.DefaultConnectionName
    '        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.CollectionName = CELProject.EngineModel)
    '        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.FdsName = record.FdsName)
    '        afdxMessageCollection.Query.Where(afdxMessageCollection.Query.UdpPortSource = record.AssociatedTxAfdxPort)
    '        afdxMessageCollection.Query.Load()

    '        If afdxMessageCollection.Count = 1 Then

    '            Dim discreteCollection As New Dacn1AfdxDiscreteCollection
    '            discreteCollection.es.Connection.Name = CELProject.DefaultConnectionName
    '            discreteCollection.Query.Where(discreteCollection.Query.CollectionName = CELProject.EngineModel)
    '            discreteCollection.Query.Where(discreteCollection.Query.FdsName = record.FdsName)
    '            discreteCollection.Query.Where(discreteCollection.Query.UdpPortSource = record.AssociatedTxAfdxPort)
    '            discreteCollection.Query.Where(discreteCollection.Query.StartBit = record.SignalPosition)
    '            discreteCollection.Query.Where(discreteCollection.Query.StopBit = record.SignalPosition)
    '            discreteCollection.Query.Load()

    '            If discreteCollection.Count = 0 Then
    '                Dim discrete As Dacn1AfdxDiscrete = discreteCollection.AddNew()
    '                discrete.ExportToCsv = True
    '                discrete.Vlid = afdxMessageCollection.Item(0).Vlid
    '                discrete.UdpPortSource = record.AssociatedTxAfdxPort
    '                discrete.UdpPortDestination = IIf(IsNothing(record.AssociatedTxAfdxPort), 0, Convert.ToInt32(record.AssociatedTxAfdxPort))
    '                discrete.TxRxEnum = eTransmissionMode.Rx '0 'Rx
    '                discrete.CollectionName = CELProject.EngineModel
    '                discrete.FdsName = record.FdsName
    '                discrete.ParameterName = record.ParameterName
    '                discrete.StartBit = record.SignalPosition
    '                discrete.StopBit = record.SignalPosition
    '                discrete.BoolFalseDefinition = record.BoolFalseDefinition
    '                discrete.BoolTrueDefinition = record.BoolTrueDefinition
    '                discrete.BoolFalseState = record.BoolFalseState
    '                discrete.BoolTrueState = record.BoolTrueState
    '                discrete.BoolLogic = GetBoolLogic(record.BoolLogic)

    '                discreteCollection.Save()

    '            Else
    '                ' Debug.Assert(False)
    '            End If
    '        Else
    '            'Debug.Assert(False)
    '        End If
    '    End If
    'End Function
    Private Function GetBoolLogic(logic As String)
        If logic = "" Then
            Return 0
        ElseIf logic = "positive" Then
            Return 1
        ElseIf logic = "negative" Then
            Return 2
        Else
            Return 0
        End If
    End Function


    Private Function GetDataType(ByRef SignalType As String) As eDataType 'ParameterType
        Select Case SignalType.ToUpper
            Case "STRING"
                Return eDataType.String
            Case "INTEGER"
                Return eDataType.Integer
            Case "FLOAT"
                Return eDataType.Float
            Case "BOOLEAN"
                Return eDataType.Boolean
            Case "OPAQUE"
                Return eDataType.Opaque
            Case "ENUMERATE"
                Return eDataType.Enumeration
            Case Else
                Return eDataType.Binary
        End Select
    End Function

    Private Function GetCodingType(ByVal record As OutputMessage, ByVal dataType As eDataType) As eCodingType
        Select Case dataType
            Case eDataType.Float
                If record.FloatCodingType.ToUpper = "SIMPLE PRECISION" Then
                    Return eCodingType.SimplePrecision
                End If
            Case eDataType.Integer
                If record.IntegerCodingType.ToUpper = "BN2" Then
                    Return eCodingType.BNR2
                Else

                End If
        End Select
        Return eCodingType.NotSpecified
    End Function
    Private Function GetCodingType(ByVal record As InputMessage, ByVal dataType As eDataType) As eCodingType
        Select Case dataType
            Case eDataType.Float
                If record.FloatCodingType.ToUpper = "SIMPLE PRECISION" Then
                    Return eCodingType.SimplePrecision
                End If
            Case eDataType.Integer
                If record.IntegerCodingType.ToUpper = "BN2" Then
                    Return eCodingType.BNR2
                Else

                End If
        End Select
        Return eCodingType.NotSpecified
    End Function
    Private Function GetMinValue(ByVal record As OutputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer
                Return record.IntegerOperationalMin
            Case eDataType.Float
                Return record.FloatOperationalMin
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GetMinValue(ByVal record As InputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer
                Return record.IntegerOperationalMin
            Case eDataType.Float
                Return record.FloatOperationalMin
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GetMaxValue(ByVal record As OutputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer
                Return record.IntegerOperationalMax
            Case eDataType.Float
                Return record.FloatOperationalMax
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GetMaxValue(ByVal record As InputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer
                Return record.IntegerOperationalMax
            Case eDataType.Float
                Return record.FloatOperationalMax
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GetLength(ByVal record As OutputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer, eDataType.Float
                Return Nothing
            Case eDataType.String
                Return record.StringLength
            Case eDataType.Opaque
                Return record.OpaqueLength
            Case Else
                Return Nothing
        End Select
        Return Nothing
    End Function
    Private Function GetLength(ByVal record As InputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer, eDataType.Float
                Return Nothing
            Case eDataType.String
                Return IIf(record.StringLength = "", 0, record.StringLength)
            Case eDataType.Opaque
                Return record.OpaqueLength
            Case Else
                Return Nothing
        End Select
        Return Nothing
    End Function
    Private Function GetAccuracy(ByVal record As OutputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer
                If record.IntegerOperationalAccuracy = "" Then
                    Return Nothing
                End If
                Return record.IntegerOperationalAccuracy
            Case eDataType.Float
                If record.FloatOperationalAccuracy = "" Then
                    Return Nothing
                End If
                Return record.FloatOperationalAccuracy
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GetAccuracy(ByVal record As InputMessage, ByVal dataType As eDataType) As Double
        Select Case dataType
            Case eDataType.Integer
                If record.IntegerOperationalAccuracy = "" Then
                    Return Nothing
                End If
                Return record.IntegerOperationalAccuracy
            Case eDataType.Float
                If record.FloatOperationalAccuracy = "" Then
                    Return Nothing
                End If
                Return record.FloatOperationalAccuracy
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function GetEngUnitId(ByVal record As OutputMessage, ByVal dataType As eDataType) As EngineeringUnit.eEngineeringUnit
        Select Case dataType
            Case eDataType.Integer
                Select Case record.IntegerOperationalUnit
                    Case ""
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "%"
                        Return EngineeringUnit.eEngineeringUnit.PCT
                    Case "mA"
                        Return EngineeringUnit.eEngineeringUnit.mA
                    Case "A"
                        Return EngineeringUnit.eEngineeringUnit.AMP
                    Case "deg C"
                        Return EngineeringUnit.eEngineeringUnit.DEGC
                    Case "psi abs"
                        Return EngineeringUnit.eEngineeringUnit.PSIA
                    Case "No Unit"
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "pph"
                        Return EngineeringUnit.eEngineeringUnit.PPH
                    Case "lb"
                        Return EngineeringUnit.eEngineeringUnit.LBS
                    Case "V"
                        Return EngineeringUnit.eEngineeringUnit.VDC
                    Case "ft"
                        Return Nothing 'TODO manque des unites de distance
                    Case "mbar"
                        Return EngineeringUnit.eEngineeringUnit.MBAR
                    Case "h"
                        Return EngineeringUnit.eEngineeringUnit.HOUR
                    Case Else
                        Debug.Assert(False)
                End Select
            Case eDataType.Float
                Select Case record.FloatOperationalUnit
                    Case ""
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "%"
                        Return EngineeringUnit.eEngineeringUnit.PCT
                    Case "mA"
                        Return EngineeringUnit.eEngineeringUnit.mA
                    Case "A"
                        Return EngineeringUnit.eEngineeringUnit.AMP
                    Case "deg C"
                        Return EngineeringUnit.eEngineeringUnit.DEGC
                    Case "psi abs"
                        Return EngineeringUnit.eEngineeringUnit.PSIA
                    Case "No Unit"
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "pph"
                        Return EngineeringUnit.eEngineeringUnit.PPH
                    Case "lb"
                        Return EngineeringUnit.eEngineeringUnit.LBS
                    Case "V"
                        Return EngineeringUnit.eEngineeringUnit.VDC
                    Case "ft"
                        Return Nothing 'TODO manque des unites de distance
                    Case "mbar"
                        Return EngineeringUnit.eEngineeringUnit.MBAR
                    Case "h"
                        Return EngineeringUnit.eEngineeringUnit.HOUR
                    Case Else
                        Debug.Assert(False)
                End Select
            Case Else
                Return EngineeringUnit.eEngineeringUnit.Empty
        End Select
        Return Nothing
    End Function
    Private Function GetEngUnitId(ByVal record As InputMessage, ByVal dataType As eDataType) As EngineeringUnit.eEngineeringUnit
        Select Case dataType
            Case eDataType.Integer
                Select Case record.IntegerOperationalUnit
                    Case ""
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "%"
                        Return EngineeringUnit.eEngineeringUnit.PCT
                    Case "mA"
                        Return EngineeringUnit.eEngineeringUnit.mA
                    Case "A"
                        Return EngineeringUnit.eEngineeringUnit.AMP
                    Case "deg C"
                        Return EngineeringUnit.eEngineeringUnit.DEGC
                    Case "psi abs"
                        Return EngineeringUnit.eEngineeringUnit.PSIA
                    Case "No Unit"
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "pph"
                        Return EngineeringUnit.eEngineeringUnit.PPH
                    Case "lb"
                        Return EngineeringUnit.eEngineeringUnit.LBS
                    Case "V"
                        Return EngineeringUnit.eEngineeringUnit.VDC
                    Case "ft"
                        Return Nothing 'TODO manque des unites de distance
                    Case "mbar"
                        Return EngineeringUnit.eEngineeringUnit.MBAR
                    Case "h"
                        Return EngineeringUnit.eEngineeringUnit.HOUR
                    Case Else
                        Debug.Assert(False)
                End Select
            Case eDataType.Float
                Select Case record.FloatOperationalUnit
                    Case ""
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "%"
                        Return EngineeringUnit.eEngineeringUnit.PCT
                    Case "mA"
                        Return EngineeringUnit.eEngineeringUnit.mA
                    Case "A"
                        Return EngineeringUnit.eEngineeringUnit.AMP
                    Case "deg C"
                        Return EngineeringUnit.eEngineeringUnit.DEGC
                    Case "psi abs"
                        Return EngineeringUnit.eEngineeringUnit.PSIA
                    Case "No Unit"
                        Return EngineeringUnit.eEngineeringUnit.Empty
                    Case "pph"
                        Return EngineeringUnit.eEngineeringUnit.PPH
                    Case "lb"
                        Return EngineeringUnit.eEngineeringUnit.LBS
                    Case "V"
                        Return EngineeringUnit.eEngineeringUnit.VDC
                    Case "ft"
                        Return Nothing 'TODO manque des unites de distance
                    Case "mbar"
                        Return EngineeringUnit.eEngineeringUnit.MBAR
                    Case "h"
                        Return EngineeringUnit.eEngineeringUnit.HOUR
                    Case Else
                        Debug.Assert(False)
                End Select
            Case Else
                Return EngineeringUnit.eEngineeringUnit.Empty
        End Select
        Return Nothing
    End Function
    Private Function GetFormat(ByVal dataType As eDataType) As String
        Select Case dataType
            Case eDataType.Integer
                Return "F0"
            Case eDataType.Float
                Return "F1"
            Case Else
                Return "F0"
        End Select
        Return Nothing
    End Function


    Private Function CustomSelector(ByVal engine As MultiRecordEngine, ByVal record As String) As Type

        If (record.StartsWith("#ICD Format Version Number")) Then
            Return GetType(IcdVersionNumber)
        ElseIf (record.StartsWith("AFDX_OUTPUT_VL")) Then
            Return GetType(OutputVL)
        ElseIf (record.StartsWith("AFDX_OUTPUT_MESSAGE")) Then
            Return GetType(OutputMessage)
        ElseIf (record.StartsWith("AFDX_INPUT_VL")) Then
            Return GetType(InputVL)
        ElseIf (record.StartsWith("AFDX_INPUT_MESSAGE")) Then
            Return GetType(InputMessage)
        ElseIf (record.StartsWith("DISCRETE_OUTPUT_LINE")) Then
            Return GetType(DiscreteOutputLine)
        ElseIf (record.StartsWith("DISCRETE_OUTPUT_SIGNAL")) Then
            Return GetType(DiscreteOutputSignal)
        ElseIf (record.StartsWith("DISCRETE_INPUT_LINE")) Then
            Return GetType(DiscreteOutputLine)
        ElseIf (record.StartsWith("DISCRETE_INPUT_SIGNAL")) Then
            Return GetType(DiscreteOutputSignal)
        Else
            Return Nothing
        End If
    End Function


    <DelimitedRecord(",")> _
    Public Class IcdVersionNumber
        Public VersionNumber As String
        Public NotUsed As String
    End Class
    <DelimitedRecord(",")> _
    Public Class OutputVL

        Public Prefix As String 'AFDX_OUTPUT_VL
        Public PhysicalPortId As String
        Public PhysicalPortSpeed As String
        Public PinName As String
        Public AfdxLineEmcProtection As String
        Public NetworkId As String
        Public ConnectorName As String
        Public VlIdentifier As String
        Public VlName As String
        Public NetworkSelect As String
        Public Bag As Integer
        Public MaxFrameSize As Integer
        Public NumberOfSubVl As Integer
        Public SubVlIdentifier As Integer
        Public TxAfdxportIdentifier As Integer
        Public AfdxPortMasterName As String
        Public AfdxPortType As String
        Public PortCharacteristic As String
        '<FieldConverter(ConverterKind.Boolean, "yes", "")> _
        <FieldConverter(GetType(YesNoNullConverter))> _
        Public IpFragmentationAllowed As Byte
        Public AfdxPortTransmissionType As String
        Public SourceIpAddress As String
        Public DestinationIpAddress As String
        Public SourceUdpPortAddress As Integer?
        Public DestinationUdpPortAddress As Integer?
        Public BufferSize As Integer

    End Class
    Public Class YesNoNullConverter
        Inherits ConverterBase
        Protected Overrides ReadOnly Property CustomNullHandling As Boolean
            Get
                Return True 'Le return True permet de catcher les NULLL/NOTHING et de le convertir en False 'MyBase.CustomNullHandling
            End Get
        End Property
        Public Overrides Function StringToField(from As String) As Object
            If IsNothing(from) Then Return False
            If from.ToUpper = "YES" Then
                Return Convert.ToByte(1)
            Else
                Return Convert.ToByte(0)  'Cas ou le champ est vide = NULL/Nothing
            End If
        End Function
        Public Overrides Function FieldToString(from As Object) As String
            Return MyBase.FieldToString(from)
        End Function
    End Class
    <DelimitedRecord(",")> _
    Public Class OutputMessage

        Public Prefix As String 'AFDX_OUTPUT_MESSAGE
        Public ApplicationName As String
        Public ApplicationDescription As String
        Public ApplicationType As String
        Public AssociatedVl As String
        Public AssociatedTxAfdxPort As String
        Public MessageName As String
        Public MessageDescription As String
        Public MessageRedDoc As String
        Public MessageType As String
        Public MessageProtocolType As String
        Public MessageLength As Integer
        <FieldNullValue(0)> _
        Public GlobalSpareLength As Integer?
        Public MessageTransmissionRate As Integer
        Public PortRefreshRate As String
        Public FdsName As String
        Public FdsDescription As String
        Public FdsLength As Integer?
        <FieldNullValue(0)> _
        Public FdsSpareLength As Integer?
        Public FdsAddressInTheMessage As Integer?
        Public FsName As String
        Public FsDescription As String
        Public FsAddressInTheMessage As Integer?
        Public ParameterType As String
        Public ParameterLocalName As String
        Public LocalNameNotProduced As String
        Public LocalNameRefreshPeriod As String
        Public LocalNameFunctionalAttribute As String
        Public LocalNameDescription As String
        Public ParameterName As String
        Public ParameterKeyword As String
        Public ParameterDescription As String
        Public ParameterRefDoc As String
        Public SignalName As String
        Public SignalType As String
        Public SignalDescription As String
        Public SignalRefDoc As String
        Public SignalNbOfBit As Integer?
        Public SignalAddress As Integer?
        Public SignalPosition As String
        Public BoolTrueDefinition As String
        Public BoolFalseDefinition As String
        Public BoolTrueState As String
        Public BoolFalseState As String
        Public BoolLogic As String
        Public StringLength As String
        Public StringFormat As String
        Public EnumerateState As String
        Public EnumerateAssociatedValue As String
        Public FloatOperationalMin As String
        Public FloatOperationalMax As String
        Public FloatOperationalUnit As String
        Public FloatOperationalAccuracy As String
        Public FloatCodingType As String
        Public IntegerOperationalMin As String
        Public IntegerOperationalMax As String
        Public IntegerOperationalUnit As String
        Public IntegerOperationalAccuracy As String
        Public IntegerCodingType As String
        Public OpaqueSize As String
        Public OpaqueLength As String
        Public OpaqueIOMCorrespondingLabel As String

    End Class
    <DelimitedRecord(",")> _
    Public Class InputVL

        Public Prefix As String
        Public PhysicalPortId As String
        Public PhysicalPortSpeed As String
        Public PinName As String
        Public AfdxLineEmcProtection As String
        Public NetworkId As String
        Public ConnectorName As String
        Public VLIdentifier As String
        Public VLName As String
        Public Networkselect As String
        Public BAG As String
        Public MaxFrameSize As String
        Public RMA As String 'Redundancy Manager Enable
        Public ICActive As String 'Integrity check enable
        Public SkewMax As String
        Public RxAfdxPortIdentifier As String
        Public AfdxPortMasterName As String
        Public AfdxPortType As String
        Public PortCharacteristic As String
        <FieldConverter(GetType(YesNoNullConverter))> _
        Public IpFragmentationAllowed As Byte
        Public DestinationIpAddress As String
        Public DestinationUdpPortAddress As String
        Public BufferSize As String

    End Class
    <DelimitedRecord(",")> _
    Public Class InputMessage

        Public Prefix As String
        Public ApplicationName As String
        Public ApplicationDescription As String
        Public ApplicationType As String
        Public AssociatedVL As String
        Public AssociatedRxAFDXPort As String
        Public MessageName As String
        Public MessageDescription As String
        Public MessageRefDoc As String
        Public MessageType As String
        Public MessageProtocolType As String
        Public MessageLength As String
        Public GlobalSparelength As String
        Public MessageTransmissionRate As String
        Public PortRefreshRate As String
        Public FDSName As String
        Public FDSDescription As String
        Public FDSLength As String
        Public FDSSpareLength As String
        Public FDSAddressInTheMessage As String
        Public FSName As String
        Public FSDescription As String
        Public FSAddressInTheMessage As String
        Public ParameterType As String
        Public ParameterLocalName As String
        Public LocalNameNotUsed As String
        Public LocalNameRefreshPeriod As String
        Public LocalNameFunctionalAttribute As String
        Public LocalNameDescription As String
        Public ParameterName As String
        Public ParameterKeyword As String
        Public ParameterDescription As String
        Public ParameterRefDoc As String
        Public SignalName As String
        Public SignalType As String
        Public SignalDescription As String
        Public SignalRefDoc As String
        Public SignalNbOfBit As String
        Public SignalAddress As String
        Public SignalPosition As String
        Public BoolTrueDefinition As String
        Public BoolFalseDefinition As String
        Public BoolTrueState As String
        Public BoolFalseState As String
        Public BoolLogicStringLength As String
        Public StringFormat As String
        Public StringLength As String
        Public EnumerateState As String
        Public EnumerateAssociatedValue As String
        Public FloatOperationalMin As String
        Public FloatOperationalMax As String
        Public FloatOperationalUnit As String
        Public FloatOperationalAccuracy As String
        Public FloatCodingType As String
        Public IntegerOperationalMin As String
        Public IntegerOperationalMax As String
        Public IntegerOperationalUnit As String
        Public IntegerOperationalAccuracy As String
        Public IntegerCodingType As String
        Public OpaqueSize As String
        Public OpaqueLength As String
        Public OpaqueIOMCorrespondingLabel As String

    End Class
    <DelimitedRecord(",")> _
    Public Class DiscreteOutputLine

        Public Prefix As String
        Public DiscreteLineName As String
        Public LineDescription As String
        Public LineEMCProtection As String
        Public ConnectorType As String
        Public PinName As String
        Public PinRole As String
        Public ConnectorName As String
        Public ConnectionName As String

    End Class
    <DelimitedRecord(",")> _
    Public Class DiscreteOutputSignal

        Public Prefix As String
        Public ApplicationName As String
        Public ApplicationDescription As String
        Public ApplicationType As String
        Public LineName As String
        Public ParameterLocalName As String
        Public LocalNameNotProduced As String
        Public LocalNameRefreshPeriod As String
        Public LocalNameFunctionalAttribute As String
        Public LocalNameDescription As String
        Public ParameterName As String
        Public ParameterDescription As String
        Public ParameterRefDoc As String
        Public Keyword As String
        Public TrueDefinition As String
        Public FalseDefinition As String
        Public SignalName As String
        Public SignalDescription As String
        Public IOType As String
        Public TrueState As String
        Public FalseState As String
        Public Unit As String
        Public Logic As String
        Public StabilizationTime As String
        Public DefaultValue As String
        Public MinCurrentRange As String
        Public MaxCurrentRange As String
        Public UnitCurrentRange As String

    End Class
    <DelimitedRecord(",")> _
    Public Class DiscreteInputLine

        Public Prefix As String
        Public DiscreteLineName As String
        Public LineDescription As String
        Public LineEMCProtection As String
        Public ConnectorType As String
        Public PinName As String
        Public PinRole As String
        Public ConnectorName As String
        Public ConnectionName As String

    End Class
    <DelimitedRecord(",")> _
    Public Class DiscreteInputSignal

        Public Prefix As String
        Public ApplicationName As String
        Public ApplicationDescription As String
        Public ApplicationType As String
        Public LineName As String
        Public ParameterLocalName As String
        Public LocalNameNotProduced As String
        Public LocalNameRefreshPeriod As String
        Public LocalNameFunctionalAttribute As String
        Public LocalNameDescription As String
        Public ParameterName As String
        Public ParameterDescription As String
        Public ParameterRefDoc As String
        Public Keyword As String
        Public TrueDefinition As String
        Public FalseDefinition As String
        Public SignalName As String
        Public SignalDescription As String
        Public IOType As String
        Public TrueState As String
        Public FalseState As String
        Public Unit As String
        Public Logic As String
        Public StabilizationTime As String
        Public DefaultValue As String
        Public MinCurrentRange As String
        Public MaxCurrentRange As String
        Public UnitCurrentRange As String

    End Class






End Class