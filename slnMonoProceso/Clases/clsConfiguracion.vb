Imports System.Data.Odbc
Imports System.Data.SqlClient

Public Class clsConfiguracion

    'Oracle
    Public Property ODBC_CadenaConexion As String = My.Settings.strConexionODBC
    Public Property ODBC_Conexion As New OdbcConnection(ODBC_CadenaConexion)
    Public Property ODBC_Comando As New OdbcCommand
    Public Property ODBC_Adaptador As New OdbcDataAdapter


    'SQL
    Public Property sqlConexion As New SqlConnection(My.Settings.strConexionGT)

    Public Property sqlConexionIntermedia As New SqlConnection() 'My.Settings.strConexionIntermedia)
    Public Property sqlComando As SqlCommand = New SqlCommand
    Public Property sqlAdaptador As SqlDataAdapter = New SqlDataAdapter


    'Propiedades de correo
    Public Property EnviarNotificaciones As Boolean = True
    Public Property ServidorDeCorreo As String = "smtp.gmail.com"
    Public Property Puerto As String = "587"
    Public Property RequiereAutenticacion As Boolean = True
    Public Property SSL As Boolean = True
    Public Property CorreoRemitente As String = "GTIntegration4@gmail.com"
    Public Property UsuarioMail As String = "GTIntegration4"
    Public Property ClaveMail As String = "interfaces4217"
    Public Property CorreosNotificaciones As String = "rlopez@generictransfer.com"
    Public Property AdjuntarArchivoCorreo As String


    'Propiedades Multiproceso Hijo
    Public Property ProcesosParalelos As Integer
    Public Property NumFilasMultiProcesos As Integer
    Public Property RutaLog As String
    Public Property RutaPlanos As String



    ''' <summary>
    ''' Se usa en diferentes puntos del software para cargar los parametros del sistema, si se agrega una nueva variable es necesario modificar el codigo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        Dim ds As New DataSet

        Try
            sqlComando.Connection = sqlConexion
            sqlComando.CommandType = CommandType.StoredProcedure
            sqlComando.CommandText = "sp_Propiedades_Select"
            sqlAdaptador.SelectCommand = sqlComando
            sqlAdaptador.Fill(ds)


            For Each Parametro As DataRow In ds.Tables(0).Rows
                If Parametro.Item("nombrePropiedad").ToString = "ProcesosParalelos" Then
                    ProcesosParalelos = Parametro.Item("valorEntero")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "NumFilasMultiProcesos" Then
                    NumFilasMultiProcesos = Parametro.Item("valorEntero")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "RutaLog" Then
                    RutaLog = Parametro.Item("valorTexto1")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "RutaPlanos" Then
                    RutaPlanos = Parametro.Item("valorTexto1")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "EnviarNotificaciones" Then
                    EnviarNotificaciones = Parametro.Item("valorBooleano")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "ServidorDeCorreo" Then
                    ServidorDeCorreo = Parametro.Item("valorTexto1").ToString
                ElseIf Parametro.Item("nombrePropiedad").ToString = "Puerto" Then
                    Puerto = Parametro.Item("valorTexto1").ToString
                ElseIf Parametro.Item("nombrePropiedad").ToString = "RequiereAutenticacion" Then
                    RequiereAutenticacion = Parametro.Item("valorBooleano")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "SSL" Then
                    SSL = Parametro.Item("valorBooleano")
                ElseIf Parametro.Item("nombrePropiedad").ToString = "CorreoRemitente" Then
                    CorreoRemitente = Parametro.Item("valorTexto1").ToString
                ElseIf Parametro.Item("nombrePropiedad").ToString = "UsuarioMail" Then
                    UsuarioMail = Parametro.Item("valorTexto1").ToString
                ElseIf Parametro.Item("nombrePropiedad").ToString = "ClaveMail" Then
                    ClaveMail = Parametro.Item("valorTexto1").ToString
                ElseIf Parametro.Item("nombrePropiedad").ToString = "CorreosNotificaciones" Then
                    CorreosNotificaciones = Parametro.Item("valorTexto1").ToString
                ElseIf Parametro.Item("nombrePropiedad").ToString = "AdjuntarArchivoCorreo" Then
                    AdjuntarArchivoCorreo = Parametro.Item("valorTexto1").ToString
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            sqlComando.Parameters.Clear()
            sqlComando.Connection.Close()
        End Try

    End Sub


End Class