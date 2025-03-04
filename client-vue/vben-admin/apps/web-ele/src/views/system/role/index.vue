<script lang="ts" setup>
import { h, reactive, ref } from 'vue';
import { getPageList, deleteData } from '#/api/system/role';
import { VxeButton } from 'vxe-pc-ui';
import FunModal from './FunctionModal.vue';
import CreateModal from './CreateModal.vue';
import { deleteConfirm } from '#/components/modal';
import { $t } from '@vben/locales';
import type { CommonOperationType } from '#/components/grid';

const funModalRef = ref();
const reVxeGridRef = ref();
const columns = [
  { type: 'checkbox', title: '', width: 60, align: 'center' },
  {
    title: 'Id',
    field: 'id',
    minWidth: 100,
  },
  {
    title: $t('role.columns.name'),
    field: 'name',
    minWidth: 100,
  },
  {
    title: $t('role.columns.description'),
    field: 'description',
    minWidth: 150,
  },
  {
    title: $t('role.columns.remark'),
    field: 'remark',
    minWidth: 150,
  },
  {
    title: $t('role.columns.functions'),
    field: 'functions',
    align: 'center',
    width: 100,
    slots: {
      default: ({ row }: { row: any }) =>
        h(VxeButton, {
          size: 'small',
          mode:'text',
          content: $t('common.view'),
          status: 'success',
          onClick() {
            funModalRef.value.showFunction(row);
          },
        }),
    },
  },
];
const formItems = [
  {
    field: 'name',
    title: $t('role.search.name'),
    span: 6,
    itemRender: { name: '$input', props: { placeholder: $t('role.search.name') } },
  }  
];
const handleInitialFormParams = () => ({
  name: '',
});
const formData = reactive<{ name: string }>(handleInitialFormParams());

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
    permissionCode: 'system.role.add',
    handleClick: handleAdd,
  },
  view: {
    permissionCode: 'system.role.view',
    handleClick: handleView,
  },
  edit: {
    permissionCode: 'system.role.edit',
    handleClick: handleEdit,
  },
  delete: {
    permissionCode: 'system.role.delete',
    handleClick: handleDelete,
  },
};
</script>
<template>
   <re-page>
    <ReVxeGrid
      ref="reVxeGridRef"
      :request="getPageList"
      :commonOperation="commonOperation"
      :formData="formData"
      :formItems="formItems"
      :columns="columns"
      @handleSearch="handleSearch"
      @handleReset="handleInitialFormParams"
    />
    <FunModal ref="funModalRef" />
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </re-page>  
</template>
