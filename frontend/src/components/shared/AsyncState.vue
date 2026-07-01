<script setup>
import { computed, ref, watch } from 'vue'
import empty from '@/assets/icons/empty.svg'

const emit = defineEmits(['empty-action'])

const props = defineProps({
  isLoading:  { 
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
  data: { 
    type: [Array, Object, String, Number, Boolean, null],
    default: null 
    },
  emptyIcon: { 
    type: [Object, Function, null], 
    default: empty 
    },
  emptyMessage: { 
    type: String, 
    default: 'There are no entries yet.' 
    },
  subEmptyMessage: { 
    type: String, 
    default: 'You can add a new entry or search for existing ones.' 
    },
  buttonText: { 
    type: String, 
    default: 'Add' 
    },
  onRetry: { 
    type: Function, 
    default: null 
    },
  isEmpty: { 
    type: Function, 
    default: null 
    },
})

const hasError = computed(() => !!props.error)

const errorMessage = computed(() => {
  if (!props.error) return ''
  if (typeof props.error === 'string') return props.error
  return props.error.message || 'An error occurred'
})

const isDataEmpty = computed(() => {
  if (props.isEmpty) return props.isEmpty(props.data)
  if (props.data == null) return true
  if (Array.isArray(props.data)) return props.data.length === 0
  return false
})

function handleRetry() {
  props.onRetry?.()
}

function handleEmptyAction(event) {
  emit('empty-action', event.currentTarget)
}

const hasLoadedOnce = ref(false)

watch(
  () => props.isLoading,
  (loading) => { if (loading) hasLoadedOnce.value = true },
  { immediate: true }
)

</script>

<template>
    <!-- 1. LOADING -->
    <div v-if="isLoading" class="async-state async-center">
        <slot name="loading">
        <span class="async-loading">Loading...</span>
        </slot>
    </div>

    <!-- 2. ERROR -->
    <div v-else-if="hasError" class="async-state async-center">
        <slot name="error" :error="error" :message="errorMessage" :retry="onRetry">
        <div class="async-panel">
            <span class="async-title">{{ errorMessage }}</span>
            <button v-if="onRetry" type="button" class="style-1" @click="handleRetry">
            Retry
            </button>
        </div>
        </slot>
    </div>

    <!-- 3. EMPTY -->
    <div v-else-if="isDataEmpty" class="async-state async-center">
        <slot name="empty">
        <div class="async-panel">
            <div v-if="emptyIcon" class="async-icon-wrap">
            <component :is="emptyIcon" class="async-icon" />
            </div>
            <div class="async-text">
            <span class="title weight-500">{{ emptyMessage }}</span>
            <span class="body outline-text">{{ subEmptyMessage }}</span>
            </div>
            <button type="button" class="style-1" @click="handleEmptyAction">
            {{ buttonText }}
            </button>
        </div>
        </slot>
    </div>

    <!-- 4. DATA -->
    <div v-else class="async-data" :class="{ 'async-data-fade': hasLoadedOnce }">
        <slot name="fetching" :is-fetching="isFetching">
        <div v-if="isFetching" class="async-fetching" />
        </slot>
        <slot :data="data" />
    </div>
</template>

<style scoped>
.async-state {
    display: flex;
    flex-grow: 1;
}

.async-center {
    align-items: center;
    justify-content: center;
    padding: 24px;
}

.async-loading {
    font-size: 16px;
    font-weight: 600;
    letter-spacing: 0.02em;
    background: linear-gradient(
        90deg,
        var(--color-text-muted) 0%,
        var(--color-text)       50%,
        var(--color-text-muted) 100%
    );
    background-size: 200% 100%;
    -webkit-background-clip: text;
    background-clip: text;
    color: transparent;
    animation: async-shimmer 1.6s ease-in-out infinite;
}

@keyframes async-shimmer {
    0% { 
        background-position: 200% 0; 
    }
    100% { 
        background-position: -200% 0; 
    }
}

.async-panel {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 16px;
    text-align: center;
    max-width: 360px;
}

.async-icon-wrap {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 24px;
    background: var(--color-surface);
    box-shadow: 0px 0px 0px 1px var(--color-border) inset;
    border-radius: 32px;
}

.async-icon { 
    width: 64px; 
    height: 64px; 
    color: var(--color-text-muted); 
}

.async-text {
    display: flex;
    flex-direction: column;
    gap: 4px;
}

.async-data {
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    max-width: 100%;
}

.async-data-fade { 
    animation: async-fade 0.25s ease; 
}

@keyframes async-fade { 
    from { 
        opacity: 0; 
    } 
    to { 
        opacity: 1; 
    } 
}

</style>