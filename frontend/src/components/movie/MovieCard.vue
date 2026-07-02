<script setup>
import { ref, watch } from 'vue'
import calendar from '@/assets/icons/calendar.svg'
import during from '@/assets/icons/during.svg'
import MovieEdit from '@/components/movie/MovieEdit.vue'
import MovieDelete from '@/components/movie/MovieDelete.vue'
import EntityCard from '@/components/shared/EntityCard.vue'
import { motion } from 'motion-v'

const props = defineProps({
    movie: {
        type: Object,
        required: true
    },
    index: {
        type: Number,
        default: 0
    }
})

function formatDate(value) {
    if (!value) return '-'
    const [h, m] = value.split(':')
    return `${Number(h)}h ${Number(m)}m`
}
</script>

<template>
    <EntityCard
        :item="movie"
        :index="index"
        >
        <template #action-buttons>
            <MovieEdit :movie="movie" />
            <MovieDelete :movie="movie" />
        </template>

        <template #content>
            <div class="simple-container direction-column padding-8 gap-24">
                <div class="simple-container direction-column gap-8">
                    <div class="simple-container direction-column">
                        <span class="display-small outline-text">{{  movie.directorName }}</span>
                        <span class="display-medium weight-500 line-height-1">{{  movie.name }}</span>
                    </div>
                    <div class="simple-container direction-column">
                        <span class="title outline-text">{{ movie.genre }}</span>
                    </div>
                </div>
    
                <div class="simple-container flex-wrap gap-8 align-center">
                    <span class="dataline">
                        <component :is="calendar" />
                        <span class="body">{{  movie.releaseYear }}</span>
                    </span>
                    <span class="dataline">
                        <component :is="during" />
                        <span class="body">{{ formatDate(movie.duration) }}</span>
                    </span>
                </div>
            </div>
        </template>
    </EntityCard>
</template>

<style scoped>

.dataline {
    display: inline-flex;
    align-items: center;
    padding: 8px 12px;
    border-radius: 24px;
    background: var(--color-surface-hover);
    gap: 4px;
}

</style>
