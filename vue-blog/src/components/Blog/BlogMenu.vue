<template>
  <div class="navBody">
    <nav class="nav">
      <div class="container">
        <h1 class="logo"><a href="#">主页</a></h1>
        <ul>
          <li><a href="#" class="current">Home</a> </li>
          <li><a href="#">T1</a> </li>
          <li><a href="#">T2</a> </li>
          <li><a href="#">T3</a> </li>
        </ul>
      </div>
    </nav>

    <div class="hero">
      <div class="container">
        <h1>Welcome To My Website</h1>
        <p>Picture,Techonolege...</p>
      </div>
    </div>

    <section class="container content">
      <h2>Content Title One</h2>
      <p>Picture,Techonolege...</p>

      <h3>Content Two</h3>
      <p>Lorem</p>
    </section>
  </div>



</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';
import { MailOutlined, AppstoreOutlined, SettingOutlined } from '@ant-design/icons-vue';
export default defineComponent({
  components: {
    MailOutlined,
    AppstoreOutlined,
    SettingOutlined,
  },
  setup() {
    const current = ref<string[]>(['mail']);
    return {
      current,
    };
  },
});
</script>
<style>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

.navBody {
  font-family: 'Open Sans', sans-serif;
  color: #FFF;
  padding-bottom: 50px;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
}

.nav {
  position: fixed;
  background-color: #222;
  top: 0;
  left: 0;
  right: 0;
  transition: all 0.3s ease-in-out;
}

.nav .container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px 0;
  transition: all 0.3s ease-in-out;
}

.nav ul {
  display: flex;
  list-style-type: none;
  align-items: center;
  justify-content: center;
}

.nav a {
  color: #fff;
  text-decoration: none;
  padding: 7px 15px;
  transition: all 0.3s ease-in-out;
}

.nav.active {
  background-color: #fff;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);
}

.nav.active a {
  color: #000
}

.nav.active .container {
  padding: 10px 0;
}

.nav a.current,
.nav a:hover {
  color: #c0392b;
  font-weight: bold;
}

.hero {
  background-image: url('../../Img/Background.jpg');
  background-repeat: no-repeat;
  background-size: cover;
  background-position: bottom center;
  height: 100vh;
  color: #fff;
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
  position: relative;
  margin-bottom: 20px;
  z-index: -2;
}

.hero::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: -1;
}

.hero h1 {
  font-size: 46px;
  margin: -20px 0 20px;
}

.hero p {
  font-size: 20px;
  letter-spacing: 1px;
}

.content h2,
.content h3 {
  font-size: 150%;
  margin: 20px 0;
}

.content p {
  /* color: #555; */
  line-height: 30px;
  letter-spacing: 1.2px;
}
</style>