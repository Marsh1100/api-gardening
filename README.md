# Proyecto Jardinería
La empresa Gardens especializada en Jardineria desea construir una aplicacion que le permita llevar el control y registro de todos sus productos y servicios. Para ello se realiza un poyecto webapi de cuatro capas usando NetCore7.0 para la administración de una veterinaria. Empleando como gestor de base de datos MySql.
### ¿Qué se va obtener?
  - DDL
  - Autenticación y autorización
  - CRUD de cada una de las tablas
  - Restricción de peticiones consecutivas
  - Paginación de los controladores Get
  - Consultas
### Pre-requisitos 📋
MySQL<br>
NetCore 7.0
### Base de datos
![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img1.png)
### Ejecutar proyecto 🔧
1. Clone el repositorio en la carpeta que desee abriendo la terminal y ejecute el siguiente code
   ```
   git clone https://github.com/Marsh1100/api-gardening
   ```
2. Acceda al la carpeta que se acaba de generar
   ```cd api-gardening ```
3. Ahora ejecute el comando ```. code``` para abrir el proyecto en Visual Studio Code
4. En la carpeta API diríjase al archivo appsettings.Development.json
     Llene los campos según sea su caso en los valores server, user y password reemplazando las comillas simples.<br>
     <b>Nota:</b> Puede cambiar el nombre de la base de datos (database) si así lo prefiere.
     ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img2.png)
6. Ahora abra una nueva terminal en Visual Studio Code
     ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img3.png)
7. Ejecute las siguientes líneas de código para migrar la Base de Datos a su servidor. <br>
     ```
       dotnet ef migrations add FirstMigration --project ./Persistence/ --startup-project ./API/ --output-dir ./Data/Migrations
     ```
     ```
       dotnet ef database update --project ./Persistence --startup-project ./API
     ```
8. Acceda a la carpeta API ```cd API ``` y ejecute el comando    ```dotnet run ```<br>
  Le aparecerá algo como esto:<br>
     ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img4.png)
<br> Las tablas de la BD 'User', 'Rol' y 'Userrol' han sido llenadas con datos semilla 🌱🌱. <br>

Ha este punto se cuenta con la conexión de la webapi con la base de datos. Con el fin de tener datos los cuales visualizar se recomienda seguir con el paso 9. <br><br>
9. Abra la consola de MySQL Command Line Client e ingrese la contraseña.
    ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img5.png)
10. Ejecute el siguiente comando para posicionarse en la BD gardering
```sql
  USE gardening;
```
11. Copie los dats del siguiente link [data](https://github.com/Marsh1100/api-gardening/blob/main/data-gardening.sql) y peguelos en la consola de MySQL Command Line Client.
    ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img6.png)

¡Listo! Ahora podrá ejecutar los endpoints sin problema.<br>
<br>
## Autenticación y autorización 
* Autenticación de un usuario registrado.<br>
  ```
  http://localhost:5000/api/User/token
  ```
    ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img7.png)
* RefreshToken<br>
  ```
  http://localhost:5000/api/User/refresh-token
  ```
    ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img8.png)
* Autorización<br>
  En el siguiente enpoint es para eliminar un cliente de la base de datos donde solo esta autorizado el Administrador
  ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img9.png)<br><br>
  ```
  http://localhost:5000/api/Employee/{id}
  ```
  <b>Nota</b>: Reemplazar {id}.<br>
  <br>Token de un usuario autorizado ✅.
  ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img10.png)

  Token de un usuario no autorizado  ❌.
  ![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img11.png)
<br><br>
## CRUD
En cada uno de los controladores se realizó el CRUD correspondiente de las tablas. En el siguiente link [Peticiones](https://github.com/Marsh1100/api-gardening/blob/main/consultas-insomnia), es un archivo contenido en el proyecto, puede importarse a Insomia para visualizar cada una de las peticiones realizadas.
## Restricción de peticiones consecutivas
Limitación de peticiones desde una Ip a la webapi.<br>
Ejemplo al intentar acceder más de 3 veces en 10 segundos a la lista de propietarios de mascotas<br>
![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img12.png)<br><br>
## Paginación de peteciones get y Versionado
Se realizó la paginación en las peticiones get de cada uno de las tablas en la version 1.1.<br>
Para el versionado se puede realizar mediante la query.  
```
http://localhost:5000/api/Client?ver=1.1
```
![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img13.png)<br>
O mediante los headers. 
```
http://localhost:5000/api/Client
```
![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img14.png)<br><br>
Ejemplo de paginación implementando los parámetros index y cantidad de registros por página.  
```
http://localhost:5000/api/Client?ver=1.1&pageSize=1&pageIndex=2
```
![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img15.png)<br><br>
Ejemplo de paginación implementando el párametro de busqueda.
``` 
http://localhost:5000/api/Client?ver=1.1&search=Tendo
```
![image](https://github.com/Marsh1100/api-gardening/blob/main/img/img16.png)<br><br>

## Ejecutando las consultas ⚙️📚

<b>Consultas Requeridas</b>
1. Devuelve un listado con el nombre de los todos los clientes españoles.
```
http://localhost:5000/api/Client/spanishClients
```
2. Devuelve un listado con los distintos estados por los que puede pasar un 
pedido.
```
http://localhost:5000/api/Request/states
```
3. Devuelve un listado con el código de cliente de aquellos clientes que 
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar 
aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta
```
http://localhost:5000/api/Client/clientsPay2008
```
  • Utilizando la función YEAR de MySQL.
  • Utilizando la función DATE_FORMAT de MySQL.
  • Sin utilizar ninguna de las funciones anteriores.

9. Devuelve un listado con el código de pedido, código de cliente, fecha 
esperada y fecha de entrega de los pedidos que no han sido entregados a 
tiempo.
```
http://localhost:5000/api/Request/requestLate
```
10. Devuelve un listado con el código de pedido, código de cliente, fecha 
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al 
menos dos días antes de la fecha esperada.
```
http://localhost:5000/api/Request/requestEarly
```
• Utilizando la función ADDDATE de MySQL.
• Utilizando la función DATEDIFF de MySQL.
• ¿Sería posible resolver esta consulta utilizando el operador de suma + o 
resta -?

11. Devuelve un listado de todos los pedidos que fueron rechazados en 2009.
```
http://localhost:5000/api/Request/requestReject
```
12. Devuelve un listado de todos los pedidos que han sido entregados en el 
mes de enero de cualquier año.
```
http://localhost:5000/api/Request/requestDelivered
```
13. Devuelve un listado con todos los pagos que se realizaron en el 
año 2008 mediante Paypal. Ordene el resultado de mayor a menor.
```
http://localhost:5000/api/Payment/payment2008
```
14. Devuelve un listado con todas las formas de pago que aparecen en la 
tabla pago. Tenga en cuenta que no deben aparecer formas de pago 
repetidas.
```
http://localhost:5000/api/Payment/paymentMethod
```
15. Devuelve un listado con todos los productos que pertenecen a la 
gama Ornamentales y que tienen más de 100 unidades en stock. El listado 
deberá estar ordenado por su precio de venta, mostrando en primer lugar 
los de mayor precio.
```
http://localhost:5000/api/Product/productsOrnamentales
```
16. Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y 
cuyo representante de ventas tenga el código de empleado 11 o 30.
```
http://localhost:5000/api/Client/clientsMadrid
```
<b>Consultas multitabla (Composición interna) </b>
1. Obtén un listado con el nombre de cada cliente y el nombre y apellido de su 
representante de ventas.
```
http://localhost:5000/api/Client/clientsAndSeller
```
2. Muestra el nombre de los clientes que hayan realizado pagos junto con el 
nombre de sus representantes de ventas.
```
http://localhost:5000/api/Client/clientsPaymentsAndSeller
```
3. Muestra el nombre de los clientes que no hayan realizado pagos junto con 
el nombre de sus representantes de ventas.
```
http://localhost:5000/api/Client/clientsWithoutPaymentsAndSeller
```
4. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus 
representantes junto con la ciudad de la oficina a la que pertenece el 
representante.
```
http://localhost:5000/api/Client/clientsPaymentsAndSellerOffice
```
5. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre 
de sus representantes junto con la ciudad de la oficina a la que pertenece el 
representante.
```
http://localhost:5000/api/Client/clientsWithoutPaymentsAndSellerOffice
```
6. Devuelve un listado que muestre el nombre de cada empleados, el nombre 
de su jefe y el nombre del jefe de sus jefe.
```
http://localhost:5000/api/Employee/employeesWithBoss
```
7. Devuelve el nombre de los clientes a los que no se les ha entregado a 
tiempo un pedido.
```
http://localhost:5000/api/Client/requestLate
```
8. Devuelve un listado de las diferentes gamas de producto que ha comprado 
cada cliente
```
http://localhost:5000/api/Client/producttypeByClient
```
<b>Consultas multitabla (Composición externa) </b>
1. Devuelve un listado que muestre solamente los clientes que no han 
realizado ningún pago.
```
http://localhost:5000/api/Client/clientsWithoutPayments
```
2. Devuelve un listado que muestre los clientes que no han realizado ningún 
pago y los que no han realizado ningún pedido.
```
http://localhost:5000/api/Client/clientsWithoutPaymentsANDrequest
```
3. Devuelve un listado que muestre solamente los empleados que no tienen un 
cliente asociado junto con los datos de la oficina donde trabajan.
```
http://localhost:5000/api/Employee/employeesWithoutClients
```
4. Devuelve un listado que muestre los empleados que no tienen una oficina 
asociada y los que no tienen un cliente asociado.
```
http://localhost:5000/api/Employee/employeesWithoutClientsAndOffice
```
5. Devuelve un listado de los productos que nunca han aparecido en un 
pedido.
```
http://localhost:5000/api/Product/productsWithoutRequest
```
6. Devuelve un listado de los productos que nunca han aparecido en un 
pedido. El resultado debe mostrar el nombre, la descripción y la imagen del 
producto.
```
http://localhost:5000/api/Product/productsWithoutRequest2
```
7. Devuelve las oficinas donde no trabajan ninguno de los empleados que 
hayan sido los representantes de ventas de algún cliente que haya realizado 
la compra de algún producto de la gama Frutales.
```
http://localhost:5000/api/Office/officesWithoutEmployee
```
8. Devuelve un listado con los clientes que han realizado algún pedido pero no 
han realizado ningún pago.
```
http://localhost:5000/api/Client/clientsRequestWithoutPayments
```
9. Devuelve un listado con los datos de los empleados que no tienen clientes 
asociados y el nombre de su jefe asociado.
```
http://localhost:5000/api/Employee/employeesBossWithoutClients
```
<b>Consultas resumen </b>
1. ¿Cuántos empleados hay en la compañía?
```

```
2. ¿Cuántos clientes tiene cada país?
```

```
3. ¿Cuál fue el pago medio en 2009?
```

```
4. ¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma 
descendente por el número de pedidos.
```

```
5. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
```

```
6. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan 
por M?
```

```
7. Devuelve el nombre de los representantes de ventas y el número de clientes 
al que atiende cada uno.
```

```
8. Calcula el número de clientes que no tiene asignado representante de 
ventas.
```

```
9. Calcula la fecha del primer y último pago realizado por cada uno de los 
clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.
```

```
10. Calcula el número de productos diferentes que hay en cada uno de los 
pedidos.
```

```
11. Calcula la suma de la cantidad total de todos los productos que aparecen en 
cada uno de los pedidos.
```

```
12. Devuelve un listado de los 20 productos más vendidos y el número total de 
unidades que se han vendido de cada uno. El listado deberá estar ordenado 
por el número total de unidades vendidas.
```

```
13. La misma información que en la pregunta anterior, pero agrupada por 
código de producto.
```

```
14. La misma información que en la pregunta anterior, pero agrupada por 
código de producto filtrada por los códigos que empiecen por OR.
```

```
15. Lista las ventas totales de los productos que hayan facturado más de 3000 
euros. Se mostrará el nombre, unidades vendidas, total facturado y total 
facturado con impuestos (21% IVA).
```

```
16. Muestre la suma total de todos los pagos que se realizaron para cada uno 
de los años que aparecen en la tabla pagos.
```

```
<b> Subconsultas </b>
1. Devuelve el nombre del cliente con mayor límite de crédito.
```

```
2. Devuelve el nombre del producto que tenga el precio de venta más caro.
```

```
3. Devuelve el nombre del producto del que se han vendido más unidades. 
(Tenga en cuenta que tendrá que calcular cuál es el número total de 
unidades que se han vendido de cada producto a partir de los datos de la 
tabla detalle_pedido)
```

```
4. Los clientes cuyo límite de crédito sea mayor que los pagos que haya 
realizado. (Sin utilizar INNER JOIN).
```

```
8. Devuelve el nombre del cliente con mayor límite de crédito.
```

```
9. Devuelve el nombre del producto que tenga el precio de venta más caro.
```

```
11. Devuelve un listado que muestre solamente los clientes que no han 
realizado ningún pago.
```

```
12. Devuelve un listado que muestre solamente los clientes que sí han realizado 
algún pago.
```

```
13. Devuelve un listado de los productos que nunca han aparecido en un 
pedido.
```

```
14. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos 
empleados que no sean representante de ventas de ningún cliente.
```

```
18. Devuelve un listado que muestre solamente los clientes que no han 
realizado ningún pago.
```

```
19. Devuelve un listado que muestre solamente los clientes que sí han realizado 
algún pago.
```

```
<b>Consultas variadas </b>
1. Devuelve el listado de clientes indicando el nombre del cliente y cuántos 
pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no 
han realizado ningún pedido.
```

```
2. Devuelve el nombre de los clientes que hayan hecho pedidos en 2008 
ordenados alfabéticamente de menor a mayor.
```

```
3. Devuelve el nombre del cliente, el nombre y primer apellido de su 
representante de ventas y el número de teléfono de la oficina del 
representante de ventas, de aquellos clientes que no hayan realizado ningún 
pago.
```

```
4. Devuelve el listado de clientes donde aparezca el nombre del cliente, el 
nombre y primer apellido de su representante de ventas y la ciudad donde 
está su oficina.
```

```
5. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos 
empleados que no sean representante de ventas de ningún cliente.
```

```
