<script setup>
import { useIsFetching } from '@tanstack/vue-query'
import Nav from '@/components/shell/Nav.vue'
import directors from '@/assets/icons/directors.svg'
import movies from '@/assets/icons/movies.svg'


const isFetching = useIsFetching();

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
    <div 
        class="app-container"
        :class="{ 'animation-loading-1': isFetching > 0 }"    
        >
        <Nav :items="navItems" />
        <div class="app-view align-center">
            <RouterView />
        </div>
        <div class="mask-gradient"></div>
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
    padding-top: max(80px, env(safe-area-inset-top));
    gap: 8px;
    overflow: auto;
    scrollbar-width: thin;
    scrollbar-gutter: stable;
}

.animation-loading-1 {
  position: relative;
   border-radius: inherit;
  overflow: hidden !important;
  pointer-events: none;
}

.animation-loading-1::before {
  content: "";
  position: absolute;
  inset: 0;
  background: linear-gradient(
    to right,
    transparent,
    color-mix(in oklch, var(--color-text) 8%, transparent),
    transparent
  ) !important;
  transform: translateX(-100%);
  border-radius: inherit;
  animation: hover-magic-1-shimmer 1s infinite;
  z-index: 2;
}

@keyframes hover-magic-1-shimmer {
  100% {
    transform: translateX(100%);
  }
}

.mask-gradient {
  position: fixed;
  bottom: 0;
  inset-inline: 0;
  height: 80px;
  background-image: linear-gradient(to top in oklab, var(--color-bg) 0%, color-mix(in oklab, var(--color-bg) 60%, transparent) 50%, transparent 100%);
  pointer-events: none;
  z-index: 20;

}

</style>