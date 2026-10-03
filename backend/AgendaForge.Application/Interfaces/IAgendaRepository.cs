using AgendaForge.Domain.Entities;

namespace AgendaForge.Application.Interfaces;

public interface IAgendaRepository {
    Task<IEnumerable<TareaGlobal>> GetAllAsync();
    Task<TareaGlobal> AddAsync(TareaGlobal tarea); // Contrato actualizado para el POST
}