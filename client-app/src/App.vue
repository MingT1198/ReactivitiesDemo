
<template>
  <n-layout>
    <n-config-provider :theme="themeRef" :locale="zhTW" :date-locale="dateZhTW">
      <n-layout>
        <n-layout-header bordered style="height: 8vh;">
          <n-flex justify="space-around" style="height: 100%; padding: 0px 15px;">
            <n-flex justify="start" style="height: 100%; flex: 3; align-items: center;">
              <div v-if="!isMobileComputed" >
                <n-menu :options="menuOptions" mode="horizontal" />
              </div>
              <div v-else >
                <n-button text @click="() => showDrawerRef=!showDrawerRef">
                  <template #icon>
                    <n-icon>
                      <MenuIcon />
                    </n-icon>
                  </template>
                </n-button>
                <n-drawer v-model:show="showDrawerRef" placement="left">
                  <n-drawer-content closable>
                    <n-menu :options="menuOptions" />
                  </n-drawer-content>
                </n-drawer>
              </div>
            </n-flex>
            <n-flex justify="end" style="height: 100%; flex: 1; align-items: center;">
              <n-button text @click="changeThemeAction">
                <template #icon>
                  <n-icon>
                    <DarkModeFilled v-show="!isDarkThemeRef" />
                    <DarkModeOutlined v-show="isDarkThemeRef" />
                  </n-icon>
                </template>
              </n-button>
            </n-flex>
          </n-flex>
        </n-layout-header>
        <n-layout has-sider :style="contentHeight">
          <n-layout-content style="width: 100vw;">
              <RouterView />
          </n-layout-content>
        </n-layout>
        <n-layout-footer bordered style="height: 6vh;">
          <n-flex justify="end">
            © MingT 2025
          </n-flex>
        </n-layout-footer>
      </n-layout>
    </n-config-provider>
  </n-layout>
</template>

<script setup lang="ts">
import type {GlobalTheme, MenuOption, ConfigProviderProps } from 'naive-ui'
import type { Ref, ComputedRef } from 'vue'

import { Menu as MenuIcon } from '@vicons/ionicons5'

import { DarkModeFilled, DarkModeOutlined } from '@vicons/material'
import { zhTW, dateZhTW } from 'naive-ui'
import { NLayout,
  NLayoutHeader,
  NLayoutContent,
  NLayoutFooter,
  NMenu,
  NConfigProvider,
  NFlex,
  NIcon,
  NDrawer,
  NDrawerContent,
  NButton,
  lightTheme,
  darkTheme,
  createDiscreteApi,
} from 'naive-ui'

import { routerHelper } from './router'
import { RouterView } from 'vue-router'
import { ref, computed, reactive, provide, onMounted, onUnmounted } from 'vue'

const menuOptions: MenuOption[] = routerHelper.getMenu();
const isDarkThemeRef: Ref<boolean> = ref(false);
const themeRef: Ref<GlobalTheme> = ref(lightTheme);
const windowWidthRef: Ref<number> = ref(window.innerWidth);
const showDrawerRef: Ref<boolean> = ref(false);

const configProviderPropsComputed: ComputedRef<ConfigProviderProps> = computed(() => ({
  theme: themeRef.value = isDarkThemeRef.value ? darkTheme : lightTheme
}));
const isMobileComputed: ComputedRef<boolean> = computed(() => windowWidthRef.value < 768);
const contentHeight: ComputedRef<object> = computed(() => isMobileComputed.value ? { height: '70vh'} : {height:'85vh'});

// 監聽視窗大小變化
onMounted(() => {
  window.addEventListener('resize', () => {
    windowWidthRef.value = window.innerWidth
  });
});
onUnmounted(() => {
  window.removeEventListener('resize', () => {
    windowWidthRef.value = window.innerWidth
  });
});

//建立脫離n-xxx-provider的物件
const { message, notification, loadingBar, modal } = createDiscreteApi(
  ['message', 'notification', 'loadingBar', 'modal'],
  {
    configProviderProps: configProviderPropsComputed
  }
);
// 創建響應式全域物件
const globalComponents = reactive({
    message, notification, loadingBar, modal
});
// 提供全域物件
provide('globalComponents', globalComponents);
provide('isMobile', isMobileComputed);

const changeThemeAction = (): void => {
  isDarkThemeRef.value = !isDarkThemeRef.value;
};

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
