﻿using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Core.Entities
{
    public class AgendaCitas
    {
        [Key]
        public int Id { get; set; }
       
        public DateOnly Fecha { get; set; }

        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }

        public bool Ocupado { get; set; }

        public int ProfesionalId { get; set; }
        public required Profesional Profesional { get; set; }

        public int PacienteId { get; set; }
        public required Paciente Paciente { get; set; }

        public int MotivoCitaId { get; set; }
        public required MotivoCita MotivoCita { get; set; }

        public int EstadoCitaId { get; set; }
        public required EstadoCita EstadoCita { get; set; }

        public int UsuarioId { get; set; }

        public bool Vencida { get; set; }   


    }
}