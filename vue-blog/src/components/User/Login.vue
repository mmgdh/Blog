<template>
  <div class="login-container">
    <h2 class="login-title">我的登陆页面</h2>
    <a-form :model="form" class="login-form" @finish="onSubmit">
      <h2 class="title">用户登录 LOGIN</h2>
      <a-form-item name="userName">
        <a-input v-model:value="form.userName">
          <a-icon slot="prefix" type="user" />
        </a-input>
      </a-form-item>
      <a-form-item name="passWord">
        <a-input-password v-model:value="form.passWord">
          <a-icon slot="prefix" type="unlock" />
        </a-input-password>
      </a-form-item>
      <a-form-item>
        <a-button class="submit" type="primary" html-type="submit">保存</a-button>
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { UserLogin } from '../../Entities/E_Users.js';
import UserLoginService from '../../Services/UserLoginService.js';

var _form: UserLogin = {
  userName: "",
  passWord: "",
}
var form = ref(_form);
const onSubmit = (value: UserLogin) => {
  UserLoginService.prototype.Login(value).then(token => {
    if (token!=""){
      localStorage.setItem('JWT', token);
      console.log("登陆成功！");
    }

  });

}

</script>
<style scoped>
.login-form {
  width: 565px;
  height: 372px;
  margin: 0 auto;
  /* background: url("../../assets/houTaiKuang.png"); */
  padding: 40px 110px;
}

/* 背景 */
.login-container {
  position: absolute;
  width: 100%;
  height: 100%;
  background: url("../../assets/BackGroud.jpg") no-repeat;
  background-size: 100% 100%;
}

/* Log */
.login-title {
  color: #fff;
  text-align: center;
  margin: 100px 0 60px 0;
  font-size: 36px;
  font-family: Microsoft Yahei;
}

/* 登陆按钮 */
.submit {
  width: 100%;
  height: 45px;
  font-size: 16px;
}

/* 用户登陆标题 */
.title {
  margin-bottom: 50px;
  color: #fff;
  font-weight: 700;
  font-size: 24px;
  font-family: Microsoft Yahei;
}

/* 输入框 */
.inputBox {
  height: 45px;
}

/* 输入框内左边距50px */
.ant-input-affix-wrapper .ant-input:not(:first-child) {
  padding-left: 50px;
}
</style>