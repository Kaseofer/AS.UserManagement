using AgendaSaludApp.Application.Dtos;
using FluentValidation;

namespace AgendaSaludApp.Application.Validators
{
    public class PacienteDtoValidator : AbstractValidator<PacienteDto>
    {
        public PacienteDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres");

            RuleFor(x => x.Dni)
                .NotEmpty().WithMessage("El DNI es obligatorio")
                .MaximumLength(20).WithMessage("Máximo 20 caracteres");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria")
                .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("La fecha debe ser anterior a hoy");

            RuleFor(x => x.Sexo)
                .NotEmpty().WithMessage("El sexo es obligatorio")
                .MaximumLength(1).WithMessage("Máximo 10 caracteres");

            RuleFor(x => x.Telefono)
                .MaximumLength(20).WithMessage("Máximo 20 caracteres");

            RuleFor(x => x.Email)
                .MaximumLength(100).WithMessage("Máximo 100 caracteres")
                .EmailAddress().WithMessage("Formato de email inválido");

            RuleFor(x => x.Observaciones)
                .MaximumLength(500).WithMessage("Máximo 500 caracteres");
        }
    }

}
