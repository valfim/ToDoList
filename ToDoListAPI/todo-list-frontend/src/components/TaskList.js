// src/components/TaskList.js
import React, { useEffect, useState } from 'react';
import { getTasks } from '../services/TaskService';

const TaskList = () => {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        const fetchTasks = async () => {
            try {
                const tasks = await getTasks();
                setTasks(tasks);
            } catch (error) {
                console.error('Error fetching tasks:', error);
            }
        };

        fetchTasks();
    }, []);

    return (
        <div>
            <h2>Task List</h2>
            <ul>
                {tasks.map((task) => (
                    <li key={task.id}>
                        <h3>{task.title}</h3>
                        <p>{task.description}</p>
                        <p>{task.isCompleted ? 'Completed' : 'Not Completed'}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default TaskList;


