import {createRouter,createWebHashHistory,RouteRecordRaw} from 'vue-router'

const routes:Array<RouteRecordRaw>=[
    {
        path:"/EditBlog",
        name:'EditBlog',
        component:()=>import('../components/Background/EditBlog.vue')
    },
    {
        path:"/ArticleList",
        name:'ArticleList',
        component:()=>import('../components/Article/BlogList.vue')
    },
    {
        path:"/ArticleTable",
        name:'ArticleTable',
        component:()=>import('../components/Background/ArticleTable.vue')
    },
    {
        path:"/ShowArticle",
        name:"ShowArticle",
        component:()=>import('../components/Article/ShowArticle.vue')
    },
    {
        path:"/Login",
        name:"Login",
        component:()=>import('../components/User/Login.vue')
    }
]

 export const router=createRouter({
    history:createWebHashHistory(),
    routes
})