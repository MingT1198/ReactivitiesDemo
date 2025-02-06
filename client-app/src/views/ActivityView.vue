<template>
    <n-h1>This is an about page</n-h1>
    <n-h2 v-for="activity in activitys" :key="activity.id">{{ activity }}</n-h2>
</template>

<script lang="ts">
import type {Ref } from 'vue';
import type { Activity } from '@/models/Activity';

import axios from 'axios';

import { NH1, NH2 } from 'naive-ui';

import { defineComponent, ref } from 'vue';

export default defineComponent({ 
    components: {
        NH1,
        NH2,
    },
    setup() {
        const activitys: Ref<Activity[]> = ref([]);

        axios.get<Activity[]>('/api/activity').then( res => {
            activitys.value = res.data;
        });

        return { activitys };
    },
});
</script>

<style>
</style>
