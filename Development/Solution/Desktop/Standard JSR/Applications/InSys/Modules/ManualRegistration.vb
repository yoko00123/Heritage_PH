Imports System.Net.NetworkInformation
Imports System
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text
Imports System.Management

Public Class ManualRegistration

#Region "Initialization"

    Private pLength As Integer = 18
    Public MD5 As New MD5CryptoServiceProvider
    Private pass As String = "z0/Yx9cro/mPx2+Lh2Qr6g=="
    Private UniqueInfo As Integer = 0

#End Region

#Region "Methods"

    Public Function GetKey() As String
        Dim pKey As String = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ProductKey')", gConnection).ToString
        pLength = pKey.Length
        pass = pKey.Substring(4, 5)
        Return pKey
    End Function

    Public Function GetProductKey() As String
        Dim ProductKey As String = ""
        Dim arrypKey() As Char
        Dim arryMac() As Char
        arrypKey = GetKey.ToCharArray
        arryMac = getUniqueInfo.ToCharArray
        Dim x As Integer
        For x = 0 To arrypKey.Length - 1
            ProductKey &= arrypKey(x).ToString
            If x < arryMac.Length Then
                ProductKey &= arryMac(x).ToString
            End If
        Next
        While x < arryMac.Length
            ProductKey &= arryMac(x).ToString
            x += 1
        End While
        Return StrReverse(ProductKey)
    End Function

    Function getUniqueInfo() As String
        Dim rslt As String = ""
        Select Case UniqueInfo
            Case 0
                rslt = GetMACAddress()
            Case 1
                rslt = GetSystemInfo("Win32_Processor", "ProcessorId")
            Case 2
                rslt = GetSystemInfo("Win32_BaseBoard")
            Case 3
                rslt = GetSystemInfo("Win32_DiskDrive")
        End Select
        Return rslt
    End Function

    Function GetMACAddress() As String
        Dim nics As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces, MacAddress As String = ""
        For Each adapter As NetworkInterface In nics
            'If adapter.Name = "Local Area Connection" Then
            MacAddress = adapter.GetPhysicalAddress.ToString()
            Exit For
            'End If
        Next
        Return MacAddress
    End Function

    Function GetSystemInfo(ByVal Win32Object As String, Optional ByVal Field As String = "SerialNumber") As String

        Dim rslt As String = String.Empty
        Dim temp As String = String.Empty
        Dim mc As ManagementClass = New ManagementClass(Win32Object)
        Dim moc As ManagementObjectCollection = mc.GetInstances
        For Each mo As ManagementObject In moc
            If (rslt = String.Empty) Then
                rslt = mo.Properties(Field).Value.ToString
            End If
        Next
        Return rslt
    End Function

#End Region

#Region "Compare"

    Public Function VerifyRegistration(ByVal ActivationKey As String) As Boolean
        Dim rslt As Boolean = False
        If Compare(GetProductKey, ActivationKey) Then
            rslt = True
        Else
            rslt = False
            If ActivationKey <> "" Then MsgBox("Invalid Activation Key has been detected. " + vbNewLine + "Re-enter activation key.", CType(MsgBoxStyle.Exclamation + vbOKOnly, MsgBoxStyle), "GIRAFFE Systems Security")
        End If
        Return rslt
    End Function

    Public Function Compare(ByVal ProductKey As String, ByVal ActivationKey As String) As Boolean
        Dim rslt As Byte() = Nothing
        Dim UTF8 As New UTF8Encoding
        Dim TDESKey As Byte() = MD5.ComputeHash(UTF8.GetBytes(pass))
        Dim Algo As New TripleDESCryptoServiceProvider
        Algo.Key = TDESKey
        Algo.Mode = CipherMode.ECB
        Algo.Padding = PaddingMode.PKCS7
        Dim DataToEncrypt As Byte() = UTF8.GetBytes(ProductKey)
        Try
            Dim Encryptor As ICryptoTransform = Algo.CreateEncryptor
            rslt = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Catch
        Finally
            Algo.Clear()
        End Try
        Return Verify(MD5, Convert.ToBase64String(rslt), ActivationKey)
    End Function

    Function Verify(ByVal md5Hash As MD5, ByVal ProductKey As String, ByVal ActivationKey As String) As Boolean
        Dim hashOfpKey As String = GetHash(md5Hash, ProductKey)

        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If 0 = comparer.Compare(hashOfpKey, ActivationKey) Then
            Return True
        Else
            Return False
        End If

    End Function

    Function GetHash(ByVal md5Hash As MD5, ByVal ProductKey As String) As String

        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(ProductKey))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        Return sBuilder.ToString()

    End Function

#End Region

End Class
