<script lang="ts" setup>
import { reactive, ref } from "vue";
import { getPageList, deleteData } from "@/api/system/organization";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeGridPropTypes, VXETable } from "vxe-table";
const reVxeGridRef = ref();
const columns: VxeGridPropTypes.Columns<any> = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "组织机构名称",
    field: "name",
    treeNode: true,
    minWidth: 100
  },
  {
    title: "负责人",
    field: "leader",
    minWidth: 100
  },
  {
    title: "联系电话",
    field: "telephone",
    minWidth: 100
  },
  {
    title: "排序",
    field: "sort",
    minWidth: 100
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
  account: ""
});
const formItems = [
  {
    field: "name",
    title: "组织机构名称",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "用户名" } }
  },
  {
    field: "leader",
    title: "负责人",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "帐号" } }
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
const formData = reactive<{ name: string; account: string }>(
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
  add: "system.organization.add",
  edit: "system.organization.edit",
  view: "system.organization.view",
  delete: "system.organization.delete"
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
