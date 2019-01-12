
Partial Class WebUserControlCheckBoxBoostrap
    Inherits System.Web.UI.UserControl

    Private Property _BootstrapType As String = "1"
    Public Property _Disable() As Boolean = False

    Protected Sub CheckBox_PreRender(sender As Object, e As EventArgs)

        If _BootstrapType = "1" Then
            sender.InputAttributes.Add("class", "ace ace-checkbox-2")
        ElseIf _BootstrapType = "2" Then
            sender.InputAttributes.Add("class", "ace ace-switch ace-switch-4 btn-empty")
        ElseIf _BootstrapType = "3" Then
            sender.InputAttributes.Add("class", "ace ace-switch ace-switch-2")
        End If

        If _Disable = True Then
            sender.Attributes.Add("onclick", "return false;")
        End If

    End Sub

    Public Property Text() As String

        Get
            Return LiteralCheckBox.Text
        End Get
        Set(value As String)
            LiteralCheckBox.Text = value
        End Set


    End Property

    Public Property BootstrapType() As String

        Get
            Return String.Empty
        End Get
        Set(value As String)
            _BootstrapType = value
        End Set


    End Property

    Public Property Disable() As Boolean

        Get
            Return False
        End Get
        Set(value As Boolean)
            _Disable = value
        End Set


    End Property


    Public Property AutoPostBack() As Boolean
        Get
            Return CheckBox.AutoPostBack
        End Get
        Set(value As Boolean)
            CheckBox.AutoPostBack = value
        End Set
    End Property

    Public Property Checked() As Boolean
        Get
            Return CheckBox.Checked
        End Get
        Set(value As Boolean)
            CheckBox.Checked = value
        End Set
    End Property

    Public Event CheckChanged As EventHandler

    Protected Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs)

        RaiseEvent CheckChanged(Me, New EventArgs())

    End Sub
End Class
