namespace AgendaForge.Application.Features.Tareas.DTOs;

public class UpdateTareaRequestDto {
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Completada { get; set; }
}