// src/App.js
import React from 'react';
import TaskForm from './components/TaskForm';
import TaskList from './components/TaskList';

function App() {
    return (
        <div className="App">
            <h1>To-Do List Application</h1>
            <TaskForm />
            <TaskList />
        </div>
    );
}

export default App;



