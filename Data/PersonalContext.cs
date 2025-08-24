using Microsoft.EntityFrameworkCore;
using PersonalAPI.Models;

namespace PersonalAPI.Data
{
    public class PersonalContext : DbContext
    {
        public PersonalContext(DbContextOptions<PersonalContext> options) : base(options)
        {
        }

        public DbSet<Personal> Personal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad Personal
            modelBuilder.Entity<Personal>(entity =>
            {
                // Configurar la clave primaria
                entity.HasKey(e => e.Codigo);
                
                // Configurar índices únicos para campos importantes
                entity.HasIndex(e => e.DNI).IsUnique().HasFilter("DNI IS NOT NULL");
                entity.HasIndex(e => e.NumIPSS).IsUnique().HasFilter("NumIPSS IS NOT NULL");
                
                // Configurar tipos char con longitudes fijas
                entity.Property(e => e.Codigo).IsFixedLength().HasMaxLength(5);
                entity.Property(e => e.Codcope).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.TipTrab).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.TipoServ).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.EstadoCivil).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.Sexo).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.Nacionalidad).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.Departamento).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.Provincia).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.Distrito).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.DNI).IsFixedLength().HasMaxLength(8);
                entity.Property(e => e.LibMilitar).IsFixedLength().HasMaxLength(10);
                entity.Property(e => e.LibTributa).IsFixedLength().HasMaxLength(7);
                entity.Property(e => e.NumIPSS).IsFixedLength().HasMaxLength(15);
                entity.Property(e => e.GrupoSanguineo).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.FactorRH).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.TipoAlta).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.TipoCont).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.VersionCC).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.CodTaller).IsFixedLength().HasMaxLength(5);
                entity.Property(e => e.TipoMano).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.TipoMano2).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.CodEspecialidad).IsFixedLength().HasMaxLength(4);
                entity.Property(e => e.Car_Tra).IsFixedLength().HasMaxLength(4);
                entity.Property(e => e.CodEspec2).IsFixedLength().HasMaxLength(4);
                entity.Property(e => e.CodSuministro).IsFixedLength().HasMaxLength(4);
                entity.Property(e => e.NivelMO).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.NivOcupacional).IsFixedLength().HasMaxLength(4);
                entity.Property(e => e.NivJerarquico).IsFixedLength().HasMaxLength(3);
                entity.Property(e => e.NivelEducativo).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.IArm).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.TTarSobretiempo).IsFixedLength().HasMaxLength(1);
                entity.Property(e => e.CodNovedad).IsFixedLength().HasMaxLength(2);
                entity.Property(e => e.CodigoAnt).IsFixedLength().HasMaxLength(5);
                entity.Property(e => e.CodigoNuevo).IsFixedLength().HasMaxLength(5);
                entity.Property(e => e.Cta_Banco).IsFixedLength().HasMaxLength(20);
                entity.Property(e => e.Cod_Banco).IsFixedLength().HasMaxLength(3);
                entity.Property(e => e.Cod_Sucursal).IsFixedLength().HasMaxLength(3);
                entity.Property(e => e.Ubigeo_Nac).IsFixedLength().HasMaxLength(6);
                entity.Property(e => e.Ubigeo_Dom).IsFixedLength().HasMaxLength(6);
                entity.Property(e => e.CTS_Banco).IsFixedLength().HasMaxLength(3);
                entity.Property(e => e.CTS_Moneda).IsFixedLength().HasMaxLength(5);
                entity.Property(e => e.MAR_JUB).IsFixedLength().HasMaxLength(1);
                
                // Configurar campos varchar con longitudes variables
                entity.Property(e => e.ApePaterno).HasMaxLength(35);
                entity.Property(e => e.ApeMaterno).HasMaxLength(35);
                entity.Property(e => e.Nombre).HasMaxLength(35);
                entity.Property(e => e.NombreTotal).HasMaxLength(100);
                entity.Property(e => e.Domicilio).HasMaxLength(100);
                entity.Property(e => e.Fono01).HasMaxLength(50);
                entity.Property(e => e.DocIngreso).HasMaxLength(100);
                entity.Property(e => e.DocBaja).HasMaxLength(100);
                entity.Property(e => e.CenCosto).HasMaxLength(4);
                entity.Property(e => e.Conyuge).HasMaxLength(50);
                entity.Property(e => e.Nro_AFP).HasMaxLength(12);
                entity.Property(e => e.Cod_AFP).HasMaxLength(5);
                entity.Property(e => e.Grupo_Profesional).HasMaxLength(2);
                entity.Property(e => e.Nivel_Profesional).HasMaxLength(4);
                entity.Property(e => e.CTS_Cuenta).HasMaxLength(20);
                entity.Property(e => e.CodigoO7).HasMaxLength(8);
                entity.Property(e => e.FechaTarifa2).HasMaxLength(10);
                
                // Configurar campos decimales con precisiones específicas
                entity.Property(e => e.TarDiar).HasPrecision(10, 2);
                entity.Property(e => e.TarDiar2).HasPrecision(10, 2);
                entity.Property(e => e.ImporteMensual).HasPrecision(14, 2);
                entity.Property(e => e.TarHoraNormal).HasPrecision(10, 2);
                entity.Property(e => e.TarHoraSobreTiempo).HasPrecision(10, 2);
                entity.Property(e => e.TarHoraDoble).HasPrecision(10, 2);
                entity.Property(e => e.TarHora25).HasPrecision(10, 2);
                entity.Property(e => e.TarHora35).HasPrecision(10, 2);
                entity.Property(e => e.TarHora100).HasPrecision(10, 2);
                entity.Property(e => e.TarHoraIncentivo).HasPrecision(18, 2);
                entity.Property(e => e.TarHoraBonificacion).HasPrecision(10, 2);
                entity.Property(e => e.PorcNLBS).HasPrecision(6, 2);
                entity.Property(e => e.PorcSLBS).HasPrecision(6, 2);
                entity.Property(e => e.SobAutorizado).HasPrecision(10, 1);
                entity.Property(e => e.SobAcumulado).HasPrecision(10, 1);
                entity.Property(e => e.SobTaller).HasPrecision(10, 1);
                entity.Property(e => e.IMPC_CTS).HasPrecision(10, 2);
                entity.Property(e => e.DAUS_CTS).HasPrecision(8, 2);
                entity.Property(e => e.IMP_CTS).HasPrecision(10, 2);
                entity.Property(e => e.Tarhora42).HasPrecision(9, 2);
                
                // Configurar tipo de dato date para FechaAFP
                entity.Property(e => e.FechaAFP).HasColumnType("date");
            });
        }
    }
}
