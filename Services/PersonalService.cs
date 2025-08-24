using Microsoft.EntityFrameworkCore;
using PersonalAPI.Data;
using PersonalAPI.Models;

namespace PersonalAPI.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly PersonalContext _context;
        private readonly ILogger<PersonalService> _logger;

        public PersonalService(PersonalContext context, ILogger<PersonalService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Personal>> GetAllAsync()
        {
            try
            {
                return await _context.Personal.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todo el personal");
                throw;
            }
        }

        public async Task<Personal?> GetByCodigoAsync(string codigo)
        {
            try
            {
                return await _context.Personal.FindAsync(codigo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personal por Código: {Codigo}", codigo);
                throw;
            }
        }

        // public async Task<Personal> CreateAsync(Personal personal)
        // {
        //     try
        //     {
        //         _context.Personal.Add(personal);
        //         await _context.SaveChangesAsync();
        //         return personal;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al crear personal");
        //         throw;
        //     }
        // }

        // public async Task<Personal?> UpdateAsync(string codigo, Personal personal)
        // {
        //     try
        //     {
        //         var existingPersonal = await _context.Personal.FindAsync(codigo);
        //         if (existingPersonal == null)
        //         {
        //             return null;
        //         }

        //         // Actualizar solo campos principales para evitar errores de tipo
        //         existingPersonal.ApePaterno = personal.ApePaterno;
        //         existingPersonal.ApeMaterno = personal.ApeMaterno;
        //         existingPersonal.Nombre = personal.Nombre;
        //         existingPersonal.NombreTotal = personal.NombreTotal;
        //         existingPersonal.Domicilio = personal.Domicilio;
        //         existingPersonal.Departamento = personal.Departamento;
        //         existingPersonal.Provincia = personal.Provincia;
        //         existingPersonal.Distrito = personal.Distrito;
        //         existingPersonal.FecNacimiento = personal.FecNacimiento;
        //         existingPersonal.Fono01 = personal.Fono01;
        //         existingPersonal.DNI = personal.DNI;
        //         existingPersonal.FechaIngreso = personal.FechaIngreso;
        //         existingPersonal.CenCosto = personal.CenCosto;
        //         existingPersonal.Car_Tra = personal.Car_Tra;
        //         existingPersonal.ImporteMensual = personal.ImporteMensual;

        //         await _context.SaveChangesAsync();
        //         return existingPersonal;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al actualizar personal con Código: {Codigo}", codigo);
        //         throw;
        //     }
        // }

        // public async Task<bool> DeleteAsync(string codigo)
        // {
        //     try
        //     {
        //         var personal = await _context.Personal.FindAsync(codigo);
        //         if (personal == null)
        //         {
        //             return false;
        //         }

        //         _context.Personal.Remove(personal);
        //         await _context.SaveChangesAsync();
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al eliminar personal con Código: {Codigo}", codigo);
        //         throw;
        //     }
        // }

        // public async Task<IEnumerable<Personal>> GetByDepartamentoAsync(string departamento)
        // {
        //     try
        //     {
        //         return await _context.Personal
        //             .Where(p => p.Departamento == departamento)
        //             .ToListAsync();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por departamento: {Departamento}", departamento);
        //         throw;
        //     }
        // }

        public async Task<IEnumerable<Personal>> GetByDNIAsync(string dni)
        {
            try
            {
                return await _context.Personal
                    .Where(p => p.DNI == dni)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personal por DNI: {DNI}", dni);
                throw;
            }
        }

        // public async Task<IEnumerable<Personal>> GetByNombreAsync(string nombre)
        // {
        //     try
        //     {
        //         return await _context.Personal
        //             .Where(p => p.NombreTotal!.Contains(nombre) || 
        //                        p.Nombre!.Contains(nombre) ||
        //                        p.ApePaterno!.Contains(nombre) ||
        //                        p.ApeMaterno!.Contains(nombre))
        //             .ToListAsync();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por nombre: {Nombre}", nombre);
        //         throw;
        //     }
        // }

        // public async Task<IEnumerable<Personal>> GetByCenCostoAsync(string cenCosto)
        // {
        //     try
        //     {
        //         return await _context.Personal
        //             .Where(p => p.CenCosto == cenCosto)
        //             .ToListAsync();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por centro de costo: {CenCosto}", cenCosto);
        //         throw;
        //     }
        // }

        // public async Task<IEnumerable<Personal>> GetByTipoTrabajoAsync(string tipTrab)
        // {
        //     try
        //     {
        //         return await _context.Personal
        //             .Where(p => p.TipTrab == tipTrab)
        //             .ToListAsync();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error al obtener personal por tipo de trabajo: {TipTrab}", tipTrab);
        //         throw;
        //     }
        // }
    }
}
