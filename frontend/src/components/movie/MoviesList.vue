<script setup>
import { AnimatePresence } from 'motion-v'
import MovieCard from '@/components/movie/MovieCard.vue'
import AsyncState from '@/components/shared/AsyncState.vue'
import { useMovies } from '@/composables/useMovies'

const { data, isLoading, isFetching, error, refetch } = useMovies();
</script>

<template>
    <AsyncState
        :is-loading="isLoading"
        :is-fetching="isFetching"
        :error="error"
        :data="data"
        :empty-message="'No movies found'"
        :sub-empty-message="'Please add some movies to get started.'"
        :button-text="'Add Movie'"
        :on-retry="refetch"
        :on-empty-action="handleAddMovie"
    >
        <template #default="{ data }">
            <div class="items-container">
                <AnimatePresence mode="popLayout">
                    <MovieCard
                        v-for="(movie, i) in data"
                        :key="movie.id"
                        :movie="movie"
                        :index="i"
                    />
                </AnimatePresence>
            </div>
        </template>
    </AsyncState>
</template>

<style scoped>
.items-container {
    position: relative;
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
    gap: 8px;
}
</style>