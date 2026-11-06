

Namespace BusinessObjects

    Public Class Dacn1Afdx
        Public Enum eTransmissionMode
            Rx = 0
            Tx = 1
        End Enum
        Public Enum eDataType
            [String] = 0
            [Integer] = 1
            [Float] = 2
            [Boolean] = 3
            Opaque = 4
            Enumeration = 5
            Binary = 6
        End Enum
        Public Enum eCodingType
            NotSpecified = 0
            BNR2 = 1
            BCD = 2
            SimplePrecision = 3
            DoublePrecision = 4
            Fault = 5
            Status = 6
        End Enum
        Public Enum ePortType
            Sampling = 1 '// sampling port
            Queuing = 2 '// queuing port
            SAP = 4 '// SAP port
            ICMP = 5 '// ICMP port (only one allowed per VL)
        End Enum
    End Class
   
End Namespace