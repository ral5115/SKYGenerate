Imports System.Net.Mail
Imports System.Net
Imports System.Text

Public Class clsCorreo
    Inherits clsConfiguracion

    Private nombreEmpresa As String = "Camara de Comercio de Cali"
    Private nombreProceso As String

    Public Function EnviarCorreoTarea(ByVal nombreProceso As String, ByVal CorreosDestinatarios As String, ByVal Mensaje As String) As String

        Try

            Me.nombreProceso = nombreProceso



            Dim smtp As New SmtpClient(ServidorDeCorreo)
            smtp.Port = Puerto
            smtp.EnableSsl = SSL
            smtp.Credentials = New NetworkCredential(UsuarioMail, ClaveMail)
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.Timeout = 30000

            Dim fromAddress As New MailAddress(CorreoRemitente, "Reporte Proceso Integracion: ")
            Dim subject As String = asuntoCorreo()
            Dim body As String = " <br> Mensaje: " & Mensaje

            'MsgBox(Mensaje)

            Dim message As New MailMessage()
            message.To.Add(CorreosDestinatarios)
            message.From = fromAddress
            message.Subject = subject
            message.Body = body
            message.IsBodyHtml = True

            smtp.Send(message)
            Return "Envio correcto"
        Catch ex As Exception
            Return "Error al enviar el correo : " & ex.Message
        End Try

    End Function


    Public Function EnviarCorreo(ByVal nombreProceso As String, ByVal Mensaje As String) As String

        Try

            Me.nombreProceso = nombreProceso

            Dim smtp As New SmtpClient(ServidorDeCorreo)
            smtp.Port = Puerto
            smtp.EnableSsl = SSL
            smtp.Credentials = New NetworkCredential(UsuarioMail, ClaveMail)
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.Timeout = 30000

            Dim fromAddress As New MailAddress(CorreoRemitente, "Reporte Proceso Integracion: ")
            Dim subject As String = asuntoCorreo()
            Dim body As String = " <br> Mensaje: " & Mensaje

            Dim message As New MailMessage()
            message.To.Add(CorreosNotificaciones)
            message.From = fromAddress
            message.Subject = subject
            message.Body = body
            message.IsBodyHtml = True

            smtp.Send(message)
            Return "Envio correcto"
        Catch ex As Exception
            Return "Error al enviar el correo : " & ex.Message
        End Try

    End Function


    'Public Function EnviarEmailAdjunto(ByVal pNombreRemitente As String,
    '                         ByVal pEmailRemitente As String,
    '                         ByVal pEmailDestinatario As String,
    '                         ByVal pAsunto As String,
    '                         ByVal pMensaje As String,
    '                         ByVal pAdjunto As String,
    '                         ByRef pError As String) As String


    '    Dim fromAddress As New MailAddress(My.Settings.MailUser, "Generic Transfer")
    '    Dim subject As String = pAsunto
    '    Dim body As String = pMensaje

    '    Dim smtp As New SmtpClient(My.Settings.smtp)
    '    smtp.Port = My.Settings.MailPuerto
    '    smtp.EnableSsl = False
    '    smtp.DeliveryMethod = SmtpDeliveryMethod.Network
    '    smtp.Credentials = New NetworkCredential(My.Settings.MailUser, My.Settings.MailPwd)
    '    smtp.Timeout = 200000

    '    Dim message As New MailMessage()
    '    message.To.Add(pEmailDestinatario)
    '    message.From = fromAddress
    '    message.Subject = subject
    '    message.Body = body
    '    message.IsBodyHtml = True
    '    Dim archivoAdjunto As Attachment = New Attachment(pAdjunto)
    '    message.Attachments.Add(archivoAdjunto)

    '    Try
    '        smtp.Send(message)
    '        pError = "ok"
    '        Return pError
    '    Catch ex As Exception
    '        pError = "ERROR: " & ex.Message
    '        Return pError
    '    End Try

    'End Function


    ''' <summary>
    ''' Estandar Asunto del Correos
    ''' </summary>
    ''' <returns></returns>
    Private Function asuntoCorreo() As String
        Return nombreEmpresa & ": " & Me.nombreProceso & " GTIntegration"
    End Function


    Public Function CuerpoCorreo(ByVal nombreProceso As String, ByVal Mensaje As String, ByVal dtDatos As DataView) As String

        Dim Cuerpo As New StringBuilder
        Me.nombreProceso = nombreProceso

        Try

            Cuerpo.Append("<div style=""font-family: Arial; font-size: 12px;"">")
            Cuerpo.Append("<div style=""font-size: 14px"">")
            Cuerpo.Append("<strong>" & Me.nombreProceso & " </strong> ")
            Cuerpo.Append("<br />")
            Cuerpo.Append("<em>PORTAL ERP Cipa")
            Cuerpo.Append("</em>")
            Cuerpo.Append("</div>")
            Cuerpo.Append("<hr />")

            Cuerpo.Append("<br />")
            Cuerpo.Append("<div style=""font-family: Arial; font-size: 16px;"">")
            Cuerpo.Append("<B>")
            Cuerpo.Append("Estado del Pedido : " & Mensaje)
            Cuerpo.Append("</B>")
            Cuerpo.Append("</div>")
            Cuerpo.Append("<br />")
            Cuerpo.Append("<br />")
            Cuerpo.Append("<strong>Detalle del pedido")
            Cuerpo.Append("</strong>")
            Cuerpo.Append("<br />")
            Cuerpo.Append("")
            Cuerpo.Append("<table border=""1"" >")
            Cuerpo.Append("<tr>")

            'Encabezados
            Cuerpo.Append("<tr>")

            Cuerpo.Append("<td bgcolor=""#CDE6D8"" style=""color: #03823B"">")
            Cuerpo.Append("Referencia</td>")

            Cuerpo.Append("<td align=""right"" bgcolor=""#CDE6D8""  style=""color: #03823B; text-align:right "">")
            Cuerpo.Append("Cantidad</td>")

            Cuerpo.Append("</tr>")

            'Detalle
            For Each dato As DataRow In dtDatos.ToTable.Rows

                Cuerpo.Append("<tr>")

                Cuerpo.Append("<td>")
                Cuerpo.Append(dato.Item("REFERENCIA").ToString)
                Cuerpo.Append("</td>")

                Cuerpo.Append("<td>")
                Cuerpo.Append(dato.Item("DESCRIPCION").ToString)
                Cuerpo.Append("</td>")

                Cuerpo.Append("</tr>")
            Next

            Cuerpo.Append("</table>")

            Cuerpo.Append("<br />")
            Cuerpo.Append("<br />")
            Cuerpo.Append("Observaciones:")
            Cuerpo.Append(Mensaje)
            Cuerpo.Append("</div>")


            Cuerpo.Append("<br />")
            Cuerpo.Append("<div>")
            Cuerpo.Append("Gracias por la solicitud de su pedido, para los detalles del despacho por favor comunicarse con facturación.")
            Cuerpo.Append("</div>")
            Cuerpo.Append("<br />")

            Return Cuerpo.ToString

        Catch ex As Exception
            Return "Error al crear el mensaje : " & ex.Message
        End Try

    End Function

End Class
