using Microsoft.EntityFrameworkCore;
using AgendaForge.Domain.Entities;

namespace AgendaForge.Infrastructure.Data;

public class AgendaForgeDbContext : DbContext {
    public AgendaForgeDbContext(DbContextOptions<AgendaForgeDbContext> options) : base(options) { }
    
    public DbSet<TareaGlobal> TareasGlobales { get; set; }
}