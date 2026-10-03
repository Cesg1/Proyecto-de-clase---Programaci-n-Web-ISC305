using AgendaForge.Application.Interfaces;
using AgendaForge.Domain.Entities;
using AgendaForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendaForge.Infrastructure.Repositories;

public class AgendaRepository : IAgendaRepository {
    private readonly AgendaForgeDbContext _context;

    public AgendaRepository(AgendaForgeDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<TareaGlobal>> GetAllAsync() {
        return await _context.TareasGlobales.ToListAsync();
    }

    public async Task<TareaGlobal> AddAsync(TareaGlobal tarea) {
        await _context.TareasGlobales.AddAsync(tarea);
        
        // SaveChangesAsync envuelve la operación en una transacción SQL (COMMIT)
        await _context.SaveChangesAsync(); 
        
        return tarea;
    }
}