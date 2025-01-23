# Quiz Web App
A simple web application for taking quizzes and viewing top scores.

## Description
This project is a quiz web app where users can answer a series of questions. They can see their scores after completing the quiz and check the top scores of all users.

## About the Project

### Backend
The backend is located in the `PresentConnectionInt` folder.

It is implemented in Visual Studio using ASP.NET and an EF Core in-memory database to store quiz entries and high scores.

The backend code is separated into folders with self-explanatory names.

Dependency injection is implemented in the `QuizController` file.

### Frontend
The frontend is located in the `Frontend` folder.

It is implemented in WebStorm using React and TypeScript. Bootstrap was used for generating HTML.

There are two pages: `Quiz` and `HighScores`, located in the `src/pages` folder. An API service for sending and receiving data is implemented using Axios.

## Technologies Used:
- **Backend:** ASP.NET, Entity Framework Core (EF Core), In-memory Database
- **Frontend:** React, TypeScript, Bootstrap, Axios
- **Other:** Visual Studio (for backend), WebStorm (for frontend)

## How to Run the Project Locally:

### Backend:
1. Open the `PresentConnectionInt` folder in Visual Studio.
2. Build and run the project to start the API.
3. The backend will be accessible at `https://localhost:7124` (default for ASP.NET htttps).

### Frontend:
1. Navigate to the `Frontend` folder.
2. Install dependencies using npm:
   ```bash
   npm install
3. npm start
4. Open the app in your browser at `http://localhost:5173` (port can differ, check the terminal)
