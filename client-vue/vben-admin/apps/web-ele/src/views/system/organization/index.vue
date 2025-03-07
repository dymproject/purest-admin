<script lang="ts" setup>
import { reactive, ref } from "vue";
import { getPageList, deleteData } from "#/api/system/organization";
import type { CommonOperationType } from '#/components/grid';
import { deleteConfirm } from '#/components/modal';
import { $t } from '@vben/locales';
import CreateModal from "./CreateModal.vue";
const reVxeGridRef = ref();
const columns = [
  { type: "checkbox", title: "", width: 60, align: "center" },
  {
    title: $t('organization.columns.name'),
    field: "name",
    treeNode: true,
  },
  {
    title: $t('organization.columns.leader'),
    field: "leader",
  },
  {
    title: $t('organization.columns.telephone'),
    field: "telephone",
  },
  {
    title: $t('organization.columns.sort'),
    field: "sort",
  },
  {
    title: $t('organization.columns.remark'),
    field: "remark",
  }
];
const handleInitialFormParams = () => ({
  name: "",
  leader: ""
});
const formItems = [
  {
    field: "name",
    title: $t('organization.search.name'),
    span: 6,
    itemRender: { name: "$input", props: { placeholder: $t('organization.search.name') } }
  }
];
const formData = reactive<{ name: string; leader: string }>(
  handleInitialFormParams()
);

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};

const createModalRef = ref();
const handleAdd = () => {
  createModalRef.value.showAddModal();
};
const handleEdit = (record: any) => {
  createModalRef.value.showEditModal(record);
};
const handleDelete = async (record: any) => {
  if (await deleteConfirm()) {
    deleteData(record.id).then(() => {
      handleSearch();
    });
  }
};
const handleView = (record: any) => {
  createModalRef.value.showViewModal(record);
};
const commonOperation: CommonOperationType = {
  add: {
    permissionCode: 'system.organization.add',
    handleClick: handleAdd,
  },
  view: {
    permissionCode: 'system.organization.view',
    handleClick: handleView,
  },
  edit: {
    permissionCode: 'system.organization.edit',
    handleClick: handleEdit,
  },
  delete: {
    permissionCode: 'system.organization.delete',
    handleClick: handleDelete,
  },
};
</script>
<template>
  <re-page>
    <re-vxe-grid
      ref="reVxeGridRef"
      :request="getPageList"
      :commonOperation="commonOperation"
      :formData="formData"
      :formItems="formItems"
      :columns="columns"
      @handleSearch="handleSearch"
      @handleReset="handleInitialFormParams"
    />
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </re-page>
</template>
