
<template>
  <n-layout>
    <n-config-provider :theme="themeRef" :locale="zhTW" :date-locale="dateZhTW">
      <n-layout>
        <n-layout-header bordered style="height: 5vh;">
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
        <n-layout has-sider style="height: 85vh;">
          <n-layout-sider
            collapse-mode="width"
            :collapsed-width="16"
            :max-width="150"
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
        <n-layout-footer bordered style="height: 6vh;">
          © MingT 2025
        </n-layout-footer>
      </n-layout>
    </n-config-provider>
  </n-layout>
</template>

<script setup lang="ts">
import type {GlobalTheme, MenuOption, ConfigProviderProps } from 'naive-ui'
import type { Ref } from 'vue'

import { DarkModeFilled, DarkModeOutlined } from '@vicons/material'
import { Home, HomeOutline } from '@vicons/ionicons5'
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
  lightTheme,
  darkTheme,
  createDiscreteApi,
} from 'naive-ui'

import { routerHelper } from './router'
import { RouterView, useRouter } from 'vue-router'
import { ref, computed, reactive, provide } from 'vue'

const router = useRouter();
const menuOptions: MenuOption[] = routerHelper.getMenu();
const isDarkThemeRef: Ref<boolean> = ref(false);
const themeRef: Ref<GlobalTheme> = ref(lightTheme);

const configProviderPropsComputed = computed<ConfigProviderProps>(() => ({
  theme: themeRef.value = isDarkThemeRef.value ? darkTheme : lightTheme
}));

//建立脫離n-xxx-provider的物件
const { message, notification, dialog, loadingBar, modal } = createDiscreteApi(
  ['message', 'dialog', 'notification', 'loadingBar', 'modal'],
  {
    configProviderProps: configProviderPropsComputed
  }
);
// 創建響應式全域物件
const globalComponents = reactive({
    message, notification, dialog, loadingBar, modal
});
// 提供全域物件
provide('globalComponents', globalComponents)

const changeThemeAction = (): void => {
  isDarkThemeRef.value = !isDarkThemeRef.value;
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
