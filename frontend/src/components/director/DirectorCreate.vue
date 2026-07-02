<script setup>
import { ref, computed } from "vue";
import MorphShell from '@/components/shared/MorphShell.vue'
import DirectorForm from '@/components/director/DirectorForm.vue'
import { useCreateDirector } from '@/composables/useDirectors'

const formOpen = ref(false)

const createDirector = useCreateDirector();

const submitting = computed(() => createDirector.isPending.value);

async function handleSubmit(payload) {
    try {
        await createDirector.mutateAsync(payload);
        formOpen.value = false;
    } catch (error) {
        console.error('Error creating director:', error);
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
                <DirectorForm
                    :active="formOpen"
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