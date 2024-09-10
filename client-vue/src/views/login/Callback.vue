<script setup lang="ts">
import { onMounted } from "vue";

function redirectWithParameter(targetUrl: string) {
  // 获取当前页面的URL
  const currentUrl = new URL(window.location.href);
  // 将相对路径转换为绝对路径
  const absoluteTargetUrl = new URL(targetUrl, currentUrl.origin).toString();
  // 创建目标URL对象
  const redirectUrl = new URL(absoluteTargetUrl);
  // 将当前URL的所有参数添加到目标URL
  currentUrl.searchParams.forEach((value, key) => {
    redirectUrl.searchParams.set(key, value);
  });
  // 重定向到新的URL
  fetch(redirectUrl)
    .then()
    .finally(() => {
      window.close();
    });
}

// 示例：在页面加载完成后立即执行重定向
onMounted(() => {
  redirectWithParameter("/api/v1/auth/callback");
});
</script>

<template>
  <div>页面回调成功，即将关闭……</div>
</template>

<style lang="scss" scoped></style>
