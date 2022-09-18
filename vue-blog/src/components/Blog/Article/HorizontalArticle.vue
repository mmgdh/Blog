<template>
    <div class="feature-article" @click="router.push({
        path: 'ShowArticle',
        query: { 'ArticleId': articledata.id }
      })">
    <div class=" feature-thumbnail">
        <img v-if="articledata" class="ob-hz-thumbnail" :src="ImgUrl + articledata.imageId" />
        <img v-else src="../../../Img/backgroud.png" />
        <span class="thumbnail-screen"></span>
    </div>
    <div class="feature-content" v-if="articledata">
        <span>
            <b> {{articledata.classify.classifyName}} </b>
            <ul v-for="tag in articledata.tags">
                <li>#{{tag.tagName}}</li>
            </ul>
        </span>
        <h1>{{articledata.title}}</h1>
        <p>{{articledata.description}}</p>
        <div class="article-footer">
            <div class="flex-center">
                <img src="../../../Img/sky.jpg" alt="">
                <span class="text-color-dim">
                    <strong class="text-color-normal">零柒贰</strong> 发布于 {{articledata.createDateTime}}
                </span>
            </div>
        </div>
    </div>
    </div>
</template>

<script setup lang='ts'>
import { computed } from '@vue/reactivity';
import { ref, onBeforeMount, watch } from 'vue'
import { useRouter } from 'vue-router'
import { Article } from '../../../Entities/E_Article';
import UploadService from "../../../Services/UploadService"
const ImgUrl = UploadService.prototype.getImageUri()
let router = useRouter()
const { articledata } = defineProps<{
    articledata: Article
}>()
</script>

<style scoped lang="less">
@ComputerHeight: 28rem;
@phoneHeight: 120%;

@media (min-width: 1024px) {
    .feature-article {
        height: @ComputerHeight;
        grid-template-columns: repeat(2, minmax(0, 1fr));
        grid-template-rows: none;

        .feature-thumbnail span {
            height: @ComputerHeight;
        }

        .feature-thumbnail img {
            height: @ComputerHeight;
        }

        .feature-content {
            padding: 3rem;
            grid-row: auto;
        }
    }
}

@media (max-width:1023px) {
    .feature-article>div:first-of-type:after {
        top: 13%;
        left: 0;
        height: 120%;
        width: 100%;
        background: var(--article-cover);
    }

    .feature-article {
        width: 100%;
        height: 120%;
        grid-template-rows: repeat(3, minmax(0, 1fr));

        .feature-thumbnail span {
            height: @phoneHeight;
        }

        .feature-thumbnail img {
            height: @phoneHeight;
        }
    }
}



.feature-article {
    transition: transform .2s ease-in-out;
    background-color: var(--background-secondary);
    border-radius: 1rem;
    display: grid;
    overflow: hidden;
    position: relative;
    top: 0;
    --tw-shadow: 0 10px 15px -3px rgba(0, 0, 0, .1), 0 4px 6px -2px rgba(0, 0, 0, .05);
    box-shadow: var(--tw-ring-offset-shadow, 0 0 #0000), var(--tw-ring-shadow, 0 0 #0000), var(--tw-shadow);
    z-index: 1;

    .feature-thumbnail {
        position: relative;

        img {
            background-repeat: no-repeat;
            background-size: cover;
            display: block;
            -o-object-fit: cover;
            object-fit: cover;
            position: absolute;
            left: 0;
            width: 120%;
            z-index: 20;

        }

        span {
            opacity: .4;
            pointer-events: none;
            position: absolute;
            width: 120%;
            z-index: 30;
            background: var(--header_gradient_css)
        }
    }

    .feature-thumbnail:after {
        pointer-events: none;
        content: "";
        position: absolute;
        z-index: 35;
        left: 71%;
        top: 0;
        height: @ComputerHeight;
        width: 50%;
        background: var(--gradient-cover);
    }

    .thumbnail-screen {
        mix-blend-mode: screen;
    }

    .feature-content {
        display: flex;
        flex-direction: column;
        padding-left: 1.5rem;
        padding-right: 1.5rem;
        padding-bottom: 1.5rem;
        position: relative;
        z-index: 40;
        grid-row: span 2/span 2;

        b {
            font-size: .75rem;
            line-height: 1rem;
            color: var(--text-accent);
            text-transform: uppercase;
            font-weight: bolder;
        }

        ul {
            display: inline-flex;
            font-size: .75rem;
            line-height: 1rem;
            padding-left: 1rem;
        }

        p {
            overflow: hidden;
            text-overflow: ellipsis;
            display: -webkit-box;
            -webkit-line-clamp: 4;
            -webkit-box-orient: vertical;
            word-wrap: break-word;
            word-break: break-all;
            font-size: 1rem;
            line-height: 1.5rem;
            margin-bottom: .5rem;
        }

        h1 {
            color: var(--text-bright);
        }

        .article-footer {
            display: flex;
            align-items: flex-end;
            align-content: flex-end;
            justify-content: flex-start;
            flex: 1 1 0%;
            font-size: .875rem;
            line-height: 1.25rem;
            width: 100%;

            img {
                border-radius: 9999px;
                margin-right: 0.5rem;
                height: 28px;
                width: 28px;
                cursor: pointer;
                max-width: 100%;
            }
        }
    }


}


.ob-hz-thumbnail {
    max-width: 120%;
}

.flex-center {
    display: flex;
    flex-direction: row;
    align-items: center;

}
</style>