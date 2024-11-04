# Sistema de Gestión de Empleados y Departamentos

## Descripción

Este proyecto es una aplicación de escritorio diseñada para gestionar empleados y departamentos en una empresa. Permite a los usuarios agregar, modificar y eliminar registros de empleados y departamentos de manera intuitiva. Utiliza MySQL como sistema de gestión de bases de datos y C# para la implementación del frontend y backend.

## Características

- **Gestión de Departamentos**: Agregar, modificar y eliminar departamentos.
- **Gestión de Empleados**: Agregar, modificar y eliminar empleados con información detallada.
- **Interfaz de Usuario Amigable**: Formulario sencillo y fácil de usar para la gestión de datos.
- **Conexión a Base de Datos**: Integración con MySQL para el almacenamiento persistente de datos.

## Tecnologías Utilizadas

- C#
- Windows Forms
- MySQL

## Instalación

1. Clona el repositorio:
   ```bash
   git clone https://github.com/SchallmoserJuan/Proyecto_MDS.git
   ```

2. Abre el proyecto en Visual Studio.

3. Configura la cadena de conexión a tu base de datos MySQL:
 * Abre la clase conexionDAL.
 * Cambia los datos de conexión (usuario y contraseña) según tu configuración de MySQL.

Crea la base de datos y tablas: Ejecuta las siguientes sentencias SQL en tu entorno de MySQL para crear la base de datos y las tablas necesarias:

```bash
CREATE DATABASE dbsistema;

USE dbsistema;

CREATE TABLE `departamentos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `departamento` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `empleadodepartamento` (
  `idDepartamento` int NOT NULL AUTO_INCREMENT,
  `idEmpleado` int DEFAULT NULL,
  PRIMARY KEY (`idDepartamento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `empleados` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) DEFAULT NULL,
  `primerapellido` varchar(45) DEFAULT NULL,
  `segundoapellido` varchar(45) DEFAULT NULL,
  `correo` varchar(45) DEFAULT NULL,
  `departamento` varchar(45) DEFAULT NULL,
  `foto` mediumblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```

4. Ejecuta el proyecto.

## Uso

1. Inicia la aplicación.
2. Navega a las secciones de gestión de empleados y departamentos.
3. Utiliza los formularios para agregar, modificar o eliminar registros.

## Contribuciones

Si deseas contribuir a este proyecto, por favor envía un pull request o abre un issue para discutir cambios.
