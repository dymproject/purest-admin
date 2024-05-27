<script setup lang="ts">
import { ref, computed, onBeforeMount, onUnmounted } from "vue";
import { ListItem, TabItem } from "./data";
import NoticeList from "./components/NoticeList.vue";
import BellIcon from "@iconify-icons/ep/bell";
import { useOnlineUserStore } from "@/store/modules/onlineUser";
import { message } from "@/utils/message";
import { useUserStoreHook } from "@/store/modules/user";
import { ElNotification } from "element-plus";
import { getDictionaryDataByCode } from "@/api/system/dictionary";
import { getUnReadNotice } from "@/api/auth";
const onlineUserStore = useOnlineUserStore();
const connection = onlineUserStore.createConnection();
connection.on("Notice", (result: ListItem) => {
  const tab = notices.value.find(x => x.key == result.type);
  tab.list.push(result);
  noticesNum.value = 0;
  notices.value.map(v => (noticesNum.value += v.list.length));
  activeKey.value = tab.key;
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
    type: "info",
    duration: 0
  });
});
const noticesNum = ref(0);
const notices = ref<TabItem[]>([]);
const activeKey = ref("");

notices.value.map(v => (noticesNum.value += v.list.length));

const getLabel = computed(
  () => item =>
    item.name + (item.list.length > 0 ? `(${item.list.length})` : "")
);
onBeforeMount(async () => {
  let noticeRecords = (await getUnReadNotice()) as Array<any>;
  let noticeTypes = (await getDictionaryDataByCode(
    "dict_notice_type"
  )) as Array<any>;
  notices.value = noticeTypes.map(v => {
    const items = noticeRecords.filter(x => x.type == v.id);
    if (items.length > 0) {
      activeKey.value = v.id.toString();
    }
    return {
      key: v.id.toString(),
      name: v.name,
      list: items,
      emptyText: "暂无"
    };
  });
  noticesNum.value = 0;
  notices.value.map(v => (noticesNum.value += v.list.length));
});
//销毁
onUnmounted(() => {
  connection.stop();
});
</script>

<template>
  <el-dropdown trigger="click" placement="bottom-end">
    <span
      :class="[
        'dropdown-badge',
        'navbar-bg-hover',
        'select-none',
        Number(noticesNum) !== 0 && 'mr-[10px]'
      ]"
    >
      <el-badge :value="Number(noticesNum) === 0 ? '' : noticesNum" :max="99">
        <span class="header-notice-icon">
          <IconifyIconOffline :icon="BellIcon" />
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
              <el-tab-pane :label="getLabel(item)" :name="`${item.key}`">
                <el-scrollbar max-height="330px">
                  <div class="noticeList-container">
                    <NoticeList :list="item.list" :emptyText="item.emptyText" />
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
