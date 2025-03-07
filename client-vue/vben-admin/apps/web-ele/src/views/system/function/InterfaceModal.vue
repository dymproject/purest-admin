<script lang="ts" setup>
import { ref, onMounted, reactive, h, nextTick, type VNode } from 'vue';
import { VxeButton, VxeSwitch } from 'vxe-pc-ui';
import { getPageList, asyncApi } from '#/api/system/interface';
import { ElLoading, ElCard } from 'element-plus';
import {
  getFunInterface,
  assignInterface,
  removeInterface,
} from '#/api/system/function';
import { $t } from '@vben/locales';
import { useAccess } from '@vben/access';
const { hasAccessByCodes } = useAccess();
import type { VxeGridPropTypes } from 'vxe-table';
const reModalRef = ref();
const reVxeGridRef = ref();

const loadMyInterfaceData = async () => {
  myInterfaceData.value = await getFunInterface(functionId.value);
};

const checkRowKeys = ref([]);
const myInterfaceData = ref([]);
const functionId = ref();
const showInterface = (record: any) => {
  checkRowKeys.value = [];
  reModalRef.value.show(
    $t('function.columns.functions') + '->' + record.name,
    true,
  );
  functionId.value = record.id;
  formData.path = '';
  nextTick(() => {
    loadMyInterfaceData();
    reVxeGridRef.value.loadData();
  });
};
const columns = [
  {
    title: 'Id',
    field: 'id',
    visible: false,
  },
  {
    title: $t('function.interfaceModal.name'),
    field: 'name',
    treeNode: true,
    minWidth: 100,
  },
  {
    title: $t('function.interfaceModal.api'),
    field: 'path',
    minWidth: 200,
  },
  {
    title: $t('function.interfaceModal.method'),
    field: 'requestMethod',
    minWidth: 60,
  },
  {
    title: '#',
    field: 'operate',
    align: 'center',
    minWidth: 60,
    slots: {
      default: ({ row }: { row: any }) => {
        if (!row.path) return null;
        row.isBind = myInterfaceData.value
          .map((x: any) => x.interfaceId)
          .includes(row.id)
          ? true
          : false;
        return !row.interfaces &&
          hasAccessByCodes(['system.function.bind']) &&
          hasAccessByCodes(['system.function.unbind'])
          ? h(VxeSwitch, {
              modelValue: row.isBind,
              size: 'medium',
              openLabel: $t('function.interfaceModal.bound'),
              closeLabel: $t('function.interfaceModal.unbound'),
              onChange(value) {
                assignInterface({
                  functionId: functionId.value,
                  interfaceId: row.id,
                }).then(async () => {
                  await loadMyInterfaceData();
                  row.isBind = value;
                });
              },
            })
          : null;
      },
    },
  },
];
const customToolbarActions: VxeGridPropTypes.ToolbarConfig = {
  slots: {
    buttons: () => {
      const buttons: VNode[] = [];
      if (hasAccessByCodes(['system.function.synchronization'])) {
        buttons.push(
          h(VxeButton, {
            icon: 'vxe-icon-refresh',
            status: 'success',
            content: $t('function.interfaceModal.synchronization'),
            onClick: handleAsyncApi,
          }),
        );
      }
      return buttons;
    },
  },
  custom: true,
};
const handleInitialFormParams = () => ({
  path: '',
});
const handleAsyncApi = () => {
  const loading = ElLoading.service({
    lock: true,
    text: $t('common.ing'),
    background: 'rgba(0, 0, 0, 0.7)',
  });
  asyncApi()
    .then((_) => {
      reVxeGridRef.value.loadData();
    })
    .finally(() => {
      loading.close();
    });
};
const formItems = [
  {
    field: 'path',
    title: $t('function.interfaceModal.api'),
    itemRender: {
      name: '$input',
      props: { placeholder: $t('function.interfaceModal.api') },
    },
  },
];

const formData = reactive<{ path: string }>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};
const handleRemoveInterface = (row: any) => {
  removeInterface(row.id).then(() => {
    loadMyInterfaceData();
    reVxeGridRef.value.loadData();
  });
};
onMounted(() => {});

defineExpose({ showInterface });
</script>
<template>
  <re-modal :height="800" :width="1400" ref="reModalRef">
    <div style="display: flex; width: 100%">
      <el-card
        :header="$t('function.interfaceModal.boundInterface')"
        style="width: 40%"
      >
        <vxe-table round size="mini" height="510" :data="myInterfaceData">
          <vxe-column
            field="name"
            width="180"
            :title="$t('function.interfaceModal.name')"
          />
          <vxe-column
            field="path"
            width="180"
            :title="$t('function.interfaceModal.api')"
          />
          <vxe-column
            field="remove"
            header-align="center"
            align="center"
            title="#"
          >
            <template #default="{ row }">
              <vxe-button
                v-if="hasAccessByCodes(['system.function.unbind'])"
                status="error"
                @click="handleRemoveInterface(row)"
              >
                {{ $t('function.interfaceModal.remove') }}
              </vxe-button>
            </template>
          </vxe-column>
        </vxe-table>
      </el-card>
      <el-card :header="$t('function.interfaceModal.list')" style="width: 60%">
        <re-vxe-grid
          ref="reVxeGridRef"
          :height="430"
          :customToolbarActions="customToolbarActions"
          :treeConfig="{ rowField: 'id', childrenField: 'interfaces' }"
          :request="getPageList"
          :formData="formData"
          :formItems="formItems"
          :columns="columns"
          @handleSearch="handleSearch"
          @handleReset="handleInitialFormParams"
        />
      </el-card>
    </div>
  </re-modal>
</template>
