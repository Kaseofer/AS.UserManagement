-- ============================================
-- CREACIÓN DE TABLAS - MICROSERVICIO USER MANAGEMENT
-- ============================================
-- Proyecto: Sistema de Gestión Médica
-- Microservicio: UserManagement (Medical)
-- Base de Datos: PostgreSQL 17
-- Fecha: 2025-10-23
-- ============================================

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- ============================================
-- SEQUENCES
-- ============================================

CREATE SEQUENCE IF NOT EXISTS medical_specialty_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

CREATE SEQUENCE IF NOT EXISTS health_insurance_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

CREATE SEQUENCE IF NOT EXISTS patient_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

-- ============================================
-- TABLA: medical_specialty
-- Descripción: Especialidades médicas
-- ============================================

CREATE TABLE IF NOT EXISTS medical_specialty (
    id INTEGER DEFAULT nextval('medical_specialty_id_seq'::regclass) PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

COMMENT ON TABLE medical_specialty IS 'Especialidades médicas del sistema';
COMMENT ON COLUMN medical_specialty.name IS 'Nombre de la especialidad: Cardiología, Pediatría, etc.';

-- ============================================
-- TABLA: health_insurance
-- Descripción: Coberturas de salud (Obras Sociales y Prepagas)
-- ============================================

CREATE TABLE IF NOT EXISTS health_insurance (
    id INTEGER DEFAULT nextval('health_insurance_id_seq'::regclass) PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    type VARCHAR(20) NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT check_insurance_type CHECK (type IN ('PREPAGA', 'OBRA_SOCIAL', 'PARTICULAR'))
);

COMMENT ON TABLE health_insurance IS 'Obras sociales, prepagas y particulares';
COMMENT ON COLUMN health_insurance.type IS 'Tipo de cobertura: PREPAGA, OBRA_SOCIAL, PARTICULAR';

-- ============================================
-- TABLA: patient
-- Descripción: Pacientes del sistema
-- ============================================

CREATE TABLE IF NOT EXISTS patient (
    id INTEGER DEFAULT nextval('patient_id_seq'::regclass) PRIMARY KEY,
    user_id UUID NOT NULL UNIQUE,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    dni VARCHAR(20) NOT NULL UNIQUE,
    birth_date DATE NOT NULL,
    gender VARCHAR(1) NOT NULL,
    phone VARCHAR(20),
    address TEXT,
    city VARCHAR(100),
    state VARCHAR(100),
    postal_code VARCHAR(10),
    health_insurance_id INTEGER,
    insurance_number VARCHAR(50),
    emergency_contact_name VARCHAR(100),
    emergency_contact_phone VARCHAR(20),
    blood_type VARCHAR(5),
    allergies TEXT,
    chronic_conditions TEXT,
    notes TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT check_gender CHECK (gender IN ('M', 'F', 'X'))
);

COMMENT ON TABLE patient IS 'Pacientes registrados en el sistema';
COMMENT ON COLUMN patient.user_id IS 'UUID del usuario en el microservicio de autenticación';
COMMENT ON COLUMN patient.dni IS 'Documento Nacional de Identidad';
COMMENT ON COLUMN patient.gender IS 'M = Masculino, F = Femenino, X = No binario';
COMMENT ON COLUMN patient.health_insurance_id IS 'FK a health_insurance';

-- ============================================
-- TABLA: professional
-- Descripción: Profesionales de la salud
-- ============================================

CREATE TABLE IF NOT EXISTS professional (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    user_id UUID NOT NULL UNIQUE,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    dni VARCHAR(20) NOT NULL UNIQUE,
    medical_license VARCHAR(50) NOT NULL UNIQUE,
    specialty_id INTEGER NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(255),
    office_address TEXT,
    consultation_fee DECIMAL(10,2),
    bio TEXT,
    years_experience INTEGER,
    education TEXT,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITHOUT TIME ZONE
);

COMMENT ON TABLE professional IS 'Profesionales de la salud registrados';
COMMENT ON COLUMN professional.user_id IS 'UUID del usuario en el microservicio de autenticación';
COMMENT ON COLUMN professional.medical_license IS 'Matrícula profesional';
COMMENT ON COLUMN professional.specialty_id IS 'FK a medical_specialty';
COMMENT ON COLUMN professional.consultation_fee IS 'Precio de consulta';

-- ============================================
-- TABLA: schedule_manager
-- Descripción: Gestores de agenda (secretarias, administrativos)
-- ============================================

CREATE TABLE IF NOT EXISTS schedule_manager (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    user_id UUID NOT NULL UNIQUE,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    dni VARCHAR(20) NOT NULL UNIQUE,
    phone VARCHAR(20),
    email VARCHAR(255),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITHOUT TIME ZONE
);

COMMENT ON TABLE schedule_manager IS 'Gestores de agenda y disponibilidad';
COMMENT ON COLUMN schedule_manager.user_id IS 'UUID del usuario en el microservicio de autenticación';