<script setup lang="ts">
import { reactive, ref } from "vue";
import { getAuditingDetail, auditingWorkflow } from "@/api/workflow/instance";
import { nextTick } from "vue";
import { VxeFormInstance, VxeFormPropTypes } from "vxe-pc-ui";
const emits = defineEmits<{ (e: "reload"): void }>();
interface AuditingInput {
  isAgree: boolean;
  auditingOpinion: string;
}
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
const auditingDetail = ref();
const showViewModal = (record: Recordable) => {
  auditingDetail.value = record;
  auditingFormData.value = defaultAuditingInput();
  showModal(`查看->${record.createByName}的${record.workflowInstanceTitle}`);
  nextTick(() => {
    getAuditingDetail(record.id).then((result: any) => {
      designConfig.value = JSON.parse(result.formContent);
      workflowFormData.value = JSON.parse(result.formData);
    });
  });
};
const designConfig = ref();
const workflowFormData = ref();
const formRef = ref<VxeFormInstance>();
const defaultAuditingInput = (): AuditingInput => {
  return {
    isAgree: true,
    auditingOpinion: ""
  };
};
const auditingFormData = ref<AuditingInput>(defaultAuditingInput());
const formItems = [
  {
    field: "isAgree",
    title: "是否同意",
    span: 24,
    titleWidth: "100",
    itemRender: {
      name: "VxeRadioGroup",
      options: [
        { label: "通过", value: true },
        { label: "驳回", value: false }
      ]
    }
  },
  {
    field: "auditingOpinion",
    title: "意见",
    titleWidth: "100",
    span: 24,
    itemRender: {
      name: "VxeTextarea",
      props: {
        rows: 4
      }
    }
  }
];
const formRules = ref<VxeFormPropTypes.Rules>({
  auditingOpinion: [{ required: true, message: "请输入意见" }]
});
const handleSubmit = async () => {
  const validate = await formRef.value.validate();
  if (!validate) {
    auditingWorkflow(auditingDetail.value.id, auditingFormData.value).then(
      _ => {
        modalOptions.modalValue = false;
        emits(`reload`);
      }
    );
  }
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
    <el-card shadow="never">
      <el-descriptions border :column="4">
        <el-descriptions-item width="50" label="流程名称">
          {{ auditingDetail.workflowInstanceTitle }}
        </el-descriptions-item>
        <el-descriptions-item width="50" label="发起人">
          {{ auditingDetail.createByName }}
        </el-descriptions-item>
        <el-descriptions-item width="100" label="发起时间">
          {{ auditingDetail.createTime }}
        </el-descriptions-item>
        <el-descriptions-item width="50" label="流程版本">
          {{ auditingDetail.version }}
        </el-descriptions-item>
      </el-descriptions>
    </el-card>
    <el-card shadow="never" class="table-card">
      <vxe-form-view
        ref="formViewRef"
        v-model="workflowFormData"
        :config="designConfig"
      >
      </vxe-form-view>
    </el-card>
    <el-card shadow="never" class="table-card">
      <vxe-form
        ref="formRef"
        :rules="formRules"
        :data="auditingFormData"
        :items="formItems"
      />
    </el-card>
    <template #footer>
      <div>
        <vxe-button content="关闭" @click="modalOptions.modalValue = false" />
        <vxe-button
          v-if="modalOptions.canSubmit"
          status="primary"
          content="确定"
          @click="handleSubmit"
        />
      </div>
    </template>
  </vxe-modal>
</template>

<style lang="scss" scoped></style>
