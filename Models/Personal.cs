using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalAPI.Models
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        [Column("Codigo")]
        [StringLength(5)]
        public string Codigo { get; set; } = string.Empty;

        [Column("codcope")]
        [StringLength(1)]
        public string? Codcope { get; set; }

        [Column("TipTrab")]
        [StringLength(1)]
        public string? TipTrab { get; set; }

        [Column("TipoServ")]
        [StringLength(1)]
        public string? TipoServ { get; set; }

        [Column("ApePaterno")]
        [StringLength(35)]
        public string? ApePaterno { get; set; }

        [Column("ApeMaterno")]
        [StringLength(35)]
        public string? ApeMaterno { get; set; }

        [Column("Nombre")]
        [StringLength(35)]
        public string? Nombre { get; set; }

        [Column("NombreTotal")]
        [StringLength(100)]
        public string? NombreTotal { get; set; }

        [Column("EstadoCivil")]
        [StringLength(1)]
        public string? EstadoCivil { get; set; }

        [Column("Sexo")]
        [StringLength(1)]
        public string? Sexo { get; set; }

        [Column("Domicilio")]
        [StringLength(100)]
        public string? Domicilio { get; set; }

        [Column("Nacionalidad")]
        [StringLength(1)]
        public string? Nacionalidad { get; set; }

        [Column("Departamento")]
        [StringLength(2)]
        public string? Departamento { get; set; }

        [Column("Provincia")]
        [StringLength(2)]
        public string? Provincia { get; set; }

        [Column("Distrito")]
        [StringLength(2)]
        public string? Distrito { get; set; }

        [Column("FecNacimiento")]
        public DateTime? FecNacimiento { get; set; }

        [Column("Fono01")]
        [StringLength(50)]
        public string? Fono01 { get; set; }

        [Column("DNI")]
        [StringLength(8)]
        public string? DNI { get; set; }

        [Column("LibMilitar")]
        [StringLength(10)]
        public string? LibMilitar { get; set; }

        [Column("LibTributa")]
        [StringLength(7)]
        public string? LibTributa { get; set; }

        [Column("NumIPSS")]
        [StringLength(15)]
        public string? NumIPSS { get; set; }

        [Column("GrupoSanguineo")]
        [StringLength(2)]
        public string? GrupoSanguineo { get; set; }

        [Column("FactorRH")]
        [StringLength(1)]
        public string? FactorRH { get; set; }

        [Column("TipoAlta")]
        [StringLength(1)]
        public string? TipoAlta { get; set; }

        [Column("FechaIngreso")]
        public DateTime? FechaIngreso { get; set; }

        [Column("FechaIngreso1")]
        public DateTime? FechaIngreso1 { get; set; }

        [Column("DocIngreso")]
        [StringLength(100)]
        public string? DocIngreso { get; set; }

        [Column("FechaBaja")]
        public DateTime? FechaBaja { get; set; }

        [Column("DocBaja")]
        [StringLength(100)]
        public string? DocBaja { get; set; }

        [Column("FechaReingreso")]
        public DateTime? FechaReingreso { get; set; }

        [Column("TipoCont")]
        [StringLength(1)]
        public string? TipoCont { get; set; }

        [Column("VersionCC")]
        [StringLength(1)]
        public string? VersionCC { get; set; }

        [Column("CenCosto")]
        [StringLength(4)]
        public string? CenCosto { get; set; }

        [Column("CodTaller")]
        [StringLength(5)]
        public string? CodTaller { get; set; }

        [Column("TipoMano")]
        [StringLength(1)]
        public string? TipoMano { get; set; }

        [Column("TipoMano2")]
        [StringLength(1)]
        public string? TipoMano2 { get; set; }

        [Column("CodEspecialidad")]
        [StringLength(4)]
        public string? CodEspecialidad { get; set; }

        [Column("Car_Tra")]
        [StringLength(4)]
        public string? Car_Tra { get; set; }

        [Column("CodEspec2")]
        [StringLength(4)]
        public string? CodEspec2 { get; set; }

        [Column("CodSuministro")]
        [StringLength(4)]
        public string? CodSuministro { get; set; }

        [Column("NivelMO")]
        [StringLength(1)]
        public string? NivelMO { get; set; }

        [Column("NivOcupacional")]
        [StringLength(4)]
        public string? NivOcupacional { get; set; }

        [Column("NivJerarquico")]
        [StringLength(3)]
        public string? NivJerarquico { get; set; }

        [Column("NivelEducativo")]
        [StringLength(2)]
        public string? NivelEducativo { get; set; }

        [Column("IArm")]
        [StringLength(2)]
        public string? IArm { get; set; }

        [Column("TarDiar", TypeName = "decimal(10,2)")]
        public decimal? TarDiar { get; set; }

        [Column("FechaTarifa")]
        public DateTime? FechaTarifa { get; set; }

        [Column("TarDiar2", TypeName = "decimal(10,2)")]
        public decimal? TarDiar2 { get; set; }

        [Column("FechaTarifa2")]
        [StringLength(10)]
        public string? FechaTarifa2 { get; set; }

        [Column("ImporteMensual", TypeName = "decimal(14,2)")]
        public decimal? ImporteMensual { get; set; }

        [Column("TarHoraNormal", TypeName = "decimal(10,2)")]
        public decimal? TarHoraNormal { get; set; }

        [Column("TarHoraSobreTiempo", TypeName = "decimal(10,2)")]
        public decimal? TarHoraSobreTiempo { get; set; }

        [Column("TarHoraDoble", TypeName = "decimal(10,2)")]
        public decimal? TarHoraDoble { get; set; }

        [Column("TarHora25", TypeName = "decimal(10,2)")]
        public decimal? TarHora25 { get; set; }

        [Column("TarHora35", TypeName = "decimal(10,2)")]
        public decimal? TarHora35 { get; set; }

        [Column("TarHora100", TypeName = "decimal(10,2)")]
        public decimal? TarHora100 { get; set; }

        [Column("TarHoraIncentivo", TypeName = "decimal(18,2)")]
        public decimal? TarHoraIncentivo { get; set; }

        [Column("TarHoraBonificacion", TypeName = "decimal(10,2)")]
        public decimal? TarHoraBonificacion { get; set; }

        [Column("TTarSobretiempo")]
        [StringLength(1)]
        public string? TTarSobretiempo { get; set; }

        [Column("PorcNLBS", TypeName = "decimal(6,2)")]
        public decimal? PorcNLBS { get; set; }

        [Column("PorcSLBS", TypeName = "decimal(6,2)")]
        public decimal? PorcSLBS { get; set; }

        [Column("SobAutorizado", TypeName = "decimal(10,1)")]
        public decimal? SobAutorizado { get; set; }

        [Column("SobAcumulado", TypeName = "decimal(10,1)")]
        public decimal? SobAcumulado { get; set; }

        [Column("SobTaller", TypeName = "decimal(10,1)")]
        public decimal? SobTaller { get; set; }

        [Column("Conyuge")]
        [StringLength(50)]
        public string? Conyuge { get; set; }

        [Column("CodNovedad")]
        [StringLength(2)]
        public string? CodNovedad { get; set; }

        [Column("FechaInicio")]
        public DateTime? FechaInicio { get; set; }

        [Column("FechaTermino")]
        public DateTime? FechaTermino { get; set; }

        [Column("FechaVencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [Column("CodigoAnt")]
        [StringLength(5)]
        public string? CodigoAnt { get; set; }

        [Column("CodigoNuevo")]
        [StringLength(5)]
        public string? CodigoNuevo { get; set; }

        [Column("FechaAFP", TypeName = "date")]
        public DateTime? FechaAFP { get; set; }

        [Column("Nro_AFP")]
        [StringLength(12)]
        public string? Nro_AFP { get; set; }

        [Column("Cod_AFP")]
        [StringLength(5)]
        public string? Cod_AFP { get; set; }

        [Column("cta_banco")]
        [StringLength(20)]
        public string? Cta_Banco { get; set; }

        [Column("cod_banco")]
        [StringLength(3)]
        public string? Cod_Banco { get; set; }

        [Column("cod_sucursal")]
        [StringLength(3)]
        public string? Cod_Sucursal { get; set; }

        [Column("ubigeo_nac")]
        [StringLength(6)]
        public string? Ubigeo_Nac { get; set; }

        [Column("ubigeo_dom")]
        [StringLength(6)]
        public string? Ubigeo_Dom { get; set; }

        [Column("grupo_profesional")]
        [StringLength(2)]
        public string? Grupo_Profesional { get; set; }

        [Column("NIVEL_Profesional")]
        [StringLength(4)]
        public string? Nivel_Profesional { get; set; }

        [Column("CTS_Banco")]
        [StringLength(3)]
        public string? CTS_Banco { get; set; }

        [Column("CTS_Moneda")]
        [StringLength(5)]
        public string? CTS_Moneda { get; set; }

        [Column("CTS_Cuenta")]
        [StringLength(20)]
        public string? CTS_Cuenta { get; set; }

        [Column("IMPC_CTS", TypeName = "decimal(10,2)")]
        public decimal? IMPC_CTS { get; set; }

        [Column("DAUS_CTS", TypeName = "decimal(8,2)")]
        public decimal? DAUS_CTS { get; set; }

        [Column("MAR_JUB")]
        [StringLength(1)]
        public string? MAR_JUB { get; set; }

        [Column("IMP_CTS", TypeName = "decimal(10,2)")]
        public decimal? IMP_CTS { get; set; }

        [Column("CodigoO7")]
        [StringLength(8)]
        public string? CodigoO7 { get; set; }

        [Column("tarhora42", TypeName = "decimal(9,2)")]
        public decimal? Tarhora42 { get; set; }
    }
}
