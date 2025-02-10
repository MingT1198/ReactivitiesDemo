import { defineStore } from 'pinia'

export const useCounterStore = defineStore('global', {
  //state: 存放全域狀態變數（類似 Vuex 的 state）
  state: () => ({
    theme: 'light', // 主題模式
    user: null,     // 使用者資訊
    apiBaseUrl: 'https://api.example.com'
  }),
  //actions: 定義可修改 state 的函式（類似 Vuex 的 mutations）
  actions: {
    toggleTheme() {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    },
    setUser(userData: any) {
      this.user = userData;
    }
  },
  //getters: 讀取 state 的計算屬性（類似 Vuex 的 getters）
  getters: {
    isDarkMode: (state) => state.theme === 'dark'
  }
})



