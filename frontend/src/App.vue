<script setup>
import Nav from '@/components/shell/Nav.vue'
import directors from '@/assets/icons/directors.svg'
import movies from '@/assets/icons/movies.svg'


const navItems = [
    {
        name: 'movies',
        label: 'Movies',
        icon: movies,
  },
    {
        name: 'directors',
        label: 'Directors',
        icon: directors,
    }
]
</script>

<template>
    <div class="app-container">
        <Nav :items="navItems" />
        <div class="app-view align-center">
            <RouterView />
        </div>
    </div>
</template>

<style scoped>
.app-container {
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    overflow: hidden;
}

.app-view {
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    padding: 16px;
    padding-top: max(16px, env(safe-area-inset-top));
    gap: 8px;
    overflow: auto;
    scrollbar-width: none;
}
</style>