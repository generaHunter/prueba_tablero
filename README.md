# Documentación de la Prueba

## Descripción del Problema y Contexto
El objetivo principal de esta solución es implementar un sistema básico de gestión de tableros y tareas similar a Trello, abordando las siguientes funcionalidades clave:

+ CRUD para tableros y tareas.
+ Mover tareas entre estados (pendiente, en progreso, completada).
+ Generar reportes que incluyan el conteo de tareas por estado y tablero, así como estadísticas básicas.

Además, se busca evaluar la capacidad para manejar datos masivos, aplicar principios SOLID, emplear el patrón CQRS y usar tecnologías modernas.

---

## Tecnologías y Herramientas Utilizadas
1. **Lenguaje y Frameworks**: .NET Core 8.
2. **Base de datos**:
    - PostgreSQL para escritura.
    - MongoDB para lectura.
3. **IDE**: Visual Studio 2022.
4. **Gestión de dependencias**: Inyección de dependencias mediante el contenedor de servicios de .NET Core.
5. **Manejo de errores**: Exception Filters.
5. **Validación**: Fluent Validation.
6. **Autenticación y seguridad**: Implementación de JWT.
7. **Documentación de la API**: Swagger.
8. **Contenedores**: Docker (Dockerfile).

---

## Diseño de la Arquitectura
La solución utiliza el enfoque Clean Architecture, estructurando el proyecto en las siguientes capas:

## API
- Contiene los controladores que exponen los endpoints.
## Core
- **Domain**: Define las entidades principales (Tablero, Tarea, Usuario, Estado).
- **Application**: Contiene la lógica de negocio, incluyendo CQRS, validaciones y mapeos.
## Infrastructure
- **Persistence**: Implementa la lógica para interactuar con PostgreSQL y MongoDB.
- **External**: Interacciones con servicios externos si fuera necesario.
## Common
- Contiene utilidades y componentes reutilizables.
---
##  Estructura del proyecto
```
prueba_tablero
├─ .git
├─ .github
│  └─ workflows
├─ .gitignore
├─ postman-collection
│  ├─ Prueba_Tablero Api Endpoint.postman_collection.json
├─ database_file
│  ├─ CREATE DATABASE tablero_database.sql
│  ├─ CREATE TABLE Estado.sql
│  ├─ CREATE TABLE Tablero.sql
│  ├─ CREATE TABLE Tarea.sql
│  └─ CREATE TABLE Usuario.sql
├─ prueba_tablero.sln
├─ README.md
└─ src
   ├─ tablero.Api
   │  ├─ appsettings.Development.json
   │  ├─ appsettings.Docker.json
   │  ├─ appsettings.json
   │  ├─ Controllers
   │  │  ├─ EstadoController.cs
   │  │  ├─ ReporteController.cs
   │  │  ├─ SeedController.cs
   │  │  ├─ TableroController.cs
   │  │  ├─ TareaController.cs
   │  │  └─ UsuarioController.cs
   │  ├─ DependecyInjectionService.cs
   │  ├─ Dockerfile
   │  ├─ Program.cs
   │  ├─ Properties
   │  │  └─ launchSettings.json
   │  ├─ tablero.Api.csproj
   │  └─ tablero.Api.http
   ├─ tablero.Application
   │  ├─ Configuration
   │  │  └─ MapperProfile.cs
   │  ├─ DataBase
   │  │  ├─ Estado
   │  │  │  ├─ Commands
   │  │  │  │  ├─ CreateEstado
   │  │  │  │  │  ├─ CreateEstadoCommand.cs
   │  │  │  │  │  └─ ICreateEstadoCommand.cs
   │  │  │  │  ├─ CreateEstadoSeed
   │  │  │  │  │  ├─ CreateEstadoSeedCommand.cs
   │  │  │  │  │  └─ ICreateEstadoSeedCommand.cs
   │  │  │  │  └─ DefaultModel
   │  │  │  │     └─ DefaultEstadoModel.cs
   │  │  │  └─ Queries
   │  │  │     └─ GetAllEstadosQuery
   │  │  │        ├─ GetAllEstadosQuery.cs
   │  │  │        └─ IGetAllEstadosQuery.cs
   │  │  ├─ IDataBaseService.cs
   │  │  ├─ IMongoDataBaseService.cs
   │  │  ├─ Reportes
   │  │  │  └─ ReporteTareas
   │  │  │     ├─ IReporteTareasQuery.cs
   │  │  │     └─ ReporteTareasQuery.cs
   │  │  ├─ SeedData
   │  │  │  └─ Commands
   │  │  │     └─ GenerateSeedData
   │  │  │        ├─ GenerateSeedDataCommand.cs
   │  │  │        └─ IGenerateSeedDataCommand.cs
   │  │  ├─ Tablero
   │  │  │  ├─ Commands
   │  │  │  │  ├─ CreateTablero
   │  │  │  │  │  ├─ CreateTableroCommand.cs
   │  │  │  │  │  └─ ICreateTableroCommand.cs
   │  │  │  │  ├─ DeleteTablero
   │  │  │  │  │  ├─ DeleteTableroCommand.cs
   │  │  │  │  │  └─ IDeleteTableroCommand.cs
   │  │  │  │  └─ UpdateTablero
   │  │  │  │     ├─ IUpdateTableroCommand.cs
   │  │  │  │     ├─ UpdateTableroCommand.cs
   │  │  │  │     └─ UpdateTableroModel.cs
   │  │  │  ├─ DefaultModel
   │  │  │  │  └─ DefaultTableroModel.cs
   │  │  │  └─ Queries
   │  │  │     ├─ GetAllTableros
   │  │  │     │  ├─ GetAllTablerosQuery.cs
   │  │  │     │  └─ IGetAllTablerosQuery.cs
   │  │  │     └─ GetTableroById
   │  │  │        ├─ GetTableroByIdQuery.cs
   │  │  │        └─ IGetTableroByIdQuery.cs
   │  │  ├─ Tarea
   │  │  │  ├─ Commands
   │  │  │  │  ├─ CreateTarea
   │  │  │  │  │  ├─ CreateTareaCommand.cs
   │  │  │  │  │  └─ ICreateTareaCommand.cs
   │  │  │  │  ├─ DefultModel
   │  │  │  │  │  └─ DefaultTareaModel.cs
   │  │  │  │  ├─ DeleteTarea
   │  │  │  │  │  ├─ DeleteTareaCommand.cs
   │  │  │  │  │  └─ IDeleteTareaCommand.cs
   │  │  │  │  ├─ UpdateEstadoTarea
   │  │  │  │  │  ├─ IUpdateTareaEstadoCommand.cs
   │  │  │  │  │  └─ UpdateTareaEstadoCommand.cs
   │  │  │  │  └─ UpdateTarea
   │  │  │  │     ├─ IUpdateTareaCommand.cs
   │  │  │  │     ├─ UpdateTareaCommand.cs
   │  │  │  │     └─ UpdateTareaModel.cs
   │  │  │  └─ Queries
   │  │  │     ├─ GetAllTareas
   │  │  │     │  ├─ GetAllTareasQuery.cs
   │  │  │     │  └─ IGetAllTareasQuery.cs
   │  │  │     ├─ GetTareaById
   │  │  │     │  ├─ GetTareaByIdQuery.cs
   │  │  │     │  └─ IGetTareaByIdQuery.cs
   │  │  │     └─ GetTareasByTableroId
   │  │  │        ├─ GetTareasByTableroId.cs
   │  │  │        └─ IGetTareasByTableroId.cs
   │  │  └─ Usuario
   │  │     ├─ Commands
   │  │     │  ├─ CreateUsuario
   │  │     │  │  ├─ CreateUsuarioCommand.cs
   │  │     │  │  └─ ICreateUsuarioCommand.cs
   │  │     │  ├─ CreateUsuarioSeed
   │  │     │  │  ├─ CreateUsuarioSeedCommand.cs
   │  │     │  │  └─ ICreateUsuarioSeedCommand.cs
   │  │     │  ├─ DeleteUsuario
   │  │     │  │  ├─ DeleteUsuarioCommand.cs
   │  │     │  │  └─ IDeleteUsuarioCommand.cs
   │  │     │  └─ UpdateUsuario
   │  │     │     ├─ IUpdateUsuarioCommand.cs
   │  │     │     ├─ UpdateUsuarioCommand.cs
   │  │     │     └─ UpdateUsuarioModel.cs
   │  │     ├─ DefaultModel
   │  │     │  └─ DefaultUsuarioModel.cs
   │  │     └─ Queries
   │  │        └─ GetUserByCredentials
   │  │           ├─ GetUserByCredentialsModel.cs
   │  │           ├─ GetUserByCredentialsQuery.cs
   │  │           └─ IGetUserByCredentialsQuery.cs
   │  ├─ DependecyInjectionService.cs
   │  ├─ Dtos
   │  │  └─ ReporteTareasDto.cs
   │  ├─ Exceptions
   │  │  └─ ExceptionManager.cs
   │  ├─ External
   │  │  └─ JWT
   │  │     └─ IGetTokenJWTService.cs
   │  ├─ Features
   │  │  └─ ResponseApiService.cs
   │  ├─ tablero.Application.csproj
   │  └─ Validators
   │     └─ User
   │        └─ GetUserByCrendetialsValidator.cs
   ├─ tablero.Common
   │  ├─ DependecyInjectionService.cs
   │  └─ tablero.Common.csproj
   ├─ tablero.Domain
   │  ├─ Entities
   │  │  ├─ Estado
   │  │  │  └─ EstadoEntity.cs
   │  │  ├─ Tablero
   │  │  │  └─ TableroEntity.cs
   │  │  ├─ Tarea
   │  │  │  └─ TareaEntity.cs
   │  │  └─ Usuario
   │  │     └─ UsuarioEntity.cs
   │  ├─ Models
   │  │  └─ BaseResponseModel.cs
   │  └─ tablero.Domain.csproj
   ├─ tablero.External
   │  ├─ DependecyInjectionService.cs
   │  ├─ GetTokenJWT
   │  │  └─ GetTokenJWTService.cs
   │  └─ tablero.External.csproj
   └─ tablero.Persistence
      ├─ Configuration
      │  ├─ EstadoConfiguration.cs
      │  ├─ TableroConfiguration.cs
      │  ├─ TareaConfiguration.cs
      │  └─ UsuarioConfiguration.cs
      ├─ DataBase
      │  ├─ DataBaseService.cs
      │  └─ MongoDataBaseService.cs
      ├─ DependecyInjectionService.cs
      └─ tablero.Persistence.csproj
```
---

## Principios SOLID
Se aplicaron los principios SOLID en el diseño de las clases:

- S (Responsabilidad Única): Cada clase tiene una única responsabilidad.
- O (Abierto/Cerrado): Las clases son abiertas para extensión, pero cerradas para modificaciones.
- L (Sustitución de Liskov): Las implementaciones pueden reemplazar las interfaces sin alterar el comportamiento esperado.
- I (Segregación de Interfaces): Las interfaces están segmentadas según las responsabilidades.
- D (Inversión de Dependencias): Las dependencias están invertidas usando interfaces.

---

## Práctica OWASP Implementada
Se implementó la autenticación con JWT, asegurando la protección contra accesos no autorizados. Además:

- Se realizó sanitización de entradas para prevenir inyecciones de SQL.

---

## Uso de Docker
Se configuró un Dockerfile para desplegar la aplicación. Este archivo incluye las siguientes configuraciones:

- Construcción de la imagen de la API.
- Exposición de puertos y configuración para facilitar el despliegue.

---

## Instrucciones para Ejecutar
1. Clona el repositorio.
2. Correr los script para la base de datos de Postgres que se encuentran en la siguiente ruta
```
    /database_file/
```

### Entorno Visual Studio
  1. Acceder al directorio raíz del proyecto.
  2. Abrir la solucion del proyecto.
  3. Restaurar los paquetes nugest de la solucion.
  4. Ejecutar el proyecto Api.

### Entorno Docker
  1. Ejecutar en la raiz del proyecto el siguiente comando para generar la imagen de docker del proyecto
```
    docker build -t tablero-api -f ./src/tablero.Api/Dockerfile .
```
  2. Ejecutar en la raiz del proyecto el siguiente comando para verificar si se creo la imagen
```
    docker images
```
  3. Ejecutar en la raiz del proyecto el siguiente comando para ejecutar la imagen
```
    docker run -d -p 8080:8080 tablero-api
```
  4. Ejecutar en la raiz del proyecto el siguiente comando para ver si el contenedor se esta ejecutando
```
    docker ps
```
  5. Acceder a la ruta para acceder al Swagger del proyecto
```
    http://localhost:8080/index.html
```
  6. Ejecutar en la raiz del proyecto el siguiente comando para detener la ejecucion del contenedor:
  - Para obtener el id del contenedor
```
    docker ps
```
  - Para detener el contenedor mediante el id del mismo
```
    docker stop [CONTAINER ID]
```

---

### Documentacion de endpoint
- El proyecto cuenta con Swagger para poder probar los endpoints.
- Se addicion una collecion de postman para poder probar los endpoints,la coleccion se encuentra en:
```
    /postman-collection/Prueba_Tablero Api Endpoint.postman_collection.json
```
---

### Ejecucion de endpoint necesarios
Endpoints necesarios para poder consumir el resto de endpoints.

+ Controlador Usuario: Endpoint para generar un usuario con nombre de usuario y contraseña **admin**, que nos permemitira generar un token para poder consumir el resto de endpoints.
  - **Tipo peticion**: GET
  - **Uri**: /api/v1/usuario/createUsuarioSeed
+ Controlador Estado: Endpoint para generar los estados que se manejan para las tareas.
  - **Tipo peticion**: GET
  - **Uri**: /api/v1/estado/generateEstadosSeed
