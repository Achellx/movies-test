<script setup>
import MoviesList from '@/components/movie/MoviesList.vue'
import MovieCreate from '@/components/movie/MovieCreate.vue'


</script>

<template>
    <div class="simple-container direction-column width-100 max-width-1000 gap-24 grow">
        <div class="simple-container align-center justify-between l-r-padding-8">
            <span class="display-large weight-600">Movies Catalog</span>
            <MovieCreate />
        </div>

        <MoviesList />
    </div>
</template>
