import { useQuery, useMutation, useQueryClient } from '@tanstack/vue-query';
import { getDirectors, createDirector, updateDirector, deleteDirector } from '@/api/directors';

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

export function useUpdateDirector() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: updateDirector,
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: directorsKey,
            });
            queryClient.invalidateQueries({
                queryKey: ['movies'],
            })
        }
    })
}

export function useDeleteDirector() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: deleteDirector,
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: directorsKey,
            });
        }
    })
}
