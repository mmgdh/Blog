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
            <a-form-item :name="['Article', 'Title']" label="标题" :rules="[{ required: true }]">
                <a-input v-model:value="SubmitArticle.Title" />
            </a-form-item>
            <a-form-item :name="['Article', 'Tags']" label="标签">
                <div :style="{float:'left'}">
                    <a-popover v-model:visible="TagCardVisible" title="标签" trigger="click">
                        <template #content>
                            <a @click="hide">Close</a>
                        </template>
                        <a-input aria-readonly="true" v-model:value="SubmitArticle.Tags" />
                    </a-popover>                   
                </div>
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

interface ArticleTag {
    TagName: string,
    Id: string
}

interface Article {
    Title: string,
    Tags: Array<ArticleTag>,
}
let SelectArticleTags: Array<ArticleTag> = [];

const AddTags = () => {
    let _ArticleTag: ArticleTag = {
        TagName: '1',
        Id: ''
    }
    SelectArticleTags.push(_ArticleTag)
}


const SubmitArticle: Article = reactive({
    Title: "1",
    Tags: SelectArticleTags
});
const onFinish = (values: any) => {
    console.log('Success:', values);
};

//#region 表单内Tag的气泡卡片
const TagCardVisible = ref<boolean>(false);

const hide = () => {
    TagCardVisible.value = false;
};
//#endregion
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