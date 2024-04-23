<script lang="ts" setup>
import { reactive, ref, h } from "vue";
import { getPageList, deleteData } from "@/api/system/notice";
import { ReVxeGrid } from "@/components/ReVxeTable";
import CreateModal from "./CreateModal.vue";
import { VxeGridPropTypes, VXETable } from "vxe-table";
import { ReDictionary } from "@/components/ReDictionary";
const reVxeGridRef = ref();
const columns: VxeGridPropTypes.Columns<any> = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "标题",
    field: "title",
    minWidth: 100
  },
  {
    title: "内容",
    field: "content",
    minWidth: 100
  },
  {
    title: "类型",
    field: "noticeTypeString",
    minWidth: 100
  },
  {
    title: "级别",
    field: "levelString",
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
    field: "title",
    title: "标题",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "标题" } }
  },
  {
    field: "noticeType",
    title: "类型",
    span: 6,
    slots: {
      default: ({ data }) => {
        return h(ReDictionary, {
          code: "dict_notice_type",
          modelValue: data.noticeType,
          placeholder: "请选择类型",
          onChange({ value }) {
            data.noticeType = value;
          }
        });
      }
    }
  },
  {
    field: "level",
    title: "级别",
    span: 6,
    slots: {
      default: ({ data }) => {
        return h(ReDictionary, {
          code: "dict_notice_level",
          modelValue: data.level,
          placeholder: "请选择级别",
          onChange({ value }) {
            data.level = value;
          }
        });
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
  add: "system.notice.add",
  edit: "system.notice.edit",
  view: "system.notice.view",
  delete: "system.notice.delete"
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
