# Bouvet Fagkaffe
This project was made as a part of my bachelors thesis in computer science at the University of Stavanger in collaboration with advisors from Bouvet ASA.
The applicaion was made to facilitate knowledge-sharing sessions between members of an organization and was inspired by a concept at Bouvet ASA called "Fagkaffe".
The application aims to:
- Socialize the gathering of ideas for lectures.
- Give members the ability to vote on candidates for lectures.
- Simplify the planning of lectures
- Make information pertaining to the lectures available from one central place.

## Technologies
The main project is written in c# with .NET Blazor.  
Data access is handeled with Entity Framework Core seperated out in to a class library.  
Authentication is set up with SAML.

## Structure
The top-level solutions consists of two projects:
- bouvet_fagkaffe_frontend
- bouvet_fagkaffe_repository

### bouvet_fagkaffe_frontend
Serves as the main project for the solution and is structured as follows:  
![image](https://github.com/audunt/bouvet_fagkaffe/assets/89785784/19fdd2e0-fc7a-4346-a1ca-2201b834de77)

- Components contain all the Blazor components of the project
- Controllers contain .NET controllers for the project
- Services contain helper functions refrenced from the components

### bouvet_fagkaffe_repository
Serves as the data access layer of the application and is structured as follows:  
![image](https://github.com/audunt/bouvet_fagkaffe/assets/89785784/606412c8-7766-43ef-9a8e-20608896bca2)

- Context contain the EF Core context
- Migrations contain EF Core migrations
- Models contain the model classes for EF Core
- Operations.cs contain all methods exposed from the class library
