# Gamesmarket
## Project Overview
This is my project focused on building a comprehensive ASP.NET Core Web API application with React to manage an online game store. The project demonstrates proficiency in RESTful API development, Entity Framework Core for data handling with MSSQL database, and integration of modern web technologies like React with TypeScript, MobX and Material UI. 

The application includes features such as CRUD operations for games and users, search functionalities, user security is guaranteed by Beaver tokens with role-based access, ordering and shopping cart functionality for users, and web interface for admin and moderators to edit games data and users roles, which showcases my skills in full-stack web development.

## Architecture:
Gamesmarket - the main project, which includes configuration and initialization of other projects and APIs.

.DAL - Responsible for interaction with the database.

.Domain - Contains the main business objects (entities).

.Service - Implements business logic.

.Interfaces - defines contracts that are used in other layers.

.Utilities - Contains helper classes and methods.

.IntegrationTests - integration tests of services and APIs.

.Tests - unit tests of individual modules with Mock objects.

.ReactWeb - The user interface built on React. The application is divided modularly according to its functionality.


## Screenshots of the store
**Home page for unregistered user**
![HomePage](https://github.com/user-attachments/assets/41742aa4-077c-4456-ba55-9fe53ca6a11d)

**Recently added games page**
![NewGamesPage](https://github.com/user-attachments/assets/8a1605b5-9c1e-4337-90f4-628f18655e78)

**All games page with administrator functionality**
![GamesPageA](https://github.com/user-attachments/assets/b1f69776-ca48-4c24-8b88-e46bdcbf740b)

**Sorting games by price - from low to high**
![Sort](https://github.com/user-attachments/assets/b70dff8a-2a34-4fbc-984b-20e03fabe70d)

**The page of a particular game with all the information about it**
![GamePage](https://github.com/user-attachments/assets/180b664f-2461-4334-9d94-6855785fc640)

**User shopping cart page**
![CartPage](https://github.com/user-attachments/assets/3106265f-46b5-42d6-a405-6139085015f7)

**Registration page**
![RegisterPage](https://github.com/user-attachments/assets/c1bbda4a-dc17-4f14-8c4a-6f6960a9aa4e)

**Login page**
![LoginPage](https://github.com/user-attachments/assets/1256aa7f-5e9b-4664-bcac-48c15b09e20f)

**Page for admins with all users data and ability to change user roles**
![UsersPage](https://github.com/user-attachments/assets/d66014f5-0d7a-4b9f-ae25-513f7d2bb5a1)

**Page for adding a game to the store with a preview of the uploaded image**
![AddGamePage](https://github.com/user-attachments/assets/a4c5e091-d905-417f-a970-b434821e28ed)

**Game that was found by search query**
![Search](https://github.com/user-attachments/assets/25c016dc-0a72-4bcf-a1f7-451bedbc9065)

**Backend api**
![api](https://github.com/user-attachments/assets/2aee45ed-fc12-4145-8b45-66d4d9821428)

## Running the Project.

1. Run the backend:

- Open the solution in Visual Studio or another IDE.

- In Solution Explorer, set the Gamesmarket as startup project.

- Select the https startup profile in the Run toolbar.

- Click the Run button or use the following command to run in the terminal:
   
   In Gamesmarket\Gamesmarket
   ```sh
   dotnet run --launch-profile https
    ```
This will start the server at https://localhost:7202

2. Run the frontend:

   In Gamesmarket\Gamesmarket.ReactWeb
   ```sh
   npm install
   ```
   ```sh
   npm run dev
   ```
This will start the frontend at https://localhost:3000
   
3. To use the Logger, you need to specify the path in nlog.config to the Logs folder

4. Data to log in as admin user: Email - admin@gmail.com Password - Qwe!23
