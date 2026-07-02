<script setup>
import { computed } from 'vue'
import { useSearch } from '@/composables/useSearch.js'
import EntityList from '@/components/shared/EntityList.vue'
import MovieCard from '@/components/movie/MovieCard.vue'
import { useMovies } from '@/composables/useMovies.js'

const { data, isLoading, isFetching, error, refetch } = useMovies();

const { query } = useSearch();

const filtered = computed(() => {
    const q = query.value.trim().toLowerCase();
    if (!q) return data.value ?? [];
    return (data.value ?? []).filter(m => 
        m.name.toLowerCase().includes(q) ||
        m.genre.toLowerCase().includes(q) ||
        m.directorName.toLowerCase().includes(q)
    )
})

</script>

<template>
    <EntityList
        :items="filtered"
        :is-loading="isLoading"
        :is-fetching="isFetching"
        :error="error"
        :empty-message="'No movies found'"
        :sub-empty-message="'Please add some movies to get started.'"
        :button-text="'Add Movie'"
        :refetch="refetch"
    >
        <template #item="{ item, index }">
            <MovieCard
                :movie="item"
                :index="index"
            />
        </template>
    </EntityList>
</template>
