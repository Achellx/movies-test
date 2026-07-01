<script setup>
import { ref } from "vue";
import MoviesList from '@/components/movie/MoviesList.vue'
import MorphShell from '@/components/shared/MorphShell.vue'
import MovieForm from '@/components/movie/MovieForm.vue'

const formOpen = ref(false)
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
                            @submit="close" 
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
    width: 380px;
}

@media (max-width: 768px) {
    .morph-form-wrap {
        width: calc(100vw - 32px);
    }
}
</style>
