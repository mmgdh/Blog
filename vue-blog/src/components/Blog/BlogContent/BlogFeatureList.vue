<template>
  <div class="container">
    <div class="Board">
      <div class="BoardContent">
        <h2>
          <p>
            EDITOR'S SELECTION</p>
          <span>
            Feature Articles
          </span>
        </h2>
      </div>
      <span class="BoardBack"></span>
    </div>

    <!-- <ul class="grid lg:grid-cols-2 gap-8">
      <template v-if="featuredArticles.length > 0">
        <li v-for="article in featuredArticles" :key="article.id">
          <ArticleCard :data="article" />
        </li>
      </template>
      <template v-else>
        <li v-for="n in 2" :key="n">
          <ArticleCard :data="{}" />
        </li>
      </template>
    </ul> -->
  </div>
</template>

<script setup lang='ts'>
import { ref } from 'vue'

</script>

<style scoped lang="less">
@import '../../../CSS/CommomCSS.less';

.container {
  .inverted-main-grid();
  .py(2rem);
  .gap(2rem);
  .box-border();
}

.Board {
  .relative();
  .overflow-hidden();
  border-radius: 1rem;
  background-color: var(--background-secondary);
  height: auto;

  .BoardContent {
    width: calc(100% - .5rem);
    height: calc(100% - .5rem);
    margin: .25rem;
    position: relative;
    z-index: 10;
    padding-bottom: 2.5rem;
    opacity: .9;
    display: flex;
    justify-content: flex-start;
    align-items: flex-end;
    .px(2rem);
    border-radius: 1rem;
    background-color: var(--background-primary);

    h2 {
      padding-bottom: 4rem;
      font-size: 1.875rem;
      line-height: 2.25rem;
      font-weight: inherit;

      p {
        display: block;
        margin-block-start: 1em;
        margin-block-end: 1em;
        margin-inline-start: 0px;
        margin-inline-end: 0px;
        background: var(--header_gradient_css);
      }

      span {
        font-size: 1.5rem;
        line-height: 2rem;
        font-weight: 600;
        position: relative;
        color: var(--text-bright);
        background: var(--header_gradient_css);
      }
    }
  }

  .BoardBack {
    position: absolute;
    z-index: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background: var(--header_gradient_css);
  }
}


@media (min-width:1024px) {
  .lg\:h-auto {
    height: auto;
  }
}

.gradientText {

  color: var(--header_gradient_css)
}

.gradientBackground {
  background-color: var(--header_gradient_css);
}
</style>