<template>
    <div>
        <a-popover v-model:visible="TagCardVisible" title="标签" trigger="click" placement="left">
            <template #content>
                <a-input-search v-model:value="InputValue" placeholder="input search Tag" style="width: 200px"
                    @search="GetOrCreate" />
                <a-divider>选择</a-divider>

                <div :class="{ width: '100px' }">
                    <a-tag v-for="Tag in AllTags" color="success" :key="Tag.id" @click="AddTags(Tag)">{{ Tag.tagName }}
                    </a-tag>
                </div>

            </template>
            <div id="DisPlayTags">
                <a-tag closable v-for="Tag in SelectArticleTags" color="success" :key="Tag.id" @close="RemoveTags(Tag)">
                    {{ Tag.tagName }}
                </a-tag>
            </div>
        </a-popover>
    </div>
</template>

<script setup lang="ts">
import { ref,onBeforeMount} from 'vue';
import { ArticleTag } from '../../Entities/E_Article';
import ArticleService from '../../Services/ArticleService'

let SelectArticleTags: any = ref([]);
let AllTags: any = ref([]);
let MessageBoxVisible = ref<boolean>(false);
const TagCardVisible = ref<boolean>(false);
const InputValue = ref<string>('');

onBeforeMount(() => {
    ArticleService.prototype.GetAllArticleTags().then((res: any) => AllTags.value = res)
})
const props = defineProps({
    FSelectArticleTags: Array<ArticleTag>,
    value:Array<ArticleTag>
}
)
const emit = defineEmits(['update:FSelectArticleTags','update:value'])
const AddTags = (Tag: ArticleTag) => {
    SelectArticleTags.value.push(Tag)
    let index = AllTags.value.findIndex((_Tag: any) => _Tag.id === Tag.id); //find index in your array
    AllTags.value.splice(index, 1);//remove element from array
    emit('update:FSelectArticleTags', SelectArticleTags.value)
    emit('update:value',SelectArticleTags.value);

}
const RemoveTags = (Tag: ArticleTag) => {
    let index = SelectArticleTags.value.findIndex((_Tag: any) => _Tag.id === Tag.id); //find index in your array
    SelectArticleTags.value.splice(index, 1);//remove element from array
    AllTags.value.push(Tag)
    emit('update:FSelectArticleTags', SelectArticleTags.value)
    emit('update:value',SelectArticleTags.value);
}
const GetOrCreate = (searchValue: string) => {
    let index = AllTags.value.findIndex((_Tag: any) => _Tag.tagName === searchValue); //find index in your array
    if (index < 0) {
        MessageBoxVisible.value = true;
    }
};
</script>
<style scoped>
#DisPlayTags {
    width: 200px;
    height: 200px;
    border: 2px;
    background-color: aquamarine;
}
</style>