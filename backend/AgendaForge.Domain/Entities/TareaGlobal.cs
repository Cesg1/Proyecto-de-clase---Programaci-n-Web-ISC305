namespace AgendaForge.Domain.Entities;

public class TareaGlobal
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTimeOffset FechaCreacion { get; set; } = DateTimeOffset.UtcNow;
    public bool Completada { get; set; } = false;
    
}