<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.TxtDesde = New System.Windows.Forms.TextBox()
        Me.TxtHasta = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtTipoDoc = New System.Windows.Forms.TextBox()
        Me.TxtCO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DTPFechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DTPFechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DTPFechaVenc = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ChkIsChecked = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnGenerar
        '
        Me.btnGenerar.Location = New System.Drawing.Point(26, 280)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(154, 23)
        Me.btnGenerar.TabIndex = 0
        Me.btnGenerar.Text = "Generar"
        Me.btnGenerar.UseVisualStyleBackColor = True
        '
        'TxtDesde
        '
        Me.TxtDesde.Location = New System.Drawing.Point(26, 135)
        Me.TxtDesde.Name = "TxtDesde"
        Me.TxtDesde.Size = New System.Drawing.Size(86, 20)
        Me.TxtDesde.TabIndex = 1
        '
        'TxtHasta
        '
        Me.TxtHasta.Location = New System.Drawing.Point(141, 135)
        Me.TxtHasta.Name = "TxtHasta"
        Me.TxtHasta.Size = New System.Drawing.Size(100, 20)
        Me.TxtHasta.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(202, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Generacion de Facturas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(138, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Hasta:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Desde:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Tipo Documento:"
        '
        'TxtTipoDoc
        '
        Me.TxtTipoDoc.Location = New System.Drawing.Point(141, 42)
        Me.TxtTipoDoc.Name = "TxtTipoDoc"
        Me.TxtTipoDoc.Size = New System.Drawing.Size(100, 20)
        Me.TxtTipoDoc.TabIndex = 7
        '
        'TxtCO
        '
        Me.TxtCO.Location = New System.Drawing.Point(141, 80)
        Me.TxtCO.Name = "TxtCO"
        Me.TxtCO.Size = New System.Drawing.Size(100, 20)
        Me.TxtCO.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Centro Operacion:"
        '
        'DTPFechaDesde
        '
        Me.DTPFechaDesde.Location = New System.Drawing.Point(26, 191)
        Me.DTPFechaDesde.Name = "DTPFechaDesde"
        Me.DTPFechaDesde.Size = New System.Drawing.Size(86, 20)
        Me.DTPFechaDesde.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Fecha Desde:"
        '
        'DTPFechaHasta
        '
        Me.DTPFechaHasta.Location = New System.Drawing.Point(141, 190)
        Me.DTPFechaHasta.Name = "DTPFechaHasta"
        Me.DTPFechaHasta.Size = New System.Drawing.Size(100, 20)
        Me.DTPFechaHasta.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(138, 174)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Fecha Hasta:"
        '
        'DTPFechaVenc
        '
        Me.DTPFechaVenc.Location = New System.Drawing.Point(26, 244)
        Me.DTPFechaVenc.Name = "DTPFechaVenc"
        Me.DTPFechaVenc.Size = New System.Drawing.Size(215, 20)
        Me.DTPFechaVenc.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 228)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Fecha Vencimiento:"
        '
        'ChkIsChecked
        '
        Me.ChkIsChecked.AutoSize = True
        Me.ChkIsChecked.Location = New System.Drawing.Point(198, 282)
        Me.ChkIsChecked.Name = "ChkIsChecked"
        Me.ChkIsChecked.Size = New System.Drawing.Size(39, 17)
        Me.ChkIsChecked.TabIndex = 16
        Me.ChkIsChecked.Text = "FE"
        Me.ChkIsChecked.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkIsChecked.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(306, 332)
        Me.Controls.Add(Me.ChkIsChecked)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DTPFechaVenc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DTPFechaHasta)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DTPFechaDesde)
        Me.Controls.Add(Me.TxtCO)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtTipoDoc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtHasta)
        Me.Controls.Add(Me.TxtDesde)
        Me.Controls.Add(Me.btnGenerar)
        Me.Name = "Form1"
        Me.Text = "Generador XML Pruebas (v1.7.14.2021)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGenerar As Button
    Friend WithEvents TxtDesde As TextBox
    Friend WithEvents TxtHasta As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtTipoDoc As TextBox
    Friend WithEvents TxtCO As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents DTPFechaDesde As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents DTPFechaHasta As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DTPFechaVenc As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents ChkIsChecked As CheckBox
End Class
