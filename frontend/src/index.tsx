// src/index.tsx
import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles/app.css'; // Assuming your global CSS is in 'app.css'
import App from './App'; // The main App component

const rootElement = document.getElementById('root') as HTMLElement;
const root = ReactDOM.createRoot(rootElement);
root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);
