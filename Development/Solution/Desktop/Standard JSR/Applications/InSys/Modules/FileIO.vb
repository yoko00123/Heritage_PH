Imports System.IO
Imports System.Net.NetworkInformation
Module FILEIO

    Public strPFold As String
    Public Sub OpenCreatedFile(ByVal strFPath As String, ByVal strPrompt As String, ByVal strTitle As String)

        If MsgBox(strPrompt, MsgBoxStyle.YesNo, strTitle) = MsgBoxResult.Yes Then

            System.Diagnostics.Process.Start(strFPath)

        End If

    End Sub
    Public Function GetMACAddress() As String
        Dim nics As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces, MacAddress As String = ""
        For Each adapter As NetworkInterface In nics
            MacAddress = adapter.GetPhysicalAddress.ToString()
            Exit For
        Next
        Return MacAddress
    End Function
    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(obj)
        Catch
            'Usually because the object is Nothing or has already been released...  Ignore.
        Finally
            If obj IsNot Nothing Then obj = Nothing
        End Try
    End Sub
    Public Function SelectFile(ByVal ofdSF As OpenFileDialog, ByVal strTitle As String, ByVal strFilter As String) As String

        With ofdSF

            If strPFold = "" Then
                .InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            Else
                .InitialDirectory = strPFold
            End If

            .Title = strTitle
            .Filter = strFilter
            .RestoreDirectory = True
            .FileName = ""

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    strPFold = Path.GetDirectoryName(.FileName)
                    Return .FileName
                Catch ex As Exception
                    Return ""
                    MessageBox.Show("Cannot read file from disk. Original Error: " & ex.Message)
                End Try
            Else
                Return ""
            End If

        End With

    End Function

    Public Function SaveFile(ByVal sfdSF As SaveFileDialog, ByVal strTitle As String, ByVal strFilter As String, Optional ByVal strDefaultFilename As String = "") As String

        With sfdSF
            If strPFold = "" Then
                .InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            Else
                .InitialDirectory = strPFold
            End If

            .Filter = strFilter
            .RestoreDirectory = True
            .FileName = strDefaultFilename
            .Title = strTitle
            .OverwritePrompt = True

            If .ShowDialog = DialogResult.OK Then
                Try
                    Return .FileName
                Catch ex As Exception
                    Return ""
                    MessageBox.Show("Cannot create file. Original Error: " & ex.Message)
                End Try
            Else
                Return ""
            End If

        End With

    End Function

    Public Function SelectFolder(ByVal fbdSF As FolderBrowserDialog, ByVal strDesc As String, ByVal NewFolder As Boolean) As String

        With fbdSF
            .Description = strDesc
            .ShowNewFolderButton = NewFolder
            '.RootFolder = Environment.SpecialFolder.MyDocuments
            If strPFold = "" Then
                .SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            Else
                .SelectedPath = strPFold
            End If
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    strPFold = .SelectedPath
                    If strPFold.LastIndexOf("\") <> strPFold.Length - 1 Then
                        strPFold &= "\"
                    End If
                    Return strPFold
                Catch ex As Exception
                    Return ""
                    MessageBox.Show("Cannot read folder from disk. Original Error: " & ex.Message)
                End Try
            Else
                Return ""
            End If
        End With

    End Function

End Module
