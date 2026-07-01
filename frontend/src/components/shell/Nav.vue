<script setup>
import { useRoute, useRouter } from 'vue-router'
import { motion } from 'motion-v'

const props = defineProps({
    items: {
        type: Array,
        default: () => []
    }
})

const route = useRoute();
const router = useRouter();
</script>


<template>
    <nav class="app-nav">
        <div class="nav-shell">
            <div class="nav-search">
            </div>

            <div class="nav-items">
                <RouterLink
                    v-for="item in items"
                    :key="item.name"
                    :to="{ name: item.name }"
                    custom
                    v-slot="{ isActive, navigate }"
                >
                    <button
                        class="nav-item"
                        :class="{ 'is-active': isActive }"
                        @click="navigate"
                    >
                        <motion.div
                            v-if="isActive"
                            layout-id="nav-pill"
                            class="nav-pill"
                            :transition="{ type: 'spring', bounce: 0.4, duration: 0.5 }"
                        />
                        <component :is="item.icon" />
                        <span class="nav-label">{{ item.label }}</span>
                </button>
            </RouterLink>
            </div>
        </div>
    </nav>
</template>

<style scoped>
nav.app-nav {
    position: fixed;
    display: flex;
    bottom: 0;
    padding: 0 12px calc(env(safe-area-inset-bottom) + 12px);
    inset-inline: 0;
    justify-content: center;
    pointer-events: none;
    z-index: 40;
}

.nav-shell {
    display: inline-flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
}

.nav-items {
    display: inline-flex;
    flex-direction: row;
    gap: 8px;
    padding: 4px;
    border-radius: 999px;
    background: var(--color-bg);
    color: var(--color-text-muted);
    box-shadow: inset 0 0 0 1px var(--color-border), 0 2px 4px rgba(0, 0, 0, 0.05);
}

.nav-search {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    padding: 4px;
    border-radius: 999px;
}

.nav-item {
    position: relative;
    display: flex;
    width: 100%;
    height: 100%;
    min-width: 44px;
    min-height: 44px;
    border-radius: 999px;
    justify-content: center;
    align-items: center;
    border: none;
    outline: none;
    cursor: pointer;
    pointer-events: auto;
    background: transparent;
    color: inherit;
    transition: color 0.3s;
}

.nav-item:hover:not(.is-active) {
    color: var(--color-text);
}

.nav-item.is-active {
    color: var(--color-text);
}

.nav-pill {
    position:absolute;
    inset: 0;
    z-index: 0;
    border-radius: 999px;
    background: var(--color-surface);
}

.nav-item svg {
    width: 20px;
    height: 20px;
}

.nav-item > svg {
    position: relative;
    z-index: 1;
}

.nav-label {
    position: absolute;
    bottom: calc(100% + 10px);
    left: 50%;
    padding: 4px 8px;
    border-radius: 12px;
    background: var(--color-surface);
    color: var(--color-text);
    box-shadow: inset 0 0 0 1px var(--color-border);
    font-size: 14px;
    font-weight: 500;
    white-space: nowrap;
    opacity: 0;
    transform: translateX(-50%) translateY(12px) scale(0.8);
    pointer-events: none;
    filter: blur(8px);
    transition: opacity 0.15s, transform 0.15s cubic-bezier(.4, 0, .2, 1), filter 0.15s cubic-bezier(.4, 0, .2, 1);
    user-select: none;
}

.nav-item:hover .nav-label {
    opacity: 1;
    transform: translateX(-50%) translateY(0) scale(1);
    filter: blur(0);
}

</style>