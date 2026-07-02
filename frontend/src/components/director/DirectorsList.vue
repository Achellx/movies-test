<script setup>
import EntityList from '@/components/shared/EntityList.vue'
import DirectorCard from '@/components/director/DirectorCard.vue'
import { useDirectors } from '@/composables/useDirectors.js'

const { data, isLoading, isFetching, error, refetch } = useDirectors();

</script>

<template>
    <EntityList
        :items="data ?? []"
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
