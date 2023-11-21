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
## DDL (Data Definition Language)
* [Link](https://github.com/Marsh1100/api-gardening/blob/main/DDL-gardening.sql) para acceder al DDL de la BD.
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
Ejemplo al intentar acceder más de 3 veces en 10 segundos a la consulta<br>
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
  * Consulta en SQL
```sql
SELECT * FROM client
WHERE country = 'Spain';
```
2. Devuelve un listado con los distintos estados por los que puede pasar un 
pedido.
```
http://localhost:5000/api/Request/states
```
  * Consulta en SQL
```sql
SELECT state AS state_request
FROM request
GROUP BY request.state;
```
3. Devuelve un listado con el código de cliente de aquellos clientes que 
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar 
aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta
```
http://localhost:5000/api/Client/clientsPay2008
```
  • Utilizando la función YEAR de MySQL.
```sql
SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE YEAR(paymentDate) = 2008;
```
  • Utilizando la función DATE_FORMAT de MySQL.
```sql
SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE DATE_FORMAT(paymentDate, '%Y') = 2008;
```
  • Sin utilizar ninguna de las funciones anteriores.
```sql
SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE paymentDate BETWEEN '2008-01-01' AND '2008-12-31' ; 
```

9. Devuelve un listado con el código de pedido, código de cliente, fecha 
esperada y fecha de entrega de los pedidos que no han sido entregados a 
tiempo.
```
http://localhost:5000/api/Request/requestLate
```
  * Consulta en SQL
```sql
SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE deliveryDate > expectedDate;
```
10. Devuelve un listado con el código de pedido, código de cliente, fecha 
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al 
menos dos días antes de la fecha esperada.
```
http://localhost:5000/api/Request/requestEarly
```
• Utilizando la función ADDDATE de MySQL.
```sql
SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE ADDDATE(expectedDate, INTERVAL -2 day ) >= deliveryDate;
```
• Utilizando la función DATEDIFF de MySQL.
```sql
SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE DATEDIFF(expectedDate, deliveryDate ) >= 2;
```
• ¿Sería posible resolver esta consulta utilizando el operador de suma + o 
resta -?
  * Consulta en SQL
```sql

```

11. Devuelve un listado de todos los pedidos que fueron rechazados en 2009.
```
http://localhost:5000/api/Request/requestReject
```
  * Consulta en SQL
```sql
SELECT * FROM request
WHERE state = 'Rechazado' AND YEAR(requestDate) = 2009;
```
12. Devuelve un listado de todos los pedidos que han sido entregados en el 
mes de enero de cualquier año.
```
http://localhost:5000/api/Request/requestDelivered
```
  * Consulta en SQL
```sql
SELECT * FROM request
WHERE state = 'Rechazado' AND DATE_FORMAT(requestDate, '%m') = 01;
```
13. Devuelve un listado con todos los pagos que se realizaron en el 
año 2008 mediante Paypal. Ordene el resultado de mayor a menor.
```
http://localhost:5000/api/Payment/payment2008
```
  * Consulta en SQL
```sql
SELECT * FROM payment
WHERE paymentMethod = 'PayPal' AND DATE_FORMAT(paymentDate , '%Y') = 2008
ORDER BY total DESC;
```
14. Devuelve un listado con todas las formas de pago que aparecen en la 
tabla pago. Tenga en cuenta que no deben aparecer formas de pago 
repetidas.
```
http://localhost:5000/api/Payment/paymentMethod
```
  * Consulta en SQL
```sql
SELECT paymentMethod FROM payment
GROUP BY paymentMethod;
```
15. Devuelve un listado con todos los productos que pertenecen a la 
gama Ornamentales y que tienen más de 100 unidades en stock. El listado 
deberá estar ordenado por su precio de venta, mostrando en primer lugar 
los de mayor precio.
```
http://localhost:5000/api/Product/productsOrnamentales
```
  * Consulta en SQL
```sql
SELECT product.id, product.productCode, product.name 
FROM product
INNER JOIN producttype ON product.idProductType = producttype.id
WHERE producttype.type = 'Ornamentales' AND product.stock>100 
ORDER BY product.salePrice DESC;
```
16. Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y 
cuyo representante de ventas tenga el código de empleado 11 o 30.
```
http://localhost:5000/api/Client/clientsMadrid
```
  * Consulta en SQL
```sql
SELECT * FROM client
WHERE city = 'Madrid' AND (idEmployee= 11 OR idEmployee = 30);
```
<b>Consultas multitabla (Composición interna) </b>
1. Obtén un listado con el nombre de cada cliente y el nombre y apellido de su 
representante de ventas.
```
http://localhost:5000/api/Client/clientsAndSeller
```
  * Consulta en SQL
```sql
SELECT client.nameclient AS client_name, CONCAT(employee.name," ",employee.firstSurname," ",employee.secondSurname) AS employee
FROM client 
JOIN employee ON client.idEmployee = employee.id;
```
2. Muestra el nombre de los clientes que hayan realizado pagos junto con el 
nombre de sus representantes de ventas.
```
http://localhost:5000/api/Client/clientsPaymentsAndSeller
```
  * Consulta en SQL
```sql
SELECT DISTINCT client.id, client.nameclient AS client_name, employee.name AS employee
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
WHERE payment.id IS NOT NULL;
```
3. Muestra el nombre de los clientes que no hayan realizado pagos junto con 
el nombre de sus representantes de ventas.
```
http://localhost:5000/api/Client/clientsWithoutPaymentsAndSeller
```
  * Consulta en SQL
```sql
SELECT DISTINCT client.id, client.nameclient AS client_name, employee.name AS employee
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
WHERE payment.id IS NULL;
```
4. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus 
representantes junto con la ciudad de la oficina a la que pertenece el 
representante.
```
http://localhost:5000/api/Client/clientsPaymentsAndSellerOffice
```
  * Consulta en SQL
```sql
SELECT DISTINCT client.nameclient AS client_name, employee.name AS employee,
office.city AS office_city
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id
WHERE payment.id IS NOT NULL;
```
5. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre 
de sus representantes junto con la ciudad de la oficina a la que pertenece el 
representante.
```
http://localhost:5000/api/Client/clientsWithoutPaymentsAndSellerOffice
```
  * Consulta en SQL
```sql
SELECT  client.nameclient AS client_name, employee.name AS employee,
office.city AS office_city
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id
WHERE payment.id IS NULL;
```
6. Devuelve un listado que muestre el nombre de cada empleados, el nombre 
de su jefe y el nombre del jefe de sus jefe.
```
http://localhost:5000/api/Employee/employeesWithBoss
```
  * Consulta en SQL
```sql
SELECT emp.name AS employee, ifnull(boss.name, "-") AS boss, ifnull(superBoss.name,"-") AS superboss
FROM employee emp 
LEFT JOIN employee boss ON emp.idBoss = boss.id
LEFT JOIN employee superBoss ON boss.idBoss = superBoss.id;
```
7. Devuelve el nombre de los clientes a los que no se les ha entregado a 
tiempo un pedido.
```
http://localhost:5000/api/Client/requestLate
```
  * Consulta en SQL
```sql
SELECT DISTINCT nameClient 
FROM client
JOIN request ON client.id = request.idClient
WHERE request.deliveryDate > request.expectedDate;
```
8. Devuelve un listado de las diferentes gamas de producto que ha comprado 
cada cliente
```
http://localhost:5000/api/Client/producttypeByClient
```
  * Consulta en SQL
```sql
SELECT DISTINCT client.nameClient, producttype.type
FROM client 
JOIN request  ON client.id = request.idClient
JOIN requestdetail ON request.id = requestdetail.idRequest
JOIN product  ON requestdetail.idProduct = product.id
JOIN producttype  ON product.idProductType = producttype.id
GROUP BY client.nameClient, producttype.type;
```
<b>Consultas multitabla (Composición externa) </b>
1. Devuelve un listado que muestre solamente los clientes que no han 
realizado ningún pago.
```
http://localhost:5000/api/Client/clientsWithoutPayments
```
  * Consulta en SQL
```sql
SELECT * FROM viewClient client
LEFT JOIN payment ON client.id = payment.idClient
WHERE payment.idClient IS NULL;
```
2. Devuelve un listado que muestre los clientes que no han realizado ningún 
pago y los que no han realizado ningún pedido.
```
http://localhost:5000/api/Client/clientsWithoutPaymentsANDrequest
```
  * Consulta en SQL
```sql
SELECT * FROM viewClient client
LEFT JOIN payment ON client.id = payment.idClient
LEFT JOIN request ON client.id = request.idClient
WHERE payment.idClient IS NULL AND request.idClient IS NULL;
```
3. Devuelve un listado que muestre solamente los empleados que no tienen un 
cliente asociado junto con los datos de la oficina donde trabajan.
```
http://localhost:5000/api/Employee/employeesWithoutClients
```
  * Consulta en SQL
```sql
SELECT DISTINCT employee.id,CONCAT(employee.name," ", employee.firstSurname," ",employee.secondSurname) AS empleado, client.nameClient,
office.officineCode, office.city, office.region, office.zipCode, office.phone, office.address1, office.address2
FROM employee
LEFT JOIN client ON employee.id = client.idEmployee
INNER JOIN office ON employee.idOffice = office.id
WHERE client.Id IS NULL;
```
4. Devuelve un listado que muestre los empleados que no tienen una oficina 
asociada y los que no tienen un cliente asociado.
```
http://localhost:5000/api/Employee/employeesWithoutClientsAndOffice
```
  * Consulta en SQL
```sql
SELECT DISTINCT employee.id,CONCAT(employee.name," ", employee.firstSurname," ",employee.secondSurname) AS empleado
FROM employee
LEFT JOIN client ON employee.id = client.idEmployee
LEFT JOIN office ON employee.idOffice = office.id
WHERE client.Id IS NULL AND employee.idOffice IS NULL;
```
5. Devuelve un listado de los productos que nunca han aparecido en un 
pedido.
```
http://localhost:5000/api/Product/productsWithoutRequest
```
  * Consulta en SQL
```sql
SELECT product.id, product.productCode, product.name, product.idProductType, product.dimensions, product.provider,
product.description, product.stock, product.salePrice, product.providerPrice
FROM product
LEFT JOIN requestdetail ON product.id = requestdetail.idProduct
WHERE requestdetail.id IS NULL;
```
6. Devuelve un listado de los productos que nunca han aparecido en un 
pedido. El resultado debe mostrar el nombre, la descripción y la imagen del 
producto.
```
http://localhost:5000/api/Product/productsWithoutRequest2
```
  * Consulta en SQL
```sql
SELECT  product.name, producttype.type, producttype.descriptionText, producttype.image
FROM product
LEFT JOIN requestdetail ON product.id = requestdetail.idProduct
INNER JOIN producttype ON product.idProductType = producttype.id
WHERE requestdetail.id IS NULL;
```
7. Devuelve las oficinas donde no trabajan ninguno de los empleados que 
hayan sido los representantes de ventas de algún cliente que haya realizado 
la compra de algún producto de la gama Frutales.
```
http://localhost:5000/api/Office/officesWithoutEmployee
```
  * Consulta en SQL
```sql
SELECT DISTINCT office.officineCode, office.address1, office.city
FROM employee
JOIN office ON employee.idOffice = office.id
JOIN client ON employee.id = client.id
JOIN request ON client.id = request.idClient
JOIN requestdetail ON request.id = requestdetail.idRequest
JOIN product ON requestdetail.idProduct = product.id
JOIN producttype ON product.idProductType = producttype.id
WHERE producttype.type != 'Frutales'; 
```
8. Devuelve un listado con los clientes que han realizado algún pedido pero no 
han realizado ningún pago.
```
http://localhost:5000/api/Client/clientsRequestWithoutPayments
```
  * Consulta en SQL
```sql
SELECT DISTINCT client.id, client.nameClient, client.nameContact, client.lastnameContact, client.phoneNumber, client.fax, 
client.address1, client.address2, client.city, client.region, client.country, client.zipCode, client.idEmployee, client.creditLimit
FROM client
JOIN request ON client.id = request.idClient
LEFT JOIN payment ON client.id = payment.idClient
WHERE client.id IN (SELECT idClient FROM request) AND payment.id IS NULL;
```
9. Devuelve un listado con los datos de los empleados que no tienen clientes 
asociados y el nombre de su jefe asociado.
```
http://localhost:5000/api/Employee/employeesBossWithoutClients
```
  * Consulta en SQL
```sql
SELECT CONCAT(emp.name," ", emp.firstSurname," ",emp.secondSurname) AS employee, IF(empBoss.name is null, 'No tiene jefe', empBoss.name) AS boss
FROM employee emp
LEFT JOIN employee empBoss ON emp.idBoss = empBoss.id
LEFT JOIN client ON emp.id = client.idEmployee
WHERE client.id IS NULL;
```
<b>Consultas resumen </b>
1. ¿Cuántos empleados hay en la compañía?
```
http://localhost:5000/api/Employee/totalEmployees
```
  * Consulta en SQL
```sql
SELECT COUNT(employee.id) AS quantity_employees
FROM employee;
```
2. ¿Cuántos clientes tiene cada país?
```
http://localhost:5000/api/Client/totalEmployeesByCountry
```
  * Consulta en SQL
```sql
SELECT client.country, COUNT(client.country) AS quantity_clients
FROM client
GROUP BY client.country;
```
3. ¿Cuál fue el pago medio en 2009?
```
http://localhost:5000/api/Payment/averagePay2009
```
  * Consulta en SQL
```sql
SELECT YEAR(payment.paymentDate) AS year, AVG(payment.total) AS average_pay
FROM payment
WHERE YEAR(payment.paymentDate) = 2009;
```
4. ¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma 
descendente por el número de pedidos.
```
http://localhost:5000/api/Request/requestByState
```
  * Consulta en SQL
```sql
SELECT request.state, COUNT(request.id) AS total_requests
FROM request
GROUP BY request.state
ORDER BY total_requests DESC;
```
5. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
```
http://localhost:5000/api/Client/totalClientsMadrid
```
  * Consulta en SQL
```sql
SELECT client.city,COUNT(client.id) AS quantity_clients
FROM client
WHERE client.city = 'Madrid';
```
6. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan 
por M?
```
http://localhost:5000/api/Client/totalClientsM
```
  * Consulta en SQL
```sql
SELECT client.city, COUNT(client.id) AS quantity_clients
FROM client
GROUP BY client.city
HAVING city LIKE 'M%';
```
7. Devuelve el nombre de los representantes de ventas y el número de clientes 
al que atiende cada uno.
```
http://localhost:5000/api/Client/totalclientsByEmployee
```
  * Consulta en SQL
```sql
SELECT employee.id AS id_employee,employee.name AS employee, COUNT(client.id) AS quantity_clients 
FROM employee
JOIN client ON employee.id = client.idEmployee
GROUP BY employee.id;
```
8. Calcula el número de clientes que no tiene asignado representante de 
ventas.
```
http://localhost:5000/api/Client/quantityWithoutSeller
```
  * Consulta en SQL
```sql
SELECT COUNT(client.id) AS quantiy_clients
FROM client
WHERE client.idEmployee IS NULL;
```
9. Calcula la fecha del primer y último pago realizado por cada uno de los 
clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.
```
http://localhost:5000/api/Client/clientsPaymentsDate
```
  * Consulta en SQL
```sql
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS client,MIN(payment.paymentDate) AS first_payment ,MAX(payment.paymentDate) AS second_payment
FROM client
JOIN payment ON client.id = payment.idClient
GROUP BY client.id;
```
10. Calcula el número de productos diferentes que hay en cada uno de los 
pedidos.
```
http://localhost:5000/api/Request/quantityProducts
```
  * Consulta en SQL
```sql
SELECT request.id AS id_request, COUNT(DISTINCT requestdetail.idProduct ) AS cantidad_productos
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idRequest;
```
11. Calcula la suma de la cantidad total de todos los productos que aparecen en 
cada uno de los pedidos.
```
http://localhost:5000/api/Request/sumProductsRequest
```
  * Consulta en SQL
```sql
SELECT request.id AS id_request, SUM(requestdetail.unitPrice * requestdetail.quantity ) AS total_products
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idRequest;
```
12. Devuelve un listado de los 20 productos más vendidos y el número total de 
unidades que se han vendido de cada uno. El listado deberá estar ordenado 
por el número total de unidades vendidas.
```
http://localhost:5000/api/Request/products20Sold
```
  * Consulta en SQL
```sql
SELECT product.id AS product_id ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
ORDER BY  total_units_sold DESC 
LIMIT 20;
```
13. La misma información que en la pregunta anterior, pero agrupada por 
código de producto.
```
http://localhost:5000/api/Request/productsCode20Sold
```
  * Consulta en SQL
```sql
SELECT product.productCode AS code_product ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
ORDER BY total_units_sold DESC 
LIMIT 20;
```
14. La misma información que en la pregunta anterior, pero agrupada por 
código de producto filtrada por los códigos que empiecen por OR.
```
http://localhost:5000/api/Request/productsCode20StartOR
```
  * Consulta en SQL
```sql
SELECT product.productCode AS code_product ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
WHERE product.productCode LIKE 'OR%'
GROUP BY requestdetail.idProduct
ORDER BY total_units_sold DESC 
LIMIT 20;
```
15. Lista las ventas totales de los productos que hayan facturado más de 3000 
euros. Se mostrará el nombre, unidades vendidas, total facturado y total 
facturado con impuestos (21% IVA).
```
http://localhost:5000/api/Request/productsTotal3000
```
  * Consulta en SQL
```sql
SELECT product.name AS product_name, SUM(requestdetail.quantity*requestdetail.unitPrice) AS total_sold,
SUM((requestdetail.quantity*requestdetail.unitPrice)*1.21) AS total_tax
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
HAVING SUM(requestdetail.quantity*requestdetail.unitPrice)>3000
ORDER BY total_sold DESC;
```
16. Muestre la suma total de todos los pagos que se realizaron para cada uno 
de los años que aparecen en la tabla pagos.
```
http://localhost:5000/api/Payment/paymentByYear
```
  * Consulta en SQL
```sql
SELECT YEAR(payment.paymentDate) AS year,SUM(payment.total) AS total_payment
FROM payment 
GROUP BY YEAR(payment.paymentDate);
```
<b> Subconsultas </b>
1. Devuelve el nombre del cliente con mayor límite de crédito.
```
http://localhost:5000/api/Client/clientCreditlimit
```
  * Consulta en SQL
```sql
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS client, client.creditLimit AS  max_credit
FROM client
WHERE client.creditlimit = (SELECT MAX(creditLimit) FROM client);
```
2. Devuelve el nombre del producto que tenga el precio de venta más caro.
```
http://localhost:5000/api/Product/moreExpensivePrice
```
  * Consulta en SQL
```sql
SELECT product.name AS product_name
FROM product
WHERE product.salePrice = (SELECT MAX(salePrice) FROM product);
```
3. Devuelve el nombre del producto del que se han vendido más unidades. 
(Tenga en cuenta que tendrá que calcular cuál es el número total de 
unidades que se han vendido de cada producto a partir de los datos de la 
tabla detalle_pedido)
```
http://localhost:5000/api/Product/mostSold
```
  * Consulta en SQL
```sql
SELECT product.id AS product_id ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
HAVING SUM(requestdetail.quantity) = (SELECT MAX(quantity) FROM requestdetail);
```
4. Los clientes cuyo límite de crédito sea mayor que los pagos que haya 
realizado. (Sin utilizar INNER JOIN).
```
http://localhost:5000/api/Client/clientCreditlimitGreaterPayments
```
  * Consulta en SQL
```sql
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS cliente
FROM client 
WHERE client.creditLimit > (SELECT SUM(total) AS pay_client FROM payment WHERE payment.idClient = client.id);
```
8. Devuelve el nombre del cliente con mayor límite de crédito.
```
http://localhost:5000/api/Client/clientCreditlimit2
```
  * Consulta en SQL
```sql
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS cliente, client.creditLimit
FROM client
WHERE  creditLimit = (SELECT MAX(creditLimit) FROM client);
```
9. Devuelve el nombre del producto que tenga el precio de venta más caro.
```
http://localhost:5000/api/Product/moreExpensivePrice2
```
  * Consulta en SQL
```sql
SELECT product.name AS product_name
FROM product
WHERE  salePrice = (SELECT MAX(salePrice) FROM product);
```
11. Devuelve un listado que muestre solamente los clientes que no han 
realizado ningún pago.
```
http://localhost:5000/api/Client/clientsWithoutPayments2
```
  * Consulta en SQL
```sql
SELECT client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE client.id NOT IN (SELECT idClient FROM payment);
```
12. Devuelve un listado que muestre solamente los clientes que sí han realizado 
algún pago.
```
http://localhost:5000/api/Client/clientsWithPayments
```
  * Consulta en SQL
```sql
SELECT client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE client.id IN (SELECT idClient FROM payment);
```
13. Devuelve un listado de los productos que nunca han aparecido en un 
pedido.
```
http://localhost:5000/api/Product/productsWithoutRequest3
```
  * Consulta en SQL
```sql
SELECT * FROM product
WHERE product.id NOT IN (SELECT idProduct FROM requestdetail);
```
14. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos 
empleados que no sean representante de ventas de ningún cliente.
```
http://localhost:5000/api/Employee/employeesWithoutClients2
```
  * Consulta en SQL
```sql
SELECT CONCAT(employee.name," ", employee.firstSurname," ",employee.secondSurname) AS employee, employee.position, office.phone AS office_phone
FROM employee
JOIN office ON employee.idOffice = office.id
WHERE employee.id NOT IN (SELECT idEmployee FROM client);
```
18. Devuelve un listado que muestre solamente los clientes que no han 
realizado ningún pago.
```
http://localhost:5000/api/Client/clientsWithoutPayments3
```
  * Consulta en SQL
```sql
SELECT  client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE NOT EXISTS(SELECT idClient FROM payment WHERE client.id = payment.idClient);
```
19. Devuelve un listado que muestre solamente los clientes que sí han realizado 
algún pago.
```
http://localhost:5000/api/Client/clientPayments
```
  * Consulta en SQL
```sql
SELECT  client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE EXISTS(SELECT idClient FROM payment WHERE client.id = payment.idClient);
```
<b>Consultas variadas </b>
1. Devuelve el listado de clientes indicando el nombre del cliente y cuántos 
pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no 
han realizado ningún pedido.
```
http://localhost:5000/api/Client/clientQuantityPayments
```
  * Consulta en SQL
```sql
SELECT  client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client, IFNULL(COUNT(request.idClient), 0) AS quantity_payments
FROM  client 
LEFT JOIN request ON client.id = request.idClient
GROUP BY client.id;
```
2. Devuelve el nombre de los clientes que hayan hecho pedidos en 2008 
ordenados alfabéticamente de menor a mayor.
```
http://localhost:5000/api/Client/clientsRequest2008
```
  * Consulta en SQL
```sql
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client 
JOIN request ON client.id = request.idClient
WHERE YEAR(request.requestDate) = 2008;
```
3. Devuelve el nombre del cliente, el nombre y primer apellido de su 
representante de ventas y el número de teléfono de la oficina del 
representante de ventas, de aquellos clientes que no hayan realizado ningún 
pago.
```
http://localhost:5000/api/Client/clientsWithoutPayments4
```
  * Consulta en SQL
```sql
SELECT client.nameContact AS client, CONCAT(employee.name," ",employee.firstSurname) AS sales_representative,
office.phone AS office_phone
FROM client
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id
WHERE payment.idClient IS NULL
ORDER BY client.nameContact;
```
4. Devuelve el listado de clientes donde aparezca el nombre del cliente, el 
nombre y primer apellido de su representante de ventas y la ciudad donde 
está su oficina.
```
http://localhost:5000/api/Client/clientsWihtEmployeeAndOffice
```
  * Consulta en SQL
```sql
SELECT client.id as id_client,client.nameContact AS client, CONCAT(employee.name," ",employee.firstSurname) AS sales_representative,
office.city AS office_city
FROM client
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id;
```
5. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos 
empleados que no sean representante de ventas de ningún cliente.
```
http://localhost:5000/api/Employee/employeesWithoutClients3
```
  * Consulta en SQL
```sql
SELECT employee.id AS id_employee,CONCAT(employee.name," ",employee.firstSurname) AS employee, employee.position, office.officineCode AS office_code
FROM employee
JOIN office ON employee.idOffice = office.id
WHERE employee.id NOT IN (SELECT idEmployee FROM client);
```
