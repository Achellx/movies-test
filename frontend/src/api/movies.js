import { http } from './http';

export async function getMovies() {
    const { data } = await http.get('/movies');
    return data;
}

export async function createMovie(payload) {
    const { data } = await http.post('/movies', payload);
    return data;
}

export async function updateMovie({id, payload}) {
    const { data } = await http.put(`/movies/${id}`, payload);
    return data;
}

export async function deleteMovie(id) {
    const { data } = await http.delete(`/movies/${id}`);
    return data;
}