<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getOrganizationTreeData } from "@/api/auth";

const value = ref();
const data = ref();

onMounted(async () => {
  const treeData = await getOrganizationTreeData();
  data.value = treeData;
});
</script>

<template>
  <el-tree-select
    check-strictly
    v-model="value"
    style="width: 100%"
    :data="data"
    :props="{
      value: 'id',
      label: 'name',
      children: 'children'
    }"
    :render-after-expand="false"
  />
</template>

<style lang="scss" scoped></style>
