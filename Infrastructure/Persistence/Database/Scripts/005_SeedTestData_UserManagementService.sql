-- ============================================
-- DATOS DE PRUEBA (SOLO DESARROLLO) - MICROSERVICIO USER MANAGEMENT
-- ============================================
-- Proyecto: Sistema de Gestión Médica
-- Microservicio: UserManagement (Medical)
-- Descripción: Datos de ejemplo para testing
-- ⚠️ ADVERTENCIA: NO ejecutar en producción
-- ============================================

-- ============================================
-- PACIENTES DE PRUEBA
-- ============================================

INSERT INTO patient (
    user_id,
    first_name,
    last_name,
    dni,
    birth_date,
    gender,
    phone,
    address,
    city,
    state,
    health_insurance_id,
    insurance_number,
    blood_type,
    is_active
) VALUES
    (
        gen_random_uuid(),
        'María',
        'González',
        '35123456',
        '1990-05-15',
        'F',
        '+54 9 11 1234-5678',
        'Av. Corrientes 1234',
        'Buenos Aires',
        'CABA',
        (SELECT id FROM health_insurance WHERE name = 'OSDE' LIMIT 1),
        '001234567',
        'O+',
        true
    ),
    (
        gen_random_uuid(),
        'Juan',
        'Pérez',
        '28987654',
        '1985-10-20',
        'M',
        '+54 9 11 9876-5432',
        'Calle Falsa 123',
        'Buenos Aires',
        'CABA',
        (SELECT id FROM health_insurance WHERE name = 'Swiss Medical' LIMIT 1),
        '987654321',
        'A+',
        true
    ),
    (
        gen_random_uuid(),
        'Laura',
        'Fernández',
        '40456789',
        '1995-03-08',
        'F',
        '+54 9 11 5555-1234',
        'San Martín 456',
        'La Plata',
        'Buenos Aires',
        (SELECT id FROM health_insurance WHERE name = 'IOMA' LIMIT 1),
        'IOMA123456',
        'B-',
        true
    )
ON CONFLICT (dni) DO NOTHING;

-- ============================================
-- PROFESIONALES DE PRUEBA
-- ============================================

INSERT INTO professional (
    id,
    user_id,
    first_name,
    last_name,
    dni,
    medical_license,
    specialty_id,
    phone,
    email,
    office_address,
    consultation_fee,
    bio,
    years_experience,
    is_active
) VALUES
    (
        '01993471-90a4-7ce0-baa7-28015ca145bf',
        '01993471-90a4-7ce0-baa7-28015ca145bf',  -- Mismo UUID que en AuthService
        'Juan',
        'Pérez',
        '30123456',
        'MN123456',
        (SELECT id FROM medical_specialty WHERE name = 'Cardiología' LIMIT 1),
        '+54 9 11 2345-6789',
        'dr.perez@hospital.com',
        'Av. Santa Fe 1234, CABA',
        15000.00,
        'Especialista en Cardiología con más de 15 años de experiencia',
        15,
        true
    )
ON CONFLICT (dni) DO NOTHING;

-- ============================================
-- GESTORES DE AGENDA DE PRUEBA
-- ============================================

INSERT INTO schedule_manager (
    user_id,
    first_name,
    last_name,
    dni,
    phone,
    email,
    is_active
) VALUES
    (
        gen_random_uuid(),
        'Ana',
        'Martínez',
        '32456789',
        '+54 9 11 3456-7890',
        'ana.martinez@clinica.com',
        true
    )
ON CONFLICT (dni) DO NOTHING;

-- ============================================
-- VERIFICACIÓN
-- ============================================

DO $$
BEGIN
    RAISE NOTICE '';
    RAISE NOTICE '═══════════════════════════════════════════════════════';
    RAISE NOTICE '✓ DATOS DE PRUEBA INSERTADOS';
    RAISE NOTICE '═══════════════════════════════════════════════════════';
    RAISE NOTICE '';
    RAISE NOTICE '📊 Datos creados:';
    RAISE NOTICE '  - Pacientes: %', (SELECT COUNT(*) FROM patient);
    RAISE NOTICE '  - Profesionales: %', (SELECT COUNT(*) FROM professional);
    RAISE NOTICE '  - Gestores de agenda: %', (SELECT COUNT(*) FROM schedule_manager);
    RAISE NOTICE '';
    RAISE NOTICE '👤 Pacientes de prueba:';
    RAISE NOTICE '  - María González (DNI: 35123456, OSDE)';
    RAISE NOTICE '  - Juan Pérez (DNI: 28987654, Swiss Medical)';
    RAISE NOTICE '  - Laura Fernández (DNI: 40456789, IOMA)';
    RAISE NOTICE '';
    RAISE NOTICE '👨‍⚕️ Profesionales de prueba:';
    RAISE NOTICE '  - Dr. Juan Pérez (MN123456, Cardiología)';
    RAISE NOTICE '';
    RAISE NOTICE '📅 Gestores de agenda:';
    RAISE NOTICE '  - Ana Martínez';
    RAISE NOTICE '';
    RAISE NOTICE '⚠️  ¡IMPORTANTE! ⚠️';
    RAISE NOTICE '  - Estos datos son SOLO para desarrollo/testing';
    RAISE NOTICE '  - NUNCA usar en producción';
    RAISE NOTICE '  - Los user_id deben coincidir con AuthService';
    RAISE NOTICE '  - Eliminar este archivo antes de deploy a producción';
    RAISE NOTICE '';
    RAISE NOTICE '═══════════════════════════════════════════════════════';
    RAISE NOTICE '';
END $$;