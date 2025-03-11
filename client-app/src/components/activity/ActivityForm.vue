<template>
    <n-form
        ref="formRef"
        label-width="auto"
        :model="formValueRef"
        :rules="rules"
    >
        <n-form-item label="title" path="title">
            <n-input v-model:value="formValueRef.title" placeholder="title" />
        </n-form-item>
        <n-form-item label="description" path="description">
            <n-input type="textarea" v-model:value="formValueRef.description" placeholder="description" />
        </n-form-item>
        <!-- <n-form-item label="category" path="category">
            <n-input v-model:value="formValueRef.category" placeholder="category" />
        </n-form-item> -->
        <n-form-item label="date" path="date">
            <n-date-picker
                v-model:formatted-value="dateComput"
                value-format="yyyy-MM-dd"
                type="date"
            />
        </n-form-item>
        <n-form-item label="city" path="city">
            <n-input v-model:value="formValueRef.city" placeholder="city" />
        </n-form-item>
        <n-form-item label="venue" path="venue">
            <n-input v-model:value="formValueRef.venue" placeholder="venue" />
        </n-form-item>
        <n-form-item>
            <n-button :loading="isSubmit" type="primary" strong ghost @click="handleValidataClick">Submit</n-button>
            <n-button strong ghost @click="cancelModeActivity">Cancel</n-button>
        </n-form-item>
    </n-form>
</template>

<script setup lang="ts">
import type { FormInst, MessageApi, FormRules, FormItemRule } from 'naive-ui'
import type { Ref, PropType, ComputedRef } from 'vue'
import type { Activity } from '@/models/Activity'

import { NForm, NFormItem, NInput, NButton, NDatePicker } from 'naive-ui'
import { ref, inject, computed } from 'vue'
import moment from 'moment'

const { activity, isSubmit } = defineProps({
    activity: {
        type: Object as PropType<Activity> | PropType<undefined>,
        required: true,
    },
    isSubmit: {
        type: Boolean,
        required: true,
    }
})

const message: MessageApi = (inject<any>('globalComponents')).message

const formRef: Ref<FormInst | null> = ref(null)
const formValueRef: Ref<Activity> = ref(activity ? { ...activity } : {} as Activity)
const rules:FormRules = {
    title: {
        required: true,
        message: 'title',
        trigger: ['input']
    },
    description: {
        message: 'description',
        trigger: ['input']
    },
    category: {
        message: 'category',
        trigger: ['input']
    },
    date: {
        required: true,
        message: "date",
        trigger: ['blur', 'change'],
    },
    city: {
        message: 'city',
        trigger: ['input']
    },
    venue: {
        message: 'venue',
        trigger: ['input']
    },
}

const dateComput: ComputedRef<string | null> = computed({
    get() {
        return formValueRef.value.date ? moment(formValueRef.value.date).format("YYYY-MM-DD") : null
    },
    set(value: string | null){
        formValueRef.value.date = value ? new Date(value).toISOString() : null
    }
})

const emit = defineEmits<{
    (e: 'cancelModeActivity'): void;
    (e: 'submitModeActivity', submitValue: Activity): void;
}>()
const handleValidataClick = (e: MouseEvent) => {
    e.preventDefault()
    formRef.value?.validate((errors) => {
        if (!errors) {
            emit('submitModeActivity', formValueRef.value)
        }
        else {
            message.error(`${errors[0][0].message}`)
        }
    })
}
const cancelModeActivity = () => {
    emit('cancelModeActivity')
}
</script>