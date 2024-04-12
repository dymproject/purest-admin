<script lang="ts" setup>
import { reactive, ref } from "vue";
import { getPageList } from "@/api/system/requestLog";
import { ReVxeGrid } from "@/components/ReVxeTable";
import { VxeGridPropTypes } from "vxe-table";
const reVxeGridRef = ref();
const columns: VxeGridPropTypes.Columns<any> = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "控制器",
    field: "controllerName",
    minWidth: 150
  },
  {
    title: "方法名",
    field: "actionName",
    minWidth: 150
  },
  {
    title: "请求类型",
    field: "requestMethod",
    minWidth: 50
  },
  {
    title: "服务器环境",
    field: "environmentName",
    minWidth: 100
  },
  {
    title: "执行耗时(ms)",
    field: "elapsedTime",
    minWidth: 100
  },
  {
    title: "客户端IP",
    field: "clientIp",
    minWidth: 100
  },
  {
    title: "请求时间",
    field: "createTime",
    minWidth: 100
  }
];
const formRef = ref();

const handleInitialFormParams = () => ({
  controllerName: "",
  actionName: "",
  requestDate: new Date()
});
const formItems = [
  {
    field: "controllerName",
    title: "控制器名称",
    span: 4,
    itemRender: { name: "$input", props: { placeholder: "控制器名称" } }
  },
  {
    field: "actionName",
    title: "方法名称",
    span: 4,
    itemRender: { name: "$input", props: { placeholder: "方法名称" } }
  },
  {
    field: "requestDate",
    title: "请求日期",
    span: 4,
    itemRender: {
      name: "$input",
      props: { type: "date", placeholder: "配置编码" }
    }
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
const formData = reactive<{
  controllerName: string;
  actionName: string;
  requestDate: Date;
  isSuccess?: boolean;
}>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};
const functions: Record<string, string> = {};
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
      <ReVxeGrid
        ref="reVxeGridRef"
        :request="getPageList"
        :functions="functions"
        :searchParams="formData"
        :columns="columns"
        :customTableActions="[]"
      />
    </el-card>
  </div>
</template>
