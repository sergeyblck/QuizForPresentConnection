import React, { useEffect, useState } from 'react';
import { Table, Container } from 'react-bootstrap';
import { getHighScores } from '../services/ApiService';
import './HighScores.css';

interface HighScore {
    id: number;
    email: string;
    score: number;
    dateTime: string;
}

const HighScores: React.FC = () => {
    const [highScores, setHighScores] = useState<HighScore[]>([]);

    useEffect(() => {
        const fetchScores = async () => {
            try {
                const scores = await getHighScores();
                // Sort the scores by 'score' in descending order
                const sortedScores = scores.sort((a: HighScore, b: HighScore) => b.score - a.score);
                setHighScores(sortedScores.slice(0, 10)); // Limit to top 10 scores
            } catch (error) {
                console.error('Error fetching high scores', error);
            }
        };
        fetchScores();
    }, []);

    const formatDate = (date: string) => new Date(date).toLocaleString(); // Format date

    return (
        <Container className="high-scores-container">
            <h1 className="high-scores-title">Top 10 High Scores</h1>
            <table className="high-scores-table">
                <thead>
                <tr>
                    <th>#</th>
                    <th className="email-column">Email</th>
                    <th>Points</th>
                    <th>Date</th>
                </tr>
                </thead>
                <tbody>
                {highScores.map((score, index) => {
                    const rankStyle =
                        index === 0 ? 'gold' :
                            index === 1 ? 'silver' :
                                index === 2 ? 'bronze' :
                                    'regular';

                    return (
                        <tr key={score.id}>
                            <td className={rankStyle}>{index + 1}</td>
                            <td className={`email-column ${rankStyle}`}>
                                <span title={score.email}>{score.email}</span> {/* Tooltip for full email */}
                            </td>
                            <td className={rankStyle}>{score.score}</td>
                            <td className={rankStyle}>{formatDate(score.dateTime)}</td>
                        </tr>
                    );
                })}
                </tbody>
            </table>
        </Container>
    );
};

export default HighScores;
