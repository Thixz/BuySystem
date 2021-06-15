CREATE DATABASE IF NOT EXISTS BUYSYSTEM DEFAULT CHARSET utf8;

USE BUYSYSTEM;

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




CREATE TABLE IF NOT EXISTS buysystem.orders 
(
id char(36) not null default 'uuid()' comment 'Order unique identifier',
orderNumber varchar(40) not null comment 'Order number',
costumerId char(36) not null default 'uuid()' comment 'Foreign Key Costumer ID',
costumerAddressId char(36) default 'uuid()' not null comment 'Foreign Key Costumer Adress',
subTotal decimal  not null comment 'Order subtotal',
freight decimal comment 'Order freight',
total decimal  not null comment 'Order total value',
creationDate datetime not null default NOW() comment 'Order created date and time',
PRIMARY KEY (id),
CONSTRAINT `fk_Order_Costumer` FOREIGN KEY (`costumerId`) REFERENCES `buysystem`.`Costumers` (`id`),
CONSTRAINT `fk_Order_Address` FOREIGN KEY (`costumerAddressId`) REFERENCES `buysystem`.`Addresses` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;





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
PRIMARY KEY (`id`),
CONSTRAINT `fk_Address_Costumer` FOREIGN KEY (`costumerId`) REFERENCES `buysystem`.`Costumers` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;




CREATE TABLE IF NOT EXISTS buysystem.orderItems
(
id char(36) not null default 'uuid()' comment 'Order items unique identifier',
orderId char(36) not null default 'uuid()' comment 'Foreign Key Order id identifier',
productId char(36) not null default 'uuid()' comment 'Foreign Key Product ID',
quantity decimal not null comment 'Product Quantity in this order',
freight decimal comment 'Freight for this product',
unityValue decimal not null comment 'Unity Product Value',
total decimal not null comment 'Total for this product',
creationDate datetime not null default NOW() comment 'Order Item created date and time',
updatedDate timestamp not null default current_timestamp on update current_timestamp comment 'Date and time when a registry update occurs',
PRIMARY KEY (id),
CONSTRAINT `fk_OrderItems_Order` FOREIGN KEY (`orderId`) REFERENCES `buysystem`.`orders` (`id`),
CONSTRAINT `fk_OrderItems_Product` FOREIGN KEY (`productId`) REFERENCES `buysystem`.`Products` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;