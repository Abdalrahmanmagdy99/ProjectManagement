# Project Management API

This is a backend API for managing projects and tasks built with **.NET 8 Core**. It uses **ASP.NET Core Identity** for authentication and authorization, **InMemoryDb** for data storage, and includes dummy data generation using the **Pogus** library. It is designed to be easy to run, test, and extend.

## Getting Started

To run this project, follow the steps below.

### 1. Prerequisites

- .NET 8 SDK installed
- Postman (for testing the APIs) 

### 2. Cloning the Repository

Clone the repository to your local machine.


### API Testing with Postman
A Postman collection is provided with the repository (Included in the project ProjectManagement.postman_collection.json ). You can use it to test the API endpoints with predefined requests and data.

Import the Postman collection from the project root.
Use the collection to interact with various endpoints such as:
- User registration and login
- Creating, updating, and deleting projects
- Managing tasks within projects.
- Managing comments to the tasks.



## Key Features
### 1. InMemoryDb for Easy Setup
The project uses InMemoryDb as the database provider. This allows for easy setup and running of the project without needing a complex database configuration. It is ideal for development and testing.

### 2. Pogus Library for Dummy Data Generation
To facilitate testing, the project uses the Pogus library to generate dummy data for projects and tasks. This makes it easy to test the APIs and understand how the system behaves with realistic data.

The property NumberOfDummyProjects in the appSettings.json file specifies the number of projects to generate with dummy data. Adjust this value to create a different number of projects.
Example configuration in appSettings.json:

{
  "NumberOfDummyProjects": 5
}
### 3. .NET 8 Core Identity for Security
The project uses .NET 8 Core Identity for secure authentication and authorization of the API endpoints. It includes:

JWT-based authentication: To secure the endpoints and ensure only authorized users can access the data.
Role-based authorization: To manage different user roles (e.g., Manager, Employee) and restrict access to specific APIs accordingly.
The API endpoints are protected using JWT tokens, and the documentation provides details on how to obtain a token and use it to authenticate requests.

### 4. GlobalExceptionHandler
A GlobalExceptionHandler middleware is implemented to handle errors consistently across the application. It ensures that errors are logged and formatted correctly, providing meaningful responses to API consumers while maintaining security.

### 5. NumberOfDummyProjects in appSettings
The NumberOfDummyProjects property in the appSettings.json file controls the number of dummy projects generated when the application starts.

Set the value to the desired number of projects you want to create for testing.
This allows for dynamic data generation without manually creating multiple projects.


## Postman Collection
A Postman collection is included in the repository to make API testing straightforward. Import the collection into Postman and use it to test the following functionalities:
- User registration and login
- Creating, updating, and deleting projects
- Adding, updating, and deleting tasks
- Role-based access to protected endpoints

### Example Usage
#### Setting the Number of Dummy Projects:
1- Open appSettings.json.
2- Modify the NumberOfDummyProjects value, e.g., "NumberOfDummyProjects": 10.
3- Run the application, and 10 dummy projects will be generated.

### Using Postman Collection:

1- Import the collection into Postman.
2- Use the login endpoint to get a JWT token.
3- Set the token in the Authorization header of subsequent requests to access protected APIs.
