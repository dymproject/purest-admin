<script lang="ts" setup>
import type { VxeGridPropTypes, VxePagerEvents } from 'vxe-table';
import { ElCard } from 'element-plus';
import { VxeButton } from 'vxe-pc-ui';
import type { PurestGridProps } from './types';
import { h, onMounted, ref, type VNode } from 'vue';
import { useAccess } from '@vben/access';
import { $t } from '#/locales';
import './style.css';

const { hasAccessByCodes } = useAccess();
const emit = defineEmits<{
  (e: 'handleSearch'): void;
  (e: 'handleReset'): void;
}>();
const props = withDefaults(defineProps<PurestGridProps>(), {
  rowKey: `id`,
  size: `small`,
  formData: {},
  customePager: () => ({
    total: 0,
    pageIndex: 1,
    pageSize: 15,
  }),
});
const formActions = [
  {
    itemRender: {
      name: '$buttons',
      children: [
        {
          props: {
            type: 'submit',
            icon: 'vxe-icon-search',
            content: $t('common.search'),
            status: 'primary',
          },
        },
        {
          props: {
            type: 'reset',
            icon: 'vxe-icon-undo',
            content: $t(`common.reset`),
          },
        },
      ],
    },
  },
];
const items = props.formItems.concat(formActions);
const operateColumns: VxeGridPropTypes.Columns<any> =
  props.commonOperation == null || props.commonOperation == undefined
    ? []
    : [
        {
          title: $t('common.operation'),
          field: 'operation',
          align: 'center',
          fixed: `right`,
          width: 210,
          slots: {
            default: ({ row }) => {
              const buttons: VNode[] = [];
              if (props.commonOperation) {
                Object.keys(props.commonOperation).forEach((key) => {
                  const operation =
                    props.commonOperation![key as 'view' | 'edit' | 'delete'];
                  if (hasAccessByCodes([operation!.permissionCode])) {
                    switch (key) {
                      case 'view':
                        buttons.push(
                          h(VxeButton, {
                            status: 'warning',
                            mode: 'text',
                            icon: 'vxe-icon-file-txt',
                            content: $t('common.view'),
                            onClick() {
                              operation!.handleClick(row);
                            },
                          }),
                        );
                        break;
                      case 'edit':
                        buttons.push(
                          h(VxeButton, {
                            status: 'primary',
                            icon: 'vxe-icon-edit',
                            mode: 'text',
                            content: $t('common.edit'),
                            onClick() {
                              operation!.handleClick(row);
                            },
                          }),
                        );
                        break;
                      case 'delete':
                        buttons.push(
                          h(VxeButton, {
                            status: 'danger',
                            mode: 'text',
                            icon: 'vxe-icon-delete',
                            content: $t('common.del'),
                            onClick() {
                              operation!.handleClick(row);
                            },
                          }),
                        );
                        break;
                    }
                  }
                });
              }
              return buttons;
            },
          },
        },
      ];
const columns = [...props.columns, ...operateColumns];
const toolbarConfig: VxeGridPropTypes.ToolbarConfig =
  props.customToolbarActions ?? {
    slots: {
      buttons: () => {
        const buttons: VNode[] = [];
        if (props.commonOperation) {
          Object.keys(props.commonOperation).forEach((key) => {
            const operation =
              props.commonOperation![key as 'add' | 'export' | 'import'];
            if (hasAccessByCodes([operation!.permissionCode])) {
              switch (key) {
                case 'add':
                  buttons.push(
                    h(VxeButton, {
                      icon: 'vxe-icon-add',
                      status: 'primary',
                      content: $t('common.add'),
                      onClick() {
                        operation!.handleClick();
                      },
                    }),
                  );
                  break;
              }
            }
          });
        }
        return buttons;
      },
    },
    custom: true,
  };
const treeOption = props.treeConfig ?? {};
const data = ref<[]>([]);
const loading = ref(false);

const loadData = async () => {
  props.request({ ...props.customePager, ...props.formData }).then((result) => {
    const { pageIndex, total, items } = result;
    data.value = items;
    props.customePager.total = total;
    props.customePager.pageIndex = pageIndex;
  });
};
const handlePageChange: VxePagerEvents.PageChange = ({
  currentPage,
  pageSize,
}) => {
  props.customePager.pageIndex = currentPage;
  props.customePager.pageSize = pageSize;
  loadData();
};
onMounted(() => {
  loadData();
});
defineExpose({ loadData });
</script>
<template>
  <div>
    <el-card>
      <vxe-form
        ref="formRef"
        :size="props.size"
        :data="props.formData"
        :items="items"
        @submit="emit('handleSearch')"
        @reset="emit('handleReset')"
      />
      <slot name="searchForm"></slot>
    </el-card>
    <el-card class="table-card">
      <vxe-grid
        :round="true"
        :columns="columns"
        :data="data"
        :height="props.height"
        :loading="loading"
        :max-height="650"
        :size="props.size"
        :resizable="true"
        :row-config="{ keyField: rowKey, isHover: true }"
        :toolbar-config="toolbarConfig"
        :tree-config="treeOption"
        :pager-config="{
          pageSizes: [15, 30, 50, 100],
          size: props.size,
          total: customePager.total,
          pageSize: customePager.pageSize,
          currentPage: customePager.pageIndex,
        }"
        @page-change="handlePageChange"
      >
      </vxe-grid>
    </el-card>
  </div>
</template>
