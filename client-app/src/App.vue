
<template>
  <n-layout>
    <n-config-provider :theme="themeRef" :locale="zhTW" :date-locale="dateZhTW">
      <n-layout>
        <n-layout-header bordered style="height: 10vh;">
          <n-flex justify="space-between" style="padding: 0px 15px;">
            <h1>頁首</h1>
            <n-button text @click="changeThemeAction">
              <template #icon>
                <DarkModeFilled v-show="!isDarkThemeRef" />
                <DarkModeOutlined v-show="isDarkThemeRef" />
              </template>
            </n-button>
          </n-flex>
        </n-layout-header>
        <n-layout has-sider style="height: 80vh;">
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
import { DarkModeFilled, DarkModeOutlined } from '@vicons/material';
import type {GlobalTheme, MenuOption } from 'naive-ui'
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

import { RouterLink, RouterView } from 'vue-router'
import type { Ref } from 'vue'
import { h, ref } from 'vue'

const menuOptions: MenuOption[] = [
  {
    label: () =>
      h(
        RouterLink,
        {
          to: {
            name: 'home',
          }
        },
        { default: () => '首頁' }
      ),
    key: 'home',
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: {
            name: 'about',
          }
        },
        { default: () => '關於' }
      ),
    key: 'about',
  }
];
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
