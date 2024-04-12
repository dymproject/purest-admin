<script setup lang="ts">
import { h, reactive, ref } from "vue";
import {
  getCategoryPageList,
  getPageList,
  deleteData
} from "@/api/system/dictonary";
import { ReVxeGrid } from "@/components/ReVxeTable";
import { VxeButton, VxeGridPropTypes, VXETable } from "vxe-table";
import CreateModal from "./CreateModal.vue";

import { hasAuth } from "@/router/utils";
import { message } from "@/utils/message";

const reVxeGridRef = ref();
const reVxeGridRef2 = ref();
const columns: VxeGridPropTypes.Columns<any> = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "Id",
    field: "id",
    visible: false,
    minWidth: 100
  },
  {
    title: "分类名称",
    field: "name",
    minWidth: 100
  },
  {
    title: "分类编码",
    field: "code",
    minWidth: 100
  },
  {
    title: "备注",
    field: "remark",
    minWidth: 100
  },
  {
    title: "操作",
    field: "view",
    align: "center",
    minWidth: 100,
    slots: {
      default: ({ row }) => [
        hasAuth("system.dictionary.view")
          ? h(VxeButton, {
              status: "error",
              type: "text",
              icon: "vxe-icon-file-txt",
              content: "查看",
              onClick() {
                handleLoadDictData(row);
              }
            })
          : null
      ]
    }
  }
];

const columns2: VxeGridPropTypes.Columns<any> = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: "Id",
    field: "id",
    visible: false,
    minWidth: 100
  },
  {
    title: "名称",
    field: "name",
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
  name: ""
});
const formItems = [
  {
    field: "name",
    title: "字典类别名称",
    span: 6,
    itemRender: { name: "$input", props: { placeholder: "字典名称" } }
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
  checkedCategory.value = defaultCheckedCategory();
  reVxeGridRef2.value.loadData();
  reVxeGridRef.value.loadData();
};

const createModalRef = ref();
const handleAdd = () => {
  if (checkedCategory.value.id == 0) {
    message("请选择字典类别");
    return;
  }
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
const dictDataSearchParams = ref<{ categoryId: number }>({ categoryId: 0 });
const defaultCheckedCategory = () => {
  return { id: 0, name: "" };
};
const checkedCategory = ref<{ name: string; id: number }>(
  defaultCheckedCategory()
);
const handleLoadDictData = (record: any) => {
  checkedCategory.value = record;
  dictDataSearchParams.value.categoryId = record.id;
  reloadDictData();
};

const reloadDictData = () => {
  reVxeGridRef2.value.loadData();
};

const functions: Record<string, string> = {
  edit: "system.dictionary.edit",
  delete: "system.dictionary.delete"
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
    <el-row>
      <el-col :span="10">
        <el-card :shadow="`never`" class="table-card">
          <template #header>
            <div class="card-header">
              <span>字典类别</span>
            </div>
          </template>
          <ReVxeGrid
            ref="reVxeGridRef"
            :request="getCategoryPageList"
            :functions="functions"
            :searchParams="formData"
            :columns="columns"
            :customTableActions="[]"
            :customToolbarActions="[]"
            @handleAdd="handleAdd"
            @handleEdit="handleEdit"
            @handleDelete="handleDelete"
          />
        </el-card>
      </el-col>
      <el-col :span="14">
        <el-card :shadow="`never`" class="table-card">
          <template #header>
            <div>
              <span>
                <span style="color: red">{{ checkedCategory.name }} </span>详情
              </span>
              <vxe-button
                v-auth="`system.dictionary.add`"
                icon="vxe-icon-add"
                style="float: right"
                size="mini"
                title="添加字典数据"
                circle
                @click="handleAdd"
              />
            </div>
          </template>
          <ReVxeGrid
            ref="reVxeGridRef2"
            :request="getPageList"
            :functions="functions"
            :searchParams="dictDataSearchParams"
            :columns="columns2"
            :customToolbarActions="[]"
            :operateColumnWidth="320"
            @handleEdit="handleEdit"
            @handleDelete="handleDelete"
          />
        </el-card>
      </el-col>
    </el-row>
    <CreateModal
      ref="createModalRef"
      :categoryId="dictDataSearchParams.categoryId"
      @reload="reloadDictData"
    />
  </div>
</template>
