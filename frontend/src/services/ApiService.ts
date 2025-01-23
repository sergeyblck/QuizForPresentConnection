import axios from 'axios';

const API_BASE_URL = 'https://localhost:7124/api/quiz';

export const getHighScores = async () => {
    try {
        const response = await axios.get(`${API_BASE_URL}/highscores`);
        return response.data;
    } catch (error) {
        console.error('Error fetching high scores', error);
        return [];
    }
};

export const getQuizzes = async () => {
    try {
        const response = await axios.get(`${API_BASE_URL}/quizzes`);
        return response.data;
    } catch (error) {
        console.error('Error fetching quizzes', error);
        return [];
    }
};

export const submitQuiz = async (quizData: { email: string; answers: { [key: string]: string | string[] } }) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/submit`, quizData);
        return response.data;
    } catch (error) {
        console.error('Error submitting quiz', error);
        throw error;
    }
};

