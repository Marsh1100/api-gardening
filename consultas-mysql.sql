/*Consultas gardening*/
USE gardening;
/*1. Devuelve un listado con el nombre de los todos los clientes españoles.*/
SELECT *
FROM gardening.client
WHERE client.country = 'Spain';

/*2.Devuelve un listado con los distintos estados por los que puede pasar un
pedido.*/
SELECT state AS state_request
FROM request
GROUP BY request.state;

/*3. Devuelve un listado con el código de cliente de aquellos clientes que
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar
aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta:
• Utilizando la función YEAR de MySQL.
• Utilizando la función DATE_FORMAT de MySQL.
• Sin utilizar ninguna de las funciones anteriores.*/

SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE paymentDate BETWEEN '2008-01-01' AND '2008-12-31' ; 

SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE YEAR(paymentDate) = 2008; 

SELECT DISTINCT  client.id
FROM client
INNER JOIN payment ON client.id = payment.idClient
WHERE DATE_FORMAT(paymentDate, '%Y') = 2008;

/*9. Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos que no han sido entregados a
tiempo.*/

SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE deliveryDate > expectedDate;

/*10.Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al
menos dos días antes de la fecha esperada.
• Utilizando la función ADDDATE de MySQL.
• Utilizando la función DATEDIFF de MySQL.
• ¿Sería posible resolver esta consulta utilizando el operador de suma + o
resta -?*/

SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE ADDDATE(expectedDate, INTERVAL -2 day ) >= deliveryDate;

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

SELECT * 
FROM product
INNER JOIN producttype ON product.idProductType = producttype.id
WHERE producttype.type = 'Ornamentales' AND product.stock>100 
ORDER BY product.salePrice DESC;

/*16. Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y 
cuyo representante de ventas tenga el código de empleado 11 o 30 */

SELECT * 
FROM client
WHERE city = 'Madrid' AND (idEmployee= 11 OR idEmployee = 30);
