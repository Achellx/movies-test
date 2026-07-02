<script setup>
import { computed } from 'vue'
import EntityList from '@/components/shared/EntityList.vue'
import DirectorCard from '@/components/director/DirectorCard.vue'
import { useDirectors } from '@/composables/useDirectors.js'
import { useSearch } from '@/composables/useSearch.js'

const { data, isLoading, isFetching, error, refetch } = useDirectors();

const { query } = useSearch();

const filtered = computed(() => {
    const q = query.value.trim().toLowerCase();
    if (!q) return data.value ?? [];
    return (data.value ?? []).filter(d => 
        d.name.toLowerCase().includes(q) ||
        d.nationality.toLowerCase().includes(q) ||
        d.age.toString().toLowerCase().includes(q)
    )
})


</script>

<template>
    <EntityList
        :items="filtered"
        :is-loading="isLoading"
        :is-fetching="isFetching"
        :error="error"
        :empty-message="'No directors found'"
        :sub-empty-message="'Please add some directors to get started.'"
        :button-text="'Add Director'"
        :refetch="refetch"
    >
        <template #item="{ item, index }">
            <div class="items-container">
                <DirectorCard
                    :director="item"
                    :index="index"
                />
            </div>
        </template>
    </EntityList>
</template>
