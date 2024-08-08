## MovieDB Project

### Overview
MovieDB is a web-based application developed using ASP.NET Core MVC, Entity Framework, and C#. This application serves as a movie management system where users can create, read, update, and delete movies. The application also allows users to create playlists, add movies to playlists, and write reviews for movies. The project incorporates user authentication and authorization, with different functionalities accessible based on user roles (Admin and Normal User).

### Features
User Authentication: Users can register, log in, and manage their accounts.
Movie Management: Users can view a list of movies.
Playlists: Users can create playlists, add movies to their playlists, update the playlists and delete these playlists. While admin can view and delete all movie playlists.
Reviews: Users can write reviews for movies, rate them, and view reviews written by other users.
API Integration: The application fetches random movie data from an external API to populate the database. https://freetestapi.com/api/v1/movies

### Technologies Used
Frontend: Razor Views (ASP.NET Core MVC)
Backend: ASP.NET Core MVC, C#, Entity Framework Core
Database: SQL Server (using Entity Framework Core)
Authentication & Authorization: ASP.NET Core Identity
External API: Fetching random movie data from a public API
Style: Bootstrap

### Database Schema

Tables and Columns
Movie.cs
movieID (Primary Key)
title (Required, Max Length: 2000)
year (Required)
rating (Default Value: 0)
plot (Max Length: 2000)
poster (Max Length: 2000)

PlayList.cs
PlayListId (Primary Key)
PlayListName (Required, Max Length: 200)

MoviePlayList.cs
PlayListId (Composite Primary Key, Foreign Key to PlayLists)
MovieId (Composite Primary Key, Foreign Key to Movies)
MovieName (Required, Max Length: 200)

Review.cs
ReviewId (Primary Key)
MovieName (Required, Max Length: 200)
UserName (Required, Max Length: 200)
Star (Required, Default Value: 0)
Comment (Max Length: 2000)
movieID (Foreign Key to Movies)
UserId (Foreign Key to Users)

User.cs
UserId (Primary Key)
UserName (Required, Max Length: 200)
Password (Required, Max Length: 100)
Email (Required, Max Length: 200)

### Relationships

Movie - Review (One-to-Many)
A movie can have multiple reviews.
A review is associated with one movie.
Foreign Key: movieID in the Reviews table references movieID in the Movies table.
Cascade delete is enabled; deleting a movie will delete its associated reviews.

User - Review (One-to-Many)
A user can write multiple reviews.
A review is written by one user.
Foreign Key: UserId in the Reviews table references UserId in the Users table.
Cascade delete is enabled; deleting a user will delete their associated reviews.

PlayList - MoviePlayList (Many-to-Many)
A playlist can contain multiple movies.
A movie can be part of multiple playlists.
This many-to-many relationship is modeled through the MoviePlayLists join table.

Project Structure
Models: Define the application's data structures (e.g., User, Movie, PlayList, Review, MoviePlayList).
DAL (Data Access Layer): Contains classes responsible for accessing the database and performing CRUD operations.
BLL (Business Logic Layer): Implements the business logic and interacts with the DAL to process data.
Controllers: Handle HTTP requests, interact with BLL to process data, and return views to the user.
Views: Razor views used to display data and collect input from users.

### Getting Started
Prerequisites
.NET 6.0 SDK or higher
SQL Server
Visual Studio 2022 or later (recommended)
Installation
Clone the repository: git clone https://github.com/olisanweze/MovieDB.git
Update the database connection string in appsettings.json: "ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=MovieDB;Trusted_Connection=True;"
}
Apply database migrations to create the necessary tables: Add-Migration InitialCreate, Update-Database

### Usage
Movie page: Lists all the movies and add new movies fetch from API.
Admin Role: Accessible with the admin role, allowing admin to view and delete all movie playlists.
Playlists: Users can create and delete their playlists, adding or removing movies to update the playlist.
Reviews: Users can write reviews for movies and view others' reviews. 