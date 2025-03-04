<script lang="ts" setup>
import { ref, onMounted, nextTick } from 'vue';
import { getTreeList } from '#/api/system/function';
import { getFunctions, assignFunction } from '#/api/system/role';
const reModalRef = ref();
const vxeTableRef = ref();
const tableData = ref<any[]>();
const checkRowKeys = ref([]);
const roleId = ref();
const showFunction = (record: any) => {
  checkRowKeys.value = [];
  reModalRef.value.show(`${record.name}`);
  roleId.value = record.id;
  nextTick(() => {
    vxeTableRef.value.clearCheckboxRow();
    getFunctions(record.id).then((data: any) => {
      const rows = data.map((item: any) =>
        vxeTableRef.value.getRowById(item.id),
      );
      vxeTableRef.value.setCheckboxRow(rows, true);
      checkRowKeys.value = data.map((item: any) => item.id);
    });
  });
};

const handleSubmit = () => {
  assignFunction(roleId.value, checkRowKeys.value);
  reModalRef.value.close();
};
const selectChangeEvent = ({
  $table,
  checked,
  row,
}: {
  $table: any;
  checked: boolean;
  row: any;
}) => {
  setTableChecked(checked, row);
  row.checked = false;
  const records = $table.getCheckboxRecords();
  checkRowKeys.value = records.map((item: any) => item.id);
};
//递归控制树节点
const setTableChecked = (checked: boolean, row: any) => {
  if (row.children != null) {
    row.children.forEach((item: any) => {
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
  <re-modal ref="reModalRef" @submit="handleSubmit">
    <vxe-table
      ref="vxeTableRef"
      :size="`mini`"
      :row-config="{ keyField: 'id' }"
      :tree-config="{}"
      :data="tableData"
      :checkbox-config="{
        checkStrictly: true,
        labelField: 'name',
        highlight: true,
      }"
      @checkbox-change="selectChangeEvent"
    >
      <!-- <vxe-column type="checkbox" title="ID" width="200" /> -->
      <vxe-column
        type="checkbox"
        field="name"
        :title="$t('role.columns.functions')"
        tree-node
      />
    </vxe-table>
  </re-modal>
</template>
