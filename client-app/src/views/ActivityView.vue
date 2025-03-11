<template>
    <n-flex vertical style="max-height: 80vh;">
        <n-button ghost size="small" type="primary" style="flex-grow:1; max-width: 50px;" @click="handleCreateActivity">
            <template #icon><n-icon><Create /></n-icon></template>
        </n-button>
        <n-flex style="flex-grow:4; justify-content: center;">
            <activity-list style="flex: 3;" :activitys="activitys" @select-activity="handleSelectActivity" @delete-activity="handleDeleteModeActivity" v-if="activitys"/>

            <n-flex vertical style="flex: 2; padding: 5px;" v-if="(activity || editMode) && !isMobile">
                <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity" v-if="activity && !editMode"/>
                <activity-form :activity="activity" :isSubmit="isSubmit" @cancel-mode-activity="handleCancelModeActivity" @submit-mode-activity="handleSubmitModeActivity" v-if="editMode"/>
            </n-flex>

            <n-flex vertical style="flex: 2;" v-else-if="(activity || editMode) && isMobile">
                <n-modal :show="(activity !== undefined)" >
                    <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity"/>
                </n-modal>
                <n-drawer :default-width="'90%'" :show="editMode" placement="right">
                    <n-drawer-content>
                      <activity-form :activity="activity" :isSubmit="isSubmit" @cancel-mode-activity="handleCancelModeActivity" @submit-mode-activity="handleSubmitModeActivity"/>
                  </n-drawer-content>
                </n-drawer>
            </n-flex>
        </n-flex>
    </n-flex>
</template>

<script setup lang="ts">
import type { Activity } from '@/models/Activity'
import type { Ref } from 'vue'
import type { LoadingBarApi } from 'naive-ui'

import { Create } from '@vicons/ionicons5'

import { v4 as uuidv4 } from 'uuid'
import { NFlex, NButton, NIcon, NDrawer, NDrawerContent, NModal } from 'naive-ui'
import { ref, inject } from 'vue'
import agent from '@/api/agent'

import ActivityList from '@/components/activity/ActivityList.vue'
import ActivityDital from '@/components/activity/ActivityDital.vue'
import ActivityForm from '@/components/activity/ActivityForm.vue'

const activitys: Ref<Activity[]> = ref([]);
const activity: Ref<Activity | undefined> = ref()
const editMode: Ref<boolean> = ref(false)
const isSubmit: Ref<boolean> = ref(false)
const isMobile: Ref<boolean> = inject('isMobile', ref(false))
const loadingBar: LoadingBarApi = (inject<any>('globalComponents')).loadingBar

const handleCancelActivity = () => { activity.value = undefined }
const handleEditModeActivity = () => { editMode.value = true }
const handleCancelModeActivity = () => {
    editMode.value = false
    activity.value = undefined

}
const handleSelectActivity = (id: string) => {
    agent.Activities.detail(id).then( res => {
        activity.value = {...res }
        editMode.value = false
    })
}
const handleCreateActivity = () => {
    editMode.value = true
    activity.value = undefined
}
const handleSubmitModeActivity = (submitValue: Activity) => {
    isSubmit.value = true
    if(submitValue.id){
        agent.Activities.update(submitValue.id, submitValue).then(()=>{
            activitys.value = [submitValue, ...activitys.value.filter(a => a.id !== submitValue.id)]
            submitFinishAndSetActivity(submitValue)
        })
    }else{
        submitValue.id = uuidv4()
        agent.Activities.create(submitValue).then(() => {
            activitys.value.unshift(submitValue)
            submitFinishAndSetActivity(submitValue)
        })
    }
}
const handleDeleteModeActivity = (id: string) => {
    agent.Activities.delete(id).then(() => {
        const index = activitys.value.findIndex(a => a.id === id)
        activitys.value = activitys.value.filter((a)=> a.id !== id)
        if(activity.value && activity.value.id === id){
            activity.value = undefined
        }
    })
}

const submitFinishAndSetActivity = (value: Activity | undefined) => {
    editMode.value = false
    activity.value = value
    isSubmit.value = false
}

//init
(
    () => {
        loadingBar.start()
        agent.Activities.list().then( res => {
            activitys.value = res
            loadingBar.finish()
        })
    }
)()


</script>

<style scoped>
</style>
