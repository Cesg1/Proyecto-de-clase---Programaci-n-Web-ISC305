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
    private readonly IValidator<CreateTareaRequestDto> _createValidator;
    private readonly IValidator<UpdateTareaRequestDto> _updateValidator;
    // Inyección de Dependencias en acción
    public TareasController(
        IAgendaRepository repository, 
        IValidator<CreateTareaRequestDto> createValidator,
        IValidator<UpdateTareaRequestDto> updateValidator) {
        _repository = repository; 
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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
    
    // GET por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTareaById(int id) {
        var tarea = await _repository.GetByIdAsync(id);
        
        if (tarea == null) {
            return NotFound($"No se encontró la tarea con el ID {id}."); // HTTP 404
        }

        var response = new TareaResponseDto {
            Id = tarea.Id,
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion,
            FechaCreacion = tarea.FechaCreacion,
            Completada = tarea.Completada
        };

        return Ok(response); // HTTP 200 OK
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
    
    //VERBO PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarTarea(int id, [FromBody] UpdateTareaRequestDto request) {
        // 1. Validar con FluentValidation
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors); // HTTP 400
        }

        // 2. Verificar si el recurso existe
        var tareaExistente = await _repository.GetByIdAsync(id);
        if (tareaExistente == null) {
            return NotFound($"No se encontró la tarea con el ID {id} para actualizar."); // HTTP 404
        }

        // 3. Modificar las propiedades de la entidad
        tareaExistente.Nombre = request.Nombre;
        tareaExistente.Descripcion = request.Descripcion;
        tareaExistente.Completada = request.Completada;

        // 4. Persistir cambios
        await _repository.UpdateAsync(tareaExistente);

        return NoContent(); // HTTP 204 No Content (Éxito sin cuerpo de respuesta)
    }
    
    
    //VERBO DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarTarea(int id) {
        // 1. Verificar si existe
        var tarea = await _repository.GetByIdAsync(id);
        if (tarea == null) {
            return NotFound($"No se encontró la tarea con el ID {id} para eliminar."); // HTTP 404
        }

        // 2. Borrar físicamente de la base de datos
        await _repository.DeleteAsync(tarea);

        return NoContent(); // HTTP 204 No Content (Éxito al borrar)
    }
    
    
}