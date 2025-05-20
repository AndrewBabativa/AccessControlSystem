
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

### AccessControlSystem.Application/

- **Commands/**  
  Aquí van los archivos `.cs` que representan comandos o acciones específicas de negocio, como `CreateUserCommand.cs`, `UpdatePermissionCommand.cs`.  
  Son clases que contienen la lógica para ejecutar operaciones (normalmente con el patrón CQRS).

- **DTOs/**  
  Acá están los Data Transfer Objects — clases simples que modelan datos para transportar entre capas o a través de la red. Ejemplo: `UserDto.cs`, `PermissionDto.cs`.

- **External/**  
  Aquí suele ir el código que se conecta con servicios externos (APIs, SDKs, integraciones de terceros). Por ejemplo, `ExternalAuthService.cs`, `PaymentGatewayClient.cs`.

- **Mappings/**  
  Aquí defines los perfiles o configuraciones para mapeo entre objetos (por ejemplo con AutoMapper). Archivos típicos: `UserMappingProfile.cs`, `PermissionMappingProfile.cs`.

### AccessControlSystem.Domain/

- **Entities/**  
  Aquí están las clases que representan las entidades del dominio, con su lógica y propiedades, por ejemplo `User.cs`, `Permission.cs`.

- **Enums/**  
  Aquí defines enumeraciones usadas en el dominio, por ejemplo `PermissionType.cs`, `UserRole.cs`.

- **Exceptions/**  
  Aquí defines excepciones específicas del dominio o negocio, como `UserNotFoundException.cs`, `PermissionDeniedException.cs`.

### AccessControlSystem.Infrastructure/

- **Persistence/**  
  Aquí van las clases que se encargan de la persistencia, como implementaciones de repositorios, contexto de base de datos (`DbContext` en EF), migraciones. Ejemplos: `UserRepository.cs`, `AccessControlDbContext.cs`.

- **Search/**  
  Acá se puede implementar la lógica relacionada con búsquedas avanzadas, por ejemplo integración con ElasticSearch o consultas específicas. Ejemplo: `UserSearchService.cs`, `PermissionSearchService.cs`.

### AccessControlSystem.API/

- **Controllers/**  
  Aquí van los controladores que exponen los endpoints REST para el sistema de control de acceso, por ejemplo `UserController.cs`, `RoleController.cs`.

---

## Estructura del Proyecto Backend (AccessControlSystem.Backend)

## Árbol de carpetas y archivos

```plaintext
AccessControlSystem.Application/
├── Commands/
│   ├── CreateUserCommand.cs
│   ├── UpdatePermissionCommand.cs
│   └── DeleteRoleCommand.cs
├── DTOs/
│   ├── UserDto.cs
│   ├── PermissionDto.cs
│   └── RoleDto.cs
├── External/
│   ├── ExternalAuthService.cs
│   └── PaymentGatewayClient.cs
└── Mappings/
    ├── UserMappingProfile.cs
    └── PermissionMappingProfile.cs

AccessControlSystem.Domain/
├── Entities/
│   ├── User.cs
│   ├── Role.cs
│   └── Permission.cs
├── Enums/
│   ├── UserRole.cs
│   └── PermissionType.cs
└── Exceptions/
    ├── UserNotFoundException.cs
    └── PermissionDeniedException.cs

AccessControlSystem.Infrastructure/
├── Persistence/
│   ├── AccessControlDbContext.cs
│   ├── UserRepository.cs
│   └── RoleRepository.cs
└── Search/
    ├── UserSearchService.cs
    └── PermissionSearchService.cs

AccessControlSystem.API/
└── Controllers/
    ├── UserController.cs
    └── RoleController.cs
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

