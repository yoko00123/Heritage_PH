Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32
Imports InSysLicensing
Public Class RegistrationForm
    Dim cryptDES3 As New TripleDESCryptoServiceProvider()
    Dim cryptMD5Hash As New MD5CryptoServiceProvider()
    'Private Const PassKey As String = "oFEFhfIGZ3v9ULuJUxwf+7G5blNdfjzPmG70mDQZT630VZWLXKbp5tffz"
    Public ProductKey As String, ProductCompany As String
    'Private Sub btnActiveProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActiveProduct.Click
    '    Dim acpath As String = SelectFile(ofdIRL, "Select License Key", "License File (.lic)|*.lic")
    '    If acpath <> "" Then
    '        ValidateLicenseKey(acpath)
    '    End If
    'End Sub
    'Private Function FDecrypt(ByVal myString As String, ByVal mykey As String) As String

    '    cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(mykey))
    '    cryptDES3.Mode = CipherMode.ECB
    '    Dim desdencrypt As ICryptoTransform = cryptDES3.CreateDecryptor()
    '    Dim buff() As Byte = Convert.FromBase64String(myString)
    '    Return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    'End Function
    Public Sub ValidateLicenseKey(ByVal strLKeyPath As String, ByVal strProdKey As String)
        Dim ilVLK As New InSysLicense, Valid As Boolean = False
        Dim LFileName As String = Path.GetFileName(strLKeyPath)
        Dim lmaddress As String = GetMACAddress()
        If File.Exists(strLKeyPath) Then
            Valid = ilVLK.ValidateLicenseKey(strLKeyPath, strProdKey, lmaddress)

            If Valid = True Then
                GSCOM.SQL.ExecuteNonQuery("update tsetting set value  = 1 where name = 'ProductRegistered'", gConnection)
                GSCOM.SQL.ExecuteNonQuery("update tsetting set value  = '" & lmaddress & "' where name = 'MacAddress'", gConnection)
                Me.Close()
            End If

        End If
        
        'Dim arrLkey() As String = File.ReadAllLines(strLKeyPath)
        'Dim curPkey As String = "", curMacAdd As String = ""
        'For Each strL As String In arrLkey
        '    If strL <> "" Then
        '        Dim temp As String = FDecrypt(strL, PassKey)
        '        Dim arrL() As String = temp.Split("|"c)
        '        For Each lic As String In arrL
        '            If curPkey = "" Then
        '                curPkey = lic
        '                Continue For
        '            End If
        '            If curMacAdd = "" Then
        '                curMacAdd = lic
        '                Continue For
        '            End If
        '        Next
        '        If curPkey <> ProductKey And curMacAdd <> GetMACAddress() Then
        '            MsgBox("Invalid Client Key")
        '        Else
        '            'activate
        '            'GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting(ProductRegistered)", gConnection)
        '            GSCOM.SQL.ExecuteNonQuery("update tsetting set value  = 1 where name = 'ProductRegistered'", gConnection)
        '            MsgBox("Product Activated")
        '        End If
        '    Else
        '        MsgBox("Invalid Client Key")
        '    End If
        '    Exit For
        'Next
    End Sub


    Private Sub GenerateClientKey(ByVal strClkPath As String)
        Dim strProdKey As Object = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ProductKey')", gConnection)
        Dim strMAddress As String = GetMACAddress()
        File.AppendAllText(strClkPath, ProductCompany & "|" & ProductKey & "|" & strMAddress)
        MsgBox("Client Key Generated")
    End Sub

    Private Sub btnClientKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientKey.Click
        Dim ckpath As String = FILEIO.SaveFile(sfdIRL, "Save Client Key", "Client Key File (.clk)|*.clk", ProductCompany)
        If ckpath <> "" Then
            File.Delete(ckpath)
            GenerateClientKey(ckpath)
        End If
    End Sub
End Class