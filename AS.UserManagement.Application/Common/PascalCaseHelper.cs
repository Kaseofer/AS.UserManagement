using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.Patient;
using AS.UserManagement.Application.Dtos.Professional;
using AS.UserManagement.Core.Entities;

namespace AS.UserManagement.Application.Common
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


        public static void NormalizePatient(PatientResponseDto pacienteDto)
        {
            pacienteDto.FirstName = ToPascalCase(pacienteDto.FirstName);
            pacienteDto.LastName = ToPascalCase(pacienteDto.LastName);
            pacienteDto.Email = pacienteDto.Email?.ToLower();
        }

        public static void NormalizeProfessional(CreateProfessionalDto profesionalDto)
        {
            profesionalDto.FirstName = ToPascalCase(profesionalDto.FirstName);
            profesionalDto.LastName = ToPascalCase(profesionalDto.LastName);
            profesionalDto.Email = profesionalDto.Email?.ToLower();
        }


        public static void NormalizeProfessional(UpdateProfessionalDto updateProfesionalDto)
        {
            updateProfesionalDto.FirstName = ToPascalCase(updateProfesionalDto.FirstName);
            updateProfesionalDto.LastName = ToPascalCase(updateProfesionalDto.LastName);
            updateProfesionalDto.Email = updateProfesionalDto.Email?.ToLower();
        }

        public static void NormalizePatient(UpdatePatientDto updatePacienteDto)
        {
            updatePacienteDto.FirstName = ToPascalCase(updatePacienteDto.FirstName);
            updatePacienteDto.LastName = ToPascalCase(updatePacienteDto.LastName);
            updatePacienteDto.Email = updatePacienteDto.Email?.ToLower();
        }

        public static void NormalizePatient(CreatePatientDto createPacienteDto)
        {
            createPacienteDto.FirstName = ToPascalCase(createPacienteDto.FirstName);
            createPacienteDto.LastName = ToPascalCase(createPacienteDto.LastName);
            createPacienteDto.Email = createPacienteDto.Email?.ToLower();
        }
    }

    
}
