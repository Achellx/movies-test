import { useQuery, useMutation, useQueryClient } from '@tanstack/vue-query';
import { getDirectors, createDirector } from '@/api/directors';

const directorsKey = ['directors'];

export function useDirectors() {
    return useQuery({
        queryKey: directorsKey,
        queryFn: getDirectors,
    })
}

export function useCreateDirector() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: createDirector,
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: directorsKey,
            });
        },
    })
}
