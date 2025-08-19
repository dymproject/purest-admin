<script setup lang="ts">
import { onLaunch, onShow, onHide } from "@dcloudio/uni-app";
import { useOnlineUserStore } from '@/store/online-user'
const onlineUserStore = useOnlineUserStore();
onLaunch(async () => {
  const connection = await onlineUserStore.createConnection();
  console.log(connection)
  
  console.log("App Launch");
});
onShow(() => {
  console.log("App Show");
});
onHide(() => {
  console.log("App Hide");
});
</script>
<style></style>
