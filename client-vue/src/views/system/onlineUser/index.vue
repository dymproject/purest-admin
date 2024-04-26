<script lang="ts" setup>
import { h, reactive, ref, onMounted } from "vue";
import { getPageList, deleteData } from "@/api/system/user";
import { ReVxeGrid } from "@/components/ReVxeTable";
import { VxeButton, VxeGridProps, VxeGridPropTypes, VXETable } from "vxe-table";
import { useOnlineUserStore } from "@/store/modules/onlineUser";
import { hasAuth } from "@/router/utils";
import { message } from "@/utils/message";
const reVxeGridRef = ref();
const formRef = ref();

const handleInitialFormParams = () => ({
  name: ""
});
const formItems = [
  {
    field: "name",
    title: "用户",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "用户" } }
  },
  {
    span: 6,
    itemRender: {
      name: "$buttons",
      children: [
        {
          props: {
            type: "submit",
            icon: "vxe-icon-search",
            content: "查询",
            status: "primary"
          }
        },
        { props: { type: "reset", icon: "vxe-icon-undo", content: "重置" } }
      ]
    }
  }
];
const formData = reactive<{ name: string }>(handleInitialFormParams());

const handleSearch = () => {
  connection.invoke("GetOnlineUsers");
};
interface OnlineUser {
  connectionId: string;
  userId: string;
  userName: string;
  connectedTime: string;
  ip: string;
  ipString: string;
}
const tableData = ref<OnlineUser[]>();
const gridOptions = reactive<VxeGridProps<OnlineUser>>({
  border: false,
  round: true,
  resizable: true,
  maxHeight: 650,
  minHeight: 650,
  align: null,
  columns: [
    { type: "seq", width: 100, align: "center" },
    { field: "connectionId", visible: false, title: "连接Id" },
    { field: "userName", title: "用户" },
    { field: "connectedTime", title: "连接时间" },
    { field: "ip", title: "ip地址" },
    { field: "ipString", title: "ip归属" },
    {
      field: "operate",
      title: "操作",
      align: "center",
      slots: {
        default: ({ data, row }) => [
          h(`p`, {}, [
            hasAuth("system.onlineuser.logout")
              ? h(
                  VxeButton,
                  {
                    style: {
                      display:
                        row.connectionId == connection.connectionId
                          ? "none"
                          : "inline"
                    },
                    status: "danger",
                    type: `text`,
                    onClick() {
                      connection.invoke("OfflineUser", row.connectionId);
                    }
                  },
                  () => "强制下线"
                )
              : null,
            hasAuth("system.onlineuser.sendmsg")
              ? h(
                  VxeButton,
                  {
                    style: {
                      marginLeft: `10px`,
                      display:
                        row.connectionId == connection.connectionId
                          ? "none"
                          : "inline"
                    },
                    type: `text`,
                    status: "primary",
                    onClick() {
                      messageOptions.modalValue = true;
                      messageOptions.connectionId = row.connectionId;
                    }
                  },
                  () => "发送消息"
                )
              : null
          ])
        ]
      }
    }
  ]
});
const onlineUserStore = useOnlineUserStore();
const connection = onlineUserStore.getConnection;

connection.on("UpdateUser", (result: OnlineUser[]) => {
  result = result.filter(item => item.connectionId != connection.connectionId);
  if (formData.name) {
    tableData.value = result.filter(item =>
      item.userName.includes(formData.name)
    );
    return false;
  }
  tableData.value = result;
});

const messageOptions = reactive({
  connectionId: "",
  modalValue: false,
  message: ""
});

const handleSendMessage = () => {
  connection.invoke(
    "SendMessage",
    messageOptions.connectionId,
    messageOptions.message
  );
  messageOptions.modalValue = false;
  messageOptions.message = "";
  message("消息发送成功");
};
onMounted(() => {
  if (connection.state == `Connected`) {
    connection.invoke("GetOnlineUsers");
  }
});
</script>
<template>
  <div>
    <el-card :shadow="`never`">
      <vxe-form
        ref="formRef"
        :data="formData"
        :items="formItems"
        @submit="handleSearch"
        @reset="handleInitialFormParams"
      />
    </el-card>
    <el-card :shadow="`never`" class="table-card">
      <vxe-grid v-bind="gridOptions" :data="tableData" />
    </el-card>
    <vxe-modal v-model="messageOptions.modalValue" show-footer>
      <template #title>
        <span style="color: red">发送消息</span>
      </template>
      <template #default>
        <vxe-textarea
          v-model="messageOptions.message"
          :rows="5"
          :maxlength="120"
          :showWordCount="true"
          placeholder="请输入要发送的消息"
        />
      </template>
      <template #footer>
        <vxe-button
          status="primary"
          content="确定"
          @click="handleSendMessage"
        />
      </template>
    </vxe-modal>
  </div>
</template>
