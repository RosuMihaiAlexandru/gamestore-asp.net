# Gamesmarket
## Project Overview
This is my project focused on building a comprehensive ASP.NET Core Web API application with React to manage an online game store. The project demonstrates proficiency in RESTful API development, Entity Framework Core for data handling with MSSQL database, and integration of modern web technologies like React with Bootstrap. 

The application includes features such as CRUD operations for games and users, search functionalities, user security is guaranteed by Beaver tokens with role-based access, ordering and shopping cart functionality for users, and web interface for admin and moderators to edit games data and users roles, which showcases my skills in full-stack web development.

## Architecture:
Gamesmarket - the main project, which includes configuration and initialization of other projects and APIs.

.DAL - Responsible for interaction with the database.

.Domain - Contains the main business objects (entities).

.Service - Implements business logic.

.Interfaces - defines contracts that are used in other layers.

.Utilities - Contains helper classes and methods.

.ReactWeb - The user interface built on React.

.IntegrationTests - integration tests of services and APIs.

.Tests - tests of individual modules with Mock objects.

## Screenshots of the store
**Home page for unregistered user**
![home-page](https://github.com/SamkoVit/Gamesmarket/assets/54777714/42ea25f0-0f18-4847-9041-94b53dec87b4)

**Website page with all games with administrator functionality**
![main-site](https://github.com/SamkoVit/Gamesmarket/assets/54777714/4c1c8494-6066-4451-bf8c-7f2526ec8697)

**Website page with a user's shopping cart**
![cart-site](https://github.com/SamkoVit/Gamesmarket/assets/54777714/dcae9e14-c2b6-41ca-9a2a-489f97d11503)

**Registration page**
![register-site](https://github.com/SamkoVit/Gamesmarket/assets/54777714/21e76d80-d5bc-4bdd-9588-98dc83610c5c)

**Admin page for changing user roles and the ability to revoke tokens of all users**
![admin](https://github.com/SamkoVit/Gamesmarket/assets/54777714/920c4eb1-c749-4d1c-86b9-ff4157dbc580)

**Page for adding a game to the store**
![create](https://github.com/SamkoVit/Gamesmarket/assets/54777714/ea4079d1-21b2-4dc0-8fa3-8009a0ed5a6f)

## Running the Project.

1. Build the backend:

   In Gamesmarket\Gamesmarket
   ```sh
   dotnet build
   dotnet run
   ```

2. Run the frontend:

   In Gamesmarket\Gamesmarket.ReactWeb> 
   ```sh
   npm run dev
   ```
3. To use the Logger, you need to specify the path in nlog.config to the Logs folder 

