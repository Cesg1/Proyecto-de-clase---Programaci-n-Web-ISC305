using FluentValidation;
using AgendaForge.Application.Features.Tareas.DTOs;

namespace AgendaForge.Application.Features.Tareas.Validators;

public class UpdateTareaValidator : AbstractValidator<UpdateTareaRequestDto> {
    public UpdateTareaValidator() {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la tarea es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");
    }
}