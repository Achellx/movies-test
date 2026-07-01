<script setup>
import MoviesList from '@/components/movie/MoviesList.vue'

</script>

<template>
    <div class="simple-container direction-column width-100 max-width-1000 gap-24 grow-1">
        <div class="simple-container align-center justify-between">
            <span class="display-large weight-600">Movies Catalog</span>
            <button
                class="style-1"
                type="button"
            >
                <span>Add</span>
            </button>
        </div>

        <MoviesList />
    </div>
</template>