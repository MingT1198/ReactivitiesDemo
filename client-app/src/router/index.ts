import type {MenuOption } from 'naive-ui'

import { Home } from '@vicons/ionicons5'

import { NIcon } from 'naive-ui'
import { h } from 'vue'
import { createRouter, createWebHistory, RouterLink } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('@/views/HomeView.vue'),
    },
    {
      path: '/activity',
      children: [
        {
          path: '',
          name: 'activity',
          component: () => import('@/views/ActivityView.vue'),
        },
        {
          path: ':id',
          name: 'activityDital',
          component: () => import('@/components/activity/ActivityDital.vue'),
        },
        {
          path: 'submit/:id?',
          name: 'activitySubmit',
          component: () => import('@/components/activity/ActivityForm.vue'),
        },
      ]
    },
  ],
})

export const routerHelper = 
{
  getMenu: ():MenuOption[] => {
    let menu:MenuOption[] = router.options.routes
    // .filter(r => r.name != "home")
    .map<MenuOption>(r => {
      const name:string = r.name ? r.name as string : r.children![0].name as string;

      if (name.toUpperCase() == "home".toUpperCase()) {
        return {
          label: () =>
          h(
            RouterLink,
            {
              to: {
                name: name,
              }
            },
          ),
          key: name,
          icon: () => h(NIcon, null, { default: () => h(Home) })
        } 
      }
      else {
        return {
          label: () =>
          h(
            RouterLink,
            {
              to: {
                name: name,
              }
            },
            { default: () => name.toUpperCase() }
          ),
          key: name,
        }
      }
    });

    return menu;
  }
}

export default router
