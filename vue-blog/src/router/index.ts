import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
    {
        path: "/BlogManage",
        name: 'BlogManage',
        component: () => import('../components/Background/Manage.vue'),
        children: [
            {
                path: "/ArticleTable",
                name: 'ArticleTable',
                component: () => import('../components/Background/ArticleTable.vue')
            },
            {
                path: "/EditBlog",
                name: 'EditBlog',
                component: () => import('../components/Background/EditBlog.vue')
            },
        ]
    },
    {
        path: "/BlogMain",
        name: "BlogMain",
        component: () => import('../components/Blog/BlogMain.vue'),
        children:[
            {
                path: "/BlogIndex",
                name: "BlogIndex",
                component: () => import('../components/Blog/BlogIndex.vue'),
            },
            {
                path: "/ShowArticle",
                name: "ShowArticle",
                component: () => import('../components/Blog/Article/ShowArticle.vue')
            },
        ]
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