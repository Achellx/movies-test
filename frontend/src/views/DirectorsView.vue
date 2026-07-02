<script setup>
import DirectorsList from '@/components/director/DirectorsList.vue'
import DirectorCreate from '@/components/director/DirectorCreate.vue'

</script>

<template>
    <div class="simple-container direction-column width-100 max-width-1000 gap-24 grow">
        <div class="simple-container align-center justify-between l-r-padding-8">
            <span class="display-large weight-600">Directors Catalog</span>
            <DirectorCreate />
        </div>

        <DirectorsList />
    </div>
</template>