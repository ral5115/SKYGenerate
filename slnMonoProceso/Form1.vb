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

    Private Function BuildXML(ByVal data As DataRow, ByVal fechaDesde As String, ByVal fechaHasta As String, ByVal FecVenc As String) As String


        Try


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


            Dim DetailsRef = objDatos.DataDetalleReferenciaXML(data.Item("f_codigo_id").ToString)
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
                    Dim ECB06 As XElement = New XElement("ECB06", item.Item("Valor").ToString)
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
            Dim campoString As XElement = New XElement("campoString", New XAttribute("name", "Notas"), data.Item("F_Notas").ToString.Replace(vbCrLf, " "))
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "FechaDesde"), fechaDesde)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "FechaHasta"), fechaHasta)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "f_codigo_id"), data.Item("f_codigo_id").ToString)
            DocPersonalizado.Add(campoString)

            campoString = New XElement("campoString", New XAttribute("name", "f_saldo_ant"), data.Item("saldo").ToString)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "f_cargos_mes"), data.Item("F_Vlrpagar").ToString)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "f_total"), (data.Item("F_Vlrpagar") + data.Item("saldo")).ToString)
            DocPersonalizado.Add(campoString)
            campoString = New XElement("campoString", New XAttribute("name", "f_barra"), "(415)7709998000094(8020)" + data.Item("F_Numero").ToString.PadLeft(10, "0") + "(3900)" + (data.Item("F_Vlrpagar") + data.Item("saldo")).ToString.PadLeft(10, "0"))
            DocPersonalizado.Add(campoString)

            Dim valorCheDia = objDatos.SumaCheque(data.Item("f_codigo_id").ToString, "P")
            Dim valorChePos = objDatos.SumaCheque(data.Item("f_codigo_id").ToString, "D")

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

    Private Sub CargarWsAdquirientes(ByVal Adq As DataRow, ByVal tipo As String)
        Try
            'servicio
            Dim ws = New wsAdq.ServiciosAdquirienteClient
            ws.ClientCredentials.UserName.UserName = My.Settings.usuario_Certicamara
            ws.ClientCredentials.UserName.Password = My.Settings.pass_Certicamara
            Dim result
            'Objeto idAdquiriente
            Dim idAdquiriente = New wsAdq.identificacionAdquirienteWS With {
                .codigoDianSpecified = True,
                .numeroIdentificacion = Adq.Item("nit").ToString
            }


            Select Case Adq.Item("tipo_ident").ToString
                Case "C" 'Cedula de ciudadania
                    idAdquiriente.codigoDian = 13
                Case "E" 'Cedula Extranjeria
                    idAdquiriente.codigoDian = 22
                Case "N" 'nit
                    If Adq.Item("dv_nit").ToString <> "" Then
                        idAdquiriente.codigoDian = 31
                        idAdquiriente.digitoDeVerificacionSpecified = True
                        idAdquiriente.digitoDeVerificacion = Adq.Item("dv_nit")
                    Else
                        idAdquiriente.codigoDian = 13
                    End If

                Case "O" 'Otros
                    idAdquiriente.codigoDian = 13
                Case "T" 'Tarjeta de Identidad
                    idAdquiriente.codigoDian = 12
            End Select

            '-----------------Crear o actualizar
            If tipo = "C" Then
                'Objeto usuario
                Dim usuario = New wsAdq.usuarioAdquirienteWS With {
                    .contrasena = Adq.Item("nit").ToString,
                    .nombreUsuario = "CCC" + Adq.Item("nit").ToString,
                    .generarContrasenaSpecified = True,
                    .generarContrasena = False
                }

                'Objeto idAdquiriente
                Dim adquiriente = New wsAdq.adquirienteWS With {
                    .acuerdoFisicoFacturacionElectronicaSpecified = True,
                    .acuerdoFisicoFacturacionElectronica = False,
                    .adjuntarPdfNotificacionesSpecified = True,
                    .adjuntarPdfNotificaciones = True,
                    .adjuntarXmlNotificacionesSpecified = True,
                    .adjuntarXmlNotificaciones = True,
                    .apellidos = Adq.Item("apellidos").ToString,
                    .cantidadDiasAceptacionAutomatica = 3,
                    .codigoCiudad = Adq.Item("cod_ciudad").ToString,
                    .codigoDepartamento = Adq.Item("cod_depto").ToString,
                    .codigoPais = "CO",
                    .direccion = Adq.Item("direccion").ToString,
                    .emailPrincipal = Adq.Item("email").ToString,
                    .enviarCorreoCertificadoSpecified = True,
                    .enviarCorreoCertificado = False,
                    .enviarCorreoDeBienvenidaSpecified = True,
                    .enviarCorreoDeBienvenida = True,
                    .enviarFisicoSpecified = True,
                    .enviarFisico = False,
                    .enviarNotificacionesSpecified = True,
                    .enviarNotificaciones = True,
                    .naturalezaSpecified = 1,
                    .observaciones = "prueba en desarrollo",
                    .registradoEnCatalogoSpecified = True,
                    .registradoEnCatalogo = True,
                    .telefono = Adq.Item("telefono").ToString,
                    .identificacionAdquirienteWS = idAdquiriente
                }

                If Adq.Item("tipo").ToString = "JURIDICO" Then
                    adquiriente.naturaleza = 0
                    adquiriente.razonSocial = Adq.Item("razon_social").ToString
                Else
                    adquiriente.naturaleza = 1
                    adquiriente.nombre = Adq.Item("nombre").ToString
                End If

                Dim crearAdq As New wsAdq.crearAdquirienteConUsuarioACliente With {
                .adquiriente = adquiriente,
                .usuario = usuario
            }
                'extraer Xml del consumo
                Dim sw1 As New StringWriter
                Dim objRequestXML As New System.Xml.Serialization.XmlSerializer(crearAdq.GetType)
                objRequestXML.Serialize(sw1, crearAdq)
                Dim r = sw1.ToString

                'MsgBox("va a generar xml")
                'genero xml en una ruta
                Dim EscriturasKardex As New StreamWriter(My.Settings.ruta_xml_final.ToString & Adq.Item("nit").ToString & "-ADQ.txt")
                EscriturasKardex.WriteLine(r)
                EscriturasKardex.Close()


                result = ws.wsAdq_IServiciosAdquiriente_crearAdquirienteConUsuarioACliente(crearAdq)
            Else

                Dim email_sec(1) As String
                email_sec(0) = Adq.Item("email_sec").ToString




                Dim actualizarAdq As New wsAdq.adquirienteActualizaWS With {
            .apellidos = Adq.Item("apellidos").ToString,
            .codigoCiudad = Adq.Item("cod_ciudad").ToString,
            .codigoDepartamento = Adq.Item("cod_depto").ToString,
            .codigoPais = "CO",
            .direccion = Adq.Item("direccion").ToString,
            .emailPrincipal = Adq.Item("email").ToString,'.emailSecundarios = email_sec,
            .nombre = Adq.Item("nombre").ToString,
            .telefono = Adq.Item("telefono").ToString
            }
                Dim actAdq As New wsAdq.actualizarAdquirienteACliente With {
                .adquiriente = idAdquiriente,
                .informacionAdquiriente = actualizarAdq
            }

                'extraer Xml del consumo
                Dim sw1 As New StringWriter
                Dim objRequestXML As New System.Xml.Serialization.XmlSerializer(actAdq.GetType)
                objRequestXML.Serialize(sw1, actAdq)
                Dim r = sw1.ToString

                'MsgBox("va a generar xml")
                'genero xml en una ruta
                Dim EscriturasKardex As New StreamWriter(My.Settings.ruta_xml_final.ToString & Adq.Item("nit").ToString & "-ADQ.txt")
                EscriturasKardex.WriteLine(r)
                EscriturasKardex.Close()

                result = ws.wsAdq_IServiciosAdquiriente_actualizarAdquirienteACliente(actAdq)
            End If



            If result.return = "Se ha creado el adquiriente exitosamente" Then
                objDatos.LogAdquirientes("C", Adq.Item("nit").ToString)
            ElseIf result.return = "Se ha actualizado el Adquiriente exitosamente" Then
                objDatos.LogAdquirientes("A", Adq.Item("nit").ToString)
            End If

        Catch ex As Exception
            'MsgBox("Error al cargar a ws Certicamara: " + ex.Message.ToString)
            'objCorreo.EnviarCorreoTarea("Resultado Consumo Servicio Certifactura Adquirientes", objTarea.CorreosNotificaciones, "Error con Adq " + Adq.Item("nit").ToString + " " + ex.Message)
        End Try


    End Sub

    Private Sub CargarWsFacturas(ByVal facturaXML As FacturaXML, ByRef enviado As Int16)



        If (facturaXML.nroPedido <> "sin nro de pedido" And facturaXML.tipodocto = "FE") Or (facturaXML.nroPedido = "sin nro de pedido" And facturaXML.tipodocto = "FCE") Then
            'MsgBox("Entro a metodo factura")
            Try


                Dim ws = New wsFact.ServiciosEmitirFacturaElectronicaClient
                ws.ClientCredentials.UserName.UserName = My.Settings.usuario_Certicamara
                ws.ClientCredentials.UserName.Password = My.Settings.pass_Certicamara
                Dim result
                Dim codigoDian As Integer

                Dim digito = facturaXML.digito
                If facturaXML.digito.Contains("schemeID=""13""") Then
                    codigoDian = 13
                Else
                    codigoDian = 31
                End If

                Dim receptor As New wsFact.identificacionClienteWS With {
                .codigoDocumentoDian = codigoDian,
                .codigoDocumentoDianSpecified = True,
                .numeroIdentificacion = facturaXML.identificacion
                }

                Dim numResol As New wsFact.numeracionResolucionWS With {
                .desde = facturaXML.desde,
                .hasta = facturaXML.hasta,
                .prefijo = facturaXML.prefijo
                }

                'productos
                Dim Productos(facturaXML.productos.Count - 1) As wsFact.productoCertifacturaWS
                For i = 0 To facturaXML.productos.Count - 1

                    Dim AtributosAdicionales(1) As wsFact.atributoAdicionalProdWS
                    AtributosAdicionales(0) = New wsFact.atributoAdicionalProdWS With {
                   .nombreAtributo = "porcentajeIva",
                    .valor = facturaXML.productos(i).porcentajeIva
                    }
                    AtributosAdicionales(1) = New wsFact.atributoAdicionalProdWS With {
                   .nombreAtributo = "descuento",
                    .valor = facturaXML.productos(i).descuento
                    }

                    Productos(i) = New wsFact.productoCertifacturaWS With {
                    .cantidad = facturaXML.productos(i).Cantidad,
                    .cantidadSpecified = True,
                    .descripcion = facturaXML.productos(i).Descipcion,
                    .identificador = facturaXML.productos(i).id,
                    .imprimible = True,
                    .imprimibleSpecified = True,
                    .pagable = True,
                    .pagableSpecified = True,
                    .valorUnitario = facturaXML.productos(i).ValBruto,
                    .valorUnitarioSpecified = True,
                    .atributosAdicionalesProd = AtributosAdicionales
                 }
                Next

                'impuestos
                Dim nombreImpto As String = ""
                Dim SubtotImpoDeduc(facturaXML.imptos.Count - 1) As wsFact.impuestoDeduccionWS
                For i = 0 To facturaXML.imptos.Count - 1
                    Select Case facturaXML.imptos(i).id.ToString
                        Case "01"
                            nombreImpto = "IVA"
                        Case "06"
                            nombreImpto = "ReteIVA"
                    End Select
                    SubtotImpoDeduc(i) = New wsFact.impuestoDeduccionWS With {
                    .baseGravableSpecified = True,
                   .baseGravable = facturaXML.imptos(i).Porcentaje,
                    .nombre = nombreImpto,
                    .valorSpecified = True,
                    .valor = facturaXML.imptos(i).Valor
                    }
                Next



                'factura final
                Dim factura = New wsFact.facturaElectronicaWS With {
                .identificacionReceptor = receptor,
                .codigoMoneda = facturaXML.codMoneda,
                .fechaExpedicion = facturaXML.fecExp,
                .fechaVencimiento = facturaXML.fecVen,
                .descripcion = facturaXML.observacion,
                .mediosPago = "10", 'fijo
                .identificadorResolucion = facturaXML.resolucion,
                .numeracionResolucionWS = numResol,
                .identificadorConsecutivo = facturaXML.consecutivo,
                .identificadorConsecutivoSpecified = True,
                .perfilEmisionSpecified = True,
                .perfilUsuarioSpecified = True,
                .perfilEmision = 0, 'fijo 0=cliente, 1=adquiriente 
                .perfilUsuario = 0,'fijo 0=cliente, 1=adquiriente 
                .productos = Productos,
                .subtotalesImpuestosDeduccion = SubtotImpoDeduc,
                .tipoFactura = "1", 'pendiente si se manejara el codigo 3
                .subtotalFactura = facturaXML.subTotalFact,
                .totalFactura = facturaXML.totalFact,
                .subtotalFacturaSpecified = True,
                .totalFacturaSpecified = True
                }

                If facturaXML.tipodocto = "FE" Then
                    factura.tipoFactura = "1"
                Else
                    factura.tipoFactura = "3"
                End If

                'Atributos adicionales pendiente definir de donde se toma
                Dim AtributosAdic(8) As wsFact.atributoAdicional

                AtributosAdic(0) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "vendedor",
                .valor = facturaXML.vendedor,
                .tipo = "Texto"
                }
                AtributosAdic(1) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "condicionPago",
                .valor = facturaXML.condicionPago,
                .tipo = "Texto"
                }
                AtributosAdic(2) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "usuarioContacto",
                .valor = facturaXML.usuarioContacto,
                .tipo = "Texto"
                }
                AtributosAdic(3) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "valorBruto",
                .valor = facturaXML.subTotalFact,
                .tipo = "Texto"
                }
                AtributosAdic(3).valor = AtributosAdic(3).valor.Replace(",", ".")

                AtributosAdic(4) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "usuario",
                .valor = facturaXML.usuario,
                .tipo = "Texto"
                }
                AtributosAdic(5) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "numeroPedido",
                .valor = facturaXML.nroPedido,
                .tipo = "Texto"
                }
                AtributosAdic(6) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "referenciaDocumento",
                .valor = facturaXML.referenciaDocumento,
                .tipo = "Texto"
                }
                AtributosAdic(7) = New wsFact.atributoAdicional With {
                   .nombreAtributo = "observacion",
                .valor = facturaXML.observacion,
                .tipo = "Texto"
                }


                'factura especializada
                Dim factEspecializada = New wsFact.documentoPersonalizado With {
                .AtributosAdicionales = AtributosAdic
                }

                Dim request As New wsFact.crearFacturaElectronica With {
            .facturaElectronicaCanonica = factura,
            .facturaEspecializada = factEspecializada
            }


                'extraer Xml del consumo
                Dim sw1 As New StringWriter
                Dim objRequestXML As New System.Xml.Serialization.XmlSerializer(request.GetType)
                objRequestXML.Serialize(sw1, request)
                Dim r = sw1.ToString

                'MsgBox("va a generar xml")
                'genero xml en una ruta
                Dim EscriturasKardex As New StreamWriter(My.Settings.ruta_xml_final.ToString & facturaXML.consecutivo.ToString & "F.txt")
                EscriturasKardex.WriteLine(r)
                EscriturasKardex.Close()


                'MsgBox("Entro a cargar factura ws" + facturaXML.consecutivo.ToString)
                result = ws.wsFact_IServiciosEmitirFacturaElectronica_crearFacturaElectronica(request)
                If result.return = "Se ha creado la factura exitosamente" Then
                    enviado = 1
                    'MsgBox(result.return)
                End If
            Catch ex As Exception
                'MsgBox(ex.Message)
                'objCorreo.EnviarCorreoTarea("Resultado Consumo Servicio Certifactura Facturas", objTarea.CorreosNotificaciones, "Error con Fact " + facturaXML.tipodocto.ToString + facturaXML.consecutivo.ToString + " " + ex.Message)

            End Try
        End If

    End Sub

    Private Sub CargarWsNotas(ByVal NotaXML As NotaXML, ByRef enviado As Int16)

        'MsgBox("Entro a cargar nota")

        Try


            Dim ws = New wsNota.ServiciosEmitirNotaCreditoElectronicaClient
            ws.ClientCredentials.UserName.UserName = My.Settings.usuario_Certicamara
            ws.ClientCredentials.UserName.Password = My.Settings.pass_Certicamara
            Dim result
            Dim codigoDian As Integer

            Dim digito = NotaXML.digito
            If NotaXML.digito = 2 Then
                codigoDian = 13
            Else
                codigoDian = 31
            End If

            Dim receptor As New wsNota.identificacionClienteWS With {
            .codigoDocumentoDian = codigoDian,
            .codigoDocumentoDianSpecified = True,
            .numeroIdentificacion = NotaXML.identificacion
            }



            'productos
            Dim Productos(NotaXML.productos.Count - 1) As wsNota.productoCertifacturaWS
            For i = 0 To NotaXML.productos.Count - 1

                Dim AtributosAdicionales(1) As wsNota.atributoAdicionalProdWS
                AtributosAdicionales(0) = New wsNota.atributoAdicionalProdWS With {
               .nombreAtributo = "porcentajeIva",
                .valor = NotaXML.productos(i).porcentajeIva
                }
                AtributosAdicionales(1) = New wsNota.atributoAdicionalProdWS With {
               .nombreAtributo = "descuento",
                .valor = NotaXML.productos(i).descuento
                }

                Productos(i) = New wsNota.productoCertifacturaWS With {
                .cantidad = NotaXML.productos(i).Cantidad,
                .cantidadSpecified = True,
                .descripcion = NotaXML.productos(i).Descipcion,
                .identificador = NotaXML.productos(i).id,
                .imprimible = True,
                .imprimibleSpecified = True,
                .pagable = True,
                .pagableSpecified = True,
                .valorUnitario = NotaXML.productos(i).ValBruto,
                .valorUnitarioSpecified = True,
                .atributosAdicionalesProd = AtributosAdicionales
             }
            Next

            'impuestos
            Dim nombreImpto As String = ""
            Dim SubtotImpoDeduc(NotaXML.imptos.Count - 1) As wsNota.impuestoDeduccionWS
            For i = 0 To NotaXML.imptos.Count - 1
                Select Case NotaXML.imptos(i).id.ToString
                    Case "01"
                        nombreImpto = "IVA"
                    Case "06"
                        nombreImpto = "ReteIVA"
                End Select
                SubtotImpoDeduc(i) = New wsNota.impuestoDeduccionWS With {
                .baseGravableSpecified = True,
               .baseGravable = NotaXML.imptos(i).Porcentaje,
                .nombre = nombreImpto,
                .valorSpecified = True,
                .valor = NotaXML.imptos(i).Valor
                }
            Next

            'nota final
            Dim nota = New wsNota.notaCreditoElectronicaWS With {
            .prefijoNota = NotaXML.prefijoNota,
            .identificacionReceptor = receptor,
            .codigoMoneda = NotaXML.codMoneda,
            .conceptoNota = "1",
            .fechaExpedicion = NotaXML.fecExp,
            .fechaVencimiento = NotaXML.fecExp,
            .numeroNota = NotaXML.consecutivo,
            .numeroNotaSpecified = True,
            .identificadorFactura = "FE_" + NotaXML.consecutivoFact + "_E",
            .observaciones = "",
            .perfilEmisionSpecified = True,
            .perfilUsuarioSpecified = True,
            .perfilEmision = 0, 'fijo 0=cliente, 1=adquiriente 
            .perfilUsuario = 0,'fijo 0=cliente, 1=adquiriente 
            .productos = Productos,
            .subtotalesImpuestosDeduccion = SubtotImpoDeduc,
            .tipoFactura = "ELECTRONICA",
            .subtotalNotaCreditoElectronica = NotaXML.subTotalFact,
            .totalNotaCreditoElectronica = NotaXML.totalFact,
            .subtotalNotaCreditoElectronicaSpecified = True,
            .totalNotaCreditoElectronicaSpecified = True
             }


            'Atributos adicionales pendiente definir de donde se toma
            Dim AtributosAdic(5) As wsNota.atributoAdicional
            AtributosAdic(0) = New wsNota.atributoAdicional With {
               .nombreAtributo = "motivo",
            .valor = NotaXML.motivo,
            .tipo = "Texto"
            }

            AtributosAdic(1) = New wsNota.atributoAdicional With {
               .nombreAtributo = "fechaFactura",
            .valor = NotaXML.fechaFactura,
            .tipo = "Texto"
            }
            AtributosAdic(2) = New wsNota.atributoAdicional With {
               .nombreAtributo = "valorBruto",
            .valor = NotaXML.subTotalFact,
            .tipo = "Texto"
            }
            AtributosAdic(2).valor = AtributosAdic(2).valor.Replace(",", ".")

            AtributosAdic(3) = New wsNota.atributoAdicional With {
               .nombreAtributo = "observacion",
            .valor = NotaXML.observacion,
            .tipo = "Texto"
            }
            AtributosAdic(4) = New wsNota.atributoAdicional With {
               .nombreAtributo = "usuario",
            .valor = NotaXML.usuario,
            .tipo = "Texto"
            }


            'nota especializada
            Dim notaEspecializada = New wsNota.documentoPersonalizado With {
            .AtributosAdicionales = AtributosAdic
            }

            Dim request As New wsNota.crearNotaCreditoElectronica With {
        .notaCreditoElectronicaCanonica = nota,
        .notaEspecializada = notaEspecializada
        }


            'extraer Xml del consumo
            Dim sw1 As New StringWriter
            Dim objRequestXML As New System.Xml.Serialization.XmlSerializer(request.GetType)
            objRequestXML.Serialize(sw1, request)
            Dim r = sw1.ToString


            'MsgBox("va a generar xml")
            'genero xml en una ruta
            Dim EscriturasKardex As New StreamWriter(My.Settings.ruta_xml_final.ToString & NotaXML.consecutivo.ToString & "N.txt")
            EscriturasKardex.WriteLine(r)
            EscriturasKardex.Close()

            'MsgBox("Va a enviar ws nota" + NotaXML.consecutivo.ToString)

            result = ws.wsNota_IServiciosEmitirNotaCreditoElectronica_crearNotaCreditoElectronica(request)

            If result.return = "Nota crédito electrónica emitida exitosamente" Then
                enviado = 1
                'MsgBox(result.return)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            'objCorreo.EnviarCorreoTarea("Resultado Consumo Servicio Certifactura Notas", objTarea.CorreosNotificaciones, "Error con Nota " + NotaXML.consecutivo.ToString + " " + ex.Message)
        End Try


    End Sub


    Private Function LeerXMLFactura(ByVal archivo As String) As FacturaXML

        'MsgBox("Entro a leer factura")

        Dim factura As New FacturaXML
        Dim documentoXml As XmlDocument
        Dim root As XmlElement
        documentoXml = New XmlDocument

        Try
            'lee documento
            documentoXml.Load(archivo)
            root = documentoXml.DocumentElement

            factura.tipodocto = root.GetElementsByTagName("siesa:DocumentType").Item(0).InnerText.Trim()
            'asigna valores tomados desde el xml
            factura.identificacion = root.GetElementsByTagName("cac:PartyIdentification").Item(1).InnerText.Trim()
            factura.digito = root.GetElementsByTagName("cac:PartyIdentification").Item(1).InnerXml.ToString
            factura.codMoneda = root.GetElementsByTagName("cbc:DocumentCurrencyCode").Item(0).InnerText.Trim()
            factura.fecExp = root.GetElementsByTagName("cbc:IssueDate").Item(0).InnerText.Trim().Replace("/", "-")
            factura.resolucion = root.GetElementsByTagName("sts:InvoiceAuthorization").Item(0).InnerText.Trim()
            factura.prefijo = root.GetElementsByTagName("sts:Prefix").Item(0).InnerText.Trim()
            factura.consecutivo = root.GetElementsByTagName("siesa:DocumentConsecutive").Item(0).InnerText.Trim()
            factura.desde = root.GetElementsByTagName("sts:From").Item(0).InnerText.Trim()
            factura.hasta = root.GetElementsByTagName("sts:To").Item(0).InnerText.Trim()

            'atributos adicionales
            factura.condicionPago = root.GetElementsByTagName("siesa:PaymentTerms").Item(0).FirstChild.InnerText.Trim()
            If root.GetElementsByTagName("cbc:Note").Item(0).HasChildNodes Then
                factura.observacion = root.GetElementsByTagName("cbc:Note").Item(0).FirstChild.InnerText.Trim()
            Else
                factura.observacion = ""
            End If

            If root.GetElementsByTagName("cac:OrderReference").Count > 0 Then
                factura.referenciaDocumento = root.GetElementsByTagName("cac:OrderReference").Item(0).FirstChild.InnerText.Trim()
            Else
                factura.referenciaDocumento = ""
            End If
            factura.usuarioContacto = root.GetElementsByTagName("cbc:Name").Item(5).InnerText.Trim()
            factura.usuario = root.GetElementsByTagName("cac:Contact").Item(0).FirstChild.InnerText.Trim()
            factura.vendedor = root.GetElementsByTagName("cac:SellerContact").Item(0).LastChild.InnerText.Trim()
            factura.fecVen = root.GetElementsByTagName("siesa:DuesDate").Item(0).InnerText.Trim().Replace("/", "-")

            'items
            Dim items As XmlNodeList = root.GetElementsByTagName("fe:InvoiceLine")
            factura.productos = New List(Of Producto)
            For Each item As XmlElement In items
                Dim producto As New Producto
                producto.id = item.GetElementsByTagName("cac:StandardItemIdentification").Item(0).FirstChild.InnerText.Trim
                producto.Cantidad = item.GetElementsByTagName("cbc:InvoicedQuantity").Item(0).InnerText.Trim
                'producto.ValBruto = item.GetElementsByTagName("cbc:LineExtensionAmount").Item(0).InnerText.Trim() '.Replace(".", ",")
                producto.ValBruto = item.GetElementsByTagName("cbc:PriceAmount").Item(0).InnerText.Trim()
                producto.Descipcion = item.GetElementsByTagName("cbc:Description").Item(0).InnerText.Trim

                If item.GetElementsByTagName("cbc:Percent").Count > 0 Then
                    producto.porcentajeIva = item.GetElementsByTagName("cbc:Percent").Item(0).InnerText.Trim
                Else
                    producto.porcentajeIva = "0"
                End If
                producto.descuento = item.GetElementsByTagName("cbc:Amount").Item(0).InnerText.Trim() '.Replace(".", ",")
                Dim descuentoFinal = (producto.descuento / producto.Cantidad)
                producto.ValBruto += descuentoFinal
                factura.productos.Add(producto)
            Next

            'impuestos
            Dim Taxes As XmlNodeList = root.GetElementsByTagName("fe:TaxTotal")
            factura.imptos = New List(Of Tax)
            For Each item As XmlElement In Taxes
                Dim tax As New Tax
                tax.id = item.GetElementsByTagName("cbc:ID").Item(0).InnerText.Trim
                tax.Porcentaje = item.GetElementsByTagName("cbc:Percent").Item(0).InnerText.Trim
                tax.Valor = item.FirstChild.InnerText.Trim() '.Replace(".", ",")
                factura.imptos.Add(tax)
            Next

            'valores totales factura
            factura.subTotalFact = root.GetElementsByTagName("cbc:TaxExclusiveAmount").Item(0).InnerText.Trim() '.Replace(".", ",")
            factura.totalFact = root.GetElementsByTagName("cbc:PayableAmount").Item(0).InnerText.Trim() '.Replace(".", ",")

            'numero de pedido tomados desde entidad dinamica de la factura, por consulta directa ala base de datos de siesa
            Dim nropedido = objDatos.NroPedidoFactura(factura.consecutivo)
            If nropedido.Tables("NroPedido").Rows.Count > 0 Then
                factura.nroPedido = nropedido.Tables("NroPedido").Rows(0)(0).ToString()
            Else
                factura.nroPedido = "sin nro de pedido"
                'MsgBox(factura.nroPedido)
            End If
        Catch ex As Exception
            'MsgBox("Error" + ex.Message)
            'respuestasAplicativo(ex.Message)
        End Try



        Return factura
    End Function

    Private Function LeerXMLNota(ByVal archivo As String) As NotaXML

        'MsgBox("Entro a leer nota")

        Dim nota As New NotaXML
        Dim documentoXml As XmlDocument
        Dim root As XmlElement
        documentoXml = New XmlDocument

        Try
            documentoXml.Load(archivo)

            root = documentoXml.DocumentElement

            nota.identificacion = root.GetElementsByTagName("cac:PartyIdentification").Item(1).InnerText.Trim()
            nota.digito = root.GetElementsByTagName("cbc:AdditionalAccountID").Item(1).InnerText.Trim()
            nota.codMoneda = root.GetElementsByTagName("cbc:DocumentCurrencyCode").Item(0).InnerText.Trim()
            nota.fecExp = root.GetElementsByTagName("cbc:IssueDate").Item(0).InnerText.Trim().Replace("/", "-")
            nota.consecutivo = root.GetElementsByTagName("siesa:DocumentConsecutive").Item(0).InnerText.Trim()
            nota.consecutivoFact = root.GetElementsByTagName("cac:InvoiceDocumentReference").Item(0).FirstChild.InnerText.Trim()

            'atributos adicionales
            nota.motivo = root.GetElementsByTagName("cac:DiscrepancyResponse").Item(0).LastChild.InnerText.Trim()
            nota.prefijoNota = root.GetElementsByTagName("siesa:Prefix").Item(0).InnerText.Trim()
            nota.fechaFactura = root.GetElementsByTagName("cbc:IssueDate").Item(1).InnerText.Trim()
            nota.observacion = root.GetElementsByTagName("cbc:Note").Item(0).FirstChild.InnerText.Trim()
            nota.usuario = root.GetElementsByTagName("cac:Contact").Item(0).FirstChild.InnerText.Trim()


            Dim items As XmlNodeList = root.GetElementsByTagName("siesa:InvoiceLine")
            nota.productos = New List(Of Producto)
            For Each item As XmlElement In items
                Dim producto As New Producto
                producto.id = item.GetElementsByTagName("siesa:StandardItemIdentification").Item(0).FirstChild.InnerText.Trim
                producto.Cantidad = item.GetElementsByTagName("siesa:InvoicedQuantity").Item(0).InnerText.Trim
                producto.ValBruto = item.GetElementsByTagName("siesa:LineExtensionAmount").Item(0).InnerText '.Trim.Replace(".", ",")
                producto.Descipcion = item.GetElementsByTagName("siesa:Description").Item(0).InnerText.Trim
                producto.porcentajeIva = item.GetElementsByTagName("siesa:Percent").Item(0).InnerText.Trim
                producto.descuento = item.GetElementsByTagName("siesa:Amount").Item(0).InnerText.Trim
                nota.productos.Add(producto)
            Next
            Dim Taxes As XmlNodeList = root.GetElementsByTagName("fe:TaxTotal")
            nota.imptos = New List(Of Tax)
            For Each item As XmlElement In Taxes
                Dim tax As New Tax
                tax.id = item.GetElementsByTagName("cbc:ID").Item(0).InnerText.Trim
                tax.Porcentaje = item.GetElementsByTagName("cbc:Percent").Item(0).InnerText.Trim
                tax.Valor = item.FirstChild.InnerText.Trim '.Replace(".", ",")
                nota.imptos.Add(tax)
            Next

            nota.subTotalFact = root.GetElementsByTagName("cbc:TaxExclusiveAmount").Item(0).InnerText.Trim() '.Replace(".", ",")
            nota.totalFact = root.GetElementsByTagName("cbc:PayableAmount").Item(0).InnerText.Trim() '.Replace(".", ",")
        Catch ex As Exception
            'MsgBox("Error" + ex.Message)
            'respuestasAplicativo(ex.Message)
        End Try

        Return nota
    End Function

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        Dim desde = TxtDesde.Text
        Dim hasta = TxtHasta.Text
        Dim tipoDoc = TxtTipoDoc.Text.ToUpper()
        Dim CO = TxtCO.Text
        Dim FecDesde = DTPFechaDesde.Value.ToString("yyyy-MM-dd")
        Dim FecHasta = DTPFechaHasta.Value.ToString("yyyy-MM-dd")
        Dim FecVenc = DTPFechaVenc.Value.ToString("yyyy-MM-dd")

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

        Try
            Dim factData As DataSet = objDatos.DataFacturaXML(desde, hasta, tipoDoc, CO)

            If factData.Tables(0).Rows.Count > 0 Then
                For Each item As DataRow In factData.Tables(0).Rows
                    Dim xml = BuildXML(item, FecDesde, FecHasta, FecVenc)
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
        End Try


        btnGenerar.Text = "Generar"
        btnGenerar.Enabled = True
        TxtDesde.Enabled = True
        TxtHasta.Enabled = True
        TxtTipoDoc.Enabled = True
        TxtCO.Enabled = True



    End Sub
End Class
