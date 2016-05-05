SELECT  Product.Name, 
COUNT(OrderProduct.IdProduct) AS 'How Many Times Ordered', 
COUNT(DISTINCT CustomerOrder.IdCustomer) AS Customers,
ROUND(SUM (Product.Price), 2) AS Total
FROM Product 
JOIN OrderProduct 
ON Product.IdProduct = OrderProduct.IdProduct
JOIN CustomerOrder ON OrderProduct.IdOrder = CustomerOrder.IdCustomerOrder
GROUP BY Product.Name



--SELECT
--p.Name,
--COUNT(op.IdProduct) AS TimesOrdered, 
--COUNT(DISTINCT co.IdCustomer) AS Customers ,
--ROUND(SUM(p.Price), 2) AS total
--FROM Product p 
--INNER JOIN OrderProduct op 
--  ON p.IdProduct = op.IdProduct 
--INNER JOIN CustomerOrder co
--  ON op.IdOrder = co.IdCustomerOrder 
--GROUP BY p.Name 
--ORDER BY TimesOrdered DESC