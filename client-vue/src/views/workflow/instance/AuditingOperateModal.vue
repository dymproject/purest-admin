<script setup lang="ts">
import { reactive, ref } from "vue";
import { getInstanceDetail, auditingWorkflow } from "@/api/workflow/instance";
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
const detailRecord = ref();
const showViewModal = (record: Recordable) => {
  detailRecord.value = record;
  auditingFormData.value = defaultAuditingInput();
  showModal(`查看->${record.createByName}的${record.description}`);
  nextTick(() => {
    getInstanceDetail(record.persistenceId).then((instance: any) => {
      designConfig.value = JSON.parse(instance.definition.formContent);
      workflowFormData.value = JSON.parse(instance.data);
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
    auditingWorkflow(detailRecord.value.persistenceId, {
      ...auditingFormData.value,
      ...{ stepId: detailRecord.value.stepId }
    }).then(_ => {
      modalOptions.modalValue = false;
      emits(`reload`);
    });
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
    <el-card body-class="card-padding" shadow="never">
      <vxe-form>
        <vxe-form-item title="流程名称" span="9">
          {{ detailRecord.description }}
        </vxe-form-item>
        <vxe-form-item title="发起人" span="5">
          {{ detailRecord.createByName }}
        </vxe-form-item>
        <vxe-form-item title="发起时间" span="6">
          {{ detailRecord.createTime }}
        </vxe-form-item>
        <vxe-form-item title="流程版本" span="4">
          {{ detailRecord.version }}
        </vxe-form-item>
      </vxe-form>
    </el-card>
    <el-card shadow="never" body-class="card-padding" class="table-card">
      <vxe-form-view
        ref="formViewRef"
        v-model="workflowFormData"
        :config="designConfig"
      >
      </vxe-form-view>
    </el-card>
    <el-card
      shadow="never"
      body-style="padding-bottom:10px!important"
      body-class="card-padding"
      class="table-card"
    >
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

<style lang="scss">
.card-padding {
  padding-top: 0 !important;
  padding-bottom: 0 !important;
}
</style>
