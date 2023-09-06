<script lang="tsx" setup>
import { useUserStoreHook } from "@/store/modules/user";
import { h, onMounted, reactive, ref } from "vue";
import { VxeButton, VxeGridPropTypes, VxePagerEvents } from "vxe-table";
const currentUser = useUserStoreHook().getCurrentUser;
const props = defineProps<{
  rowKey?: string;
  maxHeight?: number;
  searchParams?: any;
  request?: (params) => Promise<any>;
  columns: Array<any>;
  operateColumnWidth?: number;
  customTableActions?: VxeGridPropTypes.Columns<any>;
  customToolbarActions?: VxeGridPropTypes.ToolbarConfig;
  functions?: Record<string, string>;
  treeConfig?: any;
}>();

const emits = defineEmits<{
  (e: "handleAdd"): void;
  (e: "handleDelete", data: Recordable[]): void;
  (e: "handleEdit", data: Recordable): void;
  (e: "handleView", data: Recordable): void;
}>();
//权限控制
const permissions = currentUser.permissions as string[];
const actionColumns: VxeGridPropTypes.Columns<any> =
  props.customTableActions ?? [
    {
      title: "操作",
      field: "operate",
      align: "center",
      width:
        props.operateColumnWidth ?? Object.keys(props.functions).length * 60,
      slots: {
        default: ({ row }) => [
          permissions.includes(props.functions["view"])
            ? h(VxeButton, {
                status: "error",
                type: "text",
                icon: "vxe-icon-file-txt",
                content: "查看",
                onClick() {
                  emits(`handleView`, row);
                }
              })
            : null,
          permissions.includes(props.functions["edit"])
            ? h(VxeButton, {
                status: "primary",
                icon: "vxe-icon-edit",
                type: "text",
                content: "编辑",
                onClick() {
                  emits("handleEdit", row);
                }
              })
            : null,
          permissions.includes(props.functions["delete"])
            ? h(VxeButton, {
                status: "danger",
                type: "text",
                icon: "vxe-icon-delete",
                content: "删除",
                onClick() {
                  emits(`handleDelete`, row);
                }
              })
            : null
        ]
      }
    }
  ];

const gridColumns = props.columns.concat(actionColumns);
const pager = reactive({
  total: 0,
  pageIndex: 1,
  pageSize: 10
});

const toolbarConfig: VxeGridPropTypes.ToolbarConfig = props.customTableActions
  ? null
  : {
      slots: {
        buttons: () => [
          permissions.includes(props.functions["add"])
            ? h(VxeButton, {
                icon: "vxe-icon-add",
                status: "primary",
                content: "添加数据",
                onClick() {
                  emits(`handleAdd`);
                }
              })
            : null
        ]
      },
      custom: true
    };

const treeOption = props.treeConfig ?? {};

const data = ref<[]>([]);
const loading = ref(false);

const loadData = async () => {
  loading.value = true;
  props.request({ ...pager, ...props.searchParams }).then(result => {
    loading.value = false;
    const { pageIndex, total, items } = result;
    data.value = items;
    pager.total = total;
    pager.pageIndex = pageIndex;
  });
};
const handlePageChange: VxePagerEvents.PageChange = ({
  pageIndex,
  pageSize
}) => {
  pager.pageIndex = pageIndex;
  pager.pageSize = pageSize;
  loadData();
};
onMounted(() => {
  loadData();
});
defineExpose({ loadData });
</script>
<template>
  <vxe-grid
    :max-height="props.maxHeight ?? 680"
    :columns="gridColumns"
    :tree-config="treeOption"
    :data="data"
    :loading="loading"
    :row-config="{ keyField: props.rowKey ?? 'id', isHover: true }"
    :toolbar-config="toolbarConfig"
    :resizable="true"
  >
    <template #pager>
      <!--使用 pager 插槽-->
      <vxe-pager
        :layouts="[
          'Sizes',
          'PrevJump',
          'PrevPage',
          'Number',
          'NextPage',
          'NextJump',
          'FullJump',
          'Total'
        ]"
        v-model:current-page="pager.pageIndex"
        v-model:page-size="pager.pageSize"
        :total="pager.total"
        @page-change="handlePageChange"
      />
    </template>
  </vxe-grid>
</template>