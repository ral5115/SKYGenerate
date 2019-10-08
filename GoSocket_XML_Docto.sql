Select 
                                       2 F_Ambiente,
                                       F311_Id_Cia F_Cia,
                                       05 F_TipoServicio,
                                       case f350_id_clase_docto
                                            when 27 then '01'
                                            when 25 then '91'
                                            when 22 then '01'
                                       end  f_tipo,
                                       T350_Fact.F350_Id_Tipo_Docto F_Serie,
                                       T350_Fact.F350_Consec_Docto F_Numero,
                                       '' f_NumeroInterno,
	                                   F311_Fecha F_Fechaemis,
	                                   F010_Razon_Social F_Establecimiento,
	                                   F285_Descripcion F_Ptoemis,
                                       '41' F_MedioPago,
	                                   Rpad(To_Char(F311_Fecha,Convertsqlserverdateformat(112)),6,' ')  F_Periododesde,
                                       Rpad(To_Char(F311_Fecha,Convertsqlserverdateformat(112)),6,' ')  F_Periodohasta,
	                                   1 F_Tiponegociacion,  
	                                   F311_Fecha F_Fechavenc,
                                       case f208_id
                                            when 'CON' then '1'
                                            else '2'
                                       end  f_TipoNegociacion,
                                       1 F_Tipocontribuyente, 
	                                   05 F_Regimencontable,
  	                                   f010_nit F_Idemisor,
	                                   f010_razon_social F_Nmbemisor,
	                                   f010_razon_social F_Primernombre,
   	                                   'TpoObl' Tpocdgintemisor,
                                       '0-11' f_CdgIntEmisor,
                                       '' F_Cdgsucursal,
                                       T285_Cont.F015_Direccion1 F_Calle,
	                                   'CO' F_Pais,
	                                   T285_Dpto.F012_id F_Dpto,
	                                   T285_Ciu.F013_Descripcion F_Ciudad,
	                                   '11001' F_Codigopostalfiscal,
                                       T200_Fact.f200_ind_tipo_tercero F_Tipocontribuyenter, 
	                                   04 F_Regimencontabler,
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
                                       '0-99' f_CdgIntRecep,
                                       T200_ContCli.f015_direccion1 f_DomFiscalRcpCalle,
                                       T200_Dpto.F012_Id f_DomFiscalRcpDpto,
                                       'CO' f_DomFiscalRcpPais,
                                       T200_Ciu.f013_descripcion f_DomFiscalRcpCiu,
                                       '11001' f_DomFiscalRcpCodPostal,
                                       T200_Fact.f200_ind_tipo_tercero f_ContactoReceptorTipo,
                                       T200_ContCli.f015_contacto f_ContactoReceptorCont,
                                       T200_ContCli.f015_contacto f_ContactoReceptorDesc,
                                       T200_ContCli.f015_email f_ContactoReceptorEmail,
                                       T200_ContCli.f015_telefono f_ContactoReceptorTel,
                                       T200_ContCli.f015_fax f_ContactoReceptorFax,
                                       F311_Id_Moneda_Docto F_Moneda,
	                                   F311_Tasa_Conv F_Tasaconver,
	                                   F311_Vlr_Bruto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Subtotal, 
	                                   F311_Vlr_Bruto - F311_Vlr_Dscto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Mntbase,
	                                   F311_Vlr_Imp * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Mntimp,
	                                   F311_Vlr_Neto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) As F_Vlrpagar,
	                                   F_Monto_Escrito(F311_Vlr_Neto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End)) F_Vlrpalabras,
	                                   '01' F_Tipoimp,
	                                   19 F_Tasaimp,
	                                   F311_Vlr_Bruto - F311_Vlr_Dscto * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) F_Montobaseimp,
	                                   F311_Vlr_Imp * (Case F311_Ind_Nat When 0 Then - 1 Else 1 End) F_Montoimp
                                From  T350_Co_Docto_Contable T350_Fact
                                Inner Join T311_Co_Docto_Factura_Serv On F350_Rowid=F311_Rowid_Docto
                                Inner Join T200_Mm_Terceros T200_Fact On T200_Fact.F200_Rowid = F311_Rowid_Tercero
                                Inner Join T201_Mm_Clientes T201_Fact On T201_Fact.F201_Rowid_Tercero = F311_Rowid_Tercero
                                                                      And T201_Fact.F201_Id_Sucursal = F311_Id_Sucursal_Cli
                                Inner Join T208_Mm_Condiciones_Pago On F311_Id_Cond_Pago = F208_Id And F208_Id_Cia = F311_Id_Cia 
                                Inner Join T285_Co_Centro_Op On F285_Id_Cia = F311_Id_Cia And F285_Id = T350_Fact.F350_Id_Co
                                inner join t028_mm_clases_documento on f028_id = f350_id_clase_docto
                                inner join t203_mm_tipo_ident on f203_id_cia = T200_Fact.f200_id_cia and f203_id = T200_Fact.f200_id_tipo_ident

                                Left  Join T010_Mm_Companias On F010_Id = F311_Id_Cia
                                Left  Join T015_Mm_Contactos T285_Cont On T285_Cont.F015_Rowid = F285_Rowid_Contacto
                                Left  Join T015_Mm_Contactos T200_ContCli On T200_ContCli.F015_Rowid = T200_Fact.f200_rowid_contacto

                                Left  Join T011_Mm_Paises T285_Pais On T285_Pais.F011_Id = T285_Cont.F015_Id_Pais
                                Left  Join T012_Mm_Deptos T285_Dpto On T285_Dpto.F012_Id_Pais = T285_Cont.F015_Id_Pais And T285_Dpto.F012_Id = T285_Cont.F015_Id_Depto
                                Left  Join T013_Mm_Ciudades T285_Ciu On T285_Ciu.F013_Id_Pais = T285_Cont.F015_Id_Pais And T285_Ciu.F013_Id_Depto = T285_Cont.F015_Id_Depto And T285_Ciu.F013_Id = T285_Cont.F015_Id_Ciudad

                                Left  Join T011_Mm_Paises T200_Pais On T200_Pais.F011_Id = T200_ContCli.F015_Id_Pais
                                Left  Join T012_Mm_Deptos T200_Dpto On T200_Dpto.F012_Id_Pais = T200_ContCli.F015_Id_Pais And T200_Dpto.F012_Id = T200_ContCli.F015_Id_Depto
                                Left  Join T013_Mm_Ciudades T200_Ciu On T200_Ciu.F013_Id_Pais = T200_ContCli.F015_Id_Pais And T200_Ciu.F013_Id_Depto = T200_ContCli.F015_Id_Depto And T200_Ciu.F013_Id = T200_ContCli.F015_Id_Ciudad
                                Where T350_Fact.F350_Ind_Estado = 1 