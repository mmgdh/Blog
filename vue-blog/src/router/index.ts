import {createRouter,createWebHashHistory,RouteRecordRaw} from 'vue-router'

const routes:Array<RouteRecordRaw>=[
    {
        path:"/b",
        name:"b",
        component:()=>import('../components/b.vue')
    },
    {
        path:"/a",
        name:'a',
        component:()=>import('../components/a.vue')
    }
]

 export const router=createRouter({
    history:createWebHashHistory(),
    routes
})