import {createRouter,createWebHashHistory,RouteRecordRaw} from 'vue-router'

const routes:Array<RouteRecordRaw>=[
    {
        path:"/EditBlog",
        name:'EditBlog',
        component:()=>import('../components/EditBlog.vue')
    },
    {
        path:"/ArticleList",
        name:'ArticleList',
        component:()=>import('../components/Article/BlogList.vue')
    },
    {
        path:"/ShowArticle",
        name:"ShowArticle",
        component:()=>import('../components/Article/ShowArticle.vue')
    }
]

 export const router=createRouter({
    history:createWebHashHistory(),
    routes
})