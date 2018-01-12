Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ReportViewer
    Private _ReportDocument As ReportDocument

    Public Sub New(ByVal objReport As ReportDocument, ByVal ReportTitle As String, ByVal Parameters As String, Optional ByVal Preview As Boolean = False)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = ReportTitle
        Dim strParValPair() As String
        Dim strVal() As String
        Dim index As Integer

        Try

            _ReportDocument = objReport

            Dim paraValue As New CrystalDecisions.Shared.ParameterDiscreteValue
            Dim currValue As CrystalDecisions.Shared.ParameterValues

            Dim intParamCounter As Integer = objReport.DataDefinition.ParameterFields.Count
            If intParamCounter = 1 Then
                If InStr(objReport.DataDefinition.ParameterFields(0).ParameterFieldName, ".", CompareMethod.Text) > 0 Then
                    intParamCounter = 0
                End If
            End If

            If intParamCounter > 0 And Trim(Parameters).Length <> 0 Then
                strParValPair = Parameters.Split("&")
                For index = 0 To UBound(strParValPair)
                    If InStr(strParValPair(index), "=") > 0 Then
                        strVal = strParValPair(index).Split("=")
                        paraValue.Value = strVal(1)
                        currValue = objReport.DataDefinition.ParameterFields(strVal(0)).CurrentValues
                        currValue.Add(paraValue)
                        objReport.DataDefinition.ParameterFields(strVal(0)).ApplyCurrentValues(currValue)
                    End If
                Next
            End If

            'objReport.Refresh()
            'ApplyTextObject("AppTitleText", AppVariables.Title & " " & AppVariables.AppVersion)

            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.ReportSource = objReport
            'CrystalReportViewer1.RefreshReport()

            If Preview Then
                CrystalReportViewer1.Zoom(1)
            Else
                CrystalReportViewer1.PrintReport()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ApplyTextObject(ByVal vstrTextObject As String, ByVal vstrTextValue As String)
        Try
            CType(_ReportDocument.ReportDefinition.ReportObjects(vstrTextObject), CrystalDecisions.CrystalReports.Engine.TextObject).Text = vstrTextValue
        Catch ex As Exception
            'MessageBox.Show("ApplyTextObject" & Environment.NewLine & "There is no >>" & vstrTextObject & "<< object in this report!")
        End Try
    End Sub

    Private Sub LoadReport()
        Dim ReportDoc As New ReportDocument()
        'ReportDoc.Load(mPlugin.ReportFile, OpenReportMethod.OpenReportByTempCopy)
    End Sub

    Private Sub ReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class