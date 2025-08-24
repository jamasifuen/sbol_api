using PersonalAPI.Models;

namespace PersonalAPI.Services
{
    public interface IPersonalService
    {
        Task<IEnumerable<Personal>> GetAllAsync();
        Task<Personal?> GetByCodigoAsync(string codigo);
        // Task<Personal> CreateAsync(Personal personal);
        // Task<Personal?> UpdateAsync(string codigo, Personal personal);
        // Task<bool> DeleteAsync(string codigo);
        // Task<IEnumerable<Personal>> GetByDepartamentoAsync(string departamento);
        Task<IEnumerable<Personal>> GetByDNIAsync(string dni);
        // Task<IEnumerable<Personal>> GetByNombreAsync(string nombre);
        // Task<IEnumerable<Personal>> GetByCenCostoAsync(string cenCosto);
        // Task<IEnumerable<Personal>> GetByTipoTrabajoAsync(string tipTrab);
    }
}
