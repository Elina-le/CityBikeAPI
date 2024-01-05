# City Bike App

City Bike App is a UI and a backend service for displaying data from journeys made with city bikes in the Helsinki Capital area. 
The data is owned by City Bike Finland.

You can find project frontend [here](https://github.com/Elina-le/react-citybike).

The project will soon be hosted online.



## Application

City Bike App is a web application that uses a backend service to fetch data. Backend is made with .NET Framework and C# and it uses a database. Frontend is made with React. 

## Database

The data is imported from CSV files and database is structured and managed using SQL Server. Data is validated before importing and journeys that last for less than ten seconds or covered distances shorter than 10 meters are not imported. The database comprises two main tables: ’Stations’ and ’Journeys’. These tables are interconnected, establishing a relational database model.

The Journey datasets used in the database are owned by City Bike Finland:

-   <https://dev.hsl.fi/citybikes/od-trips-2021/2021-05.csv>
-   <https://dev.hsl.fi/citybikes/od-trips-2021/2021-06.csv>
-   <https://dev.hsl.fi/citybikes/od-trips-2021/2021-07.csv>

Station dataset is by Helsinki Region Transport’s (HSL):

-	Dataset: <https://opendata.arcgis.com/datasets/726277c507ef4914b0aec3cbcfcbfafc_0.csv>
- License and information: <https://www.avoindata.fi/data/en/dataset/hsl-n-kaupunkipyoraasemat/resource/a23eef3a-cc40-4608-8aa2-c730d17e8902>


## Functionalities


### Home page view

- Navigation to station and journey pages.


### Journey list view

- A list of the journeys is displayed on a table.
- Pagination is used to handle millions of rows.
- 15 journeys are fetched at a time based on the page number, to make the app fast and to make UI user friendly.
- Navigation between pages with ‘First’, ‘Previous’, ‘Next’ and ‘Last’ buttons.
- Direct page access with input field. Users can enter a specific page number to jump directly to it.


### Station list view

- A list of all the city bike stations in Helsinki Capital area.
- Searching.
- Buttons for filtering station list by the city.
- Navigation to a single station view by clicking station name.


### Single station view

- Station information: name, address, city.
- Station location on Google map
- Station statistics and calculations: number of departures and returns, average journey distances starting from and ending to the station, top 5 return and departure stations.

# Running the Application

This Web API is built using .NET Core.

## Prerequisites
-	**.Net SDK**: Ensure you have the .NET SDK installed locally.

-	**IDE**: Use Visual Studio 2022 with the ASP.NET and web development workload or Visual Studio Code with C# extension.

-	**Database**: SQL Server and SQL Server Management Studio must be installed.


## Clone repository 

Repository Link: https://github.com/Elina-le/CityBikeAPI.git

Create a local copy of the repository on your computer using Visual Studio with the provided link or through Git Bash.

## Database Setup

To set up the database, run the provided script in SQL Server Management Studio or your preferred SQL Server tool. This script creates a scaled-down version of the original database used in this project, containing all city bike station data but only 1,000 journey records (compared to over a million in the full version).

Steps:
1. Open SQL Server Management Studio.
2. Connect to your SQL Server instance.
3. Open the provided script file (found in `**/Database**` directory).
4. Execute the script to create the database and its objects.

## Configuring the Connection String

The application requires a connection string to interact with the local database.

In the **appsettings.json** file, replace the placeholder “Your connection string here” with your connection string.

Alternatively, use the user secrets file for managing sensitive data. The secret’s value is read in the Program.cs file.

Connection string template: 
"Server=(localdb)\\mssqllocaldb;Database=CityBikeDB;Trusted_Connection=True;Encrypt=False"

- Replace (localdb)\\mssqllocaldb with your server name.

## Running the Web API

To run the City Bike App’s backend, use Visual Studio or your preferred IDE. 
Launching the app opens Swagger UI on your localhost port, where you can interact with the API.

API Endpoints:
1.	**Get Journeys**: Retrieve journey data by specifying page number and size.
2.	**Get Stations**: Access data for all city bike stations.
3.	**Get Stations by Id**: Fetch details and calculations for a specific station by its ID.

## UI Integration
For a complete experience with a user interface, refer to the frontend application [here](https://github.com/Elina-le/react-citybike).
