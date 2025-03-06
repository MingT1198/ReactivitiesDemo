<template>
    <n-scrollbar style="max-height: 73vh;">
        <n-card :title="activity.title" v-for="activity in activitys" :key="activity.id">
            <template #header-extra>
                <n-button ghost size="small" type="error" @click="emitDeleteActivity(activity.id)">Delete</n-button>
                <n-button ghost size="small" type="info" @click="emitSelectActivity(activity.id)">View</n-button>
            </template>
            <n-flex vertical>
                <div v-if="activity.description">
                    {{ activity.description }}
                </div>
                <div v-if="activity.city || activity.venue">
                    <template v-if="activity.city && activity.venue">
                        {{ activity.city }}, {{ activity.venue }}
                    </template>
                    <template v-else-if="activity.city">
                        {{ activity.city }}
                    </template>
                    <template v-else-if="activity.venue">
                        {{ activity.venue }}
                    </template>
                </div>
                <n-time :time="activity.date" />
            </n-flex>
        </n-card>
    </n-scrollbar>
</template>

<script setup lang="ts">
import type { Activity } from '@/models/Activity'
import type { PropType } from 'vue'

import { NScrollbar, NCard, NButton, NFlex, NTime } from 'naive-ui'

const {activitys} = defineProps({
    activitys: {
        type: Array as PropType<Activity[]>,
        required: true
    }
})

const emit = defineEmits<{
  (e: 'selectActivity', id: string ): void
  (e: 'deleteActivity', id: string ): void
}>()
const emitSelectActivity = (id: string):void => {
  emit('selectActivity', id)
}
const emitDeleteActivity = (id: string):void => {
  emit('deleteActivity', id)
}
</script>

<style scoped>
</style>