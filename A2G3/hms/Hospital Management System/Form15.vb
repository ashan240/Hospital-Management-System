Imports System.Data.OleDb
Imports System.IO

Public Class Form15
    Dim check_flag As Int16 = 0
    Dim Flag_control As Int16 = 0
    Dim con As New OleDbConnection
    Private Sub Appointments_List()
        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter
        FlowLayoutPanel1.Controls.Clear()
        con.Open()

        da = New OleDbDataAdapter("Select * from Appointment_Database where Patient_ID like '%" & TextBox2.Text & "%' And  Date_Of_Appointment like '%" & TextBox1.Text & "%' ", con)
        da.Fill(dt)

        For Each row As DataRow In dt.Rows
            Dim newpatient As New row_table
            For Each column As DataColumn In dt.Columns

                If column.ColumnName = "Doctor_Name" Then
                    newpatient.Text = " Doctor_Name   :   " & row(column).ToString
                End If
                If column.ColumnName = "Remarks" Then
                    newpatient.Text = newpatient.Text & "        Remarks/Medicines  :  " & row(column).ToString
                End If
            Next
            FlowLayoutPanel1.Controls.Add(newpatient)
        Next

        con.Close()
    End Sub

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ESTABLISHING THE CONNECTION STRING JUST AFTER THE FORM IS LOADED

        Dim path As String = Directory.GetCurrentDirectory
        path = Directory.GetParent(path).ToString
        path = Directory.GetParent(path).ToString
        Dim connectionstring As String = "provider=microsoft.ACE.OLEDB.12.0 ; data source = " & path & "\hms_Database.accdb"
        con.ConnectionString = connectionstring
        RichTextBox1.Text = "Remarks:"
        TextBox1.Clear()
        Appointments_List()
        Flag_control = 1
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If Flag_control = 1 And CheckBox2.Checked = False Then
            TextBox1.Text = DateTimePicker1.Value.ToShortDateString
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Appointments_List()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = False And check_flag = 0 Then
            check_flag = 1
            MessageBox.Show("Completed check box is not clicked." & vbCrLf & "The reamrks added will not be added.")
        ElseIf CheckBox1.Checked = False And check_flag = 1 Then
            Me.Close()
        Else
            Dim insertString As String = "Insert into Appointment_DataBase([Remarks]) Values(?)"
            Dim cmd As OleDbCommand = New OleDbCommand(insertString, con)
            cmd.Parameters.Add(New OleDbParameter("Remarks", CType(RichTextBox1.Text, String)))
            Try
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Me.Close()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            TextBox1.Clear()
        End If
        If CheckBox2.Checked = False Then
            TextBox1.Text = DateTimePicker1.Value.ToShortDateString
        End If

    End Sub
End Class

Public Class row_table
    Inherits Button

    Public Sub New()
        AutoSize = False
        Dock = DockStyle.Top
        BackColor = Color.AliceBlue
        FlatStyle = Windows.Forms.FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        Margin = New Padding(4, 4, 4, 0)
        Size = New Size(525, 30)
        TextAlign = ContentAlignment.MiddleLeft

    End Sub
    Private Sub When_clicked() Handles Me.Click

    End Sub

End Class