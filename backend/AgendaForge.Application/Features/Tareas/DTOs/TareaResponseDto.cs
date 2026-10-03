namespace AgendaForge.Application.Features.Tareas.DTOs;

public class TareaResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTimeOffset FechaCreacion { get; set; } // Ajustado a DateTimeOffset
    public bool Completada { get; set; } // Añadido para que coincida con la entidad
}