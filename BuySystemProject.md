Objective:
Develop a back-end application that controls the sales flow of an online store. This application must consider those items:  Costumers, Products, Orders, Addresses and Order Items.

END POINTS :

**1 - Costumer**
 The endpoint costumer must have the following properties within separate routes for Post, Update, Delete and Get.
 
 Post:
***Request:***
```
{
"costumerName" : "",
"documment" : "",
"phoneNumber" : "",
"email" : "",
"birthdate" : "",
"gender" : 0
}
```
***Response:***
```
{
"id":"uuid()",
"costumerName" : "Example Example Example",
"documment" : "01010101010110",
"phoneNumber" : "0010010101",
"email" : "example@example.com",
"birthdate" : "1994-12-09",
"gender" : male
"creationDate" : 2021-05-05,
"updatedDate" : 
}
```
 Put:
 ***Request:***
```
{
"id":"",
"costumerName" : "",
"documment" : "",
"phoneNumber" : "",
"email" : "",
"birthdate" : "",
"gender" : 0
}
```
***Response:***
```
{
"id":"uuid()",
"costumerName" : "Updated Example",
"documment" : "11111000",
"phoneNumber" : "1111101010",
"email" : "updated@updated",
"birthdate" : "1992-03-19",
"gender" : female
"creationDate" : 2021-05-05,
"updatedDate" : 2021-05-06
}
```
 Get:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"id":"uuid()",
"costumerName" : "Example Example Example",
"documment" : "01010101010110",
"phoneNumber" : "0010010101",
"email" : "example@example.com",
"birthdate" : "1994-12-09",
"gender" : male
"creationDate" : 2021-05-05,
"updatedDate" : 2021-05-06
}
```
 Delete:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"Costumer was sucessfully deleted!"
}
```

 Table: buySystem.costumers:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Costumer Id</td>
    </tr>   
    <tr>
       <td>costumerName</td>
       <td>String</td>
       <td>varchar(70)</td>
       <td>Costumer Name</td>
    </tr> 
    <tr>
       <td>document</td>
       <td>String</td>
        <td>varchar(15)</td>
       <td>Costumer Document</td>
    </tr>   
    <tr>
       <td>phoneNumber</td>
       <td>String</td>
       <td>varchar(15)</td>
       <td>Costumer Phone Number</td>
    </tr>  
    <tr>
       <td>email</td>
       <td>String</td>
       <td>varchar(150)</td>
       <td>Costumer Email</td>
    </tr>  
    <tr>
       <td>birthdate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Costumer Birthdate</td>
    </tr>  
    <tr>
       <td>gender</td>
       <td>Int</td>
       <td>tinyint(4)</td>
       <td>Costumer Gender</td>
    </tr>  
    <tr>
       <td>creationDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Creation date</td>
    </tr>  
    <tr>
       <td>updatedDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Updated date</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS buysystem.costumers
(
id char(36) not null default 'uuid()' comment 'Costumer unique identifier',
costumerName varchar(70) not null comment 'Costumer Name',
document varchar(15) not null comment 'Costumer Document',
phoneNumber varchar(15) not null comment 'Costumer phone number',
email varchar(150) not null comment 'Costumer Email',
birthdate datetime not null comment 'Costumer Birthdate',
gender tinyint default 0 comment 'Costumer Gender',
creationDate datetime not null default NOW() comment 'Registry created date and time',
updatedDate timestamp not null default current_timestamp on update current_timestamp comment 'Date and time when a registry update occurs',
PRIMARY KEY (id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
  
**2 - Products**
The endpoint Products must have the following properties within separate routes for Post, Update, Delete and Get.

 Post:
***Request:***
```
{
"productName" : "",
"productCode" : "",
"quantity" : "",
"productDescription" : "",
"model" : "",
"productValue" : 0
}
```
***Response:***
```
{
"id":"uuid()",
"productName" : "Example",
"productCode" : "050100X",
"quantity" : 1000,
"productDescription" : "Example",
"model" : "Example",
"productValue" : 1000,
"creationDate" : 2021-05-05,
"updatedDate" : 
}
```

 Put:
 ***Request:***
```
{
"id" : "",
"productName" : "",
"productCode" : "",
"quantity" : "",
"productDescription" : "",
"model" : "",
"productValue" : 0,
}
```
***Response:***
```
{
"id":"uuid()",
"productName" : "Updated Example",
"productCode" : "Updated Code",
"quantity" : 1001,
"productDescription" : "Updated Description",
"model" : "Updated Model",
"productValue" : 1002,
"creationDate" : 2021-05-06,
"updatedDate" : 2021-05-07
}
```
 Get:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"id":"uuid()",
"productName" : "Example",
"productCode" : "0500X06Z",
"quantity" : 1000,
"productDescription" : "Example",
"model" : "Example",
"productValue" : 1000,
"creationDate" : 2021-05-05,
"updatedDate" : 2021-05-05
}
```
 Delete:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"Product was sucessfully deleted!"
}
```
Table: buySystem.products:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Product Id</td>
    </tr>   
    <tr>
       <td>productName</td>
       <td>String</td>
       <td>varchar(70)</td>
       <td>Product Name</td>
    </tr> 
        <tr>
       <td>productCode</td>
       <td>String</td>
       <td>varchar(50)</td>
       <td>Product Code</td>
    </tr> 
    <tr>
       <td>quantity</td>
       <td>int</td>
        <td>int(11)</td>
       <td>Product Available Quantity</td>
    </tr>   
    <tr>
       <td>productDescription</td>
       <td>String</td>
       <td>text</td>
       <td>Product Description</td>
    </tr>  
    <tr>
       <td>model</td>
       <td>String</td>
       <td>varchar(40)</td>
       <td>Product Model</td>
    </tr>  
    <tr>
       <td>productValue</td>
       <td>Int</td>
       <td>int(11)</td>
       <td>Product Value</td>
    </tr>  
    <tr>
       <td>creationDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Creation Date</td>
    </tr>  
    <tr>
       <td>updatedDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Updated date</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS buysystem.products
(
id char(36) not null default 'uuid()' comment 'Product unique identifier',
productName varchar(200) not null comment 'Product Name',
productCode varchar(50) not null comment 'Product Code', 
quantity int not null comment 'Product quantity',
productDescription text not null comment 'Product Description',
model varchar(40) not null comment 'Product model',
productValue decimal not null comment 'Product value',
creationDate datetime not null default NOW() comment 'Registry created date and time',
updatedDate timestamp not null default current_timestamp on update current_timestamp comment 'Date and time when a registry update occurs',
PRIMARY KEY (id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
**3 - Orders**
The endpoint Orders must have the following properties within separate routes for Post,  Delete and Get.


 Post:
***Request:***
```
   {
   "orderNumber" : "",
   "costumerId" : "",
   "costumerAdressId" : "",
   "subTotal" : 0.00,
   "freight" : 0.00,
   "total" : , 0.00
   }
```
***Response:***
```
   {
   "id":"",
   "orderNumber" : "1000",
   "costumerId" : "uuid()", *from costumer table*
   "costumerAdressId" : "uuid()", *from adress table*
   "subTotal" : 50.00,
   "freight" : 10.00,
   "total" : , 60.00
   "creationDate" : 2021-05-05
   }
```

 Get:
  ***Request:***
```
{
   "id":""  
}
```
***Response:***
```
{
   "id":"uuid()",
   "orderNumber" : "1002",
   "costumerId" : "uuid()", *from costumer table*
   "costumerAdressId" : "uuid()", *from adress table*
   "subTotal" : 80.00,
   "freight" : 10.00,
   "total" : , 90.00
   "creationDate" : 2021-05-05   
}
```
 Delete:
  ***Request:***
```
{
   "id":""  
}
```
***Response:***
```
{
"Order was sucessfully deleted!"
}
```
Table: buySystem.orders:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Order Id</td>
    </tr>   
    <tr>
       <td>orderNumber</td>
       <td>String</td>
       <td>varchar(40)</td>
       <td>Order Number</td>
    </tr> 
        <tr>
       <td>costumerId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Costumer Id</td>
    </tr> 
    <tr>
       <td>costumerAddressId</td>
       <td>Guid</td>
        <td>char(36)</td>
       <td>Address Id</td>
    </tr>   
    <tr>
       <td>subTotal</td>
       <td>Double</td>
       <td>decimal</td>
       <td>Order sub total</td>
    </tr>  
    <tr>
       <td>freight</td>
       <td>Double</td>
       <td>decimal</td>
       <td>Order Freight</td>
    </tr>  
    <tr>
       <td>total</td>
       <td>Double</td>
       <td>decimal</td>
       <td>Order Total</td>
    </tr>  
    <tr>
       <td>creationDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Creation Date</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS buysystem.orders 
(
id char(36) not null default 'uuid()' comment 'Order unique identifier',
orderNumber varchar(40) not null comment 'Order number',
costumerId char(36) not null default 'uuid()' comment 'Foreign Key Costumer ID',
costumerAddressId char(36) default 'uuid()' not null comment 'Foreign Key Costumer Adress',
subTotal decimal  not null comment 'Order subtotal',
freight decimal  not null comment 'Order freight',
total decimal  not null comment 'Order total value',
creationDate datetime not null default NOW() comment 'Order created date and time',
PRIMARY KEY (id),
CONSTRAINT `fk_Order_Costumer` FOREIGN KEY (`costumerId`) REFERENCES `buysystem`.`Costumers` (`id`),
CONSTRAINT `fk_Order_Address` FOREIGN KEY (`costumerAddressId`) REFERENCES `buysystem`.`Addresses` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
 **4- Addresses**
The endpoint Addresses must have the following properties within separate routes for Post,  Put, Delete and Get.

Post:
***Request:***
```
   { 
   "costumerId" : "",
   "address" : "",
   "addressNumber" : "",
   "neighborhood" : "",
   "country" : "",
   "postalCode" : , "", 
   }
```
***Response:***
```
   { 
   "id":"uuid()",
   "costumerId" : "uuid()", *from costumer table*
   "address" : "Example",
   "addressNumber" : "1750A",
   "neighborhood" : "Example",
   "country" : "Brazil",
   "postalCode" : , "03450100",
   "creationDate" : 2021-05-05,
   "updatedDate" :    
   }
```
Put:
***Request:***
```
   { 
   "costumerId" : "",
   "address" : "",
   "addressNumber" : "",
   "neighborhood" : "",
   "country" : "",
   "postalCode" : , "",  
   }
```
***Response:***
```
   { 
   "id":"uuid()",
   "costumerId" : "uuid()", *from costumer table*
   "address" : "Updated Example",
   "addressNumber" : "1750B",
   "neighborhood" : "Updated Example",
   "country" : "Updated Country",
   "postalCode" : , "03450102",
   "creationDate" : 2021-05-05,
   "updatedDate" : 2021-05-06  
   }
```
Table: buySystem.addresses:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Order Id</td>
    </tr>   
    <tr>
       <td>costumerId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Order Id</td>
    </tr> 
        <tr>
       <td>address</td>
       <td>String</td>
       <td>varchar(100)</td>
       <td>Address name</td>
    </tr> 
    <tr>
       <td>addressNumber</td>
       <td>string</td>
        <td>varchar(10)</td>
       <td>Address Number</td>
    </tr>   
    <tr>
       <td>neighborhood</td>
       <td>String</td>
       <td>varchar(100)</td>
       <td>Address neighborhood</td>
    </tr>  
    <tr>
       <td>postalCode</td>
       <td>String</td>
       <td>varchar(10)</td>
       <td>Address Postal Code</td>
    </tr>  
    <tr>
       <td>country</td>
       <td>String</td>
       <td>varchar(30)</td>
       <td>Address Country</td>
    </tr>  
    <tr>
       <td>creationDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Creation Date</td>
    </tr>  
    <tr>
       <td>updatedDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Updated Date</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS buysystem.addresses
(
id char(36) not null default 'uuid()' comment 'Adress unique identifier',
costumerId char(36) not null default 'uuid()' comment 'Foreign Key Costumer id identifier',
address varchar(100) not null comment 'Address',
addressNumber varchar(10) not null comment 'Adress number',
neighborhood varchar(100) not null comment 'Adress neighborhood',
postalCode varchar(10) not null comment 'Adress Postal Code',
country varchar(30) not null comment 'Adress Country',
creationDate datetime not null default NOW() comment 'Adress created date and time',
updatedDate timestamp not null default current_timestamp on update current_timestamp comment 'Date and time when a registry update occurs',
PRIMARY KEY (`costumerId`,`id`,`address`),
CONSTRAINT `fk_Address_Costumer` FOREIGN KEY (`costumerId`) REFERENCES `buysystem`.`Costumers` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
 **5 - Order Items**
The endpoint Order Items must have the following properties within separate routes for Post,  Delete and Get.

Post:
***Request:***
```
   {
   "orderId" : "",
   "productId" : "",
   "quantity" : "",
   "freight" : "",
   "unityValue" : "",
   "total" : , "",
   }
```
***Response:***
```
   {  
   "id":"uuid()",
   "orderId" : "uuid()", *from order table*
   "productId" : "uuid()", *from product table*
   "quantity" : 0,
   "freight" : 0.00,
   "unityValue" : 0.00,
   "total" : , 0.00,
   "creationDate" : 2021-05-05
   }
```
Put:
***Request:***
```
   {
   "id" : "",
   "orderId" : "",
   "productId" : "",
   "quantity" : "",
   "freight" : "",
   "unityValue" : "",
   "total" : , ""
   }
```
***Response:***
```
   {
   "id":"uuid()",
   "orderId" : "uuid()", *from order table*
   "productId" : "uuid()", *from product table*
   "quantity" : 10,
   "freight" : 1.00,
   "unityValue" : 1.00,
   "total" : , 20.00,
   "creationDate" : 2021-05-05,
   "updatedDate" : 2021-05-06
   }
```
Get:
***Request:***
```
   { 
   "id" : "" 
   }
```
***Response:***
```
   { 
   "id":"uuid()",
   "orderId" : "uuid()", *from order table*
   "productId" : "uuid()", *from product table*
   "quantity" : 100,
   "freight" : 10.00,
   "unityValue" : 50.00,
   "total" : , 510.00,
   "creationDate" : 2021-05-05, 
   "updatedDate" : 2021-05-06
   }
```
Delete:
***Request:***
```
   {
   "id" : ""
   }
```
***Response:***
```
   {
   "Order item was sucessfully deleted."
   }
```
   Table: buySystem.orderitems:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Order Id</td>
    </tr>   
    <tr>
       <td>orderId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Order Id</td>
    </tr> 
        <tr>
       <td>productId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Product Id</td>
    </tr> 
    <tr>
       <td>quantity</td>
       <td>Double</td>
        <td>decimal</td>
       <td>Item Quantity</td>
    </tr>   
    <tr>
       <td>freight</td>
       <td>Double</td>
       <td>decimal</td>
       <td>item freight</td>
    </tr>  
    <tr>
       <td>unityValue</td>
       <td>Double</td>
       <td>decimal</td>
       <td>Item unity Value</td>
    </tr>  
    <tr>
       <td>total</td>
       <td>Double</td>
       <td>decimal</td>
       <td>Item total</td>
    </tr>  
    <tr>
       <td>creationDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Creation Date</td>
    </tr>  
        <tr>
       <td>updatedDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Updated Date</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS buysystem.orderItems
(
id char(36) not null default 'uuid()' comment 'Order items unique identifier',
orderId char(36) not null default 'uuid()' comment 'Foreign Key Order id identifier',
productId char(36) not null default 'uuid()' comment 'Foreign Key Product ID',
quantity decimal not null comment 'Product Quantity in this order',
freight decimal  not null comment 'Freight for this product',
unityValue decimal not null comment 'Unity Product Value',
total decimal not null comment 'Total for this product',
creationDate datetime not null default NOW() comment 'Order Item created date and time',
updatedDate timestamp not null default current_timestamp on update current_timestamp comment 'Date and time when a registry update occurs',
PRIMARY KEY (id),
CONSTRAINT `fk_OrderItems_Order` FOREIGN KEY (`orderId`) REFERENCES `buysystem`.`orders` (`id`),
CONSTRAINT `fk_OrderItems_Product` FOREIGN KEY (`productId`) REFERENCES `buysystem`.`Products` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```

		 



