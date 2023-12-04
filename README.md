# Travel-Agency



## Dependencias

Este proyeto fue desarrolado con **ASP.NET Core 7** para el proyecto de back-end y **React** para el proyecto de
front-end.
Se uso **MySql Server** como gestor base de datos.

## Instalación

- Clonar el repositorio en la carpeta deseada junto al proyecto incluido en el submodulo.
- Instalar las dependencias del proyecto de React con el comando:

```
yarn install
```

en la carpeta `Travel-Agency-Client`.

- Instalar las dependencias del proyecto de ASP.NET Core con el comando:

```
make restore
```

- Crear un usario en **MySql Server** con las credenciales del script de conexión especificado en el
  archivo `appsettings.json`
  de la carpeta `Travel-Agency-Api` o modificar el scrpit de conexión con las credenciales del usuario deseado.
- Ejecutar el comando:

```
make db
```

para crear la base de datos y las tablas necesarias para el proyecto. Si lo prefiere, puede ejecutar el comando

```
make seed
```

para instanciar la base de datos con datos de prueba.

## Ejecución

- Ejecutar el comando:

```
make dev
```

para ejecutar el proyecto de ASP.NET Core.

- Ejecutar el comando:

```
make client
```

para ejecutar el proyecto de React.