<script setup>
import { ref, watch } from 'vue'
import { motion } from 'motion-v'

const props = defineProps({
    item: {
        type: Object,
        required: true
    },
    index: {
        type: Number,
        default: 0
    }
})

const flash = ref(0)

watch(
    () => JSON.stringify(props.item),
    () => { flash.value ++ }
)

</script>

<template>
    <motion.div 
        class="entity-card hover-outline"
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
            <slot name="action-buttons" />
        </div>

        <slot name="content" />
        
    </motion.div>
</template>

<style scoped>
.entity-card {
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

.edit-flash {
    position: absolute;
    inset: 0;
    border-radius: 40px;
    pointer-events: none;
    z-index: 1;
    box-shadow: inset 0 0 0 2px var(--color-text), 0 0 24px 2px color-mix(in oklch, var(--color-text) 25%, transparent);
}
</style>
