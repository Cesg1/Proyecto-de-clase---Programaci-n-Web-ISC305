namespace AgendaForge.Application.Features.Tareas.DTOs;

public class CreateTareaRequestDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}