# 🏥 SaludSys - Sistema de Gestión Hospitalaria

Sistema integral de gestión hospitalaria desarrollado con **Blazor Server** y **.NET 8**, que integra tanto bases de datos relacionales (SQL Server) como NoSQL (MongoDB) para proporcionar una solución completa y escalable.

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Arquitectura](#-arquitectura)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Roles y Funcionalidades](#-roles-y-funcionalidades)
- [Patrones de Diseño](#-patrones-de-diseño)
- [Autor](#-autor)

## ✨ Características

### Funcionalidades Principales

- 🔐 **Sistema de Autenticación Multi-Rol**
  - Pacientes, Médicos, Laboratoristas y Personal Administrativo

- 📅 **Gestión de Citas Médicas**
  - Agendamiento y cancelación de citas
  - Validación de disponibilidad
  - Reagendamiento automático

- 💊 **Prescripciones Médicas**
  - Emisión de recetas digitales
  - Control de inventario automático
  - Validación de stock

- 🧪 **Laboratorio Clínico**
  - Gestión de solicitudes de exámenes
  - Registro de resultados con archivos adjuntos
  - Almacenamiento en MongoDB para datos no estructurados

- 📊 **Inventario de Medicamentos**
  - Control de stock en tiempo real
  - Alertas de stock bajo
  - Historial de movimientos

- 📝 **Historial Clínico**
  - Registro completo de consultas
  - Timeline médica del paciente
  - Acceso segmentado por rol

## 🛠 Tecnologías

### Backend
- **.NET 8**
- **ASP.NET Core Blazor Server**
- **Entity Framework Core 8.0.6**

### Frontend
- **Blazor Server Components**
- **MudBlazor 8.15.0** (UI Framework)

### Bases de Datos
- **SQL Server** (Datos transaccionales)
- **MongoDB** (Resultados de laboratorio)

### Patrones y Arquitecturas
- Repository Pattern
- Service Layer
- Builder Pattern
- Factory Pattern
- Dependency Injection

## 🏗 Arquitectura
```
HospitalGestor/
│
├── HospitalData/              # Capa de Datos y Lógica de Negocio
│   ├── Models/                # Entidades del dominio
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Services/              # Lógica de negocio
│   ├── Builders/              # Patrón Builder
│   ├── Factories/             # Patrón Factory
│   └── Enums/                 # Enumeraciones
│
└── HospitalWeb/               # Capa de Presentación
    ├── Components/
    │   ├── Pages/             # Páginas Blazor
    │   ├── Layout/            # Layouts
    │   └── Shared/            # Componentes compartidos
    ├── wwwroot/               # Recursos estáticos
    └── Program.cs             # Punto de entrada
```

### Arquitectura de Base de Datos

**SQL Server**: Datos estructurados (usuarios, citas, prescripciones, inventario)
```
- Users, Roles
- Patients, Doctors, Staff, LaboratoryTechnicians
- Appointments, MedicalHistory
- Medications, Inventory, Prescriptions
- LabRequests, LabTests, LabAreas
```

**MongoDB**: Datos semi-estructurados (resultados de laboratorio)
```
HospitalLabDB
└── LabResults (Collection)
    ├── RequestId
    ├── PatientId
    ├── DoctorId
    ├── TestName
    ├── Results (Dictionary<string, object>)
    └── Attachments
```

## 📦 Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server 2019+](https://www.microsoft.com/sql-server/sql-server-downloads) o SQL Server Express
- [MongoDB 4.4+](https://www.mongodb.com/try/download/community)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

## 🚀 Instalación

### 1. Clonar el repositorio
```bash
git clone https://github.com/tu-usuario/hospital-gestor.git
cd hospital-gestor
```

### 2. Restaurar paquetes NuGet
```bash
dotnet restore
```

### 3. Configurar Base de Datos SQL Server

Ejecutar los scripts de creación de base de datos ubicados en:
```bash
# Scripts SQL (crear manualmente en SQL Server Management Studio)
- Crear base de datos: Hospital
- Ejecutar scripts de tablas
- Ejecutar scripts de stored procedures
- Ejecutar scripts de vistas
- Insertar datos iniciales
```

### 4. Configurar MongoDB
```bash
# Iniciar servicio MongoDB
mongod

# Crear base de datos (se crea automáticamente al primer insert)
# No requiere configuración adicional
```

### 5. Actualizar cadenas de conexión

Editar `HospitalWeb/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "HospitalDB": "Server=localhost,1433;Database=Hospital;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True",
    "MongoConnection": "mongodb://localhost:27017"
  },
  "MongoDbSettings": {
    "DatabaseName": "HospitalLabDB",
    "CollectionName": "LabResults"
  }
}
```

### 6. Ejecutar la aplicación
```bash
cd HospitalWeb
dotnet run
```

La aplicación estará disponible en: `https://localhost:5001`

## ⚙ Configuración

### Usuarios por Defecto

Después de ejecutar los scripts de inicialización, tendrás acceso con:

| Usuario | Contraseña | Rol |
|---------|-----------|-----|
| admin | password123 | Administrativo |
| doctor1 | password123 | Médico |
| paciente1 | password123 | Paciente |
| lab1 | password123 | Laboratorista |

> ⚠️ **Importante**: Cambiar estas contraseñas en producción

### Configuración de Archivos

Los archivos adjuntos se almacenan en:
```
HospitalWeb/wwwroot/uploads/
```

Asegurar que el directorio tenga permisos de escritura.

## 📁 Estructura del Proyecto

### HospitalData (Capa de Datos)
```
HospitalData/
├── Builders/
│   ├── ILabResultBuilder.cs          # Interfaz del builder
│   └── LabResultBuilder.cs           # Construcción de resultados de lab
│
├── DTOs/
│   ├── AppointmentDetailDto.cs
│   ├── CreateDoctorDto.cs
│   ├── CreatePatientDto.cs
│   ├── LabRequestDto.cs
│   └── ... (más DTOs)
│
├── Enums/
│   └── UserType.cs                    # Tipos de usuario
│
├── Factories/
│   ├── IUserEntityFactory.cs
│   └── UserEntityFactory.cs           # Creación de usuarios
│
├── Models/
│   ├── HospitalDbContext.cs           # Contexto EF Core
│   ├── User.cs
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Appointment.cs
│   ├── LabResult.cs                   # Modelo MongoDB
│   └── ... (más modelos)
│
└── Services/
    ├── IAuthService.cs
    ├── IDoctorService.cs
    ├── IPatientService.cs
    ├── ILabResultService.cs
    └── ... (implementaciones)
```

### HospitalWeb (Capa de Presentación)
```
HospitalWeb/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor           # Layout principal
│   │   └── NavMenu.razor              # Menú de navegación
│   │
│   ├── Pages/
│   │   ├── Home.razor                 # Página de inicio
│   │   ├── Login.razor                # Autenticación
│   │   ├── PanelDoctor.razor          # Dashboard médico
│   │   ├── PanelLaboratorio.razor     # Dashboard laboratorio
│   │   ├── MisCitas.razor             # Citas del paciente
│   │   ├── GestionInventario.razor    # Gestión de medicamentos
│   │   └── ... (más páginas)
│   │
│   └── Shared/
│       ├── Autorizacion.razor         # Componente de autorización
│       └── LabResultDialog.razor      # Modal de resultados
│
├── wwwroot/
│   ├── css/
│   └── uploads/                       # Archivos adjuntos
│
├── Program.cs                         # Configuración de servicios
└── appsettings.json                   # Configuración
```

## 👥 Roles y Funcionalidades

### 👨‍⚕️ Médico
- Ver agenda de citas
- Atender pacientes
- Consultar historial clínico
- Emitir prescripciones médicas
- Solicitar exámenes de laboratorio
- Reagendar o cancelar citas
- Ver resultados de laboratorio

### 🧑‍🔬 Laboratorista
- Ver solicitudes pendientes por área
- Procesar exámenes
- Registrar resultados
- Adjuntar archivos (imágenes, PDFs)
- Finalizar solicitudes

### 🧑‍💼 Administrativo
- Gestionar doctores y especialidades
- Gestionar pacientes
- Gestionar laboratoristas
- Control de inventario de medicamentos
- Visualizar calendario general de citas
- Reportes y estadísticas

### 🧑‍🦱 Paciente
- Agendar citas médicas
- Ver mis citas programadas
- Cancelar citas
- Consultar prescripciones activas
- Ver historial clínico
- Actualizar perfil personal

## 🎨 Patrones de Diseño

### Builder Pattern
```csharp
// Construcción flexible de resultados de laboratorio
var result = labResultBuilder
    .Reset()
    .SetBasicInfo(requestId, patientId, doctorId, techId, testName)
    .AddParameter("Hemoglobina", "14.5 g/dL")
    .AddParameter("Leucocitos", "7500/μL")
    .AddAttachment("radiografia.jpg", "/uploads/abc123.jpg")
    .AddObservations("Valores dentro del rango normal")
    .Build();
```

### Factory Pattern
```csharp
// Creación estandarizada de entidades de usuario
var parameters = userFactory.CreateParameters(
    firstName, lastName, email, phone,
    UserType.Medico,
    specialtyId
);
```

### Repository/Service Pattern
```csharp
// Separación de lógica de negocio y acceso a datos
public interface IDoctorService
{
    Task<List<VwDoctorAgendaSummary>> GetMyAgendaAsync(int doctorId);
    Task CompleteAppointmentAsync(int appointmentId, string notes);
    Task CreatePrescriptionAsync(CreatePrescriptionDto dto);
}
```

## 🗄️ Características de Base de Datos

### Stored Procedures Principales
- `SP_UserLogin` - Autenticación de usuarios
- `SP_CreateNewEntity` - Creación de usuarios multi-tipo
- `SP_ScheduleAppointment` - Agendamiento con validaciones
- `SP_IssueNewPrescription` - Emisión de prescripciones con control de inventario
- `SP_CreateLabRequest` - Solicitud de exámenes

### Triggers
- `TR_EnsureHistoryRecord` - Crea registro en historial al completar cita
- `TR_PreventAppointmentOverlap` - Evita traslapes de horarios
- `TR_UpdateInventory_Prescription` - Actualiza inventario al emitir receta

### Vistas
- `VW_DoctorAgendaSummary` - Agenda consolidada del médico
- `VW_PatientActivePrescriptions` - Prescripciones vigentes
- `VW_PatientAppointments` - Citas del paciente
- `VW_StaffAppointmentManagement` - Calendario general

## 🔧 Solución de Problemas Comunes

### Error de conexión a SQL Server
```bash
# Verificar que SQL Server esté ejecutándose
# Verificar credenciales en appsettings.json
# Asegurar que TrustServerCertificate=True esté presente
```

### Error de conexión a MongoDB
```bash
# Verificar que MongoDB esté ejecutándose: mongod
# Verificar puerto en appsettings.json (default: 27017)
```

### Errores de compilación
```bash
# Limpiar y reconstruir
dotnet clean
dotnet build
```

## 📚 Documentación Adicional

- [Documentación de Blazor](https://docs.microsoft.com/aspnet/core/blazor)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [MudBlazor Components](https://mudblazor.com/)
- [MongoDB C# Driver](https://mongodb.github.io/mongo-csharp-driver/)

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto es parte de un trabajo académico para la Universidad Católica Boliviana.
