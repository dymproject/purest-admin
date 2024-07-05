<script lang="ts" setup>
import { ref, onMounted, reactive, h, nextTick } from "vue";
import { VxeButton, VxeModalInstance, VxeSwitch } from "vxe-pc-ui";
import { getPageList, asyncApi } from "@/api/system/interface";
import { ElLoading } from "element-plus";
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
const columns = [
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
    minWidth: 100
  },
  {
    title: "接口地址",
    field: "path",
    minWidth: 200
  },
  {
    title: "请求方法",
    field: "requestMethod",
    minWidth: 30
  }
];
const customTableActions = [
  {
    title: "操作",
    field: "operate",
    align: "center",
    minWidth: 100,
    slots: {
      default: ({ row }) => {
        if (!row.path) return null;
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
              onChange(value) {
                assignInterface({
                  functionId: functionId.value,
                  interfaceId: row.id
                }).then(async () => {
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
const handleAsyncApi = () => {
  const loading = ElLoading.service({
    lock: true,
    text: "正在同步……",
    background: "rgba(0, 0, 0, 0.7)"
  });
  asyncApi()
    .then(_ => {
      reVxeGridRef.value.loadData();
    })
    .finally(() => {
      loading.close();
    });
};
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
        },
        {
          props: {
            type: "button",
            icon: "vxe-icon-refresh",
            content: "同步接口",
            status: "success",
            onclick: handleAsyncApi
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
    v-model="modalOptions.modalValue"
    width="1400"
    height="750"
    showFooter
    :draggable="false"
    :title="modalOptions.modalTitle"
  >
    <template #default>
      <div style="display: flex; width: 100%">
        <ReCard header="已绑定接口" style="width: 40%">
          <vxe-table height="510" :data="myInterfaceData">
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
        <ReCard header="接口列表" style="width: 60%">
          <vxe-form
            ref="formRef"
            :data="formData"
            :items="formItems"
            @submit="handleSearch"
            @reset="handleInitialFormParams"
          />
          <ReVxeGrid
            ref="reVxeGridRef"
            :min-height="450"
            :max-height="450"
            :treeConfig="{ rowField: 'id', childrenField: 'interfaces' }"
            :customToolbarActions="[]"
            :searchParams="formData"
            :showOperateColumn="false"
            :customTableActions="customTableActions"
            :getPageList
            :request="getPageList"
            :columns="columns"
          />
        </ReCard>
      </div>
    </template>
    <template #footer>
      <vxe-button content="关闭" @click="modalOptions.modalValue = false" />
      <vxe-button
        v-if="modalOptions.canSubmit"
        status="primary"
        content="确定"
        @click="handleSubmit"
      />
    </template>
  </vxe-modal>
</template>
