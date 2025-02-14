<template>
    <n-flex vertical style="max-height: 80vh;">
        <n-button ghost size="small" type="primary" style="flex-grow:1; max-width: 50px;" @click="handleCreateActivity">
            <template #icon><Create /></template>
        </n-button>
        <n-flex style="flex-grow:4; flex-wrap: nowrap; justify-content: center;">
            <activity-list :activitys="activitys" @select-activity="handleSelectActivity" v-if="activitys"/>
            <n-flex vertical style="max-width:35%; margin: 2px 15px 2px 5px;">
                <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity" v-if="activity && !(editMode)"/>
                <activity-form :activity="activity" @cancel-mode-activity="handleCancelModeActivity" v-if="editMode"/>
            </n-flex>
        </n-flex>
    </n-flex>
</template>

<script lang="ts">
import type { Activity } from '@/models/Activity';

import { Create } from '@vicons/ionicons5';

import type { Ref } from 'vue';
import { NFlex, NButton } from 'naive-ui';

import ActivityList from '@/components/activity/ActivityList.vue';
import ActivityDital from '@/components/activity/ActivityDital.vue';
import ActivityForm from '@/components/activity/ActivityForm.vue';

import axios from 'axios';
import { defineComponent, ref } from 'vue';

export default defineComponent({ 
    components: {
        ActivityList,
        ActivityDital,
        ActivityForm,
        NFlex,
        NButton,
        Create
    },
    setup() {
        const activitysRef: Ref<Activity[]| null> = ref(null);
        const activityRef: Ref<Activity | undefined> = ref(undefined);
        const editModeRef: Ref<boolean> = ref(false);

        const getActivitys = async () => {
            await axios.get<Activity[]>('/api/activity').then( res => { activitysRef.value = res.data; });
        }

        // Fetch activitys
        (
            async()=>{
                await getActivitys();
            }
        )();

        return {
            activitys: activitysRef,
            activity: activityRef,
            editMode: editModeRef,
            handleCancelActivity: ():void => { activityRef.value = undefined; },
            handleEditModeActivity: ():void => { editModeRef.value = true; },
            handleCancelModeActivity: ():void => {
                editModeRef.value = false;
                activityRef.value = undefined;
            },
            handleSelectActivity: (newValue: Activity | undefined):void => {
                activityRef.value = newValue;
                editModeRef.value = false;
            },
            handleCreateActivity: ():void => {
                editModeRef.value = true;
                activityRef.value = undefined;
            },
        };
    },
});
</script>

<style>
</style>
