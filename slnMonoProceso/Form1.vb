Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.ServiceModel
Imports System.Text
Imports System.Xml

Public Class Form1


    Dim objDatos As New clsOperacionesSQL
    'Dim objCorreo As New clsCorreo
    'Dim objTarea As New clsTarea

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Function BuildXML(ByVal data As DataRow, ByVal fechaDesde As String, ByVal fechaHasta As String, ByVal FecVenc As String, ByVal IsChecked As Boolean) As String


        Try

            Dim AfiliadoTabla = objDatos.DataTablaFactura(data.Item("F_NUMERO").ToString)



            Dim xml As XDocument = New XDocument(New XDeclaration("1.0", "utf-8", ""))
#Region "DTE"
            Dim nodoRaiz As XElement = New XElement("DTE", New XAttribute("version", "1.0"))
            xml.Add(nodoRaiz)

#Region "Documento"

            Dim documento As XElement = New XElement("Documento", New XAttribute("ID", ""))
            nodoRaiz.Add(documento)

#Region "Encabezado"
            Dim encabezado As XElement = New XElement("Encabezado")
            documento.Add(encabezado)
#Region "idDoc"
            'idDoc
            Dim idDoc As XElement = New XElement("IdDoc")
            encabezado.Add(idDoc)
            Dim ambiente As XElement = New XElement("Ambiente", data.Item("f_ambiente").ToString)
            idDoc.Add(ambiente)
            Dim TipoServicio As XElement = New XElement("TipoServicio", data.Item("F_TipoServicio").ToString)
            idDoc.Add(TipoServicio)
            Dim Tipo As XElement = New XElement("Tipo", data.Item("f_tipo").ToString)
            idDoc.Add(Tipo)
            Dim Serie As XElement = New XElement("Serie", data.Item("F_Serie").ToString)
            idDoc.Add(Serie)
            Dim Numero As XElement = New XElement("Numero", data.Item("f_Numero").ToString)
            idDoc.Add(Numero)
            Dim NumeroInterno As XElement = New XElement("NumeroInterno", "")
            idDoc.Add(NumeroInterno)
            Dim FechaEmis As XElement = New XElement("FechaEmis", data.Item("F_FECHAEMIS").ToString)
            idDoc.Add(FechaEmis)
            Dim Establecimiento As XElement = New XElement("Establecimiento", data.Item("F_ESTABLECIMIENTO").ToString)
            idDoc.Add(Establecimiento)
            Dim PtoEmis As XElement = New XElement("PtoEmis", data.Item("F_PTOEMIS").ToString)
            idDoc.Add(PtoEmis)
            Dim MedioPago As XElement = New XElement("MedioPago", data.Item("F_MedioPago").ToString)
            idDoc.Add(MedioPago)
            Dim IDPago As XElement = New XElement("IDPago", "")
            idDoc.Add(IDPago)
            Dim PeriodoDesde As XElement = New XElement("PeriodoDesde", fechaDesde)
            idDoc.Add(PeriodoDesde)
            Dim PeriodoHasta As XElement = New XElement("PeriodoHasta", fechaHasta)
            idDoc.Add(PeriodoHasta)
            Dim TermPagoCdg As XElement = New XElement("TermPagoCdg", "")
            idDoc.Add(TermPagoCdg)
            Dim FechaVenc As XElement = New XElement("FechaVenc", FecVenc)
            idDoc.Add(FechaVenc)

            ''area
            'Dim Area As XElement = New XElement("Area")
            'idDoc.Add(Area)
            'Dim IdArea As XElement = New XElement("IdArea", "")
            'Area.Add(IdArea)
            'Dim IdRevision As XElement = New XElement("IdRevision", "")
            'Area.Add(IdRevision)
            'icoterms
            Dim Incoterms As XElement = New XElement("Incoterms")
            idDoc.Add(Incoterms)
            Dim CodIncoterms As XElement = New XElement("CodIncoterms", "")
            Incoterms.Add(CodIncoterms)
            Dim IncotermDs As XElement = New XElement("IncotermDs", "")
            Incoterms.Add(IncotermDs)

            Dim tipo_Negociacion
            If data.Item("F_FECHAEMIS").ToString().Contains(FecVenc) Then
                tipo_Negociacion = "1"
            Else
                tipo_Negociacion = "2"
            End If

            Dim TipoNegociacion As XElement = New XElement("TipoNegociacion", tipo_Negociacion)
            idDoc.Add(TipoNegociacion)
            Dim Plazo As XElement = New XElement("Plazo", "")
            idDoc.Add(Plazo)

#End Region

#Region "emisor"
            Dim Emisor As XElement = New XElement("Emisor")
            encabezado.Add(Emisor)
            Dim TipoContribuyente As XElement = New XElement("TipoContribuyente", data.Item("F_TIPOCONTRIBUYENTE").ToString)
            Emisor.Add(TipoContribuyente)
            Dim RegimenContable As XElement = New XElement("RegimenContable", data.Item("F_REGIMENCONTABLE").ToString)
            Emisor.Add(RegimenContable)
            Dim IDEmisor As XElement = New XElement("IDEmisor", data.Item("F_IDEMISOR").ToString)
            Emisor.Add(IDEmisor)
            Dim NmbEmisor As XElement = New XElement("NmbEmisor", data.Item("F_NMBEMISOR").ToString)
            Emisor.Add(NmbEmisor)

            'nombreEmisor
            Dim NombreEmisor As XElement = New XElement("NombreEmisor")
            Emisor.Add(NombreEmisor)
            Dim PrimerNombre As XElement = New XElement("PrimerNombre", data.Item("F_PRIMERNOMBRE").ToString)
            NombreEmisor.Add(PrimerNombre)

            'For index = 1 To 4
            Dim CodigoEmisor As XElement = New XElement("CodigoEmisor")
            Emisor.Add(CodigoEmisor)
            Dim TpoCdgIntEmisor As XElement = New XElement("TpoCdgIntEmisor", data.Item("TPOCDGINTEMISOR").ToString)
            CodigoEmisor.Add(TpoCdgIntEmisor)
            Dim CdgIntEmisor As XElement = New XElement("CdgIntEmisor", data.Item("f_CdgIntEmisor").ToString)
            CodigoEmisor.Add(CdgIntEmisor)
            'Next

            Dim CdgSucursal As XElement = New XElement("CdgSucursal", data.Item("F_CDGSUCURSAL").ToString)
            Emisor.Add(CdgSucursal)

            'domFiscal
            Dim DomFiscal As XElement = New XElement("DomFiscal")
            Emisor.Add(DomFiscal)
            Dim Calle As XElement = New XElement("Calle", data.Item("F_Calle").ToString)
            DomFiscal.Add(Calle)
            Dim Departamento As XElement = New XElement("Departamento", data.Item("F_Dpto").ToString)
            DomFiscal.Add(Departamento)
            Dim Ciudad As XElement = New XElement("Ciudad", data.Item("F_Ciudad").ToString)
            DomFiscal.Add(Ciudad)
            Dim Pais As XElement = New XElement("Pais", data.Item("F_Pais").ToString)
            DomFiscal.Add(Pais)
            Dim CodigoPostal As XElement = New XElement("CodigoPostal", data.Item("F_CODIGOPOSTALFISCAL").ToString)
            DomFiscal.Add(CodigoPostal)

            'lugarExpend
            Dim LugarExped As XElement = New XElement("LugarExped") 'no esta en la consulta
            Emisor.Add(LugarExped)
            Calle = New XElement("Calle", data.Item("F_Calle").ToString)
            LugarExped.Add(Calle)
            Departamento = New XElement("Departamento", data.Item("F_Dpto").ToString)
            LugarExped.Add(Departamento)
            Ciudad = New XElement("Ciudad", data.Item("F_Ciudad").ToString)
            LugarExped.Add(Ciudad)
            Pais = New XElement("Pais", data.Item("F_Pais").ToString)
            LugarExped.Add(Pais)
            CodigoPostal = New XElement("CodigoPostal", data.Item("F_CODIGOPOSTALFISCAL").ToString)
            LugarExped.Add(CodigoPostal)

            'contactoEmisor
            Dim ContactoEmisor As XElement = New XElement("ContactoEmisor")
            Emisor.Add(ContactoEmisor)
            Tipo = New XElement("Tipo", data.Item("f_TipoNegociacion").ToString)
            ContactoEmisor.Add(Tipo)
            Dim Nombre As XElement = New XElement("Nombre", data.Item("F_Nmbemisor").ToString)
            ContactoEmisor.Add(Nombre)
            Dim Descripcion As XElement = New XElement("F_Primernombre", "")
            ContactoEmisor.Add(Descripcion)
            Dim eMail As XElement = New XElement("eMail", data.Item("eMail").ToString)
            ContactoEmisor.Add(eMail)
            Dim Telefono As XElement = New XElement("Telefono", data.Item("Telefono").ToString)
            ContactoEmisor.Add(Telefono)
            Dim Fax As XElement = New XElement("Fax", data.Item("Fax").ToString)
            ContactoEmisor.Add(Fax)
#End Region

#Region "Receptor"
            Dim Receptor As XElement = New XElement("Receptor")
            encabezado.Add(Receptor)
            Dim RegimenContableR As XElement = New XElement("RegimenContableR", data.Item("F_REGIMENCONTABLER").ToString)
            Receptor.Add(RegimenContableR)
            Dim TipoContribuyenteR As XElement = New XElement("TipoContribuyenteR", data.Item("F_TIPOCONTRIBUYENTER").ToString)
            Receptor.Add(TipoContribuyenteR)

            'DocRecep
            Dim DocRecep As XElement = New XElement("DocRecep")
            Receptor.Add(DocRecep)
            Dim TipoDocRecep As XElement = New XElement("TipoDocRecep", data.Item("f_TipoDocRecep").ToString)
            DocRecep.Add(TipoDocRecep)
            Dim NroDocRecep As XElement = New XElement("NroDocRecep", data.Item("f_NroDocRecep").ToString)
            DocRecep.Add(NroDocRecep)

            Dim NmbRecep As XElement = New XElement("NmbRecep", data.Item("f_NmbRecep").ToString)
            Receptor.Add(NmbRecep)

            'NombreRecep
            Dim NombreRecep As XElement = New XElement("NombreRecep")
            Receptor.Add(NombreRecep)
            PrimerNombre = New XElement("PrimerNombre", data.Item("f_PrimerNombre").ToString)
            NombreRecep.Add(PrimerNombre)

            'CodigoReceptor
            Dim CodigoReceptor As XElement = New XElement("CodigoReceptor")
            Receptor.Add(CodigoReceptor)
            Dim TpoCdgIntRecep As XElement = New XElement("TpoCdgIntRecep", data.Item("f_TpoCdgIntRecep").ToString)
            CodigoReceptor.Add(TpoCdgIntRecep)
            Dim CdgIntRecep As XElement = New XElement("CdgIntRecep", data.Item("f_CdgIntRecep").ToString)
            CodigoReceptor.Add(CdgIntRecep)

            CdgSucursal = New XElement("CdgSucursal", " ")
            Receptor.Add(CdgSucursal)

            'DomFiscalRcp
            Dim DomFiscalRcp As XElement = New XElement("DomFiscalRcp")
            Receptor.Add(DomFiscalRcp)
            Calle = New XElement("Calle", data.Item("f_DomFiscalRcpCalle").ToString)
            DomFiscalRcp.Add(Calle)
            Departamento = New XElement("Departamento", data.Item("f_DomFiscalRcpDpto").ToString)
            DomFiscalRcp.Add(Departamento)
            Ciudad = New XElement("Ciudad", data.Item("f_DomFiscalRcpCiu").ToString)
            DomFiscalRcp.Add(Ciudad)
            Pais = New XElement("Pais", data.Item("f_DomFiscalRcpPais").ToString)
            DomFiscalRcp.Add(Pais)
            CodigoPostal = New XElement("CodigoPostal", data.Item("f_DomFiscalRcpCodPostal").ToString)
            DomFiscalRcp.Add(CodigoPostal)

            'LugarRecep
            Dim LugarRecep As XElement = New XElement("LugarRecep") 'no esta en la consulta
            Receptor.Add(LugarRecep)
            Calle = New XElement("Calle", data.Item("f_DomFiscalRcpCalle").ToString)
            LugarRecep.Add(Calle)
            Departamento = New XElement("Departamento", data.Item("f_DomFiscalRcpDpto").ToString)
            LugarRecep.Add(Departamento)
            Ciudad = New XElement("Ciudad", data.Item("f_DomFiscalRcpCiu").ToString)
            LugarRecep.Add(Ciudad)
            Pais = New XElement("Pais", data.Item("f_DomFiscalRcpPais").ToString)
            LugarRecep.Add(Pais)
            CodigoPostal = New XElement("CodigoPostal", data.Item("f_DomFiscalRcpCodPostal").ToString)
            LugarRecep.Add(CodigoPostal)

            'ContactoReceptor
            Dim ContactoReceptor As XElement = New XElement("ContactoReceptor") 'no esta en consulta
            Receptor.Add(ContactoReceptor)
            Tipo = New XElement("Tipo", data.Item("f_ContactoReceptorTipo").ToString)
            ContactoReceptor.Add(Tipo)
            Nombre = New XElement("Nombre", data.Item("f_ContactoReceptorCont").ToString)
            ContactoReceptor.Add(Nombre)
            Descripcion = New XElement("Descripcion", data.Item("f_ContactoReceptorDesc").ToString)
            ContactoReceptor.Add(Descripcion)
            eMail = New XElement("eMail", data.Item("f_ContactoReceptorEmail").ToString)
            ContactoReceptor.Add(eMail)
            Telefono = New XElement("Telefono", data.Item("f_ContactoReceptorTel").ToString)
            ContactoReceptor.Add(Telefono)
            Fax = New XElement("Fax", data.Item("f_ContactoReceptorFax").ToString)
            ContactoReceptor.Add(Fax)

#End Region

#Region "Transporte"
            'Dim Transporte As XElement = New XElement("Transporte")
            'encabezado.Add(Transporte)

            ''DocTransp
            'Dim DocTransp As XElement = New XElement("DocTransp")
            'Transporte.Add(DocTransp)
            'Dim TipoDocTransp As XElement = New XElement("TipoDocTransp", "")
            'DocTransp.Add(TipoDocTransp)
            'Dim NroDocTransp As XElement = New XElement("NroDocTransp", "")
            'DocTransp.Add(NroDocTransp)

            'Dim NmbTransp As XElement = New XElement("NmbTransp")
            'Transporte.Add(NmbTransp)

            ''CodigoTransp ?
            ''For index = 1 To 3
            'Dim CodigoTransp As XElement = New XElement("CodigoTransp")
            'Transporte.Add(CodigoTransp)
            'Dim TpoCdgIntTransp As XElement = New XElement("TpoCdgIntTransp", "")
            'CodigoTransp.Add(TpoCdgIntTransp)
            'Dim CdgIntTransp As XElement = New XElement("CdgIntTransp", "")
            'CodigoTransp.Add(CdgIntTransp)
            ''Next

            ''DomFiscalTransp
            'Dim DomFiscalTransp As XElement = New XElement("DomFiscalTransp")
            'Transporte.Add(DomFiscalTransp)
            'Dim CalleDFT As XElement = New XElement("CalleDFT", "")
            'DomFiscalTransp.Add(CalleDFT)
            'Dim DepartamentoDFT As XElement = New XElement("DepartamentoDFT", "")
            'DomFiscalTransp.Add(DepartamentoDFT)
            'Dim CiudadDFT As XElement = New XElement("CiudadDFT", "")
            'DomFiscalTransp.Add(CiudadDFT)
            'Dim PaisDFT As XElement = New XElement("PaisDFT", "")
            'DomFiscalTransp.Add(PaisDFT)
            'Dim CodigoPostalDFT As XElement = New XElement("CodigoPostalDFT", "")
            'DomFiscalTransp.Add(CodigoPostalDFT)

            ''ContactoTransp
            'Dim ContactoTransp As XElement = New XElement("ContactoTransp")
            'Transporte.Add(ContactoTransp)
            'Dim TipoCT As XElement = New XElement("TipoCT", "")
            'ContactoTransp.Add(TipoCT)
            'Dim NombreCT As XElement = New XElement("NombreCT", "")
            'ContactoTransp.Add(NombreCT)
            'Dim DescripcionCT As XElement = New XElement("DescripcionCT", "")
            'ContactoTransp.Add(DescripcionCT)
            'Dim eMailCT As XElement = New XElement("eMailCT", "")
            'ContactoTransp.Add(eMailCT)
            'Dim TelefonoCT As XElement = New XElement("TelefonoCT", "")
            'ContactoTransp.Add(TelefonoCT)
            'Dim FaxCT As XElement = New XElement("FaxCT", "")
            'ContactoTransp.Add(FaxCT)

#End Region

#Region "Totales"
            Dim Totales As XElement = New XElement("Totales")
            encabezado.Add(Totales)
            Dim Moneda As XElement = New XElement("Moneda", data.Item("F_MONEDA").ToString)
            Totales.Add(Moneda)
            Dim FctConv As XElement = New XElement("FctConv", data.Item("F_TASACONVER").ToString)
            Totales.Add(FctConv)
            Dim SubTotal As XElement = New XElement("SubTotal", data.Item("F_SUBTOTAL").ToString)
            Totales.Add(SubTotal)
            Dim MntBase As XElement = New XElement("MntBase", data.Item("F_MNTBASE").ToString)
            Totales.Add(MntBase)
            Dim MntImp As XElement = New XElement("MntImp", data.Item("F_MNTIMP").ToString)
            Totales.Add(MntImp)
            Dim VlrPagar As XElement = New XElement("VlrPagar", data.Item("F_VLRPAGAR").ToString)
            Totales.Add(VlrPagar)
            Dim VlrPalabras As XElement = New XElement("VlrPalabras", data.Item("F_Vlrpalabras").ToString)
            Totales.Add(VlrPalabras)
#End Region

#Region "Impuestos"
            'For index = 1 To 4
            Dim Impuestos As XElement = New XElement("Impuestos")
            encabezado.Add(Impuestos)
            Dim TipoImp As XElement = New XElement("TipoImp", data.Item("F_Tipoimp").ToString)
            Impuestos.Add(TipoImp)
            Dim TasaImp As XElement = New XElement("TasaImp", data.Item("F_TASAIMP").ToString)
            Impuestos.Add(TasaImp)
            Dim MontoBAseImp As XElement = New XElement("MontoBAseImp", data.Item("F_MONTOBASEIMP").ToString)
            Impuestos.Add(MontoBAseImp)
            Dim MontoImp As XElement = New XElement("MontoImp", data.Item("F_MONTOIMP").ToString)
            Impuestos.Add(MontoImp)
            'Next
#End Region

#End Region



#Region "Detalle"
            Dim Details = objDatos.DataDetalleFacturaXML(data.Item("F_NUMERO").ToString, data.Item("tipo_docto").ToString)
            If Details.Tables(0).Rows.Count > 0 Then
                For Each item As DataRow In Details.Tables(0).Rows

                    Dim Detalle As XElement = New XElement("Detalle")
                    documento.Add(Detalle)
                    Dim NroLinDet As XElement = New XElement("NroLinDet", item.Item("NroLin").ToString)
                    Detalle.Add(NroLinDet)

                    'CdgItem
                    'For index2 = 1 To 2
                    Dim CdgItem As XElement = New XElement("CdgItem")
                    Detalle.Add(CdgItem)
                    Dim TpoCodigo As XElement = New XElement("TpoCodigo", item.Item("f_TpoCodigo").ToString)
                    CdgItem.Add(TpoCodigo)
                    Dim VlrCodigo As XElement = New XElement("VlrCodigo", item.Item("f_VlrCodigo").ToString)
                    CdgItem.Add(VlrCodigo)
                    'Next

                    Dim DscItem As XElement = New XElement("DscItem", item.Item("f_DscItem").ToString)
                    Detalle.Add(DscItem)
                    Dim QtyItem As XElement = New XElement("QtyItem", item.Item("f_QtyItem").ToString)
                    Detalle.Add(QtyItem)
                    Dim UnmdItem As XElement = New XElement("UnmdItem", item.Item("f_UnmdItem").ToString)
                    Detalle.Add(UnmdItem)
                    Dim PrcBrutoItem As XElement = New XElement("PrcBrutoItem", item.Item("f_PrcBrutoItem").ToString)
                    Detalle.Add(PrcBrutoItem)
                    Dim PrcNetoItem As XElement = New XElement("PrcNetoItem", item.Item("f_PrcNetoItem").ToString)
                    Detalle.Add(PrcNetoItem)

                    'cambio con logica especial 20201127
                    If IsChecked Then
                        Dim porcitem As XElement = New XElement("porcitem", item.Item("porcentaje_aplicar_tabla").ToString)
                        Detalle.Add(porcitem)
                    Else
                        Dim porcitem As XElement = New XElement("porcitem", item.Item("porcentaje_aplicar").ToString)
                        Detalle.Add(porcitem)
                    End If

                    'SubDscto
                    Dim SubDscto As XElement = New XElement("SubDscto") 'PREGUNTAR A CLIENTE
                    Detalle.Add(SubDscto)
                    Dim TipoDscto As XElement = New XElement("TipoDscto", " ")
                    SubDscto.Add(TipoDscto)
                    Dim GlosaDscto As XElement = New XElement("GlosaDscto", " ")
                    SubDscto.Add(GlosaDscto)
                    Dim PctDscto As XElement = New XElement("PctDscto", " ")
                    SubDscto.Add(PctDscto)
                    Dim MntDscto As XElement = New XElement("MntDscto", " ")
                    SubDscto.Add(MntDscto)

                    'ImpuestosDet
                    Dim ImpuestosDet As XElement = New XElement("ImpuestosDet")
                    Detalle.Add(ImpuestosDet)
                    TipoImp = New XElement("TipoImp", item.Item("TipoImp").ToString)
                    ImpuestosDet.Add(TipoImp)
                    TasaImp = New XElement("TasaImp", item.Item("f_TasaImp").ToString)
                    ImpuestosDet.Add(TasaImp)
                    MontoBAseImp = New XElement("MontoBaseImp", item.Item("f_MontoBaseImp").ToString)
                    ImpuestosDet.Add(MontoBAseImp)
                    MontoImp = New XElement("MontoImp", item.Item("f_MontoImp").ToString)
                    ImpuestosDet.Add(MontoImp)

                    Dim MontoTotalItem As XElement = New XElement("MontoTotalItem", item.Item("f_MontoTotalItem").ToString)
                    Detalle.Add(MontoTotalItem)
                    'Dim NumeroRef As XElement = New XElement("NumeroRef", "")
                    'Detalle.Add(NumeroRef)

                    'LocalItem
                    Dim LocalItem As XElement = New XElement("LocalItem")
                    Detalle.Add(LocalItem)
                    Dim TipoLoc As XElement = New XElement("TipoLoc", " ")
                    LocalItem.Add(TipoLoc)
                    Dim CodigoLoc As XElement = New XElement("CodigoLoc", " ")
                    LocalItem.Add(CodigoLoc)

                Next
            End If
#End Region



#Region "Referencia"

            Dim Referencia As XElement = New XElement("Referencia")
            Dim NroLinRef As XElement
            Dim TpoDocRef As XElement
            Dim NumeroRef As XElement
            If data.Item("NroLinRef").ToString <> "" Then
                documento.Add(Referencia)
                NroLinRef = New XElement("NroLinRef", data.Item("NroLinRef").ToString)
                Referencia.Add(NroLinRef)
                TpoDocRef = New XElement("TpoDocRef", data.Item("TpoDocRef").ToString)
                Referencia.Add(TpoDocRef)
                Dim SerieRef As XElement = New XElement("SerieRef", data.Item("SerieRef").ToString)
                Referencia.Add(SerieRef)
                NumeroRef = New XElement("NumeroRef", data.Item("NumeroRef").ToString)
                Referencia.Add(NumeroRef)
                Dim FechaRef As XElement = New XElement("FechaRef", data.Item("FechaRef").ToString)
                Referencia.Add(FechaRef)
                Dim CodRef As XElement = New XElement("CodRef", data.Item("CodRef").ToString)
                Referencia.Add(CodRef)
                Dim RazonRef As XElement = New XElement("RazonRef", data.Item("RazonRef").ToString)
                Referencia.Add(RazonRef)
                Dim ECB01 As XElement = New XElement("ECB01", data.Item("ECB01").ToString)
                Referencia.Add(ECB01)
            End If

            'cambio con logica especial 20201127
            Dim DetailsRef
            If IsChecked Then
                DetailsRef = objDatos.DataDetalleReferenciaXML(AfiliadoTabla.Tables(0).Rows(0).Item("AFILIADO").ToString)
            Else
                DetailsRef = objDatos.DataDetalleReferenciaXML(data.Item("f_codigo_id").ToString)
            End If
            Dim Indice = 1

            If DetailsRef.Tables(0).Rows.Count > 0 Then
                For Each item As DataRow In DetailsRef.Tables(0).Rows
                    Referencia = New XElement("Referencia")
                    documento.Add(Referencia)
                    NroLinRef = New XElement("NroLinRef", Indice)
                    Referencia.Add(NroLinRef)
                    TpoDocRef = New XElement("TpoDocRef", "90")
                    Referencia.Add(TpoDocRef)
                    NumeroRef = New XElement("NumeroRef", Indice)
                    Referencia.Add(NumeroRef)
                    Dim ECB02 As XElement = New XElement("ECB02", item.Item("Aprobacion").ToString)
                    Referencia.Add(ECB02)
                    Dim ECB03 As XElement = New XElement("ECB03", item.Item("Fecha_Consulta").ToString)
                    Referencia.Add(ECB03)
                    Dim ECB04 As XElement = New XElement("ECB04", item.Item("Codigo_Banco").ToString)
                    Referencia.Add(ECB04)
                    Dim ECB05 As XElement = New XElement("ECB05", item.Item("Numero_Cheque").ToString)
                    Referencia.Add(ECB05)
                    Dim ECB06 As XElement = New XElement("ECB06", FormatCurrency(item.Item("Valor").ToString).Replace("$", ""))
                    Referencia.Add(ECB06)
                    Dim ECB07 As XElement = New XElement("ECB07", item.Item("Tipo").ToString) 'cambio
                    Referencia.Add(ECB07)
                    Indice = Indice + 1
                Next
            End If

#End Region

#Region "CAE"
            Dim CAE As XElement = New XElement("CAE")
            documento.Add(CAE)
            Tipo = New XElement("Tipo", data.Item("f_CAETipo").ToString)
            CAE.Add(Tipo)
            Serie = New XElement("Serie", data.Item("f_CAESerie").ToString)
            CAE.Add(Serie)
            Dim NumeroInicial As XElement = New XElement("NumeroInicial", data.Item("f_CAENumeroInicial").ToString)
            CAE.Add(NumeroInicial)
            Dim NumeroFinal As XElement = New XElement("NumeroFinal", data.Item("f_CAENumeroFinal").ToString)
            CAE.Add(NumeroFinal)
            Dim NroResolucion As XElement = New XElement("NroResolucion", data.Item("f_CAENroResolucion").ToString)
            CAE.Add(NroResolucion)
            Dim FechaResolucion As XElement = New XElement("FechaResolucion", data.Item("f_CAEFechaResolucion").ToString)
            CAE.Add(FechaResolucion)
            Dim ClaveTC As XElement = New XElement("ClaveTC", data.Item("f_CAEClaveTC").ToString)
            CAE.Add(ClaveTC)
            Plazo = New XElement("Plazo", data.Item("f_CAEPlazo").ToString)
            CAE.Add(Plazo)




#End Region

#End Region

#Region "Personalizados"
            Dim Personalizados As XElement = New XElement("Personalizados")
            nodoRaiz.Add(Personalizados)
            Dim DocPersonalizado As XElement = New XElement("DocPersonalizado")
            Personalizados.Add(DocPersonalizado)

            Dim campoString As XElement = New XElement("campoString", New XAttribute("name", "Notas"), data.Item("F_Notas").ToString.Replace(vbCrLf, "").Replace(vbLf, ""))
            DocPersonalizado.Add(campoString)

            campoString = New XElement("campoString", New XAttribute("name", "FechaDesde"), fechaDesde)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "FechaHasta"), fechaHasta)
            DocPersonalizado.Add(campoString)

            If IsChecked Then
                campoString = New XElement("campoString", New XAttribute("name", "f_codigo_id"), AfiliadoTabla.Tables(0).Rows(0).Item("AFILIADO").ToString)
            Else
                campoString = New XElement("campoString", New XAttribute("name", "f_codigo_id"), data.Item("f_codigo_id").ToString)
            End If
            DocPersonalizado.Add(campoString)

            'cambio con logica especial 20201127
            If IsChecked Then
                campoString = New XElement("campoString", New XAttribute("name", "f_saldo_ant"), data.Item("saldo_tabla").ToString)
            Else
                campoString = New XElement("campoString", New XAttribute("name", "f_saldo_ant"), data.Item("saldo").ToString)
            End If
            DocPersonalizado.Add(campoString)

            campoString = New XElement("campoString", New XAttribute("name", "f_cargos_mes"), data.Item("F_Vlrpagar").ToString)
            DocPersonalizado.Add(campoString)

            'cambio con logica especial 20201127
            If IsChecked Then
                campoString = New XElement("campoString", New XAttribute("name", "f_total"), Convert.ToString(data.Item("F_Vlrpagar") + data.Item("saldo_tabla")))
            Else
                campoString = New XElement("campoString", New XAttribute("name", "f_total"), Convert.ToString(data.Item("F_Vlrpagar") + data.Item("saldo")))
            End If
            DocPersonalizado.Add(campoString)

            'cambio con logica especial 20201127
            If IsChecked Then
                campoString = New XElement("campoString", New XAttribute("name", "f_barra"), "(415)7709998000094(8020)" + data.Item("F_Numero").ToString.PadLeft(10, "0") + "(3900)" + (data.Item("F_Vlrpagar") + data.Item("saldo_tabla")).ToString.PadLeft(10, "0"))
            Else
                campoString = New XElement("campoString", New XAttribute("name", "f_barra"), "(415)7709998000094(8020)" + data.Item("F_Numero").ToString.PadLeft(10, "0") + "(3900)" + (data.Item("F_Vlrpagar") + data.Item("saldo")).ToString.PadLeft(10, "0"))
            End If
            DocPersonalizado.Add(campoString)

            'cambio con logica especial 20201127
            Dim valorCheDia
            If IsChecked Then
                valorCheDia = objDatos.SumaCheque(AfiliadoTabla.Tables(0).Rows(0).Item("AFILIADO").ToString, "D")
            Else
                valorCheDia = objDatos.SumaCheque(data.Item("f_codigo_id").ToString, "D")
            End If

            'cambio con logica especial 20201127
            Dim valorChePos
            If IsChecked Then
                valorChePos = objDatos.SumaCheque(AfiliadoTabla.Tables(0).Rows(0).Item("AFILIADO").ToString, "P")
            Else
                valorChePos = objDatos.SumaCheque(data.Item("f_codigo_id").ToString, "P")
            End If


            campoString = New XElement("campoString", New XAttribute("name", "f_tot_cheque_dia"), valorCheDia.Item("valor").ToString)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "f_tot_cheque_pos"), valorChePos.Item("valor").ToString)
            DocPersonalizado.Add(campoString)

            'Dim ImpresionDetalle As XElement = New XElement("ImpresionDetalle")
            '    DocPersonalizado.Add(ImpresionDetalle)
            '    Dim PersonNroLinDet As XElement = New XElement("PersonNroLinDet", "")
            '    DocPersonalizado.Add(PersonNroLinDet)
            '    Dim DetPersonAFN_01 As XElement = New XElement("DetPersonAFN_01", "")
            '    DocPersonalizado.Add(DetPersonAFN_01)
            '    Dim DetPersonAFN_03 As XElement = New XElement("DetPersonAFN_03", "")
            '    DocPersonalizado.Add(DetPersonAFN_03)
            '    Dim DetPersonAFN_04 As XElement = New XElement("DetPersonAFN_04", "")
            '    DocPersonalizado.Add(DetPersonAFN_04)
            '    Dim DetPersonAFN_05 As XElement = New XElement("DetPersonAFN_05", "")
            '    DocPersonalizado.Add(DetPersonAFN_05)
            '    Dim DetPersonAFN_06 As XElement = New XElement("DetPersonAFN_06", "")
            '    DocPersonalizado.Add(DetPersonAFN_06)

#End Region
#End Region

            'MsgBox("va a guardar en X")
            xml.Save("C:\Users\Public\Documents\" & data.Item("tipo_docto").ToString & data.Item("f_Numero").ToString & ".xml")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        Dim desde = TxtDesde.Text
        Dim hasta = TxtHasta.Text
        Dim tipoDoc = TxtTipoDoc.Text.ToUpper()
        Dim CO = TxtCO.Text
        Dim FecDesde = DTPFechaDesde.Value.ToString("yyyy-MM-dd")
        Dim FecHasta = DTPFechaHasta.Value.ToString("yyyy-MM-dd")
        Dim FecVenc = DTPFechaVenc.Value.ToString("yyyy-MM-dd")
        Dim IsChecked = ChkIsChecked.Checked

        If desde > hasta Then
            MsgBox("El consecutivo desde debe ser menor que hasta...")
            Return
        End If

        btnGenerar.Text = "Generando XML"
        btnGenerar.Enabled = False
        TxtDesde.Enabled = False
        TxtHasta.Enabled = False
        TxtTipoDoc.Enabled = False
        TxtCO.Enabled = False
        ChkIsChecked.Enabled = False

        Try
            Dim factData As DataSet = objDatos.DataFacturaXML(desde, hasta, tipoDoc, CO)

            If factData.Tables(0).Rows.Count > 0 Then
                For Each item As DataRow In factData.Tables(0).Rows
                    Dim xml = BuildXML(item, FecDesde, FecHasta, FecVenc, IsChecked)
                Next
                MsgBox("Generacion Completa...", MsgBoxStyle.Information, "Generacion de XML")

            Else
                MsgBox("No hay informacion para generar del rango asignado...", MsgBoxStyle.Exclamation, "Generacion de XML")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
            btnGenerar.Text = "Generar"
            btnGenerar.Enabled = True
            TxtDesde.Enabled = True
            TxtHasta.Enabled = True
            TxtTipoDoc.Enabled = True
            TxtCO.Enabled = True
            ChkIsChecked.Enabled = True
        End Try


        btnGenerar.Text = "Generar"
        btnGenerar.Enabled = True
        TxtDesde.Enabled = True
        TxtHasta.Enabled = True
        TxtTipoDoc.Enabled = True
        TxtCO.Enabled = True
        ChkIsChecked.Enabled = True


    End Sub
End Class
