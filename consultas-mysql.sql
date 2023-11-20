						-- Consultas gardening --

USE gardening;
/*1. Devuelve un listado con el nombre de los todos los clients españoles.*/
SELECT *
FROM gardening.client
WHERE client.country = 'Spain';

/*2.Devuelve un listado con los distintos estados por los que puede pasar un
pedido.*/
SELECT state AS state_request
FROM request
GROUP BY request.state;

/*3. Devuelve un listado con el código de client de aquellos clients que
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar
aquellos códigos de client que aparezcan repetidos. Resuelva la consulta:*/

-- • Utilizando la función YEAR de MySQL.
SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE YEAR(paymentDate) = 2008;
 
-- • Utilizando la función DATE_FORMAT de MySQL.
SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE DATE_FORMAT(paymentDate, '%Y') = 2008;
 
-- • Sin utilizar ninguna de las funciones anteriores.
SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE paymentDate BETWEEN '2008-01-01' AND '2008-12-31' ; 

/*9. Devuelve un listado con el código de pedido, código de client, fecha
esperada y fecha de entrega de los pedidos que no han sido entregados a
tiempo.*/

SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE deliveryDate > expectedDate;

/*10.Devuelve un listado con el código de pedido, código de client, fecha
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al
menos dos días antes de la fecha esperada.
• ¿Sería posible resolver esta consulta utilizando el operador de suma + o
resta -?*/
-- • Utilizando la función ADDDATE de MySQL.
SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE ADDDATE(expectedDate, INTERVAL -2 day ) >= deliveryDate;

-- • Utilizando la función DATEDIFF de MySQL.
SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE DATEDIFF(expectedDate, deliveryDate ) >= 2;

-- 11. Devuelve un listado de todos los pedidos que fueron rechazados en 2009.
SELECT * 
FROM request
WHERE state = 'Rechazado' AND YEAR(requestDate) = 2009;

-- 12. Devuelve un listado de todos los pedidos que han sido entregados en el mes de enero de cualquier año.
SELECT * 
FROM request
WHERE state = 'Entregado' AND DATE_FORMAT(requestDate, '%m') = 01;

/* 13. Devuelve un listado con todos los pagos que se realizaron en el 
año 2008 mediante Paypal. Ordene el resultado de mayor a menor*/
SELECT * 
FROM payment
WHERE paymentMethod = 'PayPal' AND DATE_FORMAT(paymentDate , '%Y') = 2008
ORDER BY total DESC;

/*14.  Devuelve un listado con todas las formas de pago que aparecen en la 
tabla pago. Tenga en cuenta que no deben aparecer formas de pago 
repetidas*/
SELECT paymentMethod
FROM payment
GROUP BY paymentMethod;

/*15.  Devuelve un listado con todos los productos que pertenecen a la gama 
Ornamentales y que tienen más de 100 unidades en stock. El listado  deberá 
estar ordenado por su precio de venta, mostrando en primer lugar los de mayor 
precio.*/

SELECT product.id, product.productCode, product.name 
FROM product
INNER JOIN producttype ON product.idProductType = producttype.id
WHERE producttype.type = 'Ornamentales' AND product.stock>100 
ORDER BY product.salePrice DESC;

/*16. Devuelve un listado con todos los clients que sean de la ciudad de Madrid y 
cuyo representante de ventas tenga el código de empleado 11 o 30 */

SELECT * 
FROM client
WHERE city = 'Madrid' AND (idEmployee= 11 OR idEmployee = 30);

-- 1.4.5 Consultas multitabla (Composición interna)
/*CI 1. Obtén un listado con el nombre de cada cliente y el nombre y apellido de su
representante de ventas.*/
SELECT client.nameclient AS client_name, CONCAT(employee.name," ",employee.firstSurname," ",employee.secondSurname) AS employee
FROM client 
JOIN employee ON client.idEmployee = employee.id;

/*CI 2. Muestra el nombre de los clientes que hayan realizado pagos junto con el
nombre de sus representantes de ventas.*/
SELECT DISTINCT client.id, client.nameclient AS client_name, employee.name AS employee
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
WHERE payment.id IS NOT NULL;
/*CI 3. Muestra el nombre de los clientes que no hayan realizado pagos junto con
el nombre de sus representantes de ventas.*/
SELECT DISTINCT client.id, client.nameclient AS client_name, employee.name AS employee
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
WHERE payment.id IS NULL;

/*CI 4. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus
representantes junto con la ciudad de la oficina a la que pertenece el
representante.*/
SELECT DISTINCT client.nameclient AS client_name, employee.name AS employee,
office.city AS office_city
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id
WHERE payment.id IS NOT NULL;

/*CI 5. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre
de sus representantes junto con la ciudad de la oficina a la que pertenece el
representante. */
SELECT  client.nameclient AS client_name, employee.name AS employee,
office.city AS office_city
FROM client 
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id
WHERE payment.id IS NULL;

/*CI 6. Devuelve un listado que muestre el nombre de cada empleados, el nombre
de su jefe y el nombre del jefe de sus jefe. */
SELECT emp.name AS employee, ifnull(boss.name, "-") AS boss, ifnull(superBoss.name,"-") AS superboss
FROM employee emp 
LEFT JOIN employee boss ON emp.idBoss = boss.id
LEFT JOIN employee superBoss ON boss.idBoss = superBoss.id;

/* CI 7. Devuelve el nombre de los clientes a los que no se les ha entregado a
tiempo un pedido. */
SELECT DISTINCT client.nameClient 
FROM client
JOIN request ON client.id = request.idClient
WHERE request.deliveryDate > request.expectedDate;


/* CI 8. Devuelve un listado de las diferentes gamas de producto que ha comprado
cada cliente.*/

SELECT DISTINCT client.nameClient, producttype.type
FROM client 
JOIN request  ON client.id = request.idClient
JOIN requestdetail ON request.id = requestdetail.idRequest
JOIN product  ON requestdetail.idProduct = product.id
JOIN producttype  ON product.idProductType = producttype.id
GROUP BY client.nameClient, producttype.type;


-- 1.4.6 Consultas multitabla (Composición externa)
/*CE 1.Devuelve un listado que muestre solamente los clients que no han
realizado ningún pago.*/
CREATE VIEW viewclient AS
SELECT client.id, client.nameClient, client.nameContact, client.lastnameContact, client.phoneNumber, client.fax, 
client.address1, client.address2, client.city, client.region, client.country, client.zipCode, client.idEmployee, client.creditLimit
FROM client;

SELECT *
FROM viewClient client
LEFT JOIN payment ON client.id = payment.idClient
WHERE payment.idClient IS NULL;

/*CE 2.Devuelve un listado que muestre los clients que no han realizado ningún
pago y los que no han realizado ningún pedido*/
SELECT *
FROM viewClient client
LEFT JOIN payment ON client.id = payment.idClient
LEFT JOIN request ON client.id = request.idClient
WHERE payment.idClient IS NULL AND request.idClient IS NULL;

/*CE 3.Devuelve un listado que muestre solamente los empleados que no tienen un
client asociado junto con los datos de la oficina donde trabajan. */

SELECT DISTINCT employee.id,CONCAT(employee.name," ", employee.firstSurname," ",employee.secondSurname) AS empleado, client.nameClient,
office.officineCode, office.city, office.region, office.zipCode, office.phone, office.address1, office.address2
FROM employee
LEFT JOIN client ON employee.id = client.idEmployee
INNER JOIN office ON employee.idOffice = office.id
WHERE client.Id IS NULL;

/*CE 4.Devuelve un listado que muestre los empleados que no tienen una oficina
asociada y los que no tienen un client asociado. */

SELECT DISTINCT employee.id,CONCAT(employee.name," ", employee.firstSurname," ",employee.secondSurname) AS empleado
FROM employee
LEFT JOIN client ON employee.id = client.idEmployee
LEFT JOIN office ON employee.idOffice = office.id
WHERE client.Id IS NULL AND employee.idOffice IS NULL;

/* CE 5.Devuelve un listado de los productos que nunca han aparecido en un
pedido.*/
SELECT product.id, product.productCode, product.name, product.idProductType, product.dimensions, product.provider,
product.description, product.stock, product.salePrice, product.providerPrice
FROM product
LEFT JOIN requestdetail ON product.id = requestdetail.idProduct
WHERE requestdetail.id IS NULL;

/*CE 6.Devuelve un listado de los productos que nunca han aparecido en un pedido. 
El resultado debe mostrar el nombre, la descripción y la imagen del producto.*/
SELECT  product.name, producttype.type, producttype.descriptionText, producttype.image
FROM product
LEFT JOIN requestdetail ON product.id = requestdetail.idProduct
INNER JOIN producttype ON product.idProductType = producttype.id
WHERE requestdetail.id IS NULL;

/*CE 7.Devuelve las oficinas donde no trabajan ninguno de los empleados que
hayan sido los representantes de ventas de algún client que haya realizado
la compra de algún producto de la gama Frutales.*/

/*CE 8.Devuelve un listado con los clients que han realizado algún pedido pero no 
han realizado ningún pago.*/
/*CE 9.Devuelve un listado con los datos de los empleados que no tienen clients 
asociados y el nombre de su jefe asociado*/
SELECT CONCAT(emp.name," ", emp.firstSurname," ",emp.secondSurname) AS employee, IF(empBoss.name is null, 'No tiene jefe', empBoss.name) AS boss
FROM employee emp
LEFT JOIN employee empBoss ON emp.idBoss = empBoss.id
LEFT JOIN client ON emp.id = client.idEmployee
WHERE client.id IS NULL;

-- 1.4.7 Resumen 
/*Resumen 1 ¿Cuántos empleados hay en la compañía?*/
SELECT COUNT(employee.id) AS quantity_employees
FROM employee;

/*Resumen 2.¿Cuántos clients tiene cada país?*/
SELECT client.country, COUNT(client.country) AS quantity_clients
FROM client
GROUP BY client.country;

/*Resumen 3.¿Cuál fue el pago medio en 2009?*/
SELECT YEAR(payment.paymentDate) AS year, AVG(payment.total) AS average_pay
FROM payment
WHERE YEAR(payment.paymentDate) = 2009;

/*Resumen 4.¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma
descendente por el número de pedidos.*/
SELECT request.state, COUNT(request.id) AS total_requests
FROM request
GROUP BY request.state
ORDER BY total_requests DESC;

/*Resumen 5. ¿Cuántos clients existen con domicilio en la ciudad de Madrid?*/
SELECT client.city,COUNT(client.id) AS quantity_clients
FROM client
WHERE client.city = 'Madrid';

/*Resumen 6. ¿Calcula cuántos clients tiene cada una de las ciudades que empiezan
por M?*/
SELECT client.city, COUNT(client.id) AS quantity_clients
FROM client
GROUP BY client.city
HAVING city LIKE 'M%';

/*Resumen 7.Devuelve el nombre de los representantes de ventas y el número de clients
al que atiende cada uno.*/
SELECT employee.id AS id_employee,employee.name AS employee, COUNT(client.id) AS quantity_clients 
FROM employee
JOIN client ON employee.id = client.idEmployee
GROUP BY employee.id;

/*Resumen 8. Calcula el número de clients que no tiene asignado representante de
ventas.*/
SELECT COUNT(client.id) AS quantiy_clients
FROM client
WHERE client.idEmployee IS NULL;

/*Resumen 9. Calcula la fecha del primer y último pago realizado por cada uno de los 
clients. El listado deberá mostrar el nombre y los apellidos de cada client. */
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS client,MIN(payment.paymentDate) AS first_payment ,MAX(payment.paymentDate) AS second_payment
FROM client
JOIN payment ON client.id = payment.idClient
GROUP BY client.id;

/*Resumen 10. Calcula el número de productos diferentes que hay en cada uno de los
pedidos.*/
SELECT request.id AS id_request, COUNT(DISTINCT requestdetail.idProduct ) AS cantidad_productos
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idRequest;

/*Resumen 11. Calcula la suma de la cantidad total de todos los productos que aparecen en
cada uno de los pedidos.
*/
SELECT request.id AS id_request, SUM(requestdetail.unitPrice * requestdetail.quantity ) AS total_products
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idRequest;

/*Resumen 12.Devuelve un listado de los 20 productos más vendidos y el número total de
unidades que se han vendido de cada uno. El listado deberá estar ordenado
por el número total de unidades vendidas. */
SELECT product.id AS product_id ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
ORDER BY  total_units_sold DESC 
LIMIT 20;

/*Resumen 13.La misma información que en la pregunta anterior, pero agrupada por
código de producto */
SELECT product.productCode AS code_product ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
ORDER BY total_units_sold DESC 
LIMIT 20;

/*resume 14. La misma información que en la pregunta anterior, pero agrupada por
código de producto filtrada por los códigos que empiecen por OR.*/
SELECT product.productCode AS code_product ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
WHERE product.productCode LIKE 'OR%'
GROUP BY requestdetail.idProduct
ORDER BY total_units_sold DESC 
LIMIT 20;

/* Resume 15.Lista las ventas totales de los productos que hayan facturado más de 3000
euros. Se mostrará el nombre, unidades vendidas, total facturado y total
facturado con impuestos (21% IVA).
*/
SELECT product.name AS product_name, SUM(requestdetail.quantity*requestdetail.unitPrice) AS total_sold,
SUM((requestdetail.quantity*requestdetail.unitPrice)*1.21) AS total_tax
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
HAVING SUM(requestdetail.quantity*requestdetail.unitPrice)>3000
ORDER BY total_sold DESC;

/*Resume 16. Muestre la suma total de todos los pagos que se realizaron para cada uno
de los años que aparecen en la tabla pagos.*/
SELECT YEAR(payment.paymentDate) AS year,SUM(payment.total) AS total_payment
FROM payment 
GROUP BY YEAR(payment.paymentDate);

-- 1.4.8 Subconsultas
/*Sub 1. Devuelve el nombre del client con mayor límite de crédito. */
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS client, client.creditLimit AS  max_credit
FROM client
WHERE client.creditlimit = (SELECT MAX(creditLimit) FROM client);

/*2. Devuelve el nombre del producto que tenga el precio de venta más caro. */
SELECT product.name AS product_name
FROM product
WHERE product.salePrice = (SELECT MAX(salePrice) FROM product);

/*3. Devuelve el nombre del producto del que se han vendido más unidades.
(Tenga en cuenta que tendrá que calcular cuál es el número total de
unidades que se han ven */
SELECT product.id AS product_id ,product.name AS product_name, SUM(requestdetail.quantity ) AS total_units_sold
FROM request
INNER JOIN requestdetail ON request.id = requestdetail.idRequest
INNER JOIN product ON requestdetail.idProduct = product.id
GROUP BY requestdetail.idProduct
HAVING SUM(requestdetail.quantity) = (SELECT MAX(quantity) FROM requestdetail);

/*4. Los clients cuyo límite de crédito sea mayor que los pagos que haya
realizado. (Sin utilizar INNER JOIN).*/
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS cliente
FROM client 
WHERE client.creditLimit > (SELECT SUM(total) AS pay_client FROM payment WHERE payment.idClient = client.id);

/*8. Devuelve el nombre del client con mayor límite de crédito. */
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS cliente, client.creditLimit
FROM client
WHERE  creditLimit = (SELECT MAX(creditLimit) FROM client);

/*9. Devuelve el nombre del producto que tenga el precio de venta más caro.*/
SELECT product.name AS product_name
FROM product
WHERE  salePrice = (SELECT MAX(salePrice) FROM product);

/*Sub 11. Devuelve un listado que muestre solamente los clients que no han
realizado ningún pago.*/
SELECT client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE client.id NOT IN (SELECT idClient FROM payment);

/*Sub 12. Devuelve un listado que muestre solamente los clients que sí han realizado
algún pago.*/
SELECT client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE client.id IN (SELECT idClient FROM payment);

/*Sub 13.Devuelve un listado de los productos que nunca han aparecido en un
pedido.*/
SELECT *
FROM product
WHERE product.id NOT IN (SELECT idProduct FROM requestdetail);

/*Sub 14.Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos
empleados que no sean representante de ventas de ningún client.*/
SELECT CONCAT(employee.name," ", employee.firstSurname," ",employee.secondSurname) AS employee, employee.position, office.phone AS office_phone
FROM employee
JOIN office ON employee.idOffice = office.id
WHERE employee.id NOT IN (SELECT idEmployee FROM client);

/*Sub 18.Devuelve un listado que muestre solamente los clients que no han
realizado ningún pago.*/
SELECT  client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE NOT EXISTS(SELECT idClient FROM payment WHERE client.id = payment.idClient);

/*Sub. 19. Devuelve un listado que muestre solamente los clients que sí han realizado
algún pago*/
SELECT  client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client
WHERE EXISTS(SELECT idClient FROM payment WHERE client.id = payment.idClient);

-- Consultas variadas
/*var 1.Devuelve el listado de clients indicando el nombre del client y cuántos
pedidos ha realizado. Tenga en cuenta que pueden existir clients que no
han realizado ningún pedido.*/

SELECT  client.id AS id_client, CONCAT(client.nameContact," ", client.lastnameContact) AS client, IFNULL(COUNT(request.idClient), 0) AS quantity_payments
FROM  client 
LEFT JOIN request ON client.id = request.idClient
GROUP BY client.id;

/*var 2. Devuelve el nombre de los clients que hayan hecho pedidos en 2008
ordenados alfabéticamente de menor a mayor*/
SELECT CONCAT(client.nameContact," ", client.lastnameContact) AS client
FROM client 
JOIN request ON client.id = request.idClient
WHERE YEAR(request.requestDate) = 2008;

/*var 3.Devuelve el nombre del client, el nombre y primer apellido de su
representante de ventas y el número de teléfono de la oficina del
representante de ventas, de aquellos clients que no hayan realizado ningún
pago.*/

SELECT client.nameContact AS client, CONCAT(employee.name," ",employee.firstSurname) AS sales_representative,
office.phone AS office_phone
FROM client
LEFT JOIN payment ON client.id = payment.idClient
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id
WHERE payment.idClient IS NULL
ORDER BY client.nameContact;

/*var 4.Devuelve el listado de clients donde aparezca el nombre del client, el
nombre y primer apellido de su representante de ventas y la ciudad donde
está su oficina.*/
SELECT client.id as id_client,client.nameContact AS client, CONCAT(employee.name," ",employee.firstSurname) AS sales_representative,
office.city AS office_city
FROM client
JOIN employee ON client.idEmployee = employee.id
JOIN office ON employee.idOffice = office.id;

/*var 5. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos
empleados que no sean representante de ventas de ningún client.*/
SELECT employee.id AS id_employee,CONCAT(employee.name," ",employee.firstSurname) AS employee, employee.position, office.officineCode AS office_code
FROM employee
JOIN office ON employee.idOffice = office.id
WHERE employee.id NOT IN (SELECT idEmployee FROM client);
