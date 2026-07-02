<script setup>
import { AnimatePresence } from 'motion-v'
import AsyncState from '@/components/shared/AsyncState.vue'

const props = defineProps({
    items: {
        type: Array,
        default: () => []
    },
    isLoading: {
        type: Boolean,
        default: false
    },
    isFetching: {
        type: Boolean,
        default: false
    },
    error: {
        type: [Object, String, null],
        default: null
    },
    refetch: {
        type: Function,
        default: null
    },
    emptyMessage: {
        type: String,
        default: 'No items found'
    },
    subEmptyMessage: {
        type: String,
        default: 'Please add some items to get started.'
    },
    buttonText: {
        type: String,
        default: 'Add'
    },
})

defineEmits(['empty-action'])

</script>

<template>
    <AsyncState
        :is-loading="isLoading"
        :is-fetching="isFetching"
        :error="error"
        :data="items"
        :empty-message="emptyMessage"
        :sub-empty-message="subEmptyMessage"
        :button-text="buttonText"
        :on-retry="refetch"
        @empty-action="$emit('empty-action', $event)"
    >
        <template #default="{ data }">
            <div class="items-container">
                <AnimatePresence mode="popLayout">
                    <slot
                        v-for="(item, i) in data"
                        name="item"
                        :key="item.id"
                        :item="item"
                        :index="i"
                    />
                </AnimatePresence>
            </div>
        </template>
    </AsyncState>
</template>

<style scoped>
.items-container {
    position: relative;
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
    gap: 8px;
}
</style>