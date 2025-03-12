<script lang="ts" setup>
import { h, reactive, ref } from 'vue';
import { $t } from '@vben/locales';
import type { CommonOperationType } from '#/components/grid';
import { getPageList, deleteData } from '#/api/system/function';
import { VxeButton } from 'vxe-pc-ui';
import { deleteConfirm } from '#/components/modal';
import CreateModal from './CreateModal.vue';
import InterfaceModal from './InterfaceModal.vue';

const createModalRef = ref();
const interfaceModalRef = ref();
const reVxeGridRef = ref();
const columns = [
  { type: 'checkbox', title: '', width: 60, align: 'center' },
  {
    title: 'Id',
    field: 'id',
    visible: false,
  },
  {
    title: $t('function.columns.name'),
    field: 'name',
    treeNode: true,
  },
  {
    title: $t('function.columns.code'),
    field: 'code',
  },
  {
    title: $t('function.columns.service'),
    field: 'interfaces',
    align: 'center',
    width: 100,
    slots: {
      default: ({ row }: { row: any }) =>
        h(VxeButton, {
          mode: 'text',
          status: 'warning',
          content: $t('common.view'),
          onClick() {
            interfaceModalRef.value.showInterface(row);
          },
        }),
    },
  },
  {
    title: $t('function.columns.remark'),
    field: 'remark',
  },
];
const handleInitialFormParams = () => ({
  name: '',
});
const formItems = [
  {
    field: 'name',
    title: $t('function.form.name'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('function.form.name') },
    },
  },
];
const formData = reactive<{ name: string }>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};
const searchOptions = {
  formData,
  formItems,
  submit: handleSearch,
  reset: handleInitialFormParams,
};

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
    permissionCode: 'system.function.add',
    handleClick: handleAdd,
  },
  view: {
    permissionCode: 'system.function.view',
    handleClick: handleView,
  },
  edit: {
    permissionCode: 'system.function.edit',
    handleClick: handleEdit,
  },
  delete: {
    permissionCode: 'system.function.delete',
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
      :columns="columns"
      :searchOptions="searchOptions"
    />
    <CreateModal ref="createModalRef" @reload="handleSearch" />
    <InterfaceModal ref="interfaceModalRef" />
  </re-page>
</template>
