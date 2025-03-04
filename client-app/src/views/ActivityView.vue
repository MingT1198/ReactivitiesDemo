<template>
    <n-flex vertical style="max-height: 80vh;">
        <n-button ghost size="small" type="primary" style="flex-grow:1; max-width: 50px;" @click="handleCreateActivity">
            <template #icon><n-icon><Create /></n-icon></template>
        </n-button>
        <n-flex style="flex-grow:4; justify-content: center;">
            <activity-list style="flex: 3;" :activitys="activities" @select-activity="handleSelectActivity" v-if="activities"/>

            <n-flex vertical style="flex: 2; padding: 5px;" v-if="(activity || editMode) && !isMobile">
                <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity" v-if="activity && !editMode"/>
                <activity-form :activity="activity" @cancel-mode-activity="handleCancelModeActivity" @submit-mode-activity="handleSubmitModeActivity" v-if="editMode"/>
            </n-flex>

            <n-flex vertical style="flex: 2;" v-else-if="(activity || editMode) && isMobile">
                <n-modal :show="(activity !== undefined)" >
                    <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity"/>
                </n-modal>
                <n-drawer :default-width="'90%'" :show="editMode" placement="right">
                    <n-drawer-content>
                      <activity-form :activity="activity" @cancel-mode-activity="handleCancelModeActivity" @submit-mode-activity="handleSubmitModeActivity"/>
                  </n-drawer-content>
                </n-drawer>
            </n-flex>
        </n-flex>
    </n-flex>
</template>

<script setup lang="ts">
import type { Activity } from '@/models/Activity'
import type { Ref } from 'vue'

import { Create } from '@vicons/ionicons5'

import { NFlex, NButton, NIcon, NDrawer, NDrawerContent, NModal } from 'naive-ui'
import { ref, inject } from 'vue'
import { storeToRefs } from 'pinia'

import ActivityList from '@/components/activity/ActivityList.vue'
import ActivityDital from '@/components/activity/ActivityDital.vue'
import ActivityForm from '@/components/activity/ActivityForm.vue'
import { useActivityStore } from '@/stores/activityStore'

const activityStore = useActivityStore()

const {activities} = storeToRefs(activityStore)
const activity: Ref<Activity | undefined> = ref(undefined)
const editMode: Ref<boolean> = ref(false)
const isMobile: Ref<boolean> = inject('isMobile', ref(false))

const handleCancelActivity = ():void => { activity.value = undefined }
const handleEditModeActivity = ():void => { editMode.value = true }
const handleCancelModeActivity = ():void => { editMode.value = false }
const handleSubmitModeActivity = (submitValue: Activity):void => {
    editMode.value = false
    activity.value = undefined
    activityStore.setActivity(submitValue)
}

const handleSelectActivity = (newValue: Activity | undefined):void => {
    activity.value = newValue
    editMode.value = false
}
const handleCreateActivity = ():void => {
    editMode.value = true
    activity.value = undefined
}

</script>

<style scoped>
</style>
