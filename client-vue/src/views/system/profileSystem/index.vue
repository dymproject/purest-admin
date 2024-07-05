<script lang="ts" setup>
import { h, reactive, ref } from "vue";
import { getPageList, deleteData, download } from "@/api/system/profileSystem";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeButton, VxeUI } from "vxe-pc-ui";

const reVxeGridRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "名称",
    field: "name",
    minWidth: 100
  },
  {
    title: "编码",
    field: "code",
    minWidth: 100
  },
  {
    title: "文件名",
    field: "fileName",
    minWidth: 200,
    slots: {
      default({ data, row }) {
        return h(
          VxeButton,
          {
            mode: `text`,
            onClick() {
              download(row.fileId);
            }
          },
          { default: () => row.fileName }
        );
      }
    }
  },
  {
    title: "文件大小（kb）",
    field: "fileSize",
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
  code: ""
});
const formItems = [
  {
    field: "name",
    title: "名称",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "名称" } }
  },
  {
    field: "code",
    title: "编码",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "编码" } }
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
const formData = reactive<{ name: string; code: string }>(
  handleInitialFormParams()
);

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};

const createModalRef = ref();
const handleAdd = () => {
  createModalRef.value.showAddModal();
};
const handleDelete = async (record: Recordable) => {
  const type = await VxeUI.modal.confirm("您确定要删除吗？");
  if (type == "confirm") {
    deleteData(record.id).then(() => {
      handleSearch();
    });
  }
};

const functions: Record<string, string> = {
  add: "system.profilesystem.add",
  delete: "system.profilesystem.delete"
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
        @handleDelete="handleDelete"
      />
    </el-card>
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </div>
</template>
