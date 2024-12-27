<script lang="ts" setup>
import { reactive, ref } from "vue";
import { getPageList, deleteData } from "@/api/system/systemconfig";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeUI } from "vxe-pc-ui";
const reVxeGridRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "配置名称",
    field: "name",
    minWidth: 100
  },
  {
    title: "配置编码",
    field: "configCode",
    minWidth: 100
  },
  {
    title: "配置值",
    field: "configValue",
    minWidth: 200
  },
  {
    title: "备注",
    field: "remark",
    minWidth: 150
  }
];
const formRef = ref();

const handleInitialFormParams = () => ({
  name: "",
  configCode: ""
});
const formItems = [
  {
    field: "name",
    title: "配置名称",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "配置名称" } }
  },
  {
    field: "configCode",
    title: "配置编码",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "配置编码" } }
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
const formData = reactive<{ name: string; configCode: string }>(
  handleInitialFormParams()
);

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};

const createModalRef = ref();
const handleAdd = () => {
  createModalRef.value.showAddModal();
};
const handleEdit = (record: Recordable) => {
  createModalRef.value.showEditModal(record);
};
const handleDelete = async (record: Recordable) => {
  const type = await VxeUI.modal.confirm("您确定要删除吗？");
  if (type == "confirm") {
    deleteData(record.id).then(() => {
      handleSearch();
    });
  }
};
const handleView = (record: Recordable) => {
  createModalRef.value.showViewModal(record);
};
const functions: Record<string, string> = {
  add: "system.systemconfig.add",
  edit: "system.systemconfig.edit",
  view: "system.systemconfig.view",
  delete: "system.systemconfig.delete"
};
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
        @handleAdd="handleAdd"
        @handleEdit="handleEdit"
        @handleDelete="handleDelete"
        @handleView="handleView"
      />
    </el-card>
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </div>
</template>
