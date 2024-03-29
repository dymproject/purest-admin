<script setup lang="ts">
import { onUnmounted, reactive, onMounted, ref, h } from "vue";
import { getSystemPlatformInfo, SystemPlatformInfo } from "@/api/auth";
import { useOnlineUserStore } from "@/store/modules/onlineUser";
import { VxeButton, VxeGridProps } from "vxe-table";
import { noticeStarDatas } from "@/layout/components/notice/data";
const defValue = (): SystemPlatformInfo => {
  return {
    frameworkDescription: "",
    osDescription: "",
    osVersion: "",
    osArchitecture: "",
    machineName: "",
    version: ""
  };
};
const systemPlatformInfo = ref<SystemPlatformInfo>(defValue());
interface OnlineUser {
  connectionId: string;
  userId: string;
  userName: string;
  loginTime: string;
  ip: string;
}
const tableData = ref<OnlineUser[]>();
const gridOptions = reactive<VxeGridProps<OnlineUser>>({
  border: true,
  height: 400,
  align: null,
  columns: [
    { type: "seq", width: 50 },
    { field: "connectionId", title: "连接Id" },
    { field: "userName", title: "用户" },
    { field: "loginTime", title: "登陆时间" },
    { field: "ip", title: "Ip地址" },
    {
      field: "operate",
      title: "操作",
      slots: {
        default: ({ data, row }) => [
          h(`p`, {}, [
            h(
              VxeButton,
              {
                style: {
                  display:
                    row.connectionId == connection.connectionId
                      ? "none"
                      : "inline"
                },
                type: `text`,
                onClick() {
                  connection.invoke("OfflineUser", row.connectionId);
                }
              },
              () => "强制下线"
            ),
            h(
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
                onClick() {
                  connection.invoke(
                    "SendNotice",
                    row.connectionId,
                    noticeStarDatas
                  );
                }
              },
              () => "打招呼"
            )
          ])
        ]
      }
    }
  ]
});
const onlineUserStore = useOnlineUserStore();
const connection = onlineUserStore.getConnection;

connection.on("UpdateUser", (result: OnlineUser[]) => {
  tableData.value = result;
});
onMounted(() => {
  if (connection.state == `Connected`) {
    connection.invoke("GetOnlineUsers");
  }
  getSystemPlatformInfo().then((result: SystemPlatformInfo) => {
    systemPlatformInfo.value = result;
  });
});
</script>

<template>
  <div>
    <el-card :shadow="`never`">
      <template #header> 系统信息 </template>
      <el-descriptions :column="2" border>
        <el-descriptions-item
          label="机器名称"
          label-align="right"
          align="center"
          width="150"
        >
          {{ systemPlatformInfo.machineName }}
        </el-descriptions-item>
        <el-descriptions-item
          label="操作系统"
          label-align="right"
          align="center"
          width="150"
        >
          {{ systemPlatformInfo.osDescription }}
        </el-descriptions-item>
        <el-descriptions-item
          label="操作系统版本"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.osVersion }}
        </el-descriptions-item>
        <el-descriptions-item
          label="平台架构"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.osArchitecture }}
        </el-descriptions-item>

        >
        <el-descriptions-item
          label="程序核心框架版本"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.version }}
        </el-descriptions-item>
        <el-descriptions-item
          label="运行框架"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.frameworkDescription }}
        </el-descriptions-item>
        <el-descriptions-item
          label="项目地址"
          label-align="right"
          :span="2"
          align="center"
        >
          如果该项目帮助了您，希望能点个 Star 鼓励一下呦！
          &nbsp;&nbsp;&nbsp;&nbsp;
          <el-link
            href="https://gitee.com/dymproject/purest-admin"
            target="_blank"
            type="success"
          >
            gitee地址
          </el-link>
          &nbsp;&nbsp;&nbsp;&nbsp;
          <el-link
            href="https://github.com/dymproject/purest-admin"
            target="_blank"
            type="danger"
          >
            github地址
          </el-link>
          &nbsp;&nbsp;&nbsp;&nbsp; 交流群：242853490
        </el-descriptions-item>
      </el-descriptions>
    </el-card>
    <el-card :shadow="`never`" class="table-card">
      <template #header>在线用户</template>
      <vxe-grid :data="tableData" v-bind="gridOptions"> </vxe-grid>
    </el-card>
  </div>
</template>

<style lang="scss" scoped></style>
