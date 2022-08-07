<template>
  <div class="login-container">
    <a-form :model="form" class="login-form" @finish="onSubmit">
      <h1 class="title">LOGIN</h1>
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
        <a-button shape="round" class="submit">保存</a-button>
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { UserLogin } from '../../Entities/E_Users.js';
import UserLoginService from '../../Services/UserLoginService.js';
import { useRouter } from 'vue-router';

let router = useRouter()
var _form: UserLogin = {
  userName: "",
  passWord: "",
}
var form = ref(_form);

const onSubmit = (value: UserLogin) => {
  UserLoginService.prototype.Login(value).then(token => {
    if (token != "") {
      localStorage.setItem('JWT', token);
      console.log("登陆成功！");
      router.push('/ArticleTable')
    }

  });

}

</script>
<style scoped>
.login-form {
  width: 560px;
  height: 30px;
  margin: 0 auto;
  /* background: url("../../assets/houTaiKuang.png"); */
  padding: 40px 110px;
}

/* 背景 */
.login-container {
  /* background: url("../../assets/BackGroud.jpg") no-repeat; */
  background-size: 100% 100%;
  background-color: #2c3e50;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.login-form {

  /* 超出部分隐藏 */
  overflow: hidden;
  display: flex;
  flex-direction: column;
  justify-content: center;

  width: 500px;
  height: 450px;
  background-color: #34495e;
  border-radius: 10px;
  box-shadow: 10px 10px 20px rgba(33, 44, 55, .3);

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
  -ms-flex-align: center;
}
</style>