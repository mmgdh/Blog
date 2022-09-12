import { BlogParam } from '../Entities/E_BlogParam';
import { get, post, Delete, put } from './_Service'

const controler = "BlogParameter";

export default class BlogInfoService {
    public async AddBlogParameter(parames: BlogParam): Promise<boolean> {
        return await post(controler + "/AddBlogParameter", parames)
    }

    public async DelBlogParameter(id: string): Promise<boolean> {
        return await Delete(controler + "/DelBlogParameter", id)
    }

    public async ModifyBlogParameter(parames: BlogParam): Promise<boolean> {
        return await put(controler + "/ModifyBlogParameter", parames)
    }
    public async GetAllBlogParameters(): Promise<Array<BlogParam>> {
        return await get(controler + "/GetAllBlogParameters")
    }
    public async GetBlogParameters(): Promise<Array<BlogParam>> {
        return await get(controler + "/GetBlogParameters")
    }

    public async GetBlogParameter(): Promise<BlogParam> {
        return await get(controler + "/GetBlogParameter")
    }
}