<script lang="ts" setup>
import { reactive, ref, h } from 'vue';
import { getPageList, deleteData, stop, normal } from '#/api/system/user';
import CreateModal from './CreateModal.vue';
import { VxeButton } from 'vxe-pc-ui';
import type { CommonOperationType } from '#/components/grid';
import { $t } from '@vben/locales';
import { deleteConfirm } from '#/components/modal';
const reVxeGridRef = ref();
const columns = [
  { type: 'checkbox', title: '', width: 60, align: 'center' },
  {
    title: $t('user.columns.name'),
    field: 'name',
    minWidth: 100,
  },
  {
    title: $t('user.columns.account'),
    field: 'account',
    minWidth: 100,
  },
  {
    title: $t('user.columns.organization'),
    field: 'organizationName',
    minWidth: 100,
  },
  {
    title: $t('user.columns.role'),
    field: 'roleName',
    minWidth: 100,
  },
  {
    title: $t('user.columns.phone'),
    field: 'telephone',
    minWidth: 100,
  },
  {
    title: $t('user.columns.email'),
    field: 'email',
    minWidth: 150,
  },
  {
    title: $t('user.columns.status'),
    field: 'status',
    minWidth: 150,
    slots: {
      default: ({ row }: { row: any }) =>
        row.status === 0
          ? h(
              VxeButton,
              {
                status: 'success',
                mode: 'text',
                onClick: () => {
                  handleChangeStatus(row);
                },
              },
              { default: () => $t('common.enable') },
            )
          : h(
              VxeButton,
              {
                status: 'danger',
                mode: 'text',
                onClick: () => {
                  handleChangeStatus(row);
                },
              },
              { default: () => $t('common.disable') },
            ),
    },
  },
  {
    title: $t(`user.columns.remark`),
    field: 'remark',
    minWidth: 150,
  },
];

const handleInitialFormParams = () => ({
  name: '',
  account: '',
  status: null,
});
const formItems = [
  {
    field: 'name',
    title: $t('user.search.name'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('user.search.name') },
    },
  },
  {
    field: 'account',
    title: $t('user.search.account'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('user.search.account') },
    },
  },
  {
    field: 'status',
    title: $t('user.search.status'),
    itemRender: {
      name: '$select',
      props: {
        options: [
          { label: $t(`common.all`), value: null },
          { label: $t(`common.enable`), value: '0' },
          { label: $t(`common.disable`), value: '1' },
        ],
        placeholder: $t('common.all'),
      },
    },
  },
];
const formData = reactive<{
  name: string;
  account: string;
  status: number | null;
}>(handleInitialFormParams());

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
const handleChangeStatus = async (record: any) => {
  if (record.status == 0) {
    await stop(record.id);
  } else {
    await normal(record.id);
  }
  handleSearch();
};
const commonOperation: CommonOperationType = {
  add: {
    permissionCode: 'system.user.add',
    handleClick: handleAdd,
  },
  view: {
    permissionCode: 'system.user.view',
    handleClick: handleView,
  },
  edit: {
    permissionCode: 'system.user.edit',
    handleClick: handleEdit,
  },
  delete: {
    permissionCode: 'system.user.delete',
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
    <CreateModal ref="createModalRef" @reload="handleSearch" />
  </re-page>
</template>
