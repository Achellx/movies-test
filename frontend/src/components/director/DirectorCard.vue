<script setup>
import person from '@/assets/icons/person.svg'
import world from '@/assets/icons/world.svg'
import age from '@/assets/icons/age.svg'
import EntityCard from '@/components/shared/EntityCard.vue'
import DirectorEdit from '@/components/director/DirectorEdit.vue'
import DirectorDelete from '@/components/director/DirectorDelete.vue'

const props = defineProps({
    director: {
        type: Object,
        required: true
    },
    index: {
        type: Number,
        default: 0
    }
})

</script>

<template>
    <EntityCard
        :item="director"
        :index="index"
        >
        <template #action-buttons>
            <DirectorEdit :director="director" />
            <DirectorDelete :director="director" />
        </template>

        <template #content>
            <div class="simple-container direction-column padding-8 gap-24">
                <div class="simple-container direction-column gap-8">
                    <component :is="person" />
                    <span class="display-small outline-text">{{  director.name }}</span>
                </div>
    
                <div class="simple-container flex-wrap gap-8 align-center">
                    <span class="dataline">
                        <component :is="world" />
                        <span class="body">{{  director.nationality }}</span>
                    </span>
                    <span class="dataline">
                        <component :is="age" />
                        <span class="body">{{ director.age }} year old</span>
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
