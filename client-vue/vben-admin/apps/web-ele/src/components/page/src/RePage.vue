<script setup lang="ts">
import { Page } from '@vben/common-ui';
import { ElCard } from 'element-plus';
</script>
<template>
  <Page>
    <ElCard>
      <slot name="header"></slot>
    </ElCard>
    <ElCard class="table-card">
      <slot></slot>
    </ElCard>
  </Page>
</template>
