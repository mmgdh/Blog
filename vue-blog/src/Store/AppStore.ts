import { ref } from 'vue'
import { defineStore } from 'pinia'
import { BlogParam } from '../Entities/E_BlogParam'
import BlogInfoService from '../Services/BlogInfoService'
import UploadService from '../Services/UploadService'



const setTheme = (theme: string) => {
    document.documentElement.setAttribute('data-theme', theme)
}

const GetParameterValue = (paramName: string) => {
    // if (!BlogParameters ||BlogParameters==null||BlogParameters.length==0) {
    //     return '';
    // }
    return BlogParamArray.find(x => x.paramName == paramName)?.paramValue
}

let BlogParamArray: BlogParam[] = [];
let ref_BlogParamArray = ref(BlogParamArray);

export const useAppStore = defineStore('AppStore', {
    state: () => {
        return {
            themeConfig: {
                theme: 'theme-light',
                header_gradient_css: 'var(--header_gradient_css)'
            },
        }
    },
    getters: {
        AuthorName(state) {

            return ref_BlogParamArray.value.find(x => x.paramName == 'Blog-AuthorName')?.paramValue;
        },
        AuthorPinYin(state) {
            return ref_BlogParamArray.value.find(x => x.paramName == 'Blog-AuthorPinYin')?.paramValue
        },
        HeadPortrait(state) {
            var ret = ''
            var pictureId = ref_BlogParamArray.value.find(x => x.paramName == 'Blog-HeadPortrait')?.paramValue

            if (pictureId) {
                ret = UploadService.prototype.getImageUri() + pictureId
            }
            return ret;
        },
        BlogParameters() {
            return ref_BlogParamArray.value
        }
    },
    actions: {
        async GetAllParameter() {
            ref_BlogParamArray.value = await BlogInfoService.prototype.GetAllBlogParameters();

        },
        GetParameterValue(paramName: string) {
            if (ref_BlogParamArray.value.length == undefined) this.GetAllParameter();
            if (ref_BlogParamArray.value.length == 0) return undefined;
            return ref_BlogParamArray.value.find(x => x.paramName == paramName)?.paramValue;
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