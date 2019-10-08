Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization

Public Class clsTarea
    Inherits clsConfiguracion

#Region "Propiedades"
    Dim _BaseDatos As String
    Dim _Tarea As Integer
    Dim _IdDocumento As Integer
    Dim _NombreDocumento As String
    Dim _EnviarNotificacion As Boolean
    Dim _InvocarWebService As Boolean
    Dim _NombreConexionWsSiesa As String
    Dim _CiaWsSiesa As String
    Dim _UsuarioWsSiesa As String
    Dim _ClaveWsSiesa As String
    Dim _RutaGeneracionPlano As String
    Dim _idLogPrincipal As Integer
    Dim _IdLog As Integer
    Dim _EnviarNotificaciones As Boolean
    Dim _Destinatarios As String



    Public Property BaseDatos() As String
        Get
            Return _BaseDatos
        End Get
        Set(ByVal Value As String)
            _BaseDatos = Value
        End Set
    End Property

    Public ReadOnly Property Destinatarios() As String
        Get
            Return _Destinatarios
        End Get
    End Property
    Public Property NombreDocumento() As String
        Get
            Return _NombreDocumento
        End Get
        Set(ByVal Value As String)
            _NombreDocumento = Value
        End Set
    End Property

    Public Property Tarea() As Integer
        Get
            Return _Tarea
        End Get
        Set(ByVal Value As Integer)
            _Tarea = Value
        End Set
    End Property

    Public ReadOnly Property IdDocumento() As Integer
        Get
            Return _IdDocumento
        End Get
    End Property

    Public ReadOnly Property InvocarWebService() As Boolean
        Get
            Return _InvocarWebService
        End Get
    End Property

    Public ReadOnly Property NombreConexionWsSiesa() As String
        Get
            Return _NombreConexionWsSiesa
        End Get
    End Property

    Public ReadOnly Property CiaWsSiesa() As String
        Get
            Return _CiaWsSiesa
        End Get
    End Property

    Public ReadOnly Property UsuarioWsSiesa() As String
        Get
            Return _UsuarioWsSiesa
        End Get
    End Property

    Public ReadOnly Property ClaveWsSiesa() As String
        Get
            Return _ClaveWsSiesa
        End Get
    End Property

    Public ReadOnly Property RutaGeneracionPlano() As String
        Get
            Return _RutaGeneracionPlano & "\"
        End Get
    End Property



    Public ReadOnly Property idLogPrincipal() As String
        Get
            Return _idLogPrincipal
        End Get
    End Property

    Public ReadOnly Property IdLog() As String
        Get
            Return _IdLog
        End Get
    End Property

    Public ReadOnly Property EnviarNotificaciones() As String
        Get
            Return _EnviarNotificaciones
        End Get
    End Property

#End Region

    Public Sub New()
        ' Put the Imports statements at the beginning of the code module

        ' Put the following code before InitializeComponent()
        ' Sets the culture to French (France)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        ' Sets the UI culture to French (France)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
    End Sub
    Public Function DatosOrigen(ByRef idTareaValido As Boolean) As DataSet

        Dim dsConfiguracionGT As New DataSet
        Dim dsFuenteDatos As New DataSet

        ConfiguracionTareaGT(dsConfiguracionGT, idTareaValido)

        'Se verifica que la tarea exista en la base de datos de GT
        If idTareaValido Then
            Select Case _BaseDatos
                Case "SQL"
                    DatosOrigenSQL(dsConfiguracionGT, dsFuenteDatos)
            End Select
        End If

        Return dsFuenteDatos

    End Function

    ''' <summary>
    ''' Recuperar la informacion de configuracion de la tarea 
    ''' </summary>
    ''' <param name="dsConfiguracionGT"></param>
    ''' <remarks></remarks>
    Private Sub ConfiguracionTareaGT(ByRef dsConfiguracionGT As DataSet, ByRef idTareaValido As Boolean)

        Dim objDA As New SqlDataAdapter
        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.StoredProcedure
        sqlComando.CommandText = "sp_TareaSeleccionar"
        sqlComando.Parameters.AddWithValue("@idTarea", _Tarea)
        objDA.SelectCommand = sqlComando

        Try

            objDA.Fill(dsConfiguracionGT)
            If dsConfiguracionGT.Tables(0).Rows.Count = 0 Then
                idTareaValido = False
                Exit Sub
            End If

            _IdDocumento = dsConfiguracionGT.Tables(0).Rows(0).Item("idDocumento")
            _NombreDocumento = dsConfiguracionGT.Tables(0).Rows(0).Item("Nombre")
            _EnviarNotificacion = dsConfiguracionGT.Tables(0).Rows(0).Item("EnviarNotificaciones")
            _InvocarWebService = dsConfiguracionGT.Tables(0).Rows(0).Item("InvocarWebService")
            _NombreConexionWsSiesa = dsConfiguracionGT.Tables(0).Rows(0).Item("NombreConexionWsSiesa").ToString
            _CiaWsSiesa = dsConfiguracionGT.Tables(0).Rows(0).Item("CiaWsSiesa").ToString
            _UsuarioWsSiesa = dsConfiguracionGT.Tables(0).Rows(0).Item("UsuarioWsSiesa").ToString
            _ClaveWsSiesa = dsConfiguracionGT.Tables(0).Rows(0).Item("ClaveWsSiesa").ToString
            _RutaGeneracionPlano = dsConfiguracionGT.Tables(0).Rows(0).Item("RutaGeneracionPlano")
            _RutaGeneracionPlano = dsConfiguracionGT.Tables(0).Rows(0).Item("RutaGeneracionPlano")
            _EnviarNotificaciones = dsConfiguracionGT.Tables(0).Rows(0).Item("EnviarNotificaciones")
            _Destinatarios = dsConfiguracionGT.Tables(0).Rows(0).Item("Destinatarios")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Recuperar la informacion de la fuente de datos para generar el plano
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DatosOrigenSQL(ByVal ConfiguracionGT As DataSet, ByRef FuenteDatos As DataSet)

        'MsgBox("Entro a ejecutar")

        Dim ObjConexionOrigen As New SqlConnection(ConfiguracionGT.Tables(0).Rows(0).Item("CadenaConexion"))

        Dim objComandoOrigen As New SqlCommand
        objComandoOrigen.Connection = ObjConexionOrigen
        Dim objDAOrigen As New SqlDataAdapter
        objDAOrigen.SelectCommand = objComandoOrigen
        objDAOrigen.SelectCommand.CommandTimeout = 99999

        Try

            For Each Seccion As DataRow In ConfiguracionGT.Tables(1).Rows

                Dim dsTmpDatosOrigen As New DataSet
                objComandoOrigen.CommandText = Seccion("Query")

                objDAOrigen.Fill(dsTmpDatosOrigen)
                dsTmpDatosOrigen.Tables(0).TableName = Seccion.Item("Descripcion")
                FuenteDatos.Tables.Add(dsTmpDatosOrigen.Tables(0).Copy)
            Next
        Catch ex As Exception
            Throw ex
            MsgBox(ex.Message.ToString & "Error")

        End Try
    End Sub

#Region "Log"

    Public Sub LogPrincipalAlmacenar()

        Dim dsConfiguracionGT As New DataSet
        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.StoredProcedure
        sqlComando.CommandText = "sp_LogPrincipalAlmacenar"

        sqlComando.Parameters.AddWithValue("idTarea", Tarea)

        Dim objParametro As New SqlParameter
        objParametro.ParameterName = "@idLogPrincipal"
        objParametro.Direction = ParameterDirection.InputOutput
        objParametro.Value = 0
        sqlComando.Parameters.Add(objParametro)

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
            _idLogPrincipal = sqlComando.Parameters.Item(1).Value
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try

    End Sub


    Public Sub LogInicio()

        Dim dsConfiguracionGT As New DataSet
        Dim sqlComando As New SqlCommand

        Dim idLog As Integer

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.StoredProcedure
        sqlComando.CommandText = "sp_LogInicio"

        sqlComando.Parameters.AddWithValue("@IdLogPrincipal", idLogPrincipal)
        sqlComando.Parameters.AddWithValue("@idTarea", _Tarea)
        Dim objParametro As New SqlParameter("@IdLog", idLog)
        objParametro.Direction = ParameterDirection.Output
        sqlComando.Parameters.Add(objParametro)
        sqlComando.Parameters.AddWithValue("@idProceso", 1)

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
            _IdLog = sqlComando.Parameters.Item(2).Value
        Catch ex As Exception
            MessageBox.Show("error " & ex.Message)
            Throw ex
        Finally
            sqlConexion.Close()
        End Try

    End Sub

    Public Sub LogFechaInicioRecuperacionDatosOrigen()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaInicioRecuperacionDatosOrigen = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogFechaFinRecuperacionDatosOrigen()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaFinRecuperacionDatosOrigen = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogRecuperacionDatosOrigen(ByVal Resultado As Integer)


        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set RecuperacionDatosOrigen = " & Resultado & " where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogFechaInicioGeneracionPlano()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaInicioGeneracionPlano = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogFechaFinGeneracionPlano()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaFinGeneracionPlano = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogGeneracionDePlano(ByVal Resultado As Integer)

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set GeneracionDePlano = " & Resultado & " where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogFechaInicioWebServiceSiesa()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaInicioWebServiceSiesa = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogFechaFinWebServiceSiesa()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaFinWebServiceSiesa = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogWebServiceSiesa(ByVal Resultado As Integer)

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set WebServiceSiesa = " & Resultado & " where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogEjecucionCompleta()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set EjecucionCompleta = 1 where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Sub LogFechaFin()

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set FechaFin = getdate() where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub


    Public Sub LogMensajesError(ByVal MensajeError As String)

        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.Text
        sqlComando.CommandText = "update TareaLog set MensajesError = '" & MensajeError.Replace("'", "") & "', EjecucionCompleta = 0 where idLog = " & _IdLog

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Sub

    Public Function ResultadoOperacion() As DataTable

        Dim dsConfiguracionGT As New DataSet
        Dim objDA As New SqlDataAdapter
        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.StoredProcedure
        sqlComando.CommandText = "sp_LogConsultar"
        sqlComando.Parameters.AddWithValue("idLog", _IdLog)
        objDA.SelectCommand = sqlComando

        Try
            objDA.Fill(dsConfiguracionGT)
            Return dsConfiguracionGT.Tables(0)
        Catch ex As Exception

        End Try


    End Function

    ''' <summary>
    ''' Actualiza el log Principal indicando la terminacion de los procesos
    ''' </summary>
    ''' <param name="procesosParalelos">Numero de procesos paralelos a realizar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LogFin(ByVal procesosParalelos As Integer) As Integer

        Dim dsConfiguracionGT As New DataSet
        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.StoredProcedure
        sqlComando.CommandText = "sp_LogFin"

        sqlComando.CommandTimeout = 60000
        sqlComando.Parameters.AddWithValue("@IdLogPrincipal", _idLogPrincipal)

        Dim objParametro As New SqlParameter
        objParametro.Direction = ParameterDirection.Output
        objParametro.Value = 0
        objParametro.ParameterName = "@Proceso"
        sqlComando.Parameters.Add(objParametro)


        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
            Return sqlComando.Parameters.Item(1).Value
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try
    End Function

    Public Sub LogDetalleAlmacenar(ByVal f_nro_linea As String, ByVal f_tipo_reg As String, ByVal f_sugtipo_reg As String,
                                        ByVal f_version As String, ByVal f_nivel As String, ByVal f_valor As String, ByVal f_detalle As String)

        Dim dsConfiguracionGT As New DataSet
        Dim sqlComando As New SqlCommand

        sqlComando.Connection = sqlConexion
        sqlComando.CommandType = CommandType.StoredProcedure
        sqlComando.CommandText = "sp_LogDetalleAlmacenar"

        sqlComando.Parameters.AddWithValue("@idLog", _IdLog)
        sqlComando.Parameters.AddWithValue("@f_nro_linea", f_nro_linea)
        sqlComando.Parameters.AddWithValue("@f_tipo_reg", f_tipo_reg)
        sqlComando.Parameters.AddWithValue("@f_subtipo_reg", f_sugtipo_reg)
        sqlComando.Parameters.AddWithValue("@f_version", f_version)
        sqlComando.Parameters.AddWithValue("@f_nivel", f_nivel)
        sqlComando.Parameters.AddWithValue("@f_valor", f_valor)
        sqlComando.Parameters.AddWithValue("@f_detalle", f_detalle)

        Try
            sqlConexion.Open()
            sqlComando.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
        End Try

    End Sub


#End Region

    Private Sub AlmacenarLog(ByVal Mensaje As String)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("C:\Users\interfaces\Documents\Log.txt", True)
        file.WriteLine(Mensaje)
        file.Close()

    End Sub
End Class