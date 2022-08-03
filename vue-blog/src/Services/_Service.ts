import axios from 'axios'
import { config } from 'md-editor-v3/lib/MdEditor/config'

// axios 配置
const service = axios.create({
      baseURL: 'http://localhost:83',
      headers: {              
        'Content-Type': 'application/json;charset=utf-8' 
      },
      timeout:10000,
      validateStatus () {
        return true
      }
    })
service.interceptors.request.use(config=>{
    
})