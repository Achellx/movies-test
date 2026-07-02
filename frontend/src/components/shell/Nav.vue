<script setup>
import { ref, nextTick, watch, onUnmounted } from 'vue'
import { motion } from 'motion-v'
import search from '@/assets/icons/search.svg'
import close from '@/assets/icons/close.svg'
import { useSearch } from '@/composables/useSearch'

const { query } = useSearch();

const props = defineProps({
    items: {
        type: Array,
        default: () => []
    }
})

const clipping = ref(false);
const shellRef = ref(null);

const spring = {
    type: 'spring',
    damping: 20,
    stiffness: 230,
    mass: 1.7
}

const searchOpen = ref(false);
const inputRef = ref(null);

function onDocClick(e) {
    if (shellRef.value && !shellRef.value.contains(e.target)) {
        closeSearch();
    }
}

watch(searchOpen, (open) => {
    if (open) {
        document.addEventListener('click', onDocClick);
    } else {
        document.removeEventListener('click', onDocClick);
    }
});

onUnmounted(() => {
    document.removeEventListener('click', onDocClick);
});

async function openSearch() {
    searchOpen.value = true;
    await nextTick();
    inputRef.value.focus();
}

function closeSearch() {
    searchOpen.value = false;
    query.value = '';
}

</script>

<template>
    <nav class="app-nav">
        <div class="nav-shell" ref="shellRef">
            <motion.div 
                class="nav-items search-pill"
                :class="{ 'is-expanded': searchOpen }"
                :layout="true"
                :transition="spring"
                @click="!searchOpen && openSearch()"
            >
                <div class="nav-item search-trigger">
                    <component :is="search" />
                    <span class="nav-label">Search</span>
                </div>

                <motion.div
                    class="search-input-wrap"
                    :initial="false"
                    :animate="{
                        width: searchOpen ? 'auto' : '0px',
                        opacity: searchOpen ? 1 : 0,
                        filter: searchOpen ? 'blur(0px)' : 'blur(4px)',
                    }"
                    :transition="spring"
                >
                    <input
                        ref="inputRef"
                        v-model="query"
                        class="search-input"
                        type="text"
                        placeholder="Search movies or directos"
                        @click.stop
                        @keydown.escape="closeSearch"
                    />            
                </motion.div>
            </motion.div>

            <motion.div 
                class="nav-items links-pill"
                :layout="true"
                :transition="spring"
            >

                <motion.div
                    class="links-clip"
                    :style="{ overflow: clipping ? 'hidden' : 'visible'}"
                    :initial="false"
                    :animate="{ width: searchOpen ? '44px' : 'auto' }"
                    :transition="spring"
                    @animationStart="clipping = true"
                    @animationComplete="clipping = false"
                >
                    <motion.div
                        class="links-group"
                        :initial="false"
                        :animate="{
                            opacity: searchOpen ? 0 : 1,
                            filter: searchOpen ? 'blur(4px)' : 'blur(0px)'
                        }"
                        :transition="{ duration: 0.2 }"
                    >
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
                    </motion.div>

                    <motion.div
                        class="links-close"
                        :initial="false"
                        :animate="{
                            opacity: searchOpen ? 1 : 0,
                            filter: searchOpen ? 'blur(0px)' : 'blur(4px)'
                        }"
                        :transition="{ duration: 0.2 }"
                    >
                        <button
                            :style="{ pointerEvents: searchOpen ? 'auto': 'none'}"
                            type="button"
                            class="nav-item"
                        >
                            <component :is="close" @click="closeSearch" />
                            <span class="nav-label">Close</span>
                        </button>
                    </motion.div>
                </motion.div>
            </motion.div>
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
    gap: 12px;
}

.nav-items {
    display: inline-flex;
    flex-direction: row;
    gap: 8px;
    padding: 4px;
    border-radius: 999px;
    background: var(--color-bg);
    color: var(--color-text-muted);
    box-shadow: inset 0 0 0 1.5px var(--color-surface-hover), 0 2px 4px rgba(0, 0, 0, 0.05);
    overflow: visible;
}

.nav-items.search-pill {
    gap: 0;
}

.search-pill {
    flex-shrink: 0;
    cursor: pointer;
}

.search-pill.is-expanded {
    flex: 1;
    cursor: text;
}

.search-input-wrap {
    display: inline-flex;
    align-items: center;
    overflow: hidden;
    pointer-events: auto;
}

.search-input {
    width: 100%;
    min-width: 0;
    border: none;
    outline: none;
    background: transparent;
    color: var(--color-text);
    font-size: 15px;
}

.links-clip {
    position: relative;
    display: inline-flex;
    align-items: center;
    height: 100%;
}

.links-group {
    display: inline-flex;
    gap: 8px;
    white-space: nowrap;
}

.links-close {
    position: absolute;
    inset: 0;
    display: flex;
    align-items: center;
    justify-content: center;
}

.nav-search {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    padding: 4px;
    height: 100%;
    width: 100%;
    min-width: 44px;
    min-height: 44px;
    border-radius: 999px;
    background: var(--color-bg);
    color: var(--color-text-muted);
    box-shadow: inset 0 0 0 1px var(--color-border);
    pointer-events: auto;
    cursor: pointer;
}

.nav-item {
    position: relative;
    display: flex;
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