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
    
    public async Task<TareaGlobal?> GetByIdAsync(int id) {
        // FindAsync busca directamente por la llave primaria (Id)
        return await _context.TareasGlobales.FindAsync(id);
    }

    public async Task UpdateAsync(TareaGlobal tarea) {
        _context.TareasGlobales.Update(tarea);
        await _context.SaveChangesAsync(); // Ejecuta el UPDATE y hace commit
    }

    public async Task DeleteAsync(TareaGlobal tarea) {
        _context.TareasGlobales.Remove(tarea);
        await _context.SaveChangesAsync(); // Ejecuta el DELETE físico y hace commit
    }
}