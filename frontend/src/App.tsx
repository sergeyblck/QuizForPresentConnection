import React from 'react';
import { BrowserRouter as Router, Route, Routes, NavLink } from 'react-router-dom';
import Quiz from './pages/Quiz';
import HighScores from './pages/HighScores';
import './App.css';

const App: React.FC = () => {
    return (
        <Router>
            <div className="app-container">
                <div className="tabs-row">
                    <div className="tabs">
                        <NavLink
                            to="/"
                            className={({ isActive }) =>
                                `tab-button ${isActive ? 'active' : ''}`
                            }
                        >
                            Quiz
                        </NavLink>
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
