import { defineStore } from 'pinia'
import { BlogParam } from '../Entities/E_BlogParam'
import BlogInfoService from '../Services/BlogInfoService'

const setTheme = (theme: string) => {
    document.documentElement.setAttribute('data-theme', theme)
  }


export const useAppStore = defineStore('AppStore', {
    state: () => {
        return {
            themeConfig:{
                theme:'theme-light'
            },
            BlogParameters: {} as Array<BlogParam>
        }
    },
    actions: {
        async GetAllParameter() {
            let ret = await BlogInfoService.prototype.GetAllBlogParameters();
            this.BlogParameters = ret;
        },
        async GetParameterValue(paramName: string) {
            if(this.BlogParameters.length==undefined) await this.GetAllParameter();
            if (this.BlogParameters.length == 0) return undefined;
            return this.BlogParameters.find(x=>x.paramName==paramName)?.paramValue;
        },

        toggleTheme(isDark?: boolean) {
            this.themeConfig.theme =
              isDark === true || this.themeConfig.theme === 'theme-light' ? 'theme-dark' : 'theme-light'
            // Cookies.set('theme', this.theme)
            setTheme(this.themeConfig.theme)
          },
    }
}
)