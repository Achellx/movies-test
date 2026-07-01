import axios from 'axios';

export const http = axios.create({
    baseURL: 'http://localhost:5084/api/v1',
    headers: {
        'Content-Type' : 'application/json'
    }
})