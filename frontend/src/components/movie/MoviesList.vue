<script setup>
import EntityList from '@/components/shared/EntityList.vue'
import MovieCard from '@/components/movie/MovieCard.vue'
import { useMovies } from '@/composables/useMovies.js'

const { data, isLoading, isFetching, error, refetch } = useMovies();

</script>

<template>
    <EntityList
        :items="data ?? []"
        :is-loading="isLoading"
        :is-fetching="isFetching"
        :error="error"
        :empty-message="'No movies found'"
        :sub-empty-message="'Please add some movies to get started.'"
        :button-text="'Add Movie'"
        :refetch="refetch"
    >
        <template #item="{ item, index }">
            <div class="items-container">
                <MovieCard
                    :movie="item"
                    :index="index"
                />
            </div>
        </template>
    </EntityList>
</template>
