-- ============================================
-- ÍNDICES - MICROSERVICIO USER MANAGEMENT
-- ============================================
-- Proyecto: Sistema de Gestión Médica
-- Microservicio: UserManagement (Medical)
-- Descripción: Índices para optimizar consultas de usuarios médicos
-- ============================================

-- ============================================
-- ÍNDICES - medical_specialty
-- ============================================

CREATE INDEX IF NOT EXISTS idx_medical_specialty_name 
    ON medical_specialty USING btree (name);

CREATE INDEX IF NOT EXISTS idx_medical_specialty_active 
    ON medical_specialty USING btree (is_active) 
    WHERE (is_active = true);

COMMENT ON INDEX idx_medical_specialty_name IS 'Búsqueda de especialidades por nombre';
COMMENT ON INDEX idx_medical_specialty_active IS 'Filtrar especialidades activas';

-- ============================================
-- ÍNDICES - health_insurance
-- ============================================

CREATE INDEX IF NOT EXISTS idx_health_insurance_name 
    ON health_insurance USING btree (name);

CREATE INDEX IF NOT EXISTS idx_health_insurance_type 
    ON health_insurance USING btree (type);

CREATE INDEX IF NOT EXISTS idx_health_insurance_active 
    ON health_insurance USING btree (is_active) 
    WHERE (is_active = true);

COMMENT ON INDEX idx_health_insurance_name IS 'Búsqueda de coberturas por nombre';
COMMENT ON INDEX idx_health_insurance_type IS 'Filtrar por tipo de cobertura';

-- ============================================
-- ÍNDICES - patient
-- ============================================

-- Índice por user_id (FK a Auth Service)
CREATE INDEX IF NOT EXISTS idx_patient_user_id 
    ON patient USING btree (user_id);

-- Índice por DNI (búsqueda frecuente)
CREATE INDEX IF NOT EXISTS idx_patient_dni 
    ON patient USING btree (dni);

-- Índice por nombre completo
CREATE INDEX IF NOT EXISTS idx_patient_name 
    ON patient USING btree (last_name, first_name);

-- Índice por cobertura
CREATE INDEX IF NOT EXISTS idx_patient_health_insurance_id 
    ON patient USING btree (health_insurance_id);

-- Índice por estado activo
CREATE INDEX IF NOT EXISTS idx_patient_is_active 
    ON patient USING btree (is_active) 
    WHERE (is_active = true);

-- Índice por fecha de creación
CREATE INDEX IF NOT EXISTS idx_patient_created_at 
    ON patient USING btree (created_at DESC);

COMMENT ON INDEX idx_patient_user_id IS 'CRÍTICO: Relación con Auth Service';
COMMENT ON INDEX idx_patient_dni IS 'Búsqueda rápida por documento';
COMMENT ON INDEX idx_patient_name IS 'Búsqueda por apellido y nombre';

-- ============================================
-- ÍNDICES - professional
-- ============================================

-- Índice por user_id (FK a Auth Service)
CREATE INDEX IF NOT EXISTS idx_professional_user_id 
    ON professional USING btree (user_id);

-- Índice por DNI
CREATE INDEX IF NOT EXISTS idx_professional_dni 
    ON professional USING btree (dni);

-- Índice por matrícula
CREATE INDEX IF NOT EXISTS idx_professional_license 
    ON professional USING btree (medical_license);

-- Índice por especialidad
CREATE INDEX IF NOT EXISTS idx_professional_specialty_id 
    ON professional USING btree (specialty_id);

-- Índice por nombre completo
CREATE INDEX IF NOT EXISTS idx_professional_name 
    ON professional USING btree (last_name, first_name);

-- Índice por estado activo
CREATE INDEX IF NOT EXISTS idx_professional_is_active 
    ON professional USING btree (is_active) 
    WHERE (is_active = true);

-- Índice compuesto: especialidad + activo
CREATE INDEX IF NOT EXISTS idx_professional_specialty_active 
    ON professional USING btree (specialty_id, is_active) 
    WHERE (is_active = true);

COMMENT ON INDEX idx_professional_user_id IS 'CRÍTICO: Relación con Auth Service';
COMMENT ON INDEX idx_professional_specialty_id IS 'Búsqueda por especialidad';
COMMENT ON INDEX idx_professional_specialty_active IS 'Profesionales activos por especialidad';

-- ============================================
-- ÍNDICES - schedule_manager
-- ============================================

-- Índice por user_id (FK a Auth Service)
CREATE INDEX IF NOT EXISTS idx_schedule_manager_user_id 
    ON schedule_manager USING btree (user_id);

-- Índice por DNI
CREATE INDEX IF NOT EXISTS idx_schedule_manager_dni 
    ON schedule_manager USING btree (dni);

-- Índice por nombre completo
CREATE INDEX IF NOT EXISTS idx_schedule_manager_name 
    ON schedule_manager USING btree (last_name, first_name);

-- Índice por estado activo
CREATE INDEX IF NOT EXISTS idx_schedule_manager_is_active 
    ON schedule_manager USING btree (is_active) 
    WHERE (is_active = true);

COMMENT ON INDEX idx_schedule_manager_user_id IS 'CRÍTICO: Relación con Auth Service';