# Propuesta Inicial de Proyecto – Programación Orientada a Objetos

**Carrera:** Ingeniería de Software  
**Materia:** Programación Orientada a Objetos  
**Periodo:** Segundo Parcial / Proyecto Final  
**Estudiante(s):** *Jesus Benjamin Oliva Blanco*  
**Fecha de entrega:** *AAAA-MM-DD*  

---
  
## 1. Datos Generales del Proyecto

| Campo | Descripción |
|--------|-------------|
| **Nombre del proyecto:** | *SaludSys* |
| **Tipo de aplicación:** | ☐ Web  |
| **Lenguaje / entorno de desarrollo:** | *C# Blazor* |
| **Repositorio Git (URL):** | *https://github.com/jesus-oliva-ctrl/hospital_gestor/edit/main* |
| **Uso de Inteligencia Artificial:** | ☐ Sí |

**Si usas IA, explica brevemente cómo y en qué etapa contribuye:**  


Durante el desarrollo del proyecto se utilizó ChatGPT (modelo GPT-5) como herramienta de apoyo para generar plantillas de clases, estructuras de base de datos y ejemplos de código, los cuales fueron comprendidos, adaptados y modificados según las necesidades del sistema hospitalario.
La IA se empleó únicamente como asistente de desarrollo y no como ejecutor directo del proyecto.

---

## 2. Descripción del Proyecto

### Resumen breve
MediTrack es un sistema hospitalario que permite gestionar la información de pacientes, médicos y personal administrativo desde una única plataforma.
Su objetivo es optimizar los procesos de registro, citas médicas y control de historial clínico.
Está dirigido a centros de salud que buscan mejorar la organización interna y la comunicación entre áreas.

### Objetivos principales
1.  Digitalizar la gestión hospitalaria, permitiendo registrar, consultar y actualizar información de pacientes, médicos y personal administrativo en una sola plataforma.
2.  Optimizar los procesos de atención médica, facilitando la programación de citas, el acceso a historiales clínicos y la comunicación entre los distintos usuarios del sistema.
3.  Asegurar la integridad y confidencialidad de los datos médicos, implementando un sistema de acceso seguro basado en roles para proteger la información sensible del paciente.

---

## 3. Diseño Técnico y Aplicación de POO

### Principios de POO aplicados
Marca los que planeas usar:
- [X] Encapsulamiento (atributos privados y métodos públicos)
- [X] Uso de constructores
- [X] Herencia
- [X] Polimorfismo
- [X] Interfaces o clases abstractas

### Clases estimadas
- **Cantidad inicial de clases:** 37  
- **Ejemplo de posibles clases:**
HospitalDbContext.cs 

Appointment.cs

Doctor.cs

Inventory.cs

MedicalHistory.cs

Medication.cs

Patient.cs

Prescription.cs

Role.cs

Specialty.cs

Staff.cs

User.cs

VwDoctorAgendaSummary.cs

Etc.

### Persistencia de datos
- [ ] Archivos locales  
- [X] Base de datos  
- [ ] En memoria (temporal)  

---

## 4. Funcionalidades Principales

| Nº | Nombre de la funcionalidad | Descripción breve | Estado actual |
|----|-----------------------------|-------------------|----------------|
| 1 | Autenticación y Sesión | Valida las credenciales del usuario contra la BD y mantiene su sesión activa. | ☐ En desarrollo |
| 2 | Autorización por Roles | Un componente de seguridad que restringe el acceso a páginas y menús según el rol del usuario. | ☐ En desarrollo |
| 3 | Gestión de informacion por Rol  | Cada Rol debe poder manejar su porpia informacion en corelacion con la Base de Datos y modificarla | ☐ Planeada |

> *(Agrega más filas si lo necesitas.)*

---

## 5. Compromiso del Estudiante

Declaro que:
- Entiendo los criterios de evaluación establecidos en las rúbricas.
- Presentaré una demostración funcional del proyecto.
- Defenderé el código que yo mismo implementé y explicaré las clases y métodos principales.
- Si usé herramientas de IA, comprendo su funcionamiento y las adapté al contexto del proyecto.

**Firma (nombre completo):** Jesus Benjamin Oliva Blanco  

---

## 6. Validación del Docente *(completa el profesor)*

| Campo | Detalle |
|--------|---------|
| **Visto bueno del docente:** | ☐ Aprobado para desarrollar ☐ Requiere ajustes ☐ Rechazado |
| **Comentarios / Observaciones:** |  |
| **Firma docente:** |  |
| **Fecha de revisión:** |  |

---

> **Instrucciones para entrega:**
> - Guarda este archivo como `README.md` dentro de tu repositorio Git.  
> - Completa todas las secciones antes de tu presentación inicial.  
> - No borres las casillas ni el formato para garantizar uniformidad del curso.  
> - El docente revisará y aprobará esta propuesta antes del desarrollo completo.

---
