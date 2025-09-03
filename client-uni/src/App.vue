<script setup lang="ts">
import { onLaunch, onShow, onHide } from "@dcloudio/uni-app";
import { useOnlineUserStore } from '@/store/online-user'
import { HubConnectionState } from "uniapp-signalr";
const onlineUserStore = useOnlineUserStore();

onLaunch(async () => {
  console.log("App Launch");
});
onShow(async () => {
  console.log("App Show");
  if (onlineUserStore.connection.state !== HubConnectionState.Connected) {
    await onlineUserStore.createConnection();
  }
});
onHide(() => {
  console.log("App Hide");
  if (onlineUserStore.connection.state === HubConnectionState.Connected) {
    onlineUserStore.connection.stop();
  }
});
</script>
<style></style>
