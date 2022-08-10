import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
    {
        path: "/EditBlog",
        name: 'EditBlog',
        component: () => import('../components/Background/EditBlog.vue')
    },
    {
        path: "/BlogList",
        name: 'BlogList',
        component: () => import('../components/Blog/Article/BlogList.vue')
    },
    {
        path: "/ArticleTable",
        name: 'ArticleTable',
        component: () => import('../components/Background/ArticleTable.vue')
    },
    {
        path: "/ShowArticle",
        name: "ShowArticle",
        component: () => import('../components/Blog/Article/ShowArticle.vue')
    },
    {
        path: "/BlogIndex",
        name: "BlogIndex",
        component: () => import('../components/Blog/BlogIndex.vue')
    },
    {
        path: "/Login",
        name: "Login",
        component: () => import('../components/User/Login.vue')
    }
]

export const router = createRouter({
    history: createWebHashHistory(),
    routes
})