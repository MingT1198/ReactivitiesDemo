<template>
    <n-flex vertical style="max-height: 80vh;">
        <n-button ghost size="small" type="primary" style="flex-grow:1; max-width: 50px;" @click="handleCreateActivity">
            <template #icon><Create /></template>
        </n-button>
        <n-flex style="flex-grow:4; justify-content: center;">
            <activity-list style="flex: 3;" :activitys="activitys" @select-activity="handleSelectActivity" v-if="activitys"/>

            <n-flex vertical style="flex: 2; padding: 5px;" v-if="(activity || editMode) && !isMobile">
                <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity" v-if="activity && !editMode"/>
                <activity-form :activity="activity" @cancel-mode-activity="handleCancelModeActivity" v-if="editMode"/>
            </n-flex>

            <n-flex vertical style="flex: 2;" v-else-if="(activity || editMode) && isMobile">
                <n-modal :show="(activity !== undefined)" >
                    <activity-dital :activity="activity" @cancel-activity="handleCancelActivity" @edit-mode-activity="handleEditModeActivity"/>
                </n-modal>
                <n-drawer :default-width="'90%'" :show="editMode" placement="right">
                    <n-drawer-content>
                      <activity-form :activity="activity" @cancel-mode-activity="handleCancelModeActivity"/>
                  </n-drawer-content>
                </n-drawer>
            </n-flex>
        </n-flex>
    </n-flex>
</template>

<script lang="ts">
import type { Activity } from '@/models/Activity';
import type { Ref, Reactive, ComputedRef } from 'vue';

import { Create } from '@vicons/ionicons5';

import { NFlex, NButton, NDrawer, NDrawerContent, NModal } from 'naive-ui';

import ActivityList from '@/components/activity/ActivityList.vue';
import ActivityDital from '@/components/activity/ActivityDital.vue';
import ActivityForm from '@/components/activity/ActivityForm.vue';

import axios from 'axios';
import { defineComponent, ref, inject } from 'vue';

export default defineComponent({ 
    components: {
        ActivityList,
        ActivityDital,
        ActivityForm,
        NFlex,
        NButton,
        Create,
        NDrawer,
        NDrawerContent,
        NModal
    },
    setup() {
        const activitysRef: Ref<Activity[]| null> = ref(null);
        const activityRef: Ref<Activity | undefined> = ref(undefined);
        const editModeRef: Ref<boolean> = ref(false);

        const isMobile = inject<ComputedRef<boolean>>('isMobile');

        const getActivitys = async () => {
            await axios.get<Activity[]>('/api/activity').then( res => {
                activitysRef.value = res.data.map( a => {
                    a.date = a.date ? new Date(a.date) : undefined;
                    return a;
                });
            });
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
            isMobile,
            handleCancelActivity: ():void => { activityRef.value = undefined; },
            handleEditModeActivity: ():void => { editModeRef.value = true; },
            handleCancelModeActivity: ():void => {
                editModeRef.value = false;
                // activityRef.value = undefined;
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

<style scoped>
</style>
