// src/types/quizTypes.ts
export interface Quiz {
    id: number;
    question: string;
    type: 'radio' | 'checkbox' | 'textbox';
    options?: string[];
    correctAnswers: string[];
}

export interface HighScore {
    id: number;
    email: string;
    score: number;
    dateTime: string;
}
