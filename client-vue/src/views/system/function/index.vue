<script lang="ts" setup>
import { h, reactive, ref } from "vue";
import { getPageList, deleteData } from "@/api/system/function";
import { ReVxeGrid } from "@/components/ReVxeTable";
import { VxeButton, VxeUI } from "vxe-pc-ui";
import CreateModal from "./CreateModal.vue";
import InterfaceModal from "./InterfaceModal.vue";

const reVxeGridRef = ref();
const interfaceModalRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "Id",
    field: "id",
    visible: false,

    minWidth: 100
  },
  {
    title: "功能名称",
    field: "name",
    treeNode: true,
    minWidth: 100
  },
  {
    title: "功能编码",
    field: "code",
    minWidth: 150
  },
  {
    title: "备注",
    field: "remark",
    minWidth: 150
  },
  {
    title: "#",
    field: "interfaces",
    align: "center",
    width: 100,
    slots: {
      default: ({ row }) =>
        h(VxeButton, {
          size: "mini",
          status: "success",
          content: "绑定接口",
          onClick() {
            interfaceModalRef.value.showInterface(row);
          }
        })
    }
  }
];
const formRef = ref();

const handleInitialFormParams = () => ({
  name: ""
});
const formItems = [
  {
    field: "name",
    title: "功能名称",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "功能名称" } }
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
  add: "system.function.add",
  edit: "system.function.edit",
  view: "system.function.view",
  delete: "system.function.delete"
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
    <InterfaceModal ref="interfaceModalRef" />
  </div>
</template>
