using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Core.Entities;

namespace AgendaSaludApp.Application.Common
{
    public static class PascalCaseHelper
    {
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Select(word =>
                char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }


        public static void NormalizarPaciente(PacienteDto pacienteDto)
        {
            pacienteDto.Nombre = ToPascalCase(pacienteDto.Nombre);
            pacienteDto.Apellido = ToPascalCase(pacienteDto.Apellido);
            pacienteDto.Email = pacienteDto.Email?.ToLower();
        }

        public static void NormalizarProfesional(ProfesionalDto profesionalDto)
        {
            profesionalDto.Nombre = ToPascalCase(profesionalDto.Nombre);
            profesionalDto.Apellido = ToPascalCase(profesionalDto.Apellido);
            profesionalDto.Email = profesionalDto.Email?.ToLower();
        }
    }

    
}
