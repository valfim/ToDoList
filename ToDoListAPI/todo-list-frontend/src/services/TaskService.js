// src/services/TaskService.js
import axios from 'axios';

const API_URL = 'http://localhost:5001/api/tasks';

export const getTasks = async () => {
    try {
        const response = await axios.get(API_URL);
        return response.data;
    } catch (error) {
        console.error('Error fetching tasks:', error);
        throw error;
    }
};

export const createTask = async (task) => {
    try {
        const response = await axios.post(API_URL, task);
        return response.data;
    } catch (error) {
        console.error('Error creating task:', error);
        throw error;
    }
};



