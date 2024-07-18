<script setup lang="ts">
import { ref, nextTick, reactive } from "vue";
import { VxeModalInstance } from "vxe-pc-ui";
import { getInstanceDetail } from "@/api/workflow/instance";
const modalOptions = reactive<{
  modalValue: boolean;
  modalTitle: string;
}>({
  modalValue: false,
  modalTitle: ""
});

const showModal = (title: string): void => {
  modalOptions.modalTitle = title;
  modalOptions.modalValue = true;
};
const detailRecord = ref();
const designConfig = ref();
const workflowFormData = ref();
const executionPointers = ref();
const showViewModal = (record: Recordable) => {
  detailRecord.value = record;
  showModal(`查看详情->${record.description}`);
  nextTick(() => {
    getInstanceDetail(record.persistenceId).then((instance: any) => {
      executionPointers.value = instance.executionPointers;
      designConfig.value = JSON.parse(instance.definition.formContent);
      workflowFormData.value = JSON.parse(instance.data);
    });
  });
};

const vxeModalRef = ref<VxeModalInstance>();

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
        <vxe-form-item title="流程名称" span="10">
          {{ detailRecord.description }}
        </vxe-form-item>
        <vxe-form-item title="发起时间" span="8">
          {{ detailRecord.createTime }}
        </vxe-form-item>
        <vxe-form-item title="流程版本" span="6">
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
    <el-card shadow="never" body-class="card-padding" class="table-card">
      <el-timeline style="padding-top: 20px" placement="top">
        <el-timeline-item
          v-for="(pointer, index) in executionPointers"
          :key="index"
          :timestamp="pointer.startTime"
        >
          <el-card
            v-if="pointer.stepName == `开始` || pointer.stepName == `结束`"
          >
            <vxe-text status="success">
              {{ pointer.stepName }}
            </vxe-text>
          </el-card>
          <el-card v-else>
            <template #header>
              <vxe-text
                :content="pointer.stepName"
                :status="pointer.status == 3 ? `success` : `warning`"
              >
              </vxe-text>
            </template>
            <vxe-table
              v-if="pointer.status == 3"
              size="mini"
              :data="pointer.auditingRecords"
              :min-height="0"
            >
              <vxe-column field="auditingTime" title="审批时间"></vxe-column>
              <vxe-column field="auditorName" title="审批人"></vxe-column>
              <vxe-column field="auditingOpinion" title="审批意见"></vxe-column>
              <vxe-column field="auditingResult" title="审批结果">
                <template #default="{ row }">
                  <vxe-tag
                    v-if="row.isAgree"
                    status="success"
                    content="通过"
                    icon="vxe-icon-check-circle"
                  ></vxe-tag>
                  <vxe-tag
                    v-else
                    status="warning"
                    content="拒绝"
                    icon="vxe-icon-close-circle"
                  ></vxe-tag>
                </template>
              </vxe-column>
            </vxe-table>
            <vxe-text
              v-else-if="pointer.status == 5"
              status="warning"
              content="待审批"
            ></vxe-text>
          </el-card>
        </el-timeline-item>
      </el-timeline>
    </el-card>
  </vxe-modal>
</template>

<style lang="scss">
.card-padding {
  padding-top: 0 !important;
  padding-bottom: 0 !important;
}
</style>
