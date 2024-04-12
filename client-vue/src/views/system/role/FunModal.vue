<script lang="ts" setup>
import { ref, onMounted, nextTick, reactive } from "vue";
import { VxeModalInstance, VxeTableEvents, VxeTableInstance } from "vxe-table";
import { getTreeList } from "@/api/system/function";
import { getFunctions, assignFunction } from "@/api/system/role";
const vxeModalRef = ref<VxeModalInstance>();
const vxeTableRef = ref<VxeTableInstance>();
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

const tableData = ref<any[]>();
const checkRowKeys = ref([]);
const roleId = ref();
const showFunction = (record: Recordable) => {
  checkRowKeys.value = [];
  showModal(`${record.name}->的功能`);
  roleId.value = record.id;
  nextTick(() => {
    vxeTableRef.value.clearCheckboxRow();
    getFunctions(record.id).then((data: any[]) => {
      const rows = data.map(item => vxeTableRef.value.getRowById(item.id));
      vxeTableRef.value.setCheckboxRow(rows, true);
      checkRowKeys.value = data.map(item => item.id);
    });
  });
};

const handleSubmit = () => {
  assignFunction(roleId.value, checkRowKeys.value);
  modalOptions.modalValue = false;
};
const selectChangeEvent: VxeTableEvents.CheckboxChange<any> = ({
  $table,
  checked,
  row
}) => {
  setTableChecked(checked, row);
  row.checked = false;
  const records = $table.getCheckboxRecords();
  checkRowKeys.value = records.map(item => item.id);
};
//递归控制树节点
const setTableChecked = (checked: boolean, row: any) => {
  if (row.children != null) {
    row.children.forEach(item => {
      const row = vxeTableRef.value.getRowById(item.id);
      vxeTableRef.value.setCheckboxRow(row, checked);
      setTableChecked(checked, item);
    });
  }
};
onMounted(() => {
  getTreeList().then((result: any) => {
    tableData.value = result;
  });
});

defineExpose({ showFunction });
</script>
<template>
  <vxe-modal
    ref="vxeModalRef"
    v-model="modalOptions.modalValue"
    width="600"
    height="500"
    showFooter
    :draggable="false"
    :title="modalOptions.modalTitle"
  >
    <template #default>
      <vxe-table
        ref="vxeTableRef"
        :row-config="{ keyField: 'id' }"
        :tree-config="{}"
        :data="tableData"
        :checkbox-config="{
          checkStrictly: true,
          labelField: 'name',
          highlight: true
        }"
        @checkbox-change="selectChangeEvent"
      >
        <!-- <vxe-column type="checkbox" title="ID" width="200" /> -->
        <vxe-column type="checkbox" field="name" title="功能" tree-node />
      </vxe-table>
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
