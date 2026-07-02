<script setup>
import { ref, watch } from 'vue'
import calendar from '@/assets/icons/calendar.svg'
import during from '@/assets/icons/during.svg'
import MovieEdit from '@/components/movie/MovieEdit.vue'
import MovieDelete from '@/components/movie/MovieDelete.vue'
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

const flash = ref(0)

watch(() => JSON.stringify([
    props.movie.name,
    props.movie.directorName,
    props.movie.genre,
    props.movie.releaseYear,
    props.movie.duration
]),
() => { flash.value ++ })

function formatDate(value) {
    if (!value) return '-'
    const [h, m] = value.split(':')
    return `${Number(h)}h ${Number(m)}m`
}
</script>

<template>
    <motion.div 
        class="movie-card hover-outline"
        :layout="true"
        :initial="{ opacity: 0, scale: 0.9 }"
        :animate="{ opacity: 1, scale: 1 }"
        :exit="{ 
            opacity: 0, 
            scale: 0.6,
            filter: 'blur(8px)',
            transition: {
                duration: 0.3
            }
        }"
        :transition="{
            layout: {
                type: 'spring',
                stiffness: 350,
                damping: 30
            },
            delay: index * 0.06,
            type: 'spring',
            bounce: 0.45,
            duration: 0.5
        }"
        >

        <motion.div
            v-if="flash"
            :key="flash"
            class="edit-flash"
            :initial="{ opacity: 0 }"
            :animate="{ opacity: [0,1,0] }"
            :transition="{ duration: 0.9, times: [0,0.15,1], ease: 'easeInOut'}"
        />

        <div class="action-buttons">
            <MovieEdit :movie="movie" />
            <MovieDelete :movie="movie" />
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
    position: relative;
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

.edit-flash {
    position: absolute;
    inset: 0;
    border-radius: 40px;
    pointer-events: none;
    z-index: 1;
    box-shadow: inset 0 0 0 2px var(--color-text), 0 0 24px 2px color-mix(in oklch, var(--color-text) 25%, transparent);
}


</style>
