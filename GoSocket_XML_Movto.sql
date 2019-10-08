select Rownum NroLin,
                                        2 f_ambiente,
                                       f311_id_cia f_Cia,
                                       t350_fact.f350_id_co f_CdgSucursal,
                                       t350_fact.f350_id_tipo_docto f_Tipo,
                                       t350_fact.f350_consec_docto f_Numero,
	                                   'INT' f_TpoCodigo,
	                                   '' f_VlrCodigo,
	                                   f189_descripcion f_DscItem,
	                                   f320_cantidad * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_QtyItem,
	                                   '' f_UnmdItem,
	                                   f320_vlr_bruto * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_PrcBrutoItem,
	                                   f320_vlr_neto * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END )  f_PrcNetoItem,
	                                   '01' TipoImp,
	                                   0 f_TasaImp,
	                                   (f320_vlr_bruto - ( f320_vlr_dscto_1 + f320_vlr_dscto_2 ) ) * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END )  f_MontoBaseImp,
	                                   f320_vlr_imp * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_MontoImp,
	                                   f320_vlr_neto * ( CASE WHEN f311_ind_nat <> 1 THEN -1 ELSE 1 END ) * ( CASE WHEN f320_ind_naturaleza = f145_ind_naturaleza THEN 1 ELSE -1 END ) f_MontoTotalItem

	  
                                from  t350_co_docto_contable t350_fact
                                INNER JOIN T311_CO_DOCTO_FACTURA_SERV ON F350_ROWID=F311_ROWID_DOCTO
                                INNER JOIN t320_co_movto_serv on f311_rowid_docto = f320_rowid_docto
                                INNER JOIN t189_mc_servicios on f189_id_cia = f320_id_cia and f320_id_servicio = f189_id
                                INNER JOIN t145_mc_conceptos on f320_id_concepto = f145_id
                                INNER JOIN t028_mm_clases_documento ON f028_id = f350_id_clase_docto
                                where t350_fact.f350_ind_estado = 1  
                                      and f311_id_cia = 1 
                                      AND f311_ind_tipo_factura = 2
                                      AND f028_id_grupo_clase_docto IN ( 203, 2101 )