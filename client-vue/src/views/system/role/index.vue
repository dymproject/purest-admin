<script lang="ts" setup>
import { h, reactive, ref } from "vue";
import { getPageList, deleteData } from "@/api/system/role";
import { ReVxeGrid } from "@/components/ReVxeTable";
import { VxeButton, VxeGridPropTypes, VXETable } from "vxe-table";
import FunModal from "./FunModal.vue";
import CreateModal from "./CreateModal.vue";

const funModalRef = ref();
const reVxeGridRef = ref();
const columns: VxeGridPropTypes.Columns<any> = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "Id",
    field: "id",
    minWidth: 100
  },
  {
    title: "角色名",
    field: "name",
    minWidth: 100
  },
  {
    title: "描述",
    field: "description",
    minWidth: 150
  },
  {
    title: "备注",
    field: "remark",
    minWidth: 150
  },
  {
    title: "功能",
    field: "funtions",
    align: "center",
    width: 100,
    slots: {
      default: ({ row }) =>
        h(VxeButton, {
          size: "mini",
          content: "绑定功能",
          status: "success",
          onClick() {
            funModalRef.value.showFunction(row);
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
    title: "角色名",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "角色名" } }
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
  const type = await VXETable.modal.confirm("您确定要删除吗？");
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
  add: "system.role.add",
  edit: "system.role.edit",
  view: "system.role.view",
  delete: "system.role.delete"
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
    <FunModal ref="funModalRef" />
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </div>
</template>
