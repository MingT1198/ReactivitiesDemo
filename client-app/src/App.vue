
<template>
  <n-layout>
    <n-config-provider :theme="themeRef" :locale="zhTW" :date-locale="dateZhTW">
      <n-layout>
        <n-layout-header bordered style="height: 8vh;">
          <n-flex justify="space-between" style="padding: 0px 15px;">
            <n-flex justify="start" style="flex: 3; align-items: center;">
              <div v-if="!isMobileComputed">
                <n-menu :options="menuOptions" mode="horizontal" />
              </div>
              <div v-else>
                <n-button text @click="() => showModalRef=!showModalRef">
                  <template #icon>
                    <MenuIcon />
                  </template>
                </n-button>
                <n-drawer v-model:show="showModalRef" placement="left">
                  <n-drawer-content title="">
                    <n-menu :options="menuOptions" />
                  </n-drawer-content>
                </n-drawer>
              </div>
            </n-flex>
            <n-flex justify="end" style="flex: 1; align-items: center;">
              <n-button text @click="changeThemeAction">
                <template #icon>
                  <DarkModeFilled v-show="!isDarkThemeRef" />
                  <DarkModeOutlined v-show="isDarkThemeRef" />
                </template>
              </n-button>
            </n-flex>
          </n-flex>
        </n-layout-header>
        <n-layout has-sider style="height: 85vh;">
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
  NDrawer,
  NDrawerContent,
  NButton,
  lightTheme,
  darkTheme,
  createDiscreteApi,
} from 'naive-ui'

import { routerHelper } from './router'
import { RouterView } from 'vue-router'
import { ref, computed, reactive, provide } from 'vue'

const menuOptions: MenuOption[] = routerHelper.getMenu();
const isDarkThemeRef: Ref<boolean> = ref(false);
const themeRef: Ref<GlobalTheme> = ref(lightTheme);
const windowWidthRef: Ref<number> = ref(window.innerWidth);
const showModalRef: Ref<boolean> = ref(false);

const configProviderPropsComputed = computed<ConfigProviderProps>(() => ({
  theme: themeRef.value = isDarkThemeRef.value ? darkTheme : lightTheme
}));
const isMobileComputed = computed<boolean>(() => windowWidthRef.value < 768);

window.addEventListener('resize', () => {
  windowWidthRef.value = window.innerWidth
})

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
