Imports Microsoft.VisualBasic

Public Class Test
  Public Class Variable

    Friend TestVariable1 As Integer
    Friend TestVariable2 As Integer

  End Class

  Public Function GetVariables(ByVal _Parameter As String) As Variable

    Dim _return As New Variable

    _return.TestVariable1 = 1
    _return.TestVariable1 = 2

    Return _return

  End Function
End Class
