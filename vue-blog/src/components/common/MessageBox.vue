<template>
    <div>
        <a-modal v-model:visible="_visible" title="Basic Modal" @ok="clickTap">
        <p>{{ContentMsg}}</p>
        </a-modal>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';

defineProps<{
    _visible:boolean,
    ContentMsg:string
}>()

const emit = defineEmits(['ok'])
const clickTap = () => {
    emit('ok')
}
</script>

