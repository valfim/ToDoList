
import React, { useState } from 'react';
import { createTask } from '../services/TaskService';

const TaskForm = () => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const newTask = { title, description, isCompleted: false, createdAt: new Date() };
            await createTask(newTask);
            setTitle('');
            setDescription('');
            // Optional: Trigger a refresh of the task list here
        } catch (error) {
            console.error('Error creating task:', error);
        }
    };

    return (
        <div>
            <h2>Create Task</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Title:
                    <input
                        type="text"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                </label>
                <br />
                <label>
                    Description:
                    <textarea
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </label>
                <br />
                <button type="submit">Create Task</button>
            </form>
        </div>
    );
};

export default TaskForm;

