import type {MenuOption } from 'naive-ui'

import { Home } from '@vicons/ionicons5'

import { NIcon } from 'naive-ui'
import { h } from 'vue'
import { createRouter, createWebHistory, RouterLink } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/activity',
      name: 'activity',
      component: () => import('../views/ActivityView.vue'),
    },
  ],
})

export const routerHelper = 
{
  getMenu: ():MenuOption[] => {
    let menu:MenuOption[] = router.options.routes
    // .filter(r => r.name != "home")
    .map<MenuOption>(r => {
      const name:string = r.name as string;

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
