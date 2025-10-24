-- ============================================
-- DATOS INICIALES (SEED DATA) - MICROSERVICIO USER MANAGEMENT
-- ============================================
-- Proyecto: Sistema de Gestión Médica
-- Microservicio: UserManagement (Medical)
-- Descripción: Especialidades médicas y coberturas de salud
-- ============================================

-- ============================================
-- ESPECIALIDADES MÉDICAS
-- ============================================

INSERT INTO medical_specialty (name, description, is_active) VALUES
    ('Cardiología', 'Especialidad médica que se ocupa de las enfermedades del corazón', true),
    ('Pediatría', 'Especialidad médica que se enfoca en la salud de los niños', true),
    ('Traumatología', 'Especialidad que trata las lesiones del aparato locomotor', true),
    ('Ginecología', 'Especialidad médica que trata las enfermedades del sistema reproductor femenino', true),
    ('Dermatología', 'Especialidad que trata las enfermedades de la piel', true),
    ('Oftalmología', 'Especialidad médica que trata las enfermedades de los ojos', true),
    ('Neurología', 'Especialidad que se ocupa del sistema nervioso', true),
    ('Psiquiatría', 'Especialidad médica que se ocupa de la salud mental', true),
    ('Odontología', 'Especialidad que trata las enfermedades de los dientes y encías', true),
    ('Nutrición', 'Especialidad que se ocupa de la alimentación y nutrición', true),
    ('Medicina General', 'Médico clínico general', true),
    ('Gastroenterología', 'Especialidad que trata el aparato digestivo', true),
    ('Endocrinología', 'Especialidad que trata las glándulas endocrinas', true),
    ('Urología', 'Especialidad que trata el aparato urinario', true),
    ('Otorrinolaringología', 'Especialidad que trata oídos, nariz y garganta', true),
    ('Kinesiología', 'Especialidad que trata mediante ejercicios y fisioterapia', true),
    ('Psicología', 'Especialidad que trata la salud mental y emocional', true),
    ('Reumatología', 'Especialidad que trata enfermedades del sistema musculoesquelético', true),
    ('Alergología', 'Especialidad que trata las alergias', true),
    ('Anestesiología', 'Especialidad que se ocupa de la anestesia', true)
ON CONFLICT (name) DO UPDATE SET
    description = EXCLUDED.description,
    is_active = EXCLUDED.is_active;

-- ============================================
-- OBRAS SOCIALES Y PREPAGAS (ARGENTINA)
-- ============================================

INSERT INTO health_insurance (name, type, is_active) VALUES
    -- PREPAGAS
    ('Swiss Medical', 'PREPAGA', true),
    ('OSDE', 'PREPAGA', true),
    ('Galeno', 'PREPAGA', true),
    ('Medicus', 'PREPAGA', true),
    ('Omint', 'PREPAGA', true),
    ('Medifé', 'PREPAGA', true),
    ('Hospital Alemán', 'PREPAGA', true),
    ('Hospital Británico', 'PREPAGA', true),
    ('Hospital Italiano', 'PREPAGA', true),
    ('Accord Salud', 'PREPAGA', true),
    
    -- OBRAS SOCIALES
    ('OSECAC', 'OBRA_SOCIAL', true),
    ('OSDE Binario', 'OBRA_SOCIAL', true),
    ('OSPEDYC', 'OBRA_SOCIAL', true),
    ('OSUTHGRA', 'OBRA_SOCIAL', true),
    ('OSDEPYM', 'OBRA_SOCIAL', true),
    ('Unión Personal', 'OBRA_SOCIAL', true),
    ('OSPE', 'OBRA_SOCIAL', true),
    ('OSPOCE', 'OBRA_SOCIAL', true),
    ('OSMATA', 'OBRA_SOCIAL', true),
    ('OSMEDICA', 'OBRA_SOCIAL', true),
    ('IOMA', 'OBRA_SOCIAL', true),
    ('PAMI', 'OBRA_SOCIAL', true),
    ('OSPJN', 'OBRA_SOCIAL', true),
    ('Obra Social Ciudad de Buenos Aires', 'OBRA_SOCIAL', true),
    ('Obra Social Ferroviaria', 'OBRA_SOCIAL', true),
    ('Obra Social del Personal de Luz y Fuerza', 'OBRA_SOCIAL', true),
    ('OSPLYFC', 'OBRA_SOCIAL', true),
    ('OSBA', 'OBRA_SOCIAL', true),
    ('AMUR', 'OBRA_SOCIAL', true),
    
    -- PARTICULAR
    ('Particular', 'PARTICULAR', true),
    ('Efectivo', 'PARTICULAR', true)
ON CONFLICT (name) DO UPDATE SET
    type = EXCLUDED.type,
    is_active = EXCLUDED.is_active;

-- ============================================
-- VERIFICACIÓN
-- ============================================

DO $$
DECLARE
    v_specialty_count INTEGER;
    v_insurance_count INTEGER;
    v_prepaga_count INTEGER;
    v_obra_social_count INTEGER;
    v_particular_count INTEGER;
BEGIN
    SELECT COUNT(*) INTO v_specialty_count FROM medical_specialty;
    SELECT COUNT(*) INTO v_insurance_count FROM health_insurance;
    SELECT COUNT(*) INTO v_prepaga_count FROM health_insurance WHERE type = 'PREPAGA';
    SELECT COUNT(*) INTO v_obra_social_count FROM health_insurance WHERE type = 'OBRA_SOCIAL';
    SELECT COUNT(*) INTO v_particular_count FROM health_insurance WHERE type = 'PARTICULAR';
    
    RAISE NOTICE '';
    RAISE NOTICE '═══════════════════════════════════════════════════════';
    RAISE NOTICE '✓ DATOS INICIALES INSERTADOS';
    RAISE NOTICE '═══════════════════════════════════════════════════════';
    RAISE NOTICE '';
    RAISE NOTICE '📊 Resumen:';
    RAISE NOTICE '  - Especialidades médicas: %', v_specialty_count;
    RAISE NOTICE '  - Coberturas totales: %', v_insurance_count;
    RAISE NOTICE '    • Prepagas: %', v_prepaga_count;
    RAISE NOTICE '    • Obras Sociales: %', v_obra_social_count;
    RAISE NOTICE '    • Particulares: %', v_particular_count;
    RAISE NOTICE '';
    RAISE NOTICE '🏥 Especialidades más comunes:';
    RAISE NOTICE '  - Medicina General';
    RAISE NOTICE '  - Cardiología';
    RAISE NOTICE '  - Pediatría';
    RAISE NOTICE '  - Traumatología';
    RAISE NOTICE '  - Ginecología';
    RAISE NOTICE '';
    RAISE NOTICE '💳 Coberturas principales:';
    RAISE NOTICE '  - Swiss Medical, OSDE, Galeno (Prepagas)';
    RAISE NOTICE '  - OSECAC, IOMA, PAMI (Obras Sociales)';
    RAISE NOTICE '  - Particular / Efectivo';
    RAISE NOTICE '';
    RAISE NOTICE '⚠️  IMPORTANTE:';
    RAISE NOTICE '  - Los datos pueden ampliarse según necesidad';
    RAISE NOTICE '  - Mantener actualizadas las coberturas';
    RAISE NOTICE '  - Las especialidades son las más comunes en Argentina';
    RAISE NOTICE '';
    RAISE NOTICE '═══════════════════════════════════════════════════════';
    RAISE NOTICE '';
END $$;