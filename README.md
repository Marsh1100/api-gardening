# Proyecto Jardinería
La empresa Gardens especializada en Jardineria desea construir una aplicacion que le permita llevar el control y registro de todos sus productos y servicios. Para ello se realiza un poyecto webapi de cuatro capas usando NetCore7.0 para la administración de una veterinaria. Empleando como gestor de base de datos MySql.
### ¿Qué se va obtener?
  - Autenticación y autorización
  - CRUD de cada una de las tablas
  - Restricción de peticiones consecutivas
  - Paginación de los controladores Get
  - Consultas
### Pre-requisitos 📋
MySQL<br>
NetCore 7.0
### Base de datos
img.1
### Ejecutar proyecto 🔧
1. Clone el repositorio en la carpeta que desee abriendo la terminal y ejecute el siguiente code
   ```
   git clone https://github.com/Marsh1100/api-gardening
   ```
2. Acceda al la carpeta que se acaba de generar
   ```cd api-gardening ```
3. Ahora ejecute el comando ```. code``` para abrir el proyecto en Visual Studio Code
4. En la carpeta API diríjase al archivo appsettings.Development.json
     Llene los campos según sea su caso en los valores server, user y password reemplazando las comillas simples.

     <b>Nota:</b> Puede cambiar el nombre de la base de datos (database) si así lo prefiere.
     img.png2
6. Ahora abra una nueva terminal en Visual Studio Code
   img.png3
7. Ejecute las siguientes líneas de código para migrar la Base de Datos a su servidor. <br>
     ```
       dotnet ef migrations add FirstMigration --project ./Persistence/ --startup-project ./API/ --output-dir ./Data/Migrations
     ```
     ```
       dotnet ef database update --project ./Persistence --startup-project ./API
     ```
9. Acceda a la carpeta API ```cd API ``` y ejecute el comando    ```dotnet run ```<br>
  Le aparecerá algo como esto:<br>
   image.png4<br>
Las tablas de la BD 'User', 'Rol' y 'Userrol' han sido llenadas con datos semilla 🌱🌱. <br>

Ha este punto se cuenta con la conexión de la webapi con la base de datos. Con el fin de tener datos los cuales visualizar se recomienda seguir con el paso 9.
9. Abra la consola de MySQL e ingrese la contraseña.
img5
10. Ejecute el siguiente comando para posicionarse en la BD gardering
```sql
  USE gardening;
```
11. Copie los datos del siguiente link data
¡Listo! Ahora podrá ejecutar los endpoints sin problema.<br>
