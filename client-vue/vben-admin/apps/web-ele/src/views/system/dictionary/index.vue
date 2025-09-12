<script setup lang="ts">
import { h, reactive, ref } from 'vue';
import {
  getCategoryPageList,
  getPageList,
  deleteData,
} from '#/api/system/dictionary';
import { ElCard } from 'element-plus';
import { VxeButton } from 'vxe-pc-ui';
import CreateModal from './CreateModal.vue';
import { deleteConfirm } from '#/components/modal';
import type { CommonOperationType } from '#/components/grid';
import { ElMessage } from 'element-plus';
import { $t } from '@vben/locales';
const reVxeGridRef = ref();
const reVxeGridRef2 = ref();
const columns = [
  {
    title: 'Id',
    field: 'id',
    visible: false,
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns.name'),
    field: 'name',
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns.code'),
    field: 'code',
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns.remark'),
    field: 'remark',
    minWidth: 100,
  },
  {
    title: $t('common.operation'),
    field: 'view',
    align: 'center',
    minWidth: 100,
    slots: {
      default: ({ row }: { row: any }) =>
        h(VxeButton, {
          status: 'error',
          mode: 'text',
          icon: 'vxe-icon-file-txt',
          content: $t('common.view'),
          onClick() {
            handleLoadDictData(row);
          },
        }),
    },
  },
];

const columns2 = [
  {
    title: 'Id',
    field: 'id',
    visible: false,
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns2.name'),
    field: 'name',
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns2.name'),
    field: 'name',
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns2.code'),
    field: 'code',
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns2.sort'),
    field: 'sort',
    minWidth: 100,
  },
  {
    title: $t('dictionary.columns2.remark'),
    field: 'remark',
    minWidth: 150,
  },
];
const handleInitialFormParams = () => ({
  name: '',
});
const formItems = [
  {
    field: 'name',
    title: $t('dictionary.search.name'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('dictionary.search.name') },
    },
  },
];
const formData = reactive<{ name: string }>(handleInitialFormParams());
const handleSearch = () => {
  checkedCategory.value = defaultCheckedCategory();
  reVxeGridRef2.value.loadData();
  reVxeGridRef.value.loadData();
};
const searchOptions = {
  formData,
  formItems,
  submit: handleSearch,
  reset: handleInitialFormParams,
};
const createModalRef = ref();
const handleAdd = () => {
  if (checkedCategory.value.id == 0) {
    ElMessage.warning($t('dictionary.tooltip.checkCategory'));
    return;
  }
  createModalRef.value.showAddModal();
};
const handleEdit = (record: any) => {
  createModalRef.value.showEditModal(record);
};
const handleDelete = async (record: any) => {
  if (await deleteConfirm()) {
    deleteData(record.id).then(() => {
      reloadDictData(checkedCategory.value.id);
    });
  }
};
const defaultCheckedCategory = () => {
  return { id: 0, name: '' };
};
const checkedCategory = ref<{ name: string; id: number }>(
  defaultCheckedCategory(),
);
const handleLoadDictData = (record: any) => {
  checkedCategory.value = record;
  reloadDictData(record.id);
};

const reloadDictData = (categoryId: number) => {
  reVxeGridRef2.value.loadData({ categoryId });
};

const commonOperation: CommonOperationType = {
  add: {
    permissionCode: 'system.dictionary.add',
    handleClick: handleAdd,
  },
  edit: {
    permissionCode: 'system.dictionary.edit',
    handleClick: handleEdit,
  },
  delete: {
    permissionCode: 'system.dictionary.delete',
    handleClick: handleDelete,
  },
};
</script>

<template>
  <re-page>
    <div style="display: flex; width: 100%">
      <el-card :header="$t('dictionary.category')" style="width: 40%">
        <re-vxe-grid
          ref="reVxeGridRef"
          :request="getCategoryPageList"
          :columns="columns"
          :searchOptions="searchOptions"
        />
      </el-card>
      <el-card
        :header="
          $t(`dictionary.data`) +
          `${checkedCategory.name != '' ? '：' : ''}${checkedCategory.name}`
        "
        style="width: 60%"
      >
        <re-vxe-grid
          ref="reVxeGridRef2"
          :request="getPageList"
          :commonOperation="commonOperation"
          :columns="columns2"
        />
      </el-card>
      <CreateModal
        ref="createModalRef"
        :categoryId="checkedCategory.id"
        @reload="reloadDictData(checkedCategory.id)"
      />
    </div>
  </re-page>
</template>
