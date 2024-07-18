<script lang="ts" setup>
import { ref, h } from "vue";
import { getAuditingPageList } from "@/api/workflow/instance";
import { ReVxeGrid } from "@/components/ReVxeTable";
import AuditingOperateModal from "./AuditingOperateModal.vue";
import { VxeButton } from "vxe-pc-ui";
import { hasAuth } from "@/router/utils";
const reVxeGridRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "流程名称",
    field: "description",
    minWidth: 100
  },
  {
    title: "发起人",
    field: "createByName",
    minWidth: 100
  },
  {
    title: "流程版本",
    field: "version",
    minWidth: 100
  },
  {
    title: "发起时间",
    field: "createTime",
    minWidth: 100
  }
];
const formRef = ref();

const handleInitialFormParams = () => ({
  createBy: null,
  status: 0
});
const formItems = [
  {
    field: "createBy",
    title: "发起人",
    span: 6,
    itemRender: {
      name: "VxeInput",
      props: {
        placeholder: "发起人"
      }
    }
  },
  {
    field: "status",
    title: "流程状态",
    span: 6,
    itemRender: {
      name: "VxeSelect",
      options: [
        { label: `待处理`, value: 0 },
        { label: `已通过`, value: 1 },
        { label: `已驳回`, value: 2 }
      ],
      props: {
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
  createBy: number | null;
  status: number | null;
}>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};

const handleReset = () => {
  formData.value = handleInitialFormParams();
};

const auditingOperateModalRef = ref();

const handleView = (record: Recordable) => {
  auditingOperateModalRef.value.showViewModal(record);
};
const customTableActions = [
  {
    title: "操作",
    field: "operate",
    align: "center",
    fixed: `right`,
    width: 180,
    slots: {
      default: ({ row }) => [
        hasAuth("workflow.auditing.approve")
          ? h(VxeButton, {
              status: "error",
              mode: "text",
              icon: "vxe-icon-file-txt",
              content: "流程审批",
              onClick() {
                handleView(row);
              }
            })
          : h(VxeButton, {
              status: "error",
              mode: "text",
              icon: "vxe-icon-file-txt",
              content: "无权限"
            })
      ]
    }
  }
];
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
        :request="getAuditingPageList"
        :searchParams="formData"
        :columns="columns"
        :customToolbarActions="{}"
        :customTableActions="customTableActions"
      />
    </el-card>
    <AuditingOperateModal
      ref="auditingOperateModalRef"
      @reload="handleSearch"
    />
  </div>
</template>
