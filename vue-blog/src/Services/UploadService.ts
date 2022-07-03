import {get,post} from '../axiosInstance'


export default class UploadService{
    async UploadImg(parames:FormData,timeout:number=10000){
       return await post("FileUpload/Upload",parames,timeout)
    }



}
