<template>
    <n-card :title="activityComputed.title">
      <template #cover>
        <n-image
            v-if="!(activityComputed.category == null || activityComputed.category == undefined)"
            :object-fit="'contain'"
            :src="`/assets/categoryImages/${activityComputed.category}.jpg`"
        >
        </n-image>
      </template>
      {{ activityComputed.description }}
      <template #footer>
        <n-time type="date" :time="activityComputed.date ? new Date(activityComputed.date) : undefined" />
      </template>
      <template #action>
        <n-flex>
          <n-button type="primary" strong ghost @click="emitEditModeActivity">Edit</n-button>
          <n-button strong ghost @click="emitCancelActivity">Cancel</n-button>
        </n-flex>
      </template>
    </n-card>
</template>
  
<script setup lang="ts">
import type { PropType, ComputedRef } from 'vue';
import type { Activity } from '@/models/Activity';

import { NCard, NButton, NImage, NTime, NFlex } from 'naive-ui'
import { computed } from 'vue'

const {activity} = defineProps({
    activity: {
        type: Object as PropType<Activity> | PropType<undefined>,
        required: true,
    }
})

const activityComputed:ComputedRef<Activity> = computed(() => 
  activity ?? ({} as Activity)
)

const emit = defineEmits<{
  (e: 'cancelActivity'): void;
  (e: 'editModeActivity'): void;
}>()

const emitCancelActivity = () => {
  emit('cancelActivity');
}

const emitEditModeActivity = () => {
  emit('editModeActivity');
}
</script>

<style scoped>
.flex {
    justify-content: "center"
}
.n-button {
    flex:1;
}
</style>
  