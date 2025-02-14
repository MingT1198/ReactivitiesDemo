<template>
    <n-scrollbar style="max-height: 73vh;">
        <n-card :title="activity.title" v-for="activity in activitys" :key="activity.id">
            <template #header-extra>
                <n-button ghost size="small" type="info" @click="emitSelectActivity(activity.id)">View</n-button>
            </template>
            <n-flex vertical>
                <div>
                    {{ activity.description }}
                </div>
                <div>
                    {{ activity.city }}, {{ activity.venue }}
                </div>
                <n-time :time="activity.date" />
            </n-flex>
        </n-card>
    </n-scrollbar>
</template>

<script setup lang="ts">
import type { Activity } from '@/models/Activity';

import { NScrollbar, NCard, NButton, NFlex, NTime } from 'naive-ui';

const {activitys} = defineProps({
    activitys: {
        type: Array as () => Activity[],
        required: true
    }
});

const emit = defineEmits<{
  (e: 'selectActivity', newValue: Activity | undefined): void;
}>();
const emitSelectActivity = (id: string):void => {
  const activity: Activity | undefined = activitys.find(a => a.id === id);
  emit('selectActivity', activity);
}
</script>

<style>
</style>