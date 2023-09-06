<script lang="ts" setup>
import { ref, onMounted, reactive, h, nextTick } from "vue";
import {
  VxeButton,
  VxeGridPropTypes,
  VxeModalInstance,
  VxeSwitch
} from "vxe-table";
import { getPageList } from "@/api/system/interface";
import {
  getFunInterface,
  assignInterface,
  removeInterface
} from "@/api/system/function";
import { ReVxeGrid } from "@/components/ReVxeTable";
const vxeModalRef = ref<VxeModalInstance>();
const reVxeGridRef = ref();
const modalOptions = reactive<{
  modalValue: boolean;
  modalTitle: string;
  canSubmit: boolean;
}>({
  modalValue: false,
  modalTitle: "",
  canSubmit: true
});

const showModal = (title: string, canSubmit?: boolean): void => {
  modalOptions.modalTitle = title;
  modalOptions.modalValue = true;
  modalOptions.canSubmit = canSubmit ?? true;
};

const loadMyInterfaceData = async () => {
  myInterfaceData.value = await getFunInterface(functionId.value);
};

const checkRowKeys = ref([]);
const myInterfaceData = ref([]);
const functionId = ref();
const showInterface = (record: Recordable) => {
  checkRowKeys.value = [];
  showModal(`${record.name}->的接口`, false);
  functionId.value = record.id;
  formData.path = "";
  nextTick(() => {
    loadMyInterfaceData();
    reVxeGridRef.value.loadData();
  });
};

const handleSubmit = () => {
  modalOptions.modalValue = false;
};
const columns: VxeGridPropTypes.Columns<any> = [
  {
    title: "Id",
    field: "id",
    visible: false,
    minWidth: 100
  },
  {
    title: "名称",
    field: "name",
    treeNode: true,
    minWidth: 200
  },
  {
    title: "接口地址",
    field: "path",
    minWidth: 200
  },
  {
    title: "请求方法",
    field: "requestMethod",
    minWidth: 80
  }
];
const customTableActions: VxeGridPropTypes.Columns<any> = [
  {
    title: "操作",
    field: "operate",
    align: "center",
    width: 100,
    slots: {
      default: ({ row }) => {
        row.isBind = myInterfaceData.value
          .map(x => x.interfaceId)
          .includes(row.id)
          ? true
          : false;
        return !row.interfaces
          ? h(VxeSwitch, {
              modelValue: row.isBind,
              size: "medium",
              openLabel: "已绑定",
              closeLabel: "未绑定",
              onChange({ value }) {
                assignInterface(functionId.value, row.id).then(async () => {
                  await loadMyInterfaceData();
                  row.isBind = value;
                });
              }
            })
          : null;
      }
    }
  }
];
const formRef = ref();

const handleInitialFormParams = () => ({
  path: ""
});
const formItems = [
  {
    field: "path",
    title: "接口地址",
    span: 12,
    itemRender: { name: "$input", props: { placeholder: "接口地址" } }
  },
  {
    span: 12,
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
        }
      ]
    }
  }
];
const formData = reactive<{ path: string }>(handleInitialFormParams());

const handleSearch = () => {
  reVxeGridRef.value.loadData();
};
const handleRemoveInterface = (row: Recordable) => {
  removeInterface(row.id).then(() => {
    loadMyInterfaceData();
    reVxeGridRef.value.loadData();
  });
};
onMounted(() => {});

defineExpose({ showInterface });
</script>
<template>
  <vxe-modal
    ref="vxeModalRef"
    width="1200"
    height="850"
    showFooter
    v-model="modalOptions.modalValue"
    :draggable="false"
    :title="modalOptions.modalTitle"
  >
    <template #default>
      <div style="display: flex; width: 100%">
        <ReCard header="已绑定接口">
          <vxe-table height="590" :data="myInterfaceData">
            <vxe-column field="name" width="180" title="名称" />
            <vxe-column field="path" width="180" title="接口地址" />
            <vxe-column
              field="remove"
              width="100"
              header-align="center"
              align="center"
              title="#"
            >
              <template #default="{ row }">
                <vxe-button
                  type="error"
                  size="mini"
                  @click="handleRemoveInterface(row)"
                >
                  移除
                </vxe-button>
              </template>
            </vxe-column>
          </vxe-table>
        </ReCard>
        <ReCard header="接口列表" body-style="width:660px">
          <vxe-form
            ref="formRef"
            :data="formData"
            :items="formItems"
            @submit="handleSearch"
            @reset="handleInitialFormParams"
          />
          <ReVxeGrid
            ref="reVxeGridRef"
            :max-height="580"
            :treeConfig="{ rowField: 'id', children: 'interfaces' }"
            :searchParams="formData"
            :showOperateColumn="false"
            :customTableActions="customTableActions"
            :request="getPageList"
            :columns="columns"
          />
        </ReCard>
      </div>
    </template>
    <template #footer>
      <vxe-button content="关闭" @click="modalOptions.modalValue = false" />
      <vxe-button
        status="primary"
        content="确定"
        v-if="modalOptions.canSubmit"
        @click="handleSubmit"
      />
    </template>
  </vxe-modal>
</template>
