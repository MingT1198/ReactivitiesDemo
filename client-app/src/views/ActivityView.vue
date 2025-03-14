<template>
    <n-flex vertical style="max-height: 80vh;" >
        <n-button ghost size="small" type="primary" style="flex-grow:1; max-width: 50px;" @click="create">
            <template #icon><n-icon><Create /></n-icon></template>
        </n-button>
        <n-flex style="flex-grow:4; justify-content: center;">
            <activity-list style="flex: 3;" />

            <n-flex vertical style="flex: 2; padding: 5px;" v-if="!isMobile">
                
            </n-flex>

            <n-flex vertical style="flex: 2;" v-else-if="isMobile">
                <n-modal>
                    
                </n-modal>
                <n-drawer :default-width="'90%'" placement="right">
                    <n-drawer-content>
                      
                  </n-drawer-content>
                </n-drawer>
            </n-flex>
        </n-flex>
    </n-flex>
</template>

<script setup lang="ts">
import type { Ref } from 'vue'

import { Create } from '@vicons/ionicons5'

import { NFlex, NButton, NIcon, NDrawer, NDrawerContent, NModal } from 'naive-ui'
import { ref, inject } from 'vue'
import { useRouter } from 'vue-router'

import ActivityList from '@/components/activity/ActivityList.vue'

const isMobile: Ref<boolean> = inject('isMobile', ref(false))
const router = useRouter()
const create = () => router.push({name:'submit'})

</script>

<style scoped>
</style>
