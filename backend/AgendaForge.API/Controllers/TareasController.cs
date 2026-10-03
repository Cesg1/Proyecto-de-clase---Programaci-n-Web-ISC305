using AgendaForge.Application.Features.Tareas.DTOs;
using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using AgendaForge.Application.Interfaces;
using AgendaForge.Domain.Entities;

namespace AgendaForge.API.Controllers;

[ApiController]
[Route("api/[controller]")] // Define la ruta base: http://localhost:puerto/api/tareas
public class TareasController : ControllerBase {
    private readonly IAgendaRepository _repository;
    private readonly IValidator<CreateTareaRequestDto> _validator;
    // Inyección de Dependencias en acción
    public TareasController(IAgendaRepository repository) {
        _repository = repository; 
    }

    // VERBO GET: Solicitar lectura de datos
    [HttpGet]
    public async Task<IActionResult> GetTareas() {
        var tareas = await _repository.GetAllAsync();
        
        // Mapeo Manual: Entidad -> DTO
        var response = tareas.Select(t => new TareaResponseDto {
            Id = t.Id,
            Nombre = t.Nombre,
            Descripcion = t.Descripcion,
            FechaCreacion = t.FechaCreacion
        });

        return Ok(response);
    }

    // VERBO POST: Solicitar creación de un recurso
    [HttpPost]
    public async Task<IActionResult> CrearTarea([FromBody] CreateTareaRequestDto request) {
        // 1. Ejecutar FluentValidation
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors); // HTTP 400 automático con detalles
        }

        // 2. Mapeo Manual: DTO -> Entidad (Solo transferimos lo permitido)
        var nuevaTarea = new TareaGlobal {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            FechaCreacion = DateTime.UtcNow
        };

        // 3. Persistencia
        var tareaCreada = await _repository.AddAsync(nuevaTarea);
        
        // 4. Mapeo de Retorno
        var response = new TareaResponseDto {
            Id = tareaCreada.Id,
            Nombre = tareaCreada.Nombre,
            Descripcion = tareaCreada.Descripcion,
            FechaCreacion = tareaCreada.FechaCreacion
        };

        return CreatedAtAction(nameof(GetTareas), new { id = response.Id }, response);
    }
}