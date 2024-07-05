<script lang="ts" setup>
import { ref, h } from "vue";
import { getSelfPageList } from "@/api/workflow/instance";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeButton } from "vxe-pc-ui";
import { hasAuth } from "@/router/utils";

const statusOptions = [
  {
    label: "Runnable",
    value: 0
  },
  {
    label: "Suspended",
    value: 1
  },
  {
    label: "Complete",
    value: 2
  },
  {
    label: "Terminated",
    value: 3
  }
];
const reVxeGridRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "流程描述",
    field: "description",
    minWidth: 100
  },
  {
    title: "创建时间",
    field: "createTime",
    minWidth: 100
  },
  {
    title: "流程状态",
    field: "status",
    minWidth: 100,
    slots: {
      default: ({ row }): any =>
        statusOptions.find(x => x.value == row.status)?.label
    }
  },
  {
    title: "版本",
    field: "version",
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
  title: "",
  level: null,
  noticeType: null
});
const formItems = [
  {
    field: "workflowStatus",
    title: "流程状态",
    span: 6,
    itemRender: {
      name: "$select",
      props: {
        options: statusOptions,
        placeholder: "工作流状态"
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
const formData = ref<{
  title: string;
  level: number | null;
  noticeType: number | null;
}>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};

const handleReset = () => {
  formData.value = handleInitialFormParams();
};

const createModalRef = ref();
const handleAdd = () => {
  createModalRef.value.showAddModal();
};

const handleView = (record: Recordable) => {
  createModalRef.value.showViewModal(record);
};
// const functions: Record<string, string> = {
//   add: "system.notice.add",
//   edit: "system.notice.edit",
//   view: "system.notice.view",
//   delete: "system.notice.delete"
// };
const customTableActions = [
  {
    title: "操作",
    field: "operate",
    align: "center",
    fixed: `right`,
    width: 180,
    slots: {
      default: ({ row }) => [
        hasAuth("system.notice.add")
          ? h(VxeButton, {
              status: "error",
              type: "text",
              icon: "vxe-icon-file-txt",
              content: "查看",
              onClick: handleView
            })
          : null,
        hasAuth("system.notice.add")
          ? h(VxeButton, {
              status: "primary",
              icon: "vxe-icon-edit",
              type: "text",
              content: "重新发起",
              onClick: handleAdd
            })
          : null
      ]
    }
  }
];

const toolbarConfig = {
  slots: {
    buttons: () => [
      hasAuth("system.notice.add")
        ? h(VxeButton, {
            icon: "vxe-icon-add",
            status: "primary",
            content: "发起流程",
            onClick: handleAdd
          })
        : null
    ]
  },
  custom: true
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
        @reset="handleReset"
      />
    </el-card>
    <el-card :shadow="`never`" class="table-card">
      <ReVxeGrid
        ref="reVxeGridRef"
        :request="getSelfPageList"
        :searchParams="formData"
        :columns="columns"
        :customToolbarActions="toolbarConfig"
        :customTableActions="customTableActions"
      />
    </el-card>
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </div>
</template>
