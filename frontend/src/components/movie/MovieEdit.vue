<script setup>
import { ref, computed } from "vue";
import MorphShell from '@/components/shared/MorphShell.vue'
import MovieForm from '@/components/movie/MovieForm.vue'
import { useDirectors } from '@/composables/useDirectors'
import { useUpdateMovie } from '@/composables/useMovies'
import edit from '@/assets/icons/edit.svg'

const props = defineProps({
    movie: {
        type: Object,
        required: true
    }
})

const formOpen = ref(false)

const { data: directors } = useDirectors();

const activeDirectors = computed(() => (directors.value ?? []).filter(d => d.active))

const updateMovie = useUpdateMovie();

const submitting = computed(() => updateMovie.isPending.value);

async function handleSubmit(payload) {
    try {
        await updateMovie.mutateAsync({ id: props.movie.id, payload });
        formOpen.value = false;
    } catch (error) {
        console.error('Error updating movie:', error);
    }
}

</script>

<template>
    <MorphShell
        v-model="formOpen"
        :border-radius="32"
    >
        <template #collapsed="{ open }">
            <button
                class="style-2 background"
                type="button"
                @click="open"
            >
                <component :is="edit" />
            </button>
        </template>

        <template #expanded="{ close }">
            <div class="morph-form-wrap">
                <MovieForm 
                    :active="formOpen"
                    :initial-values="movie"
                    :directors="activeDirectors"
                    :submitting="submitting"
                    title="Edit Movie"
                    submit-label="Save"
                    @submit="handleSubmit" 
                    @cancel="close" 
                />
            </div>
        </template>
    </MorphShell>
</template>

<style scoped>
.morph-form-wrap {
    width: 300px;
}

@media (max-width: 680px) {
    .morph-form-wrap {
        width: calc(100dvw - 64px);
    }
}
</style>