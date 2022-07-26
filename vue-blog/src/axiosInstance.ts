import axios from 'axios'
import { number } from 'vue-types'

// axios 配置
const service = axios.create({
      // 联调
      baseURL: 'http://localhost:83',
      headers: {              
    //     get: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8' },
    //     post: { 'Content-Type': 'application/json;charset=utf-8' }
      },
    //    timeout:2000,
      validateStatus () {
        return true
      }
    })

/**
 * get 请求方法
 * @param url
 * @param params
 * @returns {Promise}
 */
export function get (url:any, params:any=undefined, timeout:number=50000):any {
      return new Promise((resolve, reject) => {
        service
          .get(url, { params: params,timeout:timeout})
          .then((response) => {
            resolve(response.data)
          })
          .catch((err) => {
            reject(err)
          })
      })
    }
// const API =axios.create({
//     baseURL:'https://localhost:7177',
//     timeout:2000
// })


/**
 * post 请求方法
 * @param url
 * @param data
 * @returns {Promise}
 */
export function post (url:any, data = {}, timeout:number=50000):any {
      return new Promise((resolve, reject) => {
        service.post(url, data, { timeout: timeout }).then(
          (response) => {
            resolve(response.data)
          },
          (err) => {
            reject(err)
          }
        )
      })
    }


    /**
 * post 请求方法
 * @param url
 * @param data
 * @returns {Promise}
 */
export function put (url:any, data = {}, timeout:number=50000):any {
      return new Promise((resolve, reject) => {
        service.put(url, data, { timeout: timeout }).then(
          (response) => {
            resolve(response.data)
          },
          (err) => {
            reject(err)
          }
        )
      })
    }

    /**
 * Delete 请求方法
 * @param url
 * @param data
 * @returns {Promise}
 */
export function Delete (url:any, data = {}, timeout:number=50000):any {
      return new Promise((resolve, reject) => {
        service.delete(url, {params:data}).then(
          (response) => {
            resolve(response.data)
          },
          (err) => {
            reject(err)
          }
        )
      })
    }