import { http } from './http';

export async function getDirectors() {
    const { data } = await http.get('/directors');
    return data;
}

export async function createDirector(payload) {
    const { data } = await http.post('/directors', payload);
    return data;
}