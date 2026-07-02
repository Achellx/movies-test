<script setup>
import { ref, computed } from "vue";
import MorphShell from '@/components/shared/MorphShell.vue'
import DirectorForm from '@/components/director/DirectorForm.vue'
import { useUpdateDirector } from '@/composables/useDirectors'
import edit from '@/assets/icons/edit.svg'

const props = defineProps({
    director: {
        type: Object,
        required: true
    }
})

const formOpen = ref(false)

const updateDirector = useUpdateDirector();

const submitting = computed(() => updateDirector.isPending.value);

async function handleSubmit(payload) {
    try {
        await updateDirector.mutateAsync({ id: props.director.id, payload });
        formOpen.value = false;
    } catch (error) {
        console.error('Error updating director:', error);
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
                <DirectorForm 
                    :active="formOpen"
                    :initial-values="director"
                    :submitting="submitting"
                    title="Edit Director"
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