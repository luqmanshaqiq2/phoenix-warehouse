1. 

## domain models

Product, Warehouse, Inventory, Customer, Order, OrderItem, InventoryReservation, StockMovement.

2. 

## database context

WarehouseDbContext.cs entity mapping & config


3. 

## database migration

SQLServer - phoenix_warehouse


4.

## avoiding repo pattern

EfCore directly talks with DB with C# obj

5. 

## service files

db access & business logic with edge cases

6. 

## n + 1 query problem solved

used eager loading with Include()
for APIs use Select()

7.



