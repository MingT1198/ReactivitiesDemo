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
        <n-form-item label="city" path="city">
            <n-input v-model:value="formValueRef.city" placeholder="city" />
        </n-form-item>
        <n-form-item label="venue" path="venue">
            <n-input v-model:value="formValueRef.venue" placeholder="venue" />
        </n-form-item>
        <n-form-item>
            <n-button type="primary" strong ghost @click="handleValidateClick">Submit</n-button>
            <n-button strong ghost @click="cancelModeActivity">Cancel</n-button>
        </n-form-item>
    </n-form>
</template>

<script setup lang="ts">
    import type { FormInst } from 'naive-ui'
    import type { Reactive, Ref, PropType } from 'vue'
    import type { Activity } from '@/models/Activity'

    import { NForm, NFormItem, NInput, NButton } from 'naive-ui'
    import { ref, watch, inject } from 'vue'
    
    const prop = defineProps({
        activity: {
            type: Object as PropType<Activity> | PropType<undefined>,
            required: true,
        }
    })

    const globalComponents:Reactive<any> = inject('globalComponents')

    const formRef: Ref<FormInst | null> = ref(null)
    const formValueRef: Ref<Activity> = ref({} as Activity)
    const rules = {
        title: {
            required: true,
            message: 'title',
            trigger: ['input']
        },
        description: {
            // required: true,
            message: 'description',
            trigger: ['input']
        },
        category: {
            // required: true,
            message: 'category',
            trigger: ['input']
        },
        city: {
            // required: true,
            message: 'city',
            trigger: ['input']
        },
        venue: {
            // required: true,
            message: 'venue',
            trigger: ['input']
        },
    }

    watch(() => prop.activity, (newValue) => {
        formValueRef.value = newValue ? {...newValue } : {} as Activity
    }, { immediate: true })

    const emit = defineEmits<{
        (e: 'cancelModeActivity'): void;
        (e: 'submitModeActivity', submitValue: Activity): void;
    }>()
    const handleValidateClick = (e: MouseEvent) => {
        e.preventDefault()
        formRef.value?.validate((errors) => {
        if (!errors) {
            emit('submitModeActivity', formValueRef.value)
        }
        else {
            globalComponents.message.error(errors[0][0].message)
        }
        })
    }
    const cancelModeActivity = () => {
        emit('cancelModeActivity')
    }
</script>