<script setup lang="ts">
import { onUnmounted, ref } from "vue";
import { TabItem } from "./data";
import NoticeList from "./noticeList.vue";
import Bell from "@iconify-icons/ep/bell";
import { useOnlineUserStore } from "@/store/modules/onlineUser";
import { ElNotification } from "element-plus";
import { message } from "@/utils/message";
import { useUserStoreHook } from "@/store/modules/user";
const onlineUserStore = useOnlineUserStore();
const connection = onlineUserStore.createConnection();
connection.on("Notice", (result: TabItem[]) => {
  notices.value = result;
  noticesNum.value = 0;
  notices.value.map(v => (noticesNum.value += v.list.length));
  activeKey.value = result[0].key;
});
connection.on("logout", () => {
  message("您已被强制下线！3秒后返回登陆页面", { type: "error" });
  setTimeout(() => {
    useUserStoreHook().logOut();
  }, 3000);
});
connection.on("Message", (result: string) => {
  ElNotification({
    title: "新消息",
    message: result,
    type: "info"
  });
});
const noticesNum = ref(0);
const notices = ref<TabItem[]>([]);
const activeKey = ref("");
notices.value.map(v => (noticesNum.value += v.list.length));
//销毁
onUnmounted(() => {
  connection.stop();
});
</script>

<template>
  <el-dropdown trigger="click" placement="bottom-end">
    <span class="select-none dropdown-badge navbar-bg-hover">
      <el-badge :show-zero="false" :value="noticesNum" :max="99">
        <span class="header-notice-icon">
          <IconifyIconOffline :icon="Bell" />
        </span>
      </el-badge>
    </span>
    <template #dropdown>
      <el-dropdown-menu>
        <el-tabs
          v-model="activeKey"
          :stretch="true"
          class="dropdown-tabs"
          :style="{ width: notices.length === 0 ? '200px' : '330px' }"
        >
          <el-empty
            v-if="notices.length === 0"
            description="暂无消息"
            :image-size="60"
          />
          <span v-else>
            <template v-for="item in notices" :key="item.key">
              <el-tab-pane
                :label="`${item.name}(${item.list.length})`"
                :name="`${item.key}`"
              >
                <el-scrollbar max-height="330px">
                  <div class="noticeList-container">
                    <NoticeList :list="item.list" />
                  </div>
                </el-scrollbar>
              </el-tab-pane>
            </template>
          </span>
        </el-tabs>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
</template>

<style lang="scss" scoped>
.dropdown-badge {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 40px;
  height: 48px;
  margin-right: 10px;
  cursor: pointer;

  .header-notice-icon {
    font-size: 18px;
  }
}

.dropdown-tabs {
  .noticeList-container {
    padding: 15px 24px 0;
  }

  :deep(.el-tabs__header) {
    margin: 0;
  }

  :deep(.el-tabs__nav-wrap)::after {
    height: 1px;
  }

  :deep(.el-tabs__nav-wrap) {
    padding: 0 36px;
  }
}
</style>
