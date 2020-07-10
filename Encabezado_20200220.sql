Select 1 F_Ambiente,
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

	 T350_Fact.f350_notas as F_Notas,
	 T200_fact.f200_id as f_codigo_id   ---REPILAS CAMBIO
     
      

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