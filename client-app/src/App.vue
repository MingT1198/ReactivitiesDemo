
<template>
  <n-layout>
    <n-config-provider :theme="themeRef" :locale="zhTW" :date-locale="dateZhTW">
      <n-layout>
        <n-layout-header bordered style="height: 7vh;">
          <n-flex justify="space-between" style="padding: 0px 15px;">
            <n-button quaternary circle @click="router.push({name: 'home'})">
              <template #icon>
                <Home v-show="!isDarkThemeRef" />
                <HomeOutline v-show="isDarkThemeRef" />
              </template>
            </n-button>
            <n-button text @click="changeThemeAction">
              <template #icon>
                <DarkModeFilled v-show="!isDarkThemeRef" />
                <DarkModeOutlined v-show="isDarkThemeRef" />
              </template>
            </n-button>
          </n-flex>
        </n-layout-header>
        <n-layout has-sider style="height: 83vh;">
          <n-layout-sider
            collapse-mode="width"
            :collapsed-width="16"
            :width="200"
            :show-collapsed-content="false"
            show-trigger="arrow-circle"
            bordered
          >
            <n-menu :options="menuOptions" />
          </n-layout-sider>
          <n-layout-content>
            <RouterView />
          </n-layout-content>
        </n-layout>
        <n-layout-footer bordered style="height: 10vh;">
          頁腳
        </n-layout-footer>
      </n-layout>
    </n-config-provider>
  </n-layout>
</template>

<script setup lang="ts">
import type {GlobalTheme, MenuOption } from 'naive-ui'
import type { Ref } from 'vue'

import { DarkModeFilled, DarkModeOutlined } from '@vicons/material';
import { Home, HomeOutline } from '@vicons/ionicons5';
import { zhTW, dateZhTW } from 'naive-ui'
import { NLayout,
  NLayoutSider,
  NLayoutHeader,
  NLayoutContent,
  NLayoutFooter,
  NMenu,
  NConfigProvider,
  NFlex,
  NButton,
  darkTheme
} from 'naive-ui'

import { routerHelper } from './router';
import { RouterLink, RouterView, useRouter } from 'vue-router'
import { h, ref } from 'vue'

const router = useRouter();
const menuOptions: MenuOption[] = routerHelper.getMenu();
const isDarkThemeRef: Ref<boolean> = ref(false);
const themeRef: Ref<GlobalTheme | null> = ref(null);

const changeThemeAction = (): void => {
  isDarkThemeRef.value = !isDarkThemeRef.value;
  themeRef.value = isDarkThemeRef.value ? darkTheme : null;
}
</script>

<style scoped>
.n-layout-footer{
  padding: 24px;
}
.n-layout-content {
  padding: 12px;
}

.n-layout-sider {
  padding: 16px;
}

</style>
