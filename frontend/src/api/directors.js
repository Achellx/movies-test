import { http } from './http';

export async function getDirectors() {
    const { data } = await http.get('/directors');
    return data;
}

export async function createDirector(payload) {
    const { data } = await http.post('/directors', payload);
    return data;
}

export async function updateDirector({id, payload}) {
    const { data } = await http.put(`/directors/${id}`, payload);
    return data;
}

export async function deleteDirector(id) {
    const { data } = await http.delete(`/directors/${id}`);
    return data;
}