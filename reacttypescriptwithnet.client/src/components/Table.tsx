import React, { useState, useEffect } from 'react';

interface Forecast {
    id: number;
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

interface TableProps {
    forecasts: Forecast[];
    addForecast: (forecast: Forecast) => void;
}

const Table: React.FC<TableProps> = ({ forecasts, addForecast }) => {
    const [newForecast, setNewForecast] = useState<Partial<Forecast>>({
        date: '',
        temperatureC: 0,
        summary: ''
    });

const handleAddForecast = () => {
    if (newForecast.date && newForecast.temperatureC && newForecast.summary) {
        addForecast({
            id: 0,
            date: newForecast.date,
            temperatureC: newForecast.temperatureC,
            temperatureF: 32 + (newForecast.temperatureC / 0.5556),
            summary: newForecast.summary
        } as Forecast);
        setNewForecast({ date: '', temperatureC: 0, summary: '' });
    }
};

return (
    <div>
        <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
                <tr>
                    <td>
                        <input
                            type="date"
                            value={newForecast.date}
                            onChange={(e) => setNewForecast({ ...newForecast, date: e.target.value })}
                        />
                    </td>
                    <td>
                        <input
                            type="number"
                            value={newForecast.temperatureC}
                            onChange={(e) => setNewForecast({ ...newForecast, temperatureC: parseInt(e.target.value) })}
                        />
                    </td>
                    <td>
                        {32 + (newForecast.temperatureC / 0.5556)}
                    </td>
                    <td>
                        <input
                            type="text"
                            value={newForecast.summary}
                            onChange={(e) => setNewForecast({ ...newForecast, summary: e.target.value })}
                        />
                    </td>
                    <td>
                        <button onClick={handleAddForecast}>Add</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
);
};

export default Table;
