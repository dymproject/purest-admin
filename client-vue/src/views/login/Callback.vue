<script setup lang="ts">
function redirectWithParameter(targetUrl: string) {
  // 获取当前页面的URL
  const currentUrl = new URL(window.location.href);

  // 创建目标URL对象
  const redirectUrl = new URL(targetUrl);

  // 将当前URL的所有参数添加到目标URL
  currentUrl.searchParams.forEach((value, key) => {
    redirectUrl.searchParams.set(key, value);
  });

  // 重定向到新的URL
  window.location.href = redirectUrl.toString();
}

// 示例：在页面加载完成后立即执行重定向
window.onload = function () {
  redirectWithParameter("https://example.com/new-target");
};
</script>

<template>
  <div></div>
</template>

<style lang="scss" scoped></style>
