
Imports System.Security.Cryptography
Imports System.Data.OleDb
Imports System.Text
Imports System.IO

Public Class Form21
    Dim con As New OleDbConnection

#Region "Functions"
    Private Function StringtoMd5(ByRef Content As String) As String
        Dim M5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim ByteString() As Byte = System.Text.Encoding.ASCII.GetBytes(Content)
        ByteString = M5.ComputeHash(ByteString)
        Dim FinalString As String = Nothing

        For Each bt As Byte In ByteString
            FinalString &= bt.ToString("x2")
        Next
        Return FinalString.ToUpper()
    End Function
#End Region
    Private Function GetPassword() As String
        Dim str As String = ""
        Try

            con.Open()
            ' Dim res As String = DateTime.ParseExact(d, "DD-MM-YYYY", VBCodeProvider)

            Dim query As String = "Select * From Password_DataBase where UserName like 'staff'"
            Dim cmd As OleDbCommand = New OleDbCommand(query, con)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            While (reader.Read())
                Console.WriteLine(reader.GetString(1))
                str = reader.GetString(1)
            End While

            reader.Close()
            cmd.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()

        End Try
        Return str
    End Function
    Dim Username As String = "staff"
    Dim Password As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Password = GetPassword()
        Try
            If TextBox1.Text = "" And TextBox2.Text = "" And TextBox2.Text = "" Then
                MsgBox("You Have Not Entered Username And Password , please Fill The Details")

            ElseIf Username = TextBox1.Text And Password = StringtoMd5(TextBox2.Text) Then
                Form12.Show()
                Form12.Button4.Visible = False
                Form12.Button5.Visible = False
                Form12.Button6.Visible = False
                Form12.Button7.Visible = False
                ' Form12.Button1.Visible = False
                Form12.Button16.Visible = False
                Form12.Button17.Visible = False
                Form12.FlowLayoutPanel2.Enabled = False
                Me.Close()



            Else
                MsgBox("Invalid Username / Password Of Admin", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        Form1.Show()
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Form21_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'ESTABLISHING THE CONNECTION STRING JUST AFTER THE FORM IS LOADED
        Dim path As String = Directory.GetCurrentDirectory
        path = Directory.GetParent(path).ToString
        path = Directory.GetParent(path).ToString
        Dim connectionstring As String = "provider=microsoft.ACE.OLEDB.12.0 ; data source = " & path & "\hms_Database.accdb"

        con.ConnectionString = connectionstring

    End Sub
End Class