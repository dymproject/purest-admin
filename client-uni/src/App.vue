<script setup lang="ts">
import { onLaunch, onShow, onHide } from "@dcloudio/uni-app";
import { useUserStore } from "./store/user";
const userStore = useUserStore();
onLaunch(() => {
  console.log("App Launch");
  if (!userStore.isLogin) {
    userStore.logout();
  }
});
onShow(() => {
  console.log("App Show");
});
onHide(() => {
  console.log("App Hide");
});
</script>
<style></style>
