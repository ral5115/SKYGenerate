Imports System.Data.SqlClient
Imports System.Data.OracleClient

Public Class clsOperacionesSQL


    Public Function DataFacturaXML(ByVal desde As String, ByVal hasta As String, ByVal tipoDoc As String, ByVal CO As String) As DataSet

        Dim oracleconnetion = New OracleConnection(My.Settings.strConexionIntermedia)

        oracleconnetion.Open()

        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Dim cmd As New OracleCommand("Select 1 F_Ambiente,
                                        F311_Id_Cia F_Cia,
                                        case f350_id_clase_docto
	                                        when 25 then '11'
			                                when 27 then '05'
			                                when 22 then '05'
			                                when 149 then '11'
	                                    end F_TipoServicio,
                                        case f350_id_clase_docto
                                            when 27 then '01'
                                            when 25 then '91'
                                            when 22 then '01'
			                                when 149 then '92'  
                                        end  f_tipo,
                                        T350_Fact.F350_Id_Tipo_Docto tipo_docto,
                                        f022_prefijo F_Serie, 
                                        T350_Fact.F350_Consec_Docto F_Numero,
                                        '' f_NumeroInterno,

                                        TO_CHAR(F311_Fecha, 'YYYY-MM-DD HH:MM:SS') F_Fechaemis,
                                        SUBSTRING(F010_Razon_Social,1,50) F_Establecimiento,
                                        F285_Descripcion F_Ptoemis,
                                        '41' F_MedioPago,
                                        TO_CHAR(F311_Fecha, 'YYYY-MM-DD')  F_Periododesde,
                                        TO_CHAR(F311_Fecha, 'YYYY-MM-DD')  F_Periodohasta,
                                        1 F_Tiponegociacion,  
                                        TO_CHAR(F311_Fecha, 'YYYY-MM-DD') F_Fechavenc,
                                        case f208_id
                                        when 'CON' then '1'
                                        else '2'
                                        end  f_TipoNegociacion,
                                        1 F_Tipocontribuyente, 
                                        '04' F_Regimencontable,
                                        f010_nit F_Idemisor,
                                        f010_razon_social F_Nmbemisor,
                                        f010_razon_social F_Primernombre,
                                        'TpoObl' Tpocdgintemisor,
                                        'O-11' f_CdgIntEmisor,
                                        '' F_Cdgsucursal,
                                        T285_Cont.F015_Direccion1 F_Calle,
                                        'CO' F_Pais,
                                        T285_Dpto.F012_id F_Dpto,
                                        T285_Dpto.F012_id || T285_Ciu.F013_id F_Ciudad,
                                        '11001' F_Codigopostalfiscal,
                                        T200_Fact.f200_ind_tipo_tercero F_Tipocontribuyenter, 
                                        '48' F_Regimencontabler,
                                        case f203_id
                                        when 'Z' then '11'
                                        when 'T' then '12'
                                        when 'C' then '13'
                                        when 'E' then '22'
                                        when 'N' then '31'
                                        when 'X' then '42'
                                        else 'O'
                                        end f_TipoDocRecep,
                                        T200_fact.f200_nit f_NroDocRecep,
                                        T200_fact.f200_razon_social f_NmbRecep,
                                        T200_fact.f200_razon_social  f_PrimerNombre,
                                        T285_Cont.f015_email eMail,
                                        T285_Cont.f015_telefono Telefono,
                                        T285_Cont.f015_fax Fax,
                                        'TpoObl' f_TpoCdgIntRecep,
                                        'O-99' f_CdgIntRecep,
                                        nvl(T200_ContCli.f015_direccion1,T201_ContCli.f015_direccion1) f_DomFiscalRcpCalle,
                                        T200_Dpto.F012_Id f_DomFiscalRcpDpto,
                                        'CO' f_DomFiscalRcpPais,
                                        T200_Dpto.F012_Id || T200_Ciu.f013_id f_DomFiscalRcpCiu,
                                        '11001' f_DomFiscalRcpCodPostal,
                                        T200_Fact.f200_ind_tipo_tercero f_ContactoReceptorTipo,
                                        nvl(T200_fact.f200_razon_social,T200_ContCli.f015_contacto) f_ContactoReceptorCont,
                                        nvl(T200_fact.f200_razon_social,T200_ContCli.f015_contacto) f_ContactoReceptorDesc,
                                        f_generico_hallar_movto_ent(f350_id_cia,T201_fact.F201_Rowid_Movto_Entidad,'EUNOECO011','co011_correo_fe',1) f_ContactoReceptorEmail,
                                        T200_ContCli.f015_telefono f_ContactoReceptorTel,
                                        T200_ContCli.f015_fax f_ContactoReceptorFax,
                                        F311_Id_Moneda_Docto F_Moneda,
                                        F311_Tasa_Conv F_Tasaconver,
                                        F311_Vlr_Bruto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Subtotal, 
                                        T350_Fact.f350_total_base_gravable * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Mntbase,
                                        F311_Vlr_Imp * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Mntimp,
                                        F311_Vlr_Neto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Vlrpagar,
                                        F_Monto_Escrito(F311_Vlr_Neto) F_Vlrpalabras,
                                        '01' F_Tipoimp,
                                        case when F311_Vlr_Imp > 0 then 19 else 0 end  F_Tasaimp,
                                        T350_Fact.f350_total_base_gravable * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) F_Montobaseimp,
                                        F311_Vlr_Imp * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) F_Montoimp,
                                        '01' f_CAETipo,  
                                        f022_prefijo f_CAESerie, 
                                        f022_cons_inicial f_CAENumeroInicial, 
                                        f022_cons_final f_CAENumeroFinal, 
                                        f022_nro_resolucion f_CAENroResolucion, 
                                        f022_fecha_resolucion f_CAEFechaResolucion,
                                        case f350_id_clase_docto
	                                    when 25 then '20191'
			                            when 27 then case T350_Fact.F350_Id_Tipo_Docto
			                                              when 'COB' then '5602aec7666f7eb1e5c71a4e981ae679f7ea1e51da7816a6f62e9178c726a4b8'
							                              when 'FCE' then 'd14f55644f4b0578f95656d050880823eb6417eb12c462728304fcd2ea1d9048'
							                              when 'OPE' then 'c52662bd7a9c4bd6598a6ada6c3189524c52dc5cddf5b21b620213f231533e32'
							                              else 'fc8eac422eba16e22ffd8c6f94b3f40a6e38162c'
			                                         end
			                            when 22 then case T350_Fact.F350_Id_Tipo_Docto
			                                              when 'COB' then '5602aec7666f7eb1e5c71a4e981ae679f7ea1e51da7816a6f62e9178c726a4b8'
							                              when 'FCE' then 'd14f55644f4b0578f95656d050880823eb6417eb12c462728304fcd2ea1d9048'
							                              when 'OPE' then 'c52662bd7a9c4bd6598a6ada6c3189524c52dc5cddf5b21b620213f231533e32'
							                              else 'fc8eac422eba16e22ffd8c6f94b3f40a6e38162c'
			                                         end
			                            when 149 then '20191'
	                               end f_CAEClaveTC,
                                        TO_CHAR(f022_fecha_resolucion_vcto, 'YYYY-MM-DD') f_CAEPlazo,
                                case f350_id_clase_docto 
                                        when 25  then '1'
	                                    when 149 then '1'
                                end NroLinRef,
                                f_generico_hallar_movto_ent(f350_id_cia,f350_rowid_movto_entidad,case f350_id_clase_docto when 25  then 'EUNOECO022'
                                        when 149 then 'EUNOECO030' end, case f350_id_clase_docto when 25  then 'co022_co_docto_base' 
                                        when 149 then'co030_co_docto_base' end ,1) TpoDocRef,
       
                                f_generico_hallar_movto_ent(f350_id_cia,f350_rowid_movto_entidad,case f350_id_clase_docto when 25  then 'EUNOECO022'
                                        when 149 then 'EUNOECO030' end, case f350_id_clase_docto when 25  then 'co022_tipo_docto_base' 
                                        when 149 then'co030_tipo_docto_base' end ,1) SerieRef,
       
                                f_generico_hallar_movto_ent(f350_id_cia,f350_rowid_movto_entidad,case f350_id_clase_docto when 25  then 'EUNOECO022'
                                        when 149 then 'EUNOECO030' end, case f350_id_clase_docto when 25  then 'co022_docto_base' 
                                        when 149 then'co030_docto_base' end ,1) NumeroRef,
       
                                f_generico_hallar_movto_ent(f350_id_cia,f350_rowid_movto_entidad,case f350_id_clase_docto when 25  then 'EUNOECO022'
                                        when 149 then 'EUNOECO030' end, case f350_id_clase_docto when 25  then 'co022_fecha_docto_base' 
                                        when 149 then'co030_fecha_docto_base' end ,2) FechaRef,
       
                                case f350_id_clase_docto 
                                        when 25  then '1'
	                                    when 149 then '1'
                                end CodRef,

                                f_generico_hallar_movto_ent_2(f350_id_cia,f350_rowid_movto_entidad,case f350_id_clase_docto when 25  then 'EUNOECO019'
                                        when 149 then 'EUNOECO015' end, case f350_id_clase_docto when 25  then 'co019_concepto_nd' 
                                        when 149 then'co015_concepto_nc' end ,7,0) RazonRef,
       
                                f_generico_hallar_movto_ent(f350_id_cia,f350_rowid_movto_entidad,case f350_id_clase_docto when 25  then 'EUNOECO022'
                                        when 149 then 'EUNOECO030' end, case f350_id_clase_docto when 25  then 'co022_uuid_docto_base' 
                                        when 149 then'co030_uuid_docto_base' end ,1) ECB01,
                                T350_Fact.f350_notas as F_Notas
	   
	    

                                From  T350_Co_Docto_Contable T350_Fact
                                Inner Join T311_Co_Docto_Factura_Serv On F350_Rowid=F311_Rowid_Docto
                                Inner Join T200_Mm_Terceros T200_Fact On T200_Fact.F200_Rowid = F311_Rowid_Tercero
                                Inner Join T201_Mm_Clientes T201_Fact On T201_Fact.F201_Rowid_Tercero = F311_Rowid_Tercero
                                And T201_Fact.F201_Id_Sucursal = F311_Id_Sucursal_Cli
                                Inner Join T208_Mm_Condiciones_Pago On F311_Id_Cond_Pago = F208_Id And F208_Id_Cia = F311_Id_Cia 
                                Inner Join T285_Co_Centro_Op On F285_Id_Cia = F311_Id_Cia And F285_Id = T350_Fact.F350_Id_Co
                                inner join t028_mm_clases_documento on f028_id = f350_id_clase_docto
                                inner join t203_mm_tipo_ident on f203_id_cia = T200_Fact.f200_id_cia and f203_id = T200_Fact.f200_id_tipo_ident
                                inner join t022_mm_consecutivos on f022_id_cia = t350_fact.f350_id_cia and f022_id_co = t350_fact.f350_id_co  and f022_id_tipo_docto = t350_fact.f350_id_tipo_docto  

                                Left  Join T010_Mm_Companias On F010_Id = F311_Id_Cia
                                Left  Join T015_Mm_Contactos T285_Cont On T285_Cont.F015_Rowid = F285_Rowid_Contacto
                                Left  Join T015_Mm_Contactos T200_ContCli On T200_ContCli.F015_Rowid = T200_Fact.f200_rowid_contacto
                                Left  Join T015_Mm_Contactos T201_ContCli On T201_ContCli.F015_Rowid = T201_Fact.f201_rowid_contacto
                                Left  Join T011_Mm_Paises T285_Pais On T285_Pais.F011_Id = T285_Cont.F015_Id_Pais
                                Left  Join T012_Mm_Deptos T285_Dpto On T285_Dpto.F012_Id_Pais = T285_Cont.F015_Id_Pais And T285_Dpto.F012_Id = T285_Cont.F015_Id_Depto
                                Left  Join T013_Mm_Ciudades T285_Ciu On T285_Ciu.F013_Id_Pais = T285_Cont.F015_Id_Pais And T285_Ciu.F013_Id_Depto = T285_Cont.F015_Id_Depto And T285_Ciu.F013_Id = T285_Cont.F015_Id_Ciudad

                                Left  Join T011_Mm_Paises T200_Pais On T200_Pais.F011_Id = T200_ContCli.F015_Id_Pais
                                Left  Join T012_Mm_Deptos T200_Dpto On T200_Dpto.F012_Id_Pais = T200_ContCli.F015_Id_Pais And T200_Dpto.F012_Id = T200_ContCli.F015_Id_Depto
                                Left  Join T013_Mm_Ciudades T200_Ciu On T200_Ciu.F013_Id_Pais = T200_ContCli.F015_Id_Pais And T200_Ciu.F013_Id_Depto = T200_ContCli.F015_Id_Depto And T200_Ciu.F013_Id = T200_ContCli.F015_Id_Ciudad
                                Where T350_Fact.F350_Ind_Estado = 1
                                 and T350_Fact.F350_Consec_Docto between " & desde & " and " & hasta & " and T350_Fact.F350_Id_Tipo_Docto =" & "'" & tipoDoc & "' and T350_Fact.F350_Id_co ='" & CO & "'", oracleconnetion)
        cmd.CommandType = CommandType.Text
        da.SelectCommand = cmd

        Try
            da.Fill(ds, "FactXML")

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            oracleconnetion.Close()
        End Try

        Return ds

    End Function

    Public Function DataDetalleFacturaXML(ByVal numFact As String, ByVal tipoDoc As String) As DataSet

        Dim oracleconnetion = New OracleConnection(My.Settings.strConexionIntermedia)

        oracleconnetion.Open()

        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Dim cmd As New OracleCommand("select Rownum NroLin,
                                           2 f_ambiente,
                                           f311_id_cia f_Cia,
                                           t350_fact.f350_id_co f_CdgSucursal,
                                           t350_fact.f350_id_tipo_docto f_Tipo,
                                           t350_fact.f350_consec_docto f_Numero,
                                         'INT' f_TpoCodigo,
                                         f189_id f_VlrCodigo,
                                         f189_descripcion f_DscItem,
                                         f320_cantidad * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_QtyItem,
                                         '' f_UnmdItem,
                                         f320_vlr_bruto * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_PrcBrutoItem,
                                         f320_vlr_neto * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END )  f_PrcNetoItem,
                                         case when f320_vlr_imp > 0 then '01' else ' ' end TipoImp,
                                         case when f320_vlr_imp > 0 then '19' else ' ' end f_TasaImp, 
                                         case when f320_vlr_imp > 0 then to_char((f320_vlr_bruto - ( f320_vlr_dscto_1 + f320_vlr_dscto_2 ) ) * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END )) else ' ' end f_MontoBaseImp,
                                         case when f320_vlr_imp > 0 then to_char(f320_vlr_imp * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END )) else  ' ' end f_MontoImp,
                                         (f320_vlr_bruto - ( f320_vlr_dscto_1 + f320_vlr_dscto_2 ) ) * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_MontoTotalItem 
      
                                    from  t350_co_docto_contable t350_fact
                                    INNER JOIN T311_CO_DOCTO_FACTURA_SERV ON F350_ROWID=F311_ROWID_DOCTO
                                    INNER JOIN t320_co_movto_serv on f311_rowid_docto = f320_rowid_docto
                                    INNER JOIN t189_mc_servicios on f189_id_cia = f320_id_cia and f320_id_servicio = f189_id
                                    INNER JOIN t145_mc_conceptos on f320_id_concepto = f145_id
                                    INNER JOIN t028_mm_clases_documento ON f028_id = f350_id_clase_docto
                                    inner join t022_mm_consecutivos on f022_id_cia = t350_fact.f350_id_cia  
                                           and f022_id_co = t350_fact.f350_id_co  and f022_id_tipo_docto = t350_fact.f350_id_tipo_docto 
                                    where t350_fact.f350_ind_estado = 1  
                                          and f311_id_cia = 1 
                                          AND f311_ind_tipo_factura = 2
                                          AND f028_id_grupo_clase_docto IN ( 203, 2101 )
                                         and t350_fact.f350_consec_docto = " & numFact & " and T350_Fact.F350_Id_Tipo_Docto = " & "'" & tipoDoc & "'", oracleconnetion)
        cmd.CommandType = CommandType.Text
        da.SelectCommand = cmd

        Try
            da.Fill(ds, "FactDetailXML")

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            oracleconnetion.Close()
        End Try

        Return ds

    End Function
    Public Function DataEntidadDinamica() As DataSet

        Dim oracleconnetion = New OracleConnection(My.Settings.strConexionIntermedia)

        oracleconnetion.Open()

        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Dim cmd As New OracleCommand("Select Num_Documento f350_consec_docto
                                     , nropedido f753_dato_texto
                                      From UNOEE.Tsr_Mvtos_Fatc_Erp
                                     INNER JOIN unoee.t350_co_docto_contable on f350_id_cia = Cia 
                                                                            and f350_id_co = CoDoc 
                                                                            and f350_id_tipo_docto = Tipo_Documento  and f350_consec_docto = Num_Documento
                                     where nropedido is not null
                                     and f350_rowid_movto_entidad is null", oracleconnetion)
        cmd.CommandType = CommandType.Text
        da.SelectCommand = cmd

        Try
            da.Fill(ds, "Entidad")

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            oracleconnetion.Close()
        End Try

        Return ds

    End Function

    Public Function DataAdquirientes(ByVal tipo As String) As DataSet
        Dim script As String
        If tipo = "C" Then
            script = "select 
                rtrim(F015_DIRECCION1) direccion,
                NVL(f200_DV_NIT,'') dv_nit,
                rtrim(f200_ID) nit,
                CASE WHEN f200_ind_tipo_tercero =2 THEN 'JURIDICO' ELSE 'NATURAL' END tipo,
                NVL(rtrim(F200_APELLIDO1||' '||F200_APELLIDO2),'N/A') apellidos,
                rtrim(F200_RAZON_SOCIAL) razon_social,
                rtrim(f200_nombres) nombre,
                NVL(F015_TELEFONO,0) telefono,
                NVL(rtrim(f015_email),'NA@SinEmail.com') email,
                NVL(f200_id_tipo_ident,'N') tipo_ident,
                f015_id_depto cod_depto,
                f015_id_depto||f015_id_ciudad cod_ciudad
                from unoee.T200_MM_TERCEROS
                inner join unoee.T201_MM_CLIENTES on f200_rowid = f201_rowid_tercero
                inner join  unoee.T015_MM_CONTACTOS on F201_ROWID_CONTACTO = F015_ROWID
                LEFT JOIN unoee.Tsr_Adquirientes ON rtrim(ID_ADQUIRIENTE) = rtrim(f200_ID)
                WHERE ID_ADQUIRIENTE IS NULL and f201_id_sucursal = '001'
                and f200_ind_estado = 1 and f201_ind_estado_activo = 1"
        Else
            script = "select 
                rtrim(F015_DIRECCION1) direccion,
                NVL(f200_DV_NIT,'') dv_nit,
                rtrim(f200_ID) nit,
                CASE WHEN f200_DV_NIT IS NULL THEN 'NATURAL' ELSE 'JURIDICO' END tipo,
                NVL(rtrim(F200_APELLIDO1||' '||F200_APELLIDO2),'N/A') apellidos,
                rtrim(F200_RAZON_SOCIAL) razon_social,
                rtrim(f200_nombres) nombre,
                NVL(F015_TELEFONO,0) telefono,
                NVL(SUBSTR(NVL(rtrim(f015_email),'NA@SinEmail.com;'),0,INSTR(NVL(rtrim(f015_email),'NA@SinEmail.com'),';',1)-1),rtrim(f015_email)) email,
                NVL( REPLACE(SUBSTR( NVL(rtrim(f015_email),'NA@SinEmail.com;'),INSTR(NVL(rtrim(f015_email),'NA@SinEmail.com'),';',1)+1,LENGTH(NVL(rtrim(f015_email),'NA@SinEmail.com;')) ),';',',' ),rtrim(f015_email) ) email_sec,
                NVL(f200_id_tipo_ident,'N') tipo_ident,
                f015_id_depto cod_depto,
                f015_id_depto||f015_id_ciudad cod_ciudad
                from unoee.T200_MM_TERCEROS
                inner join unoee.T201_MM_CLIENTES on f200_rowid = f201_rowid_tercero
                inner join  unoee.T015_MM_CONTACTOS on F201_ROWID_CONTACTO = F015_ROWID
                inner JOIN unoee.Tsr_Adquirientes ON ID_ADQUIRIENTE = rtrim(f200_ID)
                WHERE fecha_tercero <> f200_ts or fecha_cliente <> f201_ts and f201_id_sucursal = 001
                and f200_ind_estado = 1 and f201_ind_estado_activo = 1"
        End If

        Dim oracleconnetion = New OracleConnection(My.Settings.strConexionIntermedia)

        oracleconnetion.Open()

        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Dim cmd As New OracleCommand(script, oracleconnetion)
        cmd.CommandType = CommandType.Text
        da.SelectCommand = cmd
        Try
            da.Fill(ds, "Adquirientes")

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            oracleconnetion.Close()
        End Try

        Return ds


    End Function

    Public Sub LogAdquirientes(ByVal tipo As String, ByVal idAdq As String)

        Dim script As String

        If tipo = "C" Then
            script = String.Format("insert into unoee.Tsr_Adquirientes (ID_ADQUIRIENTE, FECHA_TERCERO, FECHA_CONTACTO, FECHA_CLIENTE)
                select
                rtrim(f200_ID) nit, f200_ts, f015_ts, f201_ts
                from unoee.T200_MM_TERCEROS
                inner join unoee.T201_MM_CLIENTES on f200_rowid = f201_rowid_tercero
                inner join unoee.T015_MM_CONTACTOS on F201_ROWID_CONTACTO = F015_ROWID
                WHERE f201_id_sucursal = '001' and rtrim(f200_ID) = '{0}'", idAdq)
        Else
            script = String.Format("UPDATE unoee.Tsr_Adquirientes
                                       SET (FECHA_TERCERO, FECHA_CONTACTO, FECHA_CLIENTE) = (select
                                                    f200_ts, f015_ts, f201_ts
                                                    from unoee.T200_MM_TERCEROS
                                                    inner join unoee.T201_MM_CLIENTES on f200_rowid = f201_rowid_tercero
                                                    inner join  unoee.T015_MM_CONTACTOS on F201_ROWID_CONTACTO = F015_ROWID
                                                    where rtrim(f200_ID) = '{0}' and f201_id_sucursal = '001')
                                    where ID_ADQUIRIENTE = '{0}' ", idAdq)
        End If


        Dim oracleconnetion = New OracleConnection(My.Settings.strConexionIntermedia)
        'oracleconnetion.ConnectionTimeout = 999999
        oracleconnetion.Open()
        Dim cmd As New OracleCommand(script, oracleconnetion)
        cmd.CommandType = CommandType.Text

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            oracleconnetion.Close()
        End Try


    End Sub

    Public Function NroPedidoFactura(ByVal consec As String) As DataSet
        Dim script As String
        script = String.Format("select nropedido from unoee.Tsr_Mvtos_Fatc_Erp
                                where nropedido is not null 
                                and tipo_documento like 'FE%' 
                                and num_documento = {0}", consec)

        Dim oracleconnetion = New OracleConnection(My.Settings.strConexionIntermedia)

        oracleconnetion.Open()

        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Dim cmd As New OracleCommand(script, oracleconnetion)
        cmd.CommandType = CommandType.Text
        da.SelectCommand = cmd
        Try
            da.Fill(ds, "NroPedido")

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            oracleconnetion.Close()
        End Try

        Return ds

    End Function
End Class
