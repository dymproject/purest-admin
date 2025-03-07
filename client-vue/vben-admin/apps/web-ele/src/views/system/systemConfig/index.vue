<script lang="ts" setup>
import { reactive, ref } from 'vue';
import { getPageList, deleteData } from '#/api/system/systemConfig';
import CreateModal from './CreateModal.vue';
import { deleteConfirm } from '#/components/modal';
import { $t } from '@vben/locales';
import type { CommonOperationType } from '#/components/grid';

const reVxeGridRef = ref();
const columns = [
  { type: 'checkbox', title: '', width: 60, align: 'center' },
  {
    title: $t('systemConfig.columns.name'),
    field: 'name',
    minWidth: 100,
  },
  {
    title: $t('systemConfig.columns.configCode'),
    field: 'configCode',
    minWidth: 100,
  },
  {
    title: $t('systemConfig.columns.configValue'),
    field: 'configValue',
    minWidth: 200,
  },
  {
    title: $t('systemConfig.columns.remark'),
    field: 'remark',
    minWidth: 150,
  },
];

const handleInitialFormParams = () => ({
  name: '',
  configCode: '',
});
const formItems = [
  {
    field: 'name',
    title: $t('systemConfig.search.name'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('systemConfig.search.name') },
    },
  },
  {
    field: 'configCode',
    title: $t('systemConfig.search.configCode'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('systemConfig.search.configCode') },
    },
  },
];
const formData = reactive<{ name: string; configCode: string }>(
  handleInitialFormParams(),
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
    permissionCode: 'system.systemconfig.add',
    handleClick: handleAdd,
  },
  view: {
    permissionCode: 'system.systemconfig.view',
    handleClick: handleView,
  },
  edit: {
    permissionCode: 'system.systemconfig.edit',
    handleClick: handleEdit,
  },
  delete: {
    permissionCode: 'system.systemconfig.delete',
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
