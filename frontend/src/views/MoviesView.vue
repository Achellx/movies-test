<script setup>
import { ref, computed } from "vue";
import MoviesList from '@/components/movie/MoviesList.vue'
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
    <div class="simple-container direction-column width-100 max-width-1000 gap-24 grow-1">
        <div class="simple-container align-center justify-between l-r-padding-8">
            <span class="display-large weight-600">Movies Catalog</span>
            <MorphShell
                v-model="formOpen"
                :border-radius="32"
                class="add-morph"
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
        </div>

        <MoviesList />
    </div>
</template>

<style scoped>
.l-r-padding-8 {
    padding-left: 8px;
    padding-right: 8px;
}

.add-morph {
    --morph-left: auto;
    --morph-right: 0;
}

.morph-form-wrap {
    width: 270px;
}

@media (max-width: 680px) {
    .morph-form-wrap {
        max-height: unset;
        height: 100%;
        width: calc(100dvw - 32px);
    }
}
</style>
