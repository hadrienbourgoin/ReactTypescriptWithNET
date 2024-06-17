import React, { useState, useEffect } from 'react';
import Table from './components/Table';
import './App.css';

interface Forecast {
    id: number;
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

const App: React.FC = () => {
    const [forecasts, setForecasts] = useState<Forecast[] | undefined>(undefined);

    useEffect(() => {
        populateWeatherData();
    }, []);

    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }

    async function addForecast(forecast: Forecast) {
        const response = await fetch('weatherforecast', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(forecast)
        });
        if (response.ok) {
            const newForecast = await response.json();
            setForecasts([...forecasts!, newForecast]);
        }
    }

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <Table forecasts={forecasts} addForecast={addForecast} />;

    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            {contents}
        </div>
    );
};

export default App;
