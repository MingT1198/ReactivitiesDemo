import type { Activity } from '@/models/Activity'
import type { Ref, ComputedRef } from 'vue'
import type { LoadingBarApi } from 'naive-ui'

import { defineStore } from 'pinia'
import {ref, computed, inject } from 'vue'
import { v4 as uuidv4 } from 'uuid'

import agent from '@/api/agent'

export const useActivityStore = defineStore('activity', () => {
  //state
  const activities: Ref<Map<string, Activity>> = ref(new Map())
  const activity: Ref<Activity | undefined> = ref()
  const editMode: Ref<boolean> = ref(false)
  const isSubmit: Ref<boolean> = ref(false)
  
  //getter
  const getActivitys: ComputedRef<[key: string, Activity[]][]> = computed(() => {
    return Object.entries( Array.from(activities.value.values()).reduce(
      (temp, data) => {
        const date = data.date!
        temp[date] = temp[date] ? [...temp[date], data] : [data]
        return temp
      },
      {} as {[key: string]: Activity[]})
    )
  })
  const getActivity: ComputedRef<Activity | undefined> = computed(() => activity.value)
  const getEditMode: ComputedRef<boolean> = computed(() => editMode.value)
  
  //action
  const loadingBar: LoadingBarApi = (inject<any>('globalComponents')).loadingBar
  const getList = () =>{
    loadingBar.start()
    agent.Activities.list().then( res => {
      res.forEach((a) => activities.value.set(a.id, a))
      loadingBar.finish()
    })
  }
  const selectActivity = (id: string) => {
    loadingBar.start()
    agent.Activities.detail(id).then( res => {
      activity.value = res
      loadingBar.finish()
      editMode.value = false
    })
  }
  const submitModeActivity = (submitValue: Activity) => {
      isSubmit.value = true
      if(submitValue.id){
        agent.Activities.update(submitValue.id, submitValue).then(()=>{
          activities.value.set(submitValue.id, submitValue)
          isSubmit.value = false
        })
      }else{
        submitValue.id = uuidv4()
        agent.Activities.create(submitValue).then(() => {
          activities.value.set(submitValue.id, submitValue)
          isSubmit.value = false
        })
      }
  }
  const deleteModeActivity = (id: string) => {
      isSubmit.value = true
      agent.Activities.delete(id).then(() => {
        activities.value.delete(id)
      })
  }

  return {
    getActivitys,
    getActivity,
    getEditMode,
    getList,
    selectActivity,
    submitModeActivity,
    deleteModeActivity,
  }
})



