<template>
    <Md v-model="content"></Md>

    <div id="BlogButton">
        <a-button shape="round" type="primary" @click="showDrawer">提交</a-button>
        <a-button>草稿</a-button>
    </div>



    <a-drawer v-model:visible="visible" class="custom-class" title="博文信息" placement="right"
        @after-visible-change="afterVisibleChange">
        <a-form :model="formState" v-bind="layout" name="nest-messages" :validate-messages="validateMessages"
            @finish="onFinish">
            <a-form-item :name="['Article', 'Title']" label="标题" :rules="[{ required: true }]">
                <a-input v-model:value="formState.user.name" />
            </a-form-item>
            <a-form-item :name="['Article', 'Tags']" label="Email" :rules="[{ type: 'email' }]">
                <a-input v-model:value="formState.user.email" />
            </a-form-item>
            <a-form-item :name="['user', 'age']" label="Age" :rules="[{ type: 'number', min: 0, max: 99 }]">
                <a-input-number v-model:value="formState.user.age" />
            </a-form-item>
            <a-form-item :name="['user', 'website']" label="Website">
                <a-input v-model:value="formState.user.website" />
            </a-form-item>
            <a-form-item :name="['user', 'introduction']" label="Introduction">
                <a-textarea v-model:value="formState.user.introduction" />
            </a-form-item>
            <a-form-item :wrapper-col="{ ...layout.wrapperCol, offset: 8 }">
                <a-button type="primary" html-type="submit">Submit</a-button>
            </a-form-item>
        </a-form>
    </a-drawer>
</template>


<script setup lang='ts'>
import { ref, reactive } from 'vue'
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'
//#region  markdown
const content = ref<string>('');
//#endregion
//#region 右侧抽屉
const visible = ref<boolean>(false);

const afterVisibleChange = (bool: boolean) => {
    console.log('visible', bool);
};

const showDrawer = () => {
    visible.value = true;
};
//#endregion

//#region 抽屉内表单
const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};

const validateMessages = {
    required: '${label} is required!',
    types: {
        email: '${label} is not a valid email!',
        number: '${label} is not a valid number!',
    },
    number: {
        range: '${label} must be between ${min} and ${max}',
    },
};

const formState = reactive({
    Article: {
        Title: '',
        Tags: undefined,
        email: '',
        website: '',
        introduction: '',
    },
});
const onFinish = (values: any) => {
    console.log('Success:', values);
};
//#endregion
</script>

<style>
#BlogButton {
    height: 80px;
    display: flex;
    justify-content: right;
    align-items: center;
    gap: 20px;
}
</style>