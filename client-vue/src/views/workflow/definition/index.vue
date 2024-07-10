<script lang="ts" setup>
import { reactive, ref, h } from "vue";
import { getPageList, deleteData, lock } from "@/api/workflow/definition";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeButton, VxeUI } from "vxe-pc-ui";
import { message } from "@/utils/message";
const reVxeGridRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "模板名称",
    field: "name",
    minWidth: 100
  },
  {
    title: "模板Id",
    field: "definitionId",
    minWidth: 200
  },
  {
    title: "版本",
    field: "version",
    minWidth: 100
  },
  {
    title: "状态",
    field: "isLockedStr",
    align: `center`,
    minWidth: 100,
    slots: {
      default: ({ row }) =>
        h(VxeButton, {
          circle: true,
          status: row.isLocked ? "success" : `warning`,
          icon: row.isLocked ? "vxe-icon-lock-fill" : `vxe-icon-unlock-fill`,
          onClick: () => handleChangeStatus(row)
        })
    }
  },
  {
    title: "备注",
    field: "remark",
    minWidth: 250
  }
];
const formRef = ref();

const handleInitialFormParams = () => ({
  name: "",
  version: null,
  isLocked: null
});
const formItems = [
  {
    field: "name",
    title: "模板名称",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "模板名称" } }
  },
  {
    field: "version",
    title: "版本",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "版本" } }
  },
  {
    field: "isLocked",
    title: "是否锁定",
    span: 6,
    itemRender: {
      name: "$select",
      props: {
        options: [
          { label: "全部", value: null },
          { label: "解锁", value: false },
          { label: "锁定", value: true }
        ],
        placeholder: "是否锁定"
      }
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
  name: string;
  version: number | null;
  isLocked: boolean | null;
}>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};

const createModalRef = ref();
const handleAdd = () => {
  createModalRef.value.showAddModal();
};
const handleEdit = (record: Recordable) => {
  if (record.isLocked) {
    message("已锁定的流程无法进行：解锁、删除、编辑操作", { type: "warning" });
    return false;
  }
  createModalRef.value.showEditModal(record);
};
const handleDelete = async (record: Recordable) => {
  if (record.isLocked) {
    message("已锁定的流程无法进行：解锁、删除、编辑操作", { type: "warning" });
    return false;
  }
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
const handleChangeStatus = async (record: Recordable) => {
  if (record.isLocked) {
    message("已锁定的流程无法进行：解锁、删除、编辑操作", { type: "warning" });
    return false;
  }
  await lock(record.id);
  handleSearch();
};
const functions: Record<string, string> = {
  add: "system.user.add",
  edit: "system.user.edit",
  view: "system.user.view",
  delete: "system.user.delete"
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
