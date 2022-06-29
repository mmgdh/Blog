<template>
    <Md v-model="content"></Md>

    <div id="BlogButton">
        <a-button shape="round" type="primary" @click="showDrawer">提交</a-button>
        <a-button>草稿</a-button>
    </div>



    <a-drawer v-model:visible="visible" class="custom-class" title="博文信息" placement="right"
        @after-visible-change="afterVisibleChange">

        <a-form :model="SubmitArticle" v-bind="layout" name="nest-messages" :validate-messages="validateMessages"
            @finish="onFinish">
            <a-form-item name="Title" label="标题" :rules="[{ required: true }]">
                <a-input v-model:value="SubmitArticle.Title" />
            </a-form-item>
            <a-form-item name='Tags' label="标签">
                <ArticleTagSelectVue v-model:value="SubmitArticle.Tags">

                </ArticleTagSelectVue>
            </a-form-item>
            <a-form-item :wrapper-col="{ ...layout.wrapperCol, offset: 8 }">
                <a-button type="primary" html-type="submit">Submit</a-button>
            </a-form-item>
        </a-form>

    </a-drawer>


</template>


<script setup lang='ts'>
import { ref, reactive, onMounted, toRaw } from 'vue'
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'
import { Article, ArticleTag } from '../Entities/E_Article'
import ArticleTagSelectVue from './common/ArticleTagSelect.vue'
import ArticleService from '../Services/ArticleService'

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
 let TagSelectRef = ref()
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

let SubmitArticle = ref({
        Title: '',
        Content: content,
        Tags: undefined
});
const onFinish = (values: Article) => {
    console.log('Success:', values);
    values.Content=content.value;
    ArticleService.prototype.AddArticle(values)
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