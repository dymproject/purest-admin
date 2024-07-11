<script lang="ts" setup>
import { ref, h } from "vue";
import { getSelfPageList } from "@/api/workflow/instance";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeButton, VxeTag } from "vxe-pc-ui";
import { hasAuth } from "@/router/utils";

const statusOptions = [
  {
    label: "运行中",
    value: 0
  },
  {
    label: "已挂起",
    value: 1
  },
  {
    label: "已完成",
    value: 2
  },
  {
    label: "已终止",
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
      default: ({ row }) => {
        const label = statusOptions.find(x => x.value == row.status)?.label;
        switch (row.status) {
          case 0:
            return h(VxeTag, { status: "primary", content: label });
          case 1:
            return h(VxeTag, { status: "warning", content: label });
          case 2:
            return h(VxeTag, { status: "success", content: label });
          case 3:
            return h(VxeTag, { status: "error", content: label });
        }
      }
    }
  },
  {
    title: "当前节点",
    field: "currentNodeName",
    minWidth: 100
  },
  {
    title: "节点状态",
    field: "currentNodeStatusString",
    minWidth: 100
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
  workflowStatus: null
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
        clearable: true,
        placeholder: "流程状态"
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
  workflowStatus: number | null;
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
    width: 200,
    slots: {
      default: ({ row }) => [
        hasAuth("system.notice.add")
          ? h(VxeButton, {
              status: "warning",
              mode: "text",
              icon: "vxe-icon-file-txt",
              content: "审批记录",
              onClick: handleView
            })
          : null,
        hasAuth("system.notice.add")
          ? h(VxeButton, {
              status: "primary",
              icon: "vxe-icon-edit",
              mode: "text",
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
