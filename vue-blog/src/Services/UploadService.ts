import {get,post} from './_Service'

const controler = "FileUpload";

export default class UploadService{
    async UploadImg(parames:FormData,timeout:number=10000){
       return await post(controler+"/Upload",parames,timeout);
    }

    async GetImg(ImgId:string){
        return await post(controler+"/GetImage",ImgId);
    }


}
