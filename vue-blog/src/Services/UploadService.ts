import {get,post} from './_Service'


export default class UploadService{
    async UploadImg(parames:FormData,timeout:number=10000){
       return await post("FileUpload/Upload",parames,timeout)
    }



}
