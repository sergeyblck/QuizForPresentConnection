import React, { useState, useEffect } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { getQuizzes, submitQuiz } from '../services/ApiService.ts';
import './Quiz.css';

interface QuizAnswers {
    [key: string]: string | string[];
}

interface QuizForm {
    email: string;
    answers: QuizAnswers;
}

interface Quiz {
    id: number;
    question: string;
    type: 'Radio' | 'Checkbox' | 'Textbox';
    options: string[];
    correctAnswers: string[];
}

const Quiz: React.FC = () => {
    const [quizzes, setQuizzes] = useState<Quiz[]>([]);
    const [answers, setAnswers] = useState<QuizAnswers>({});
    const [email, setEmail] = useState<string>('');
    const [isSubmitted, setIsSubmitted] = useState<boolean>(false);
    const [finalScore, setFinalScore] = useState<number>(0);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchQuizzes = async () => {
            const quizData = await getQuizzes();
            setQuizzes(quizData);
        };

        fetchQuizzes();

        const savedState = localStorage.getItem('quizState');
        if (savedState) {
            const parsedState = JSON.parse(savedState);
            setIsSubmitted(parsedState.isSubmitted);
            setFinalScore(parsedState.finalScore);
            setAnswers(parsedState.answers);
        }
    }, []);

    const handleAnswerChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value, type } = event.target;
        const checked = (event.target as HTMLInputElement).checked;

        setAnswers((prevAnswers) => {
            const updatedAnswers = { ...prevAnswers };

            if (type === 'checkbox') {
                if (!updatedAnswers[name]) updatedAnswers[name] = [];
                if (checked) {
                    (updatedAnswers[name] as string[]).push(value);
                } else {
                    updatedAnswers[name] = (updatedAnswers[name] as string[]).filter((val) => val !== value);
                }
            } else if (type === 'radio' || type === 'text') {
                updatedAnswers[name] = [value];
            }

            return updatedAnswers;
        });
    };

    const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(event.target.value);
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        const formData: QuizForm = {
            email,
            answers,
        };

        const formattedAnswers = Object.keys(formData.answers).reduce((acc, key) => {
            const questionId = key.replace('question', '');
            const answer = formData.answers[key];
            acc[parseInt(questionId)] = Array.isArray(answer) ? answer : [answer];
            return acc;
        }, {} as { [key: number]: string[] });

        try {
            const response = await submitQuiz({
                email: formData.email,
                answers: formattedAnswers,
            });

            console.log('Quiz submitted successfully:', response);
            setFinalScore(response.score);
            setIsSubmitted(true);

            localStorage.setItem('quizState', JSON.stringify({
                isSubmitted: true,
                finalScore: response.score,
                answers: formData.answers,
            }));
        } catch (error) {
            console.error('Error submitting quiz:', error);
            alert('There was an error submitting your quiz. Please try again.');
        }
    };

    const handleLeaderboardClick = () => {
        navigate('/HighScores');
    };

    const handleRetakeQuiz = () => {
        setEmail('');
        setAnswers({});
        setIsSubmitted(false);
        localStorage.removeItem('quizState');
    };

    if (isSubmitted) {
        return (
            <div className="quiz-container">
                <Form className="quiz-form" onSubmit={handleSubmit}>
                    <div className="quiz-thank-you-message">
                        <h2>Thank you for participating in our quiz!</h2>
                        <p>Your final mark is:</p>
                        <h3 style={{ color: 'green' }}>
                            {finalScore} <span style={{ color: 'black' }}>/ 1000</span>
                        </h3>
                        <Button onClick={handleRetakeQuiz} style={{ marginRight: '10px' }}>
                            Play the Quiz Again
                        </Button>
                        <Button onClick={handleLeaderboardClick}>Leaderboard</Button>
                    </div>
                </Form>
            </div>
        );
    }

    return (
        <div className="quiz-container">
            <Form className="quiz-form" onSubmit={handleSubmit}>
                <h2 className="quiz-title">Quiz Title</h2>

                <div className="question-text">
                    <Form.Label>Email Address</Form.Label>
                    <Form.Control
                        type="email"
                        name="email"
                        value={email}
                        onChange={handleEmailChange}
                        required
                    />
                </div>

                {quizzes.map((quiz) => (
                    <div className="question" key={quiz.id}>
                        <div className="question-text">{quiz.question}</div>
                        {quiz.type === 'Radio' &&
                            quiz.options.map((option, idx) => (
                                <Form.Check
                                    key={idx}
                                    type="radio"
                                    label={option}
                                    name={`question${quiz.id}`}
                                    value={option}
                                    onChange={handleAnswerChange}
                                />
                            ))}
                        {quiz.type === 'Checkbox' &&
                            quiz.options.map((option, idx) => (
                                <Form.Check
                                    key={idx}
                                    type="checkbox"
                                    label={option}
                                    name={`question${quiz.id}`}
                                    value={option}
                                    onChange={handleAnswerChange}
                                />
                            ))}
                        {quiz.type === 'Textbox' && (
                            <Form.Control
                                type="text"
                                name={`question${quiz.id}`}
                                onChange={handleAnswerChange}
                            />
                        )}
                    </div>
                ))}

                <Button type="submit" className="submit-btn">
                    Submit
                </Button>
            </Form>
        </div>
    );
};

export default Quiz;
