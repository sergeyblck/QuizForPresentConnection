import React from 'react';
import { BrowserRouter as Router, Route, Routes, NavLink } from 'react-router-dom';
import Quiz from './pages/Quiz'; // Import the Quiz component
import HighScores from './pages/HighScores'; // Import the HighScores component
import './App.css'; // Your custom CSS

const App: React.FC = () => {
    return (
        <Router>
            <div className="app-container">
                {/* Independent black row for tabs */}
                <div className="tabs-row">
                    <div className="tabs">
                        {/* Quiz Tab - NavLink with a className function */}
                        <NavLink
                            to="/"
                            className={({ isActive }) =>
                                `tab-button ${isActive ? 'active' : ''}`
                            }
                        >
                            Quiz
                        </NavLink>
                        {/* HighScores Tab - NavLink with a className function */}
                        <NavLink
                            to="/highscores"
                            className={({ isActive }) =>
                                `tab-button ${isActive ? 'active' : ''}`
                            }
                        >
                            Top Results
                        </NavLink>
                    </div>
                </div>

                <main className="content">
                    {/* Define the routes for the Quiz and HighScores pages */}
                    <Routes>
                        <Route path="/" element={<Quiz />} />
                        <Route path="/highscores" element={<HighScores />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
};

export default App;
