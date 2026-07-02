<script setup>
import deleteIcon from '@/assets/icons/deleteIcon.svg'
import calendar from '@/assets/icons/calendar.svg'
import during from '@/assets/icons/during.svg'
import MovieEdit from '@/components/movie/MovieEdit.vue'
import { motion } from 'motion-v'

defineProps({
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
    <motion.div 
        class="movie-card hover-outline"
        :initial="{ opacity: 0, scale: 0.9 }"
        :animate="{ opacity: 1, scale: 1 }"
        :transition="{
            delay: index * 0.06,
            type: 'spring',
            bounce: 0.45,
            duration: 0.5
        }"
        >
        <div class="action-buttons">
            <MovieEdit :movie="movie" />
            <button
                class="style-2 background"
                type="button"
            >
                <component :is="deleteIcon" />
            </button>
        </div>

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
    </motion.div>
</template>

<style scoped>
.movie-card {
    display: flex;
    flex-direction: column;
    border-radius: 40px;
    padding:  16px;
    background-color: var(--color-surface);
    gap: 24px;
    justify-content: space-between;
    aspect-ratio: 1 / 1;
}

.action-buttons {
    display: flex;
    justify-content: flex-end;
    gap: 8px;
}

.dataline {
    display: inline-flex;
    align-items: center;
    padding: 8px 12px;
    border-radius: 24px;
    background: var(--color-surface-hover);
    gap: 4px;
}

</style>
