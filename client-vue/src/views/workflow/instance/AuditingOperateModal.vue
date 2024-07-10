<script setup lang="ts">
import { reactive } from "vue";
const emits = defineEmits<{ (e: "reload"): void }>();
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
const showViewModal = (record: Recordable) => {
  showModal(
    `查看->${record.createByName}的${record.workflowInstanceTitle}`,
    false
  );
};
defineExpose({ showViewModal });
</script>

<template>
  <vxe-modal
    ref="vxeModalRef"
    v-model="modalOptions.modalValue"
    width="1000"
    height="800"
    showFooter
    :title="modalOptions.modalTitle"
  >
    <ReCard>
      <el-descriptions :column="4" title="流程信息">
        <el-descriptions-item label="流程名称"
          >kooriookami</el-descriptions-item
        >
        <el-descriptions-item label="发起人">18100000000</el-descriptions-item>
        <el-descriptions-item label="流程版本">
          <el-tag>School</el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="发起时间"> 111 </el-descriptions-item>
      </el-descriptions>
    </ReCard>
    <ReCard class="table-card">
      <el-descriptions title="流程内容">
        <el-descriptions-item label="Username"
          >kooriookami</el-descriptions-item
        >
        <el-descriptions-item label="Telephone"
          >18100000000</el-descriptions-item
        >
        <el-descriptions-item label="Place">Suzhou</el-descriptions-item>
        <el-descriptions-item label="Remarks">
          <el-tag size="small">School</el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="Address">
          No.1188, Wuzhong Avenue, Wuzhong District, Suzhou, Jiangsu Province
        </el-descriptions-item>
      </el-descriptions>
    </ReCard>
    <template #footer>
      <div>
        <vxe-button content="关闭" @click="modalOptions.modalValue = false" />
        <vxe-button
          v-if="modalOptions.canSubmit"
          status="primary"
          content="确定"
          @click=""
        />
      </div>
    </template>
  </vxe-modal>
</template>

<style lang="scss" scoped></style>
