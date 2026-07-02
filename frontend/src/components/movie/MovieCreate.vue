<script setup>
import { ref, computed } from "vue";
import MorphShell from '@/components/shared/MorphShell.vue'
import MovieForm from '@/components/movie/MovieForm.vue'
import { useDirectors } from '@/composables/useDirectors'
import { useCreateMovie } from '@/composables/useMovies'

const formOpen = ref(false)

const { data: directors } = useDirectors();

const activeDirectors = computed(() => (directors.value ?? []).filter(d => d.active))

const createMovie = useCreateMovie();

const submitting = computed(() => createMovie.isPending.value);

async function handleSubmit(payload) {
    try {
        await createMovie.mutateAsync(payload);
        formOpen.value = false;
    } catch (error) {
        console.error('Error creating movie:', error);
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
                class="style-1"
                type="button"
                @click="open"
            >
                <span>Add</span>
            </button>
        </template>

        <template #expanded="{ close }">
            <div class="morph-form-wrap">
                <MovieForm
                    :active="formOpen"
                    :directors="activeDirectors"
                    :submitting="submitting"
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