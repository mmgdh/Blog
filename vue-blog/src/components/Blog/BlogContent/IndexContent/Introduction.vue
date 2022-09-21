<template>
  <div class="card">
    <div class="UserPicture"></div>
    <div class="info">
      <h1>MMGDH</h1>
      <h2>Full Stack developer</h2>
    </div>
    <div class="detail">
      <div class="col">
        <h3>342</h3>
        <h4>文章</h4>
      </div>
      <div class="col">
        <h3>120k</h3>
        <h4>关注</h4>
      </div>
      <div class="col">
        <h3>285</h3>
        <h4>粉丝</h4>
      </div>
    </div>
    <div class="buttons">
      <button id="follow"><a href="https://b23.tv/tURaixv">＋关注</a></button>
      <button id="message">私信</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useAppStore } from '../../../../Store/AppStore';
import { onBeforeMount, ref } from 'vue'
import UploadService from "../../../../Services/UploadService"
const ImgUrl = UploadService.prototype.getImageUri()
const refPictureUrl = ref(ImgUrl+'8877529d-f0d8-4f57-8d30-cfbd553d9fcc')
onBeforeMount(async () => {
  const ParamStore = useAppStore();
  const PictureUrl = await ParamStore.GetParameterValue("MyHeadPortraitUrl");
  refPictureUrl.value = `url(${ImgUrl+PictureUrl})`
})



</script>
<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900&display=swap');

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'Poppins', sans-serif;
}

body {
  background: rgb(241, 169, 91);
  background: linear-gradient(45deg, rgba(241, 169, 91, 1) 27%, rgba(180, 86, 235, 1) 100%);
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

a {
  text-decoration: none;
  color: #FFFFFF;
}

h1 {
  font-weight: 500;
  font-size: 23px;
}

h2 {
  font-weight: 500;
  font-size: 15px;
  color: darkgrey;
}

h3 {
  font-weight: 500;
  font-size: 18px;
}

h4 {
  font-weight: 500;
  font-size: 14px;
  color: darkgrey;
}

.card {
  position: relative;
  background-color: #fff;
  width: 350px;
  height: 200px;
  box-shadow: 0 15px 50px rgba(0, 0, 0, 0.35);
  border-radius: 15px;
  transition: all 0.4s ease-in-out;
}

.card:hover {
  height: 450px;
}

.UserPicture {
  position: absolute;
  top: -45px;
  left: 50%;
  transform: translate(-50%, 0%);
  background-image: v-bind(refPictureUrl);
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  height: 150px;
  width: 150px;
  border-radius: 15px;
  box-shadow: 0 15px 50px rgba(0, 0, 0, 0.35);
  transition: all 0.4s ease-in-out;
}

.card:hover .UserPicture {
  height: 250px;
  width: 250px;
}

.info {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-items: center;
  padding: 125px 0px 10px 0px;
  transition: all 0.4s ease-in-out;
}

.card:hover .info {
  display: flex;
  padding: 230px 0px 10px 0px;
}

.detail {
  position: relative;
  display: flex;
  opacity: 0;
  z-index: -10;
  justify-content: space-between;
  padding: 10px 50px;
  transform: translateX(-20px);
  transition-timing-function: linear;
  transition-duration: 0s, 0.2s, 0.2s;
  transition-property: z-index, opacity, transform;
  transition-delay: 0.2s, 0s, 0s;
}

.col {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.buttons {
  position: relative;
  display: flex;
  opacity: 0;
  z-index: -10;
  padding: 10px 50px;
  justify-content: space-between;
  transform: translateX(-20px);
  transition-timing-function: linear;
  transition-duration: 0s, 0.2s, 0.2s;
  transition-property: z-index, opacity, transform;
  transition-delay: 0.2s, 0s, 0s;
}

button {
  padding: 10px 30px;
  border-radius: 5px;
  cursor: pointer;
}

#follow {
  background-color: #ff5f96;
  border: 1px solid #ff5f96;
  color: #fff;
  font-weight: 500;
  transition: all 0.3s linear;
}

#follow:hover {
  background-color: #fa377b;
}

#message {
  background-color: transparent;
  border: 1px solid grey;
  color: grey;
  font-weight: 500;
  transition: all 0.3s linear;
}

#message:hover {
  background-color: darkgrey;
  color: #fff;
}

.card:hover .detail,
.card:hover .buttons {
  opacity: 1;
  z-index: 0;
  transform: translateX(0px);
  transition-timing-function: linear;
  transition-duration: 0s, 0.3s, 0.3s;
  transition-property: z-index, opacity, transform;
  transition-delay: 0s, 0.2s, 0.2s;
}
</style>