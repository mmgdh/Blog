<template>
    <Md v-model="content" @on-upload-img="onUploadImg"></Md>

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
            <a-form-item name="Classify" label="分类" has-feedback
                :rules="[{ required: true, message: 'Please select your Classify!' }]">
                <a-select v-model:value="SubmitArticle.Classify" placeholder="Please select a Classify">
                    <a-select-option v-for="classify in Ref_ArticleCLassify" :value="classify.id" :key="classify.id">
                        {{ classify.classifyName }}
                    </a-select-option>
                </a-select>
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


    <MessageBox :_visible='SaveMessageShow' :ContentMsg="Message" @ok="CCC"></MessageBox>
</template>


<script setup lang='ts'>
import { ref, onBeforeMount } from 'vue'
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'
import { Article, ArticleTag, ArticleClassify } from '../../Entities/E_Article'
import ArticleTagSelectVue from '../common/ArticleTagSelect.vue'
import ArticleService from '../../Services/ArticleService'
import UploadService from '../../Services/UploadService'
import MessageBox from '../common/MessageBox.vue'
import { useRouter } from 'vue-router'
import { message } from 'ant-design-vue';

let router = useRouter()
let _ArticleCLassify: Array<ArticleClassify> = [];
let Ref_ArticleCLassify = ref(_ArticleCLassify);

onBeforeMount(async () => {
    ArticleService.prototype.GetAllArticleClassify().then((res: any) => Ref_ArticleCLassify.value = res)
})

//#region  markdown
const content = ref<string>('');

//MarkDown图片上传功能
const onUploadImg = async (files: any, callback: any) => {
    const res = await Promise.all(
        files.map((file: any) => {
            return new Promise((rev, rej) => {
                const form = new FormData();
                form.append('file', file);

                UploadService.prototype.UploadImg(form)
                    .then((res: any) => rev(res))
                    .catch((error: any) => rej(error));
            });
        })
    );

    callback(res.map((item) => item));
};
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

let SubmitArticle = ref({
    Title: '',
    Classify: "",
    Image: "",
    Content: content,
    Tags: undefined
});
let SaveMessageShow = ref(false);
const Message: string = "保存成功"
const onFinish = (values: Article) => {
    console.log('Success:', values);
    values.content = content.value;
    ArticleService.prototype.AddArticle(values).then((res) => {
        if (res != "") {
            SaveMessageShow.value = true;
            message.success("保存成功!")
            router.push("/ArticleTable");
        }
        else{
            message.error("保存失败");
        }

    });
};
let CCC = () => {
    SaveMessageShow.value = false;
}

let Clear = () => {
    visible.value = false;
    content.value = "";

}
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