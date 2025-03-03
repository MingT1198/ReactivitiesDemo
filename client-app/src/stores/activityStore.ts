import type { Activity } from '@/models/Activity'
import type { Ref } from 'vue'

import { v4 as uuidv4 } from 'uuid'
import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useActivityStore = defineStore('activity', () => {
  const activities:Ref<Activity[] | null> = ref([
      {id:"0cefba23-ed58-48ee-81d4-e51888e3e761",title:"Past Activity 2",date:new Date("2024-10-08T05:36:56"),description:"Activity 1 month ago",city:"Paris",venue:"Louvre"},
      {id:"88f615b1-b136-4286-ab10-674f100b5b30",title:"Future Activity 1",date:new Date("2024-12-08T05:36:56"),description:"Activity 1 month in future",city:"London",venue:"Natural History Museum"},
      {id:"b12f67c3-63e7-4613-8d15-83630536475c",title:"Past Activity 1",date:new Date("2024-09-08T05:36:56"),description:"Activity 2 months ago",city:"Lonon",venue:"Pub"}
    ] as Activity[]
  )

  const setActivity = (activityData: Activity):void => {
    if (activities.value === null) {
      activities.value = [] as Activity[]
    }

    activityData.date = new Date()

    if (activities.value.find(a => a.id === activityData.id)) {
      for (let i = 0; i < activities.value.length; i++) {
        if (activities.value[i].id === activityData.id) {
          activities.value[i] = activityData
        }
      }
    } else {
      activityData.id = uuidv4()
      activities.value.push(activityData)
    }
  }

  return {
    activities,
    setActivity
  }
})



