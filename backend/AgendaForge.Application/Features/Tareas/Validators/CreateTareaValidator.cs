using FluentValidation;
using AgendaForge.Application.Features.Tareas.DTOs;

namespace AgendaForge.Application.Features.Tareas.Validators;

public class CreateTareaValidator : AbstractValidator<CreateTareaRequestDto>
{
    public CreateTareaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El título de la tarea no puede estar vacío.")
            .MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El título es demasiado largo.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");
    }
}