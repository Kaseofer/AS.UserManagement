-- ============================================
-- CONSTRAINTS Y CLAVES - MICROSERVICIO USER MANAGEMENT
-- ============================================
-- Proyecto: Sistema de Gestión Médica
-- Microservicio: UserManagement (Medical)
-- Descripción: Primary Keys, Unique Constraints y Foreign Keys
-- ============================================

-- ============================================
-- PRIMARY KEYS (ya definidas en CREATE TABLE)
-- ============================================
-- medical_specialty.id (SERIAL)
-- health_insurance.id (SERIAL)
-- patient.id (SERIAL)
-- professional.id (UUID)
-- schedule_manager.id (UUID)

-- ============================================
-- UNIQUE CONSTRAINTS (ya definidas en CREATE TABLE)
-- ============================================
-- medical_specialty.name (UNIQUE)
-- health_insurance.name (UNIQUE)
-- patient.user_id (UNIQUE)
-- patient.dni (UNIQUE)
-- professional.user_id (UNIQUE)
-- professional.dni (UNIQUE)
-- professional.medical_license (UNIQUE)
-- schedule_manager.user_id (UNIQUE)
-- schedule_manager.dni (UNIQUE)

-- ============================================
-- FOREIGN KEYS
-- ============================================

-- patient -> health_insurance
ALTER TABLE ONLY patient
    ADD CONSTRAINT patient_health_insurance_id_fkey 
    FOREIGN KEY (health_insurance_id) 
    REFERENCES health_insurance(id) 
    ON DELETE SET NULL;

-- professional -> medical_specialty
ALTER TABLE ONLY professional
    ADD CONSTRAINT professional_specialty_id_fkey 
    FOREIGN KEY (specialty_id) 
    REFERENCES medical_specialty(id) 
    ON DELETE RESTRICT;

-- ============================================
-- CHECK CONSTRAINTS ADICIONALES
-- ============================================

-- patient: validar fecha de nacimiento no sea futura
ALTER TABLE patient
    ADD CONSTRAINT check_patient_birth_date 
    CHECK (birth_date <= CURRENT_DATE);

-- patient: validar edad mínima (0 años) y máxima (150 años)
ALTER TABLE patient
    ADD CONSTRAINT check_patient_age 
    CHECK (birth_date >= CURRENT_DATE - INTERVAL '150 years');

-- professional: validar consultation_fee no sea negativo
ALTER TABLE professional
    ADD CONSTRAINT check_professional_fee 
    CHECK (consultation_fee IS NULL OR consultation_fee >= 0);

-- professional: validar años de experiencia
ALTER TABLE professional
    ADD CONSTRAINT check_professional_experience 
    CHECK (years_experience IS NULL OR years_experience >= 0);

-- ============================================
-- COMENTARIOS
-- ============================================

COMMENT ON CONSTRAINT patient_health_insurance_id_fkey ON patient 
    IS 'Relación paciente con cobertura - SET NULL mantiene paciente aunque se elimine cobertura';

COMMENT ON CONSTRAINT professional_specialty_id_fkey ON professional 
    IS 'Relación profesional con especialidad - RESTRICT evita eliminar especialidades en uso';

COMMENT ON CONSTRAINT check_patient_birth_date ON patient 
    IS 'Valida que la fecha de nacimiento no sea futura';

COMMENT ON CONSTRAINT check_professional_fee ON professional 
    IS 'Valida que el honorario no sea negativo';