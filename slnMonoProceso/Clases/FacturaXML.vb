Public Class FacturaXML

    Public Property tipodocto As String
    Public Property identificacion As String
    Public Property digito As String
    Public Property codMoneda As String
    Public Property fecExp As String
    Public Property fecVen As String
    Public Property medioPago As String
    Public Property resolucion As String
    Public Property consecutivo As Integer
    Public Property desde As Integer
    Public Property hasta As Integer
    Public Property prefijo As String
    Public Property totalFact As Decimal
    Public Property subTotalFact As Decimal
    'atributos adicionales
    Public Property nroPedido As String
    Public Property vendedor As String
    Public Property usuarioContacto As String
    Public Property observacion As String
    Public Property usuario As String
    Public Property codigoBarras As String
    Public Property referenciaDocumento As String
    Public Property condicionPago As String

    Public Property productos As List(Of Producto)
    Public Property imptos As List(Of Tax)
End Class
