# MIS 333K Order Entry Site

## Overview

This project expands upon the bookstore website by adding order and vendor functionalities. The goal is to practice editing data on model classes with 1:many and many:many relationships, while also learning how to use route values to carry data across HTTP Request/Response cycles.

## Instructions

### Part One: Database and Model Setup

1. Create a new database server and database on Azure.
2. Create a new Visual Studio project named `LastName_FirstName_HW5`.
3. Install necessary NuGet packages (EntityFramework, Identity).
4. Add `AppDbContext` and update necessary files.
5. Add `AppUser`, `Product`, `Supplier`, and `Order` classes to the Models folder.
6. Implement necessary relationships and add DbSets in `AppDbContext`.
7. Build and add migration to initialize the database.
8. Run the project and navigate to the seeding home page to add roles and users.
9. Scaffold controllers with views for `Product`, `Supplier`, `Order`, and `OrderDetail`.

### Part Two: Add Authorize Tags and Clean Up Unused Actions/Views

1. Set up authorization for admin and customer roles.
2. Restrict access to certain actions based on roles.
3. Modify views to display information based on user roles.
4. Remove unnecessary links and actions.

### Part Three: Allow the User to Edit the Supplier/Product Relationship

1. Allow the user to associate suppliers with products.
2. Implement views and actions for editing supplier/product relationships.
3. Display associated products on supplier details and vice versa.

### Part Four: Allow the User to Create an Order and Add Products

1. Implement order creation with authorization and business rules.
2. Allow the user to add products to orders with quantity and price.
3. Display order details and calculate order summary.
4. Enable editing and deleting order details.
5. Add appropriate views, actions, and functionality.

### Deployment

1. Publish the website to a new web app on Azure.

