import {createRouter,createWebHashHistory,RouteRecordRaw} from 'vue-router'

const routes:Array<RouteRecordRaw>=[
    {
        path:"/b",
        name:"b",
        component:()=>import('../components/b.vue')
    },
    {
        path:"/EditBlog",
        name:'EditBlog',
        component:()=>import('../components/EditBlog.vue')
    }
]

 export const router=createRouter({
    history:createWebHashHistory(),
    routes
})