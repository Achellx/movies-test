<script setup>
import { ref, computed } from "vue";
import MorphShell from '@/components/shared/MorphShell.vue'
import deleteIcon from '@/assets/icons/deleteIcon.svg'
import ConfirmModal from '@/components/shared/ConfirmModal.vue'
import { useDeleteMovie } from '@/composables/useMovies'

const props = defineProps({
    movie: {
        type: Object,
        required: true,
    },
})

const formOpen = ref(false)

const deleteMovie = useDeleteMovie();

const busy = computed(() => deleteMovie.isPending.value);

async function handleDelete() {
    try {
        await deleteMovie.mutateAsync(props.movie.id);
        formOpen.value = false;
    } catch (error) {
        console.error("Error deleting movie:", error);
    }
}
</script>

<template>
    <MorphShell
        v-model="formOpen"
        class="padding-24"
        :border-radius="40"
    >
        <template #collapsed="{ open }">
            <button
                class="style-2 background hover-delete"
                type="button"
                @click="open"
            >
                <component :is="deleteIcon" />
            </button>
        </template>

        <template #expanded="{ close }">
            <div class="morph-form-wrap">
                <ConfirmModal
                    title="Delete Movie"
                    confirmLabel="Delete"
                    :danger="true"
                    @confirm="handleDelete"
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