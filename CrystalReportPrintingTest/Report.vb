Public Class Report
    Shared Function Print(data As DataTable, fileName As String, preview As Boolean, Optional xmlFileName As String = "") As Boolean



        Dim bReturn As Boolean = False
        Dim crReport As String = fileName
        Try


            If data.Rows.Count <> 0 Then
                Try
                    data.TableName = "TableName"
                    If xmlFileName <> "" Then
                        data.WriteXml(xmlFileName & ".xml", XmlWriteMode.IgnoreSchema)
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                'Dim app_path As String = Application.StartupPath
                'Dim rptFile As String = app_path & "\" & crReport

                If System.IO.File.Exists(fileName) Then
                    Dim cPrint As New ReportProcessor("Print Preview", fileName, data, "")
                    cPrint.RunReport(preview)
                Else
                    MessageBox.Show("Report file " & crReport & " not found!")
                End If

            Else
                MessageBox.Show("No Records to Print")
            End If






        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return bReturn
    End Function

End Class
