Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ReportProcessor
    Private Shared _ReportFile As String
    Private Shared _ReportDS As DataTable
    Private Shared _ReportTitle As String
    Private Shared _ReportParameter As String
    Public Sub New(reporttitle As String, reportfile As String, reportds As DataTable, Optional reportparam As String = "")
        _ReportTitle = reporttitle
        _ReportFile = reportfile
        _ReportDS = reportds
        _ReportParameter = reportparam
    End Sub

    Shared Function RunReport(Optional preview As Boolean = True) As Boolean
        Try
            If _ReportDS.Rows.Count <> 0 Then
                Dim dsData As DataTable = _ReportDS
                Dim ReportDoc As New ReportDocument
                ReportDoc.Load(_ReportFile, OpenReportMethod.OpenReportByTempCopy)
                ReportDoc.SetDataSource(dsData)
                ReportDoc.Refresh()

                If preview Then
                    Dim fx As New ReportViewer(ReportDoc, _ReportTitle, _ReportParameter, True)
                    'fx.WindowState = FormWindowState.Maximized
                    fx.ShowDialog()
                Else
                    Dim fx As New ReportViewer(ReportDoc, _ReportTitle, _ReportParameter)
                    'fx.ShowDialog()
                End If
                Return True
            Else
                MessageBox.Show("No Records to Print")
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return True
    End Function
End Class
