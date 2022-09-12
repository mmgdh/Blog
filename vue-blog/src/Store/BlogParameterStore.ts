import { defineStore } from 'pinia'
import { BlogParam } from '../Entities/E_BlogParam'
import BlogInfoService from '../Services/BlogInfoService'

export const useParamStore = defineStore('BlogParam', {
    state: () => {
        return {
            BlogParameters: {} as Array<BlogParam>
        }
    },
    actions:{
        async GetAllParameter(){
            let ret =await BlogInfoService.prototype.GetAllBlogParameters();
            this.BlogParameters=ret;
        },
        GetParameterValue(paramName:string){
            return this.BlogParameters.find(x=>x.paramName==paramName)?.paramValue;
        }
    }
}
)