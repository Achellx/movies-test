import { createRouter, createWebHistory } from 'vue-router';
import MoviesView from '@/views/MoviesView.vue';
import DirectorsView from '@/views/DirectorsView.vue';

const routes = [
    { path: '/', redirect: '/movies'},
    { path: '/movies', name: 'movies', component: MoviesView },
    { path: '/directors', name: "directors", component: DirectorsView },
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router;