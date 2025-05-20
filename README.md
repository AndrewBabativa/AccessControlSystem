
# 🛡️ Access Control System

Sistema de control de permisos para usuarios con arquitectura distribuida, desarrollado con:

- 🔧 .NET 7 (Backend)
- ⚛️ React (Frontend)
- 🐳 Docker + Docker Compose
- 🐘 SQL Server
- 🔍 Elasticsearch
- 🔄 Kafka + Zookeeper

---

## ✅ Requisitos del Challenge

Este sistema fue desarrollado para cumplir con los requerimientos técnicos del challenge solicitado por la empresa **N5 Company**, que incluyen:

- Crear una base de datos con las tablas **Permissions** y **PermissionTypes**, y su relación.
- Crear una API REST con ASP.NET Core, utilizando Entity Framework y persistiendo datos en SQL Server.
- Implementar los servicios:
  - `RequestPermission`
  - `ModifyPermission`
  - `GetPermissions`
- Cada operación debe registrar el permiso también en un índice de Elasticsearch con la misma estructura de la tabla `Permissions`.
- Cada operación debe enviar un mensaje a Kafka con la siguiente estructura:

```json
{
  "id": "guid",
  "nameOperation": "request | modify | get"
}
```

- Uso de los patrones:
  - Repository
  - Unit of Work
  - CQRS 
- Pruebas unitarias y de integración para cada uno de los servicios.
- Frontend con React y Axios, componentes con Material-UI.
- Containerización con Docker y Docker Compose.

---

## 📁 Estructura del Proyecto

```plaintext
AndresBG/
├── AccessControlSystem.Backend/     # Backend .NET
├── AccessControlSystem.Web/         # Frontend React
├── docker-compose.yml               # Orquestación de contenedores
├── README.md                        # Documento técnico
└── .gitignore
```

# AccessControlSystem

## Descripción del Proyecto

AccessControlSystem es una aplicación modular diseñada para gestionar el control de acceso mediante la definición de usuarios, roles y permisos. La arquitectura está dividida en capas claras para mantener el código limpio, escalable y fácil de mantener, siguiendo principios de diseño como CQRS, DDD y separación de responsabilidades.

---
## Estructura del Proyecto Backend (AccessControlSystem.Backend)

## AccessControlSystem.Application/

- **Commands/**  
  Contiene las clases que representan comandos o acciones específicas del negocio, implementando la lógica para realizar operaciones concretas, siguiendo generalmente el patrón CQRS (Command Query Responsibility Segregation). Por ejemplo: `CreatePermissionTypeCommand.cs`, `ModifyPermissionCommand.cs`.

- **DTOs/**  
  Aquí se encuentran los Data Transfer Objects (DTOs), clases simples que modelan y transfieren datos entre capas o servicios, evitando exponer entidades directamente. Ejemplos: `PermissionDto.cs`, `PermissionTypeDto.cs`.

- **External/**  
  Contiene interfaces y clases para integrar servicios externos o sistemas de terceros, como clientes de Kafka, servicios de búsqueda externa, APIs o SDKs. Por ejemplo: `IKafkaProducer.cs`, `IElasticsearchService.cs`.

- **Mappings/**  
  Define perfiles de mapeo para convertir entre entidades, DTOs y otros objetos, comúnmente usando AutoMapper u otra librería de mapeo. Ejemplo: `MappingProfile.cs`.

- **Queries/**  
  Aquí van las clases que implementan las consultas o queries para obtener datos, generalmente parte del patrón CQRS. Incluye los handlers y objetos de consulta. Ejemplos: `GetPermissionsQuery.cs`, `GetPermissionsHandler.cs`.

---

## AccessControlSystem.Domain/

- **Entities/**  
  Contiene las clases que representan las entidades centrales del dominio, con sus propiedades y lógica de negocio encapsulada. Por ejemplo: `Permission.cs`, `PermissionType.cs`.

- **Enums/**  
  Define enumeraciones propias del dominio que representan tipos o estados específicos, por ejemplo: `OperationType.cs`.

- **Repositories/**  
  Interfaces que definen contratos para la persistencia y recuperación de entidades, promoviendo abstracción y desacoplamiento. Ejemplos: `IPermissionRepository.cs`, `IUnitOfWork.cs`.

---

## AccessControlSystem.Infrastructure/

- **Persistence/**  
  Implementación concreta de repositorios, unidad de trabajo y el contexto de base de datos (ej. Entity Framework `DbContext`). Gestiona la interacción directa con la base de datos. Ejemplos: `PermissionRepository.cs`, `ApplicationDbContext.cs`.

- **Messaging/**  
  Clases y configuraciones para la comunicación con sistemas de mensajería o eventos, como Kafka. Ejemplo: `KafkaProducer.cs`, `KafkaSettings.cs`.

- **Search/**  
  Implementación de servicios relacionados con búsquedas avanzadas o integraciones con motores como Elasticsearch. Ejemplo: `ElasticsearchService.cs`.

---

## AccessControlSystem.API/

- **Controllers/**  
  Controladores Web API que exponen los endpoints REST para la interacción con el sistema, manejan las solicitudes HTTP y devuelven las respuestas adecuadas. Ejemplos: `PermissionController.cs`, `PermissionTypeController.cs`.

---

## Árbol de carpetas y archivos

```plaintext
AccessControlSystem.API/
└── Controllers/
    ├── PermissionController.cs
    └── PermissionTypeController.cs

AccessControlSystem.Application/
├── Commands/
│   ├── CreatePermissionTypeCommand.cs
│   ├── CreatePermissionTypeHandler.cs
│   ├── ModifyPermissionCommand.cs
│   ├── ModifyPermissionHandler.cs
│   ├── RequestPermissionCommand.cs
│   └── RequestPermissionHandler.cs
├── DTOs/
│   ├── PermissionDto.cs
│   ├── PermissionEventDto.cs
│   └── PermissionTypeDto.cs
├── External/
│   ├── IElasticsearchService.cs
│   └── IKafkaProducer.cs
├── Mappings/
│   └── MappingProfile.cs
└── Queries/
    ├── GetPermissionsHandler.cs
    ├── GetPermissionsQuery.cs
    ├── GetPermissionTypeHandler.cs
    └── GetPermissionTypeQuery.cs

AccessControlSystem.Domain/
├── Entities/
│   ├── Permission.cs
│   └── PermissionType.cs
├── Enums/
│   └── OperationType.cs
└── Repositories/
    ├── IPermissionRepository.cs
    ├── IPermissionTypeRepository.cs
    ├── IRepository.cs
    └── IUnitOfWork.cs

AccessControlSystem.Infrastructure/
├── Messaging/
│   ├── KafkaProducer.cs
│   └── KafkaSettings.cs
├── Persistence/
│   ├── ApplicationDbContext.cs
│   ├── PermissionRepository.cs
│   ├── PermissionTypeRepository.cs
│   ├── Repository.cs
│   └── UnitOfWork.cs
└── Search/
    ├── ElasticsearchService.cs
    └── ElasticSettings.cs

Tests/
└── UnitTests/
    ├── GetPermissionsHandlerTests.cs
    ├── ModifyPermissionHandlerTests.cs
    └── RequestPermissionHandlerTests.cs

```

## Estructura del Proyecto Frontend (AccessControlSystem.Web)

El frontend de **AccessControlSystem** está construido con **React** y utiliza **Vite** como herramienta de construcción. La arquitectura está diseñada para garantizar mantenibilidad y escalabilidad:

- **src/**: Directorio raíz del código fuente.
  - **App.jsx / main.jsx**: Punto de entrada y componente raíz de la aplicación.
  - **api/**: Contiene clientes API reutilizables y funciones para interactuar con los servicios del backend.
  - **assets/**: Recursos estáticos como imágenes y archivos SVG.
  - **components/**: Componentes de interfaz de usuario reutilizables y modulares.
  - **dtos/**: Definiciones de objetos de transferencia de datos (DTO) usados en el frontend para tipado fuerte y consistencia.
  - **pages/**: Vistas o páginas de alto nivel de la aplicación.
  - **utils/**: Funciones auxiliares para formateo y lógica común.
- **.env**: Variables de entorno y configuración.
- **vite.config.js**: Configuración específica de Vite para el proyecto.

Esta arquitectura modular sigue el principio de separación de responsabilidades, donde cada carpeta cumple una función específica, haciendo que la base de código sea más fácil de entender y extender.

## Árbol de carpetas y archivos
```
AccessControlSystem.Frontend/
├── .env
├── vite.config.js
└── src/
    ├── App.css
    ├── App.jsx
    ├── index.css
    ├── main.jsx
    ├── api/
    │   ├── axiosClient.jsx
    │   ├── permissions.jsx
    │   └── permissionType.jsx
    ├── assets/
    │   └── react.svg
    ├── components/
    │   ├── ModifyPermissionForm.jsx
    │   ├── PermissionForm.jsx
    │   ├── PermissionList.jsx
    │   └── PermissionTypeForm.jsx
    ├── dtos/
    │   └── PermissionDTO.jsx
    ├── pages/
    │   └── PermissionsPage.jsx
    └── utils/
        └── formatDate.js
```

## ⚙️ Requisitos Previos

Tener instalado:

- Docker
- Docker Compose
- Git

## 🚀 Pasos para la ejecución del proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/AndrewBabativa/AccessControlSystem.git
cd AccessControlSystem
```

### 2. Construir y levantar los servicios

📍 Este comando debe ejecutarse desde la raíz del proyecto.

```bash
docker-compose up --build -d
```
![image](https://github.com/user-attachments/assets/4c584ee9-1e1f-42ba-affa-3d5db1e31267)


Esto descargará las imágenes necesarias, construirá los proyectos y levantará todos los servicios definidos.

![image](https://github.com/user-attachments/assets/639177cb-d757-4bc4-a6ed-d3a4126ec104)

### 3. Ejecutar el script de creación de la base de datos (init.sql) 

Conéctate a `localhost,1433` con usuario `sa` y la contraseña 'Test1234!'

Ejecuta el script ubicado en:

```bash
AccessControlSystem.Backend/sql/backup
```
![image](https://github.com/user-attachments/assets/0aa2edf5-76a3-4c26-8f98-e6349ae09020)

---

## 🔍 Servicios Levantados

| Contenedor                         | Descripción                                     | Puertos expuestos                |
|-----------------------------------|------------------------------------------------|----------------------------------|
| accesscontrol-api                 | Servicio backend en .NET Core                   | 5000                             |
| accesscontrol-frontend            | Aplicación frontend React                       | 3000                             |
| sqlserver                         | Base de datos SQL Server                        | 1433                             |
| kafka                             | Broker Kafka para mensajería distribuida        | 9092                             |
| zookeeper                         | Servicio Zookeeper requerido por Kafka          | 2181                             |
| elasticsearch                     | Servicio Elasticsearch para búsqueda            | 9200                             |

---



## 📸 Pasos para probar la aplicación 

### Paso 1: Acceder al Frontend

Ir a: [http://localhost:3000](http://localhost:3000)  

![image](https://github.com/user-attachments/assets/cddc8e6c-1e2f-43f7-9003-8d9871676586)

### Paso 2: Probar solicitud de permiso

- Completa los datos del formulario.

![image](https://github.com/user-attachments/assets/3d1e5cca-f4ea-45eb-87d3-f7f2e7c5ec10)

#! Si como en este caso no hay creado ningun Tipo de Permiso se debe crear uno ya que es un campo obligatorio

![image](https://github.com/user-attachments/assets/51d9e020-2ca5-4184-b8bf-550439e46a9f)

![image](https://github.com/user-attachments/assets/017c99b8-3428-473f-9071-d340bf9db429)

![image](https://github.com/user-attachments/assets/584cae1b-e8e7-4a27-95fd-0586902d4675)

- Ya continuamos con la creación del Permiso normal:

![image](https://github.com/user-attachments/assets/56b09efc-907a-4c81-af24-e1e7ea442910)

- Envíalos con el botón correspondiente.
![image](https://github.com/user-attachments/assets/da362523-6f62-4989-b354-e5119733c140)

- Queda listado en la grilla el nuevo Permiso creado:

![image](https://github.com/user-attachments/assets/b00ec248-a171-44f6-90a8-6c4fb2b6351b)

![image](https://github.com/user-attachments/assets/f404f815-0a5e-4526-9d52-5a5683e14c1a)

## Verificación Elastic Search



### Paso 3: Consultar permisos

- Al iniciar la App se muestran los permisos listados:

![image](https://github.com/user-attachments/assets/e489ee48-25a5-475f-87ec-fc7dc5487591)

### Paso 4: Modificar un permiso

- Selecciona un permiso.
![image](https://github.com/user-attachments/assets/27501454-3b20-44bd-8aa0-41133c0510f4)

![image](https://github.com/user-attachments/assets/47734c5a-d894-4e80-9b25-aefbe3d2b4ee)

### Paso 5: Verificar Swagger del backend

Ir a: [http://localhost:5000/swagger](http://localhost:5000/swagger)  
![image](https://github.com/user-attachments/assets/01ff47cf-d01a-446a-b1cf-c99864394021)

### Paso 6: Verificar índice en Elasticsearch

- Paso 1: Abrir Postman
  Abre la aplicación Postman en tu computadora.
  (Si no la tienes, podés descargarla gratis desde https://www.postman.com/downloads/)

- Paso 2: Crear una nueva petición GET
  En Postman, clic en New > HTTP Request.

 En la barra de URL pegar el siguiente CURL y dar enter

```bash
curl -X GET "http://localhost:9200/permissions/_search?pretty" -H 'Content-Type: application/json' -d'
{
  "query": {
    "match_all": {}
  },
  "size": 10
}
'
```
- Se evidencia el resultado de el dato guardado 
## Creación

![image](https://github.com/user-attachments/assets/e94ab630-a601-49e7-b280-ee01c2009890)

## Modificación

![image](https://github.com/user-attachments/assets/247cee91-d788-43ec-9858-5928285256dd)

### Paso 7: Verificar Kafka

Accede al contenedor de Kafka:

```bash
docker exec -it andresbg-kafka-1 bash
kafka-console-consumer --topic permissions-events --bootstrap-server localhost:9092 --from-beginning
```

![image](https://github.com/user-attachments/assets/47bf83ec-40af-487b-a3e8-e90f3b95d94a)

Captura consola mostrando mensajes de operaciones:

![image](https://github.com/user-attachments/assets/01ad69b3-003b-419a-bfc8-1a42469da762)

---

## 🧪 Tests

Pruebas unitarias e integración están implementadas para cubrir los 3 servicios principales:

- `RequestPermission`
- `ModifyPermission`
- `GetPermissions`

![image](https://github.com/user-attachments/assets/49af2207-0e88-4c3a-b7f9-24774a647054)


---

## 📦 Docker

Contenerización completa de todos los servicios. Para reconstruir:

```bash
docker-compose down
docker-compose up --build -d
```

---

## 🎯 Tecnologías y Patrones

- ASP.NET Core, Entity Framework
- CQRS, Repository y Unit of Work
- Elasticsearch, Kafka, Docker
- React + Material UI
- Axios para comunicación HTTP

---

## 📂 Repositorio

Subido en GitHub: [https://github.com/tu-usuario/AccessControlSystem](https://github.com/tu-usuario/AccessControlSystem)

