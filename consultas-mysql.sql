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
resta -?

SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE ADDDATE(expectedDate, INTERVAL -2 day ) >= deliveryDate;

SELECT id, idClient, expectedDate, deliveryDate
FROM request
WHERE DATEDIFF(expectedDate, deliveryDate ) >= 2;

-- 11. Devuelve un listado de todos los pedidos que fueron rechazados en 2009.



