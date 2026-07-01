import { useQuery, useMutation, useQueryClient } from '@tanstack/vue-query';
import { getMovies, createMovie } from '@/api/movies';

const moviesKey = ['movies'];

export function useMovies() {
    return useQuery({
        queryKey: moviesKey,
        queryFn: getMovies,
    })
}

export function useCreateMovie() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: createMovie,
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: moviesKey,
            });
        },
    })
}
