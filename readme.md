🏥 SaludSys

SaludSys es una solución integral para la administración de centros médicos, desarrollada con tecnologías modernas de Microsoft. Esta aplicación web permite la gestión eficiente de pacientes, doctores, citas médicas, inventarios farmacéuticos e historiales clínicos mediante una interfaz intuitiva y responsiva.

🚀 Tecnologías Utilizadas

Este proyecto está construido sobre un stack robusto y escalable:

Framework Principal: .NET 8 (Blazor Server Side)

Interfaz de Usuario: MudBlazor (Componentes Material Design)

Base de Datos: Microsoft SQL Server

Acceso a Datos: Enfoque híbrido con Entity Framework Core 8 y Stored Procedures (SQL Nativo) para alto rendimiento.

Lenguaje: C# 12

📋 Requisitos Previos

Antes de ejecutar la aplicación, asegúrate de tener instalado:

SDK de .NET 8.0 o superior.

Microsoft SQL Server (Express, Developer o Enterprise).

Visual Studio 2022 (con la carga de trabajo de desarrollo ASP.NET) o VS Code.

⚙️ Configuración e Instalación

1. Clonar el Repositorio

git clone [https://github.com/tu-usuario/hospital-gestor.git](https://github.com/tu-usuario/hospital-gestor.git)
cd hospital-gestor


2. Configuración de Base de Datos ⚠️

Este proyecto depende de Procedimientos Almacenados (Stored Procedures) y Vistas para su lógica crítica (login, agendamiento, etc.).

Asegúrate de tener una instancia de SQL Server corriendo.

Ejecuta el script de base de datos DatabaseScript.sql (si está disponible en la carpeta /sql o solicítalo al administrador del DB) para crear las tablas, vistas y procedimientos necesarios como SP_UserLogin, SP_ScheduleAppointment, etc.

Sin estos objetos SQL, la aplicación dará errores en tiempo de ejecución.

3. Configurar Cadena de Conexión

Abre el archivo HospitalWeb/appsettings.json y actualiza la cadena de conexión HospitalDB con tus credenciales locales:

"ConnectionStrings": {
  "HospitalDB": "Server=localhost;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;"
}


4. Ejecutar la Aplicación

Desde la terminal en la carpeta HospitalWeb:

dotnet watch run


O presiona F5 si estás usando Visual Studio.

La aplicación estará disponible en https://localhost:7165 (o el puerto configurado en launchSettings.json).

📖 Guía de Uso por Roles

El sistema cuenta con tres perfiles de usuario principales. A continuación, se detalla qué puede hacer cada uno:

🧑‍🦱 Paciente

Registrarse/Login: Acceso seguro al portal.

Agendar Citas: Seleccionar especialidad, doctor y horario disponible en tiempo real.

Mis Citas: Ver estado de citas futuras (Programada, Completada, Cancelada).

Historial y Recetas: Consultar historial médico propio y recetas prescritas.

👨‍⚕️ Médico

Panel de Doctor: Vista rápida de la agenda del día.

Gestión de Citas: Iniciar consultas y registrar diagnósticos.

Historial Clínico: Acceso (lectura/escritura) al historial de los pacientes asignados.

Prescripciones: Crear recetas médicas que descuentan automáticamente del inventario.

👔 Administrativo

Gestión de Personal: Dar de alta nuevos doctores y asignar especialidades.

Gestión de Pacientes: Crear o editar perfiles de pacientes.

Inventario Farmacéutico: Control de stock de medicamentos (entradas/salidas).

Reportes: Vista global de citas y ocupación hospitalaria.

🏗️ Estructura del Proyecto

La solución sigue una arquitectura en capas para facilitar el mantenimiento:

HospitalWeb (Frontend)

Capa de presentación Blazor Server.

Components/Pages: Contiene las vistas (.razor) como AgendarCita, PanelDoctor.

Components/Layout: Estructura visual (MainLayout, NavMenu).

Services: (En proceso de refactorización) Inyección de lógica de UI.

HospitalData (Backend / DAL)

Capa de acceso a datos y reglas de negocio.

Models: Entidades de EF Core (Patient, Doctor, Appointment).

DTOs: Objetos de transferencia de datos para vistas y formularios seguros.

Services/Repositories: Lógica que conecta C# con SQL Server.

Configurations: (Nuevo) Configuraciones limpias de Entity Framework (SRP).
