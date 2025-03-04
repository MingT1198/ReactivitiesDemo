<template>
    <MdPreview :id="id" :modelValue="content" />
    <MdCatalog :editorId="id" :scrollElement="scrollElement" />
</template>

<script setup lang="ts">
import type { Ref } from 'vue'

import { ref } from 'vue'
import { MdPreview, MdCatalog } from 'md-editor-v3'
import 'md-editor-v3/lib/preview.css'

const id = 'preview-only';
const scrollElement = document.documentElement;
const content: Ref<string> = ref(`
# 技術說明

## 前端
- **框架**: Vue 3 (JavaScript)
- **UI 元件庫**: Naive UI
- **MarkDown**: md-editor-v3

## 資料庫
- **DB**: SQLite

## 後端
- **框架**: .NET 8

## 引用元件
- Vue 3 - [官方網站](https://vuejs.org/)
- Naive UI - [官方網站](https://www.naiveui.com/)
- SQLite - [官方網站](https://www.sqlite.org/)
- .NET 8 - [官方網站](https://dotnet.microsoft.com/)
- md-editor-v3 - [官方網站](https://imzbf.github.io/md-editor-v3/zh-CN)
`)
</script>