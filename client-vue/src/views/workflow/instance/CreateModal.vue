<script lang="ts" setup>
import { ref, nextTick, reactive, onBeforeMount } from "vue";
import {
  VxeFormPropTypes,
  VxeFormInstance,
  VxeModalInstance,
  VxeFormViewInstance
} from "vxe-pc-ui";
import { getDefinitions } from "@/api/workflow/definition";
import { startWorkflow } from "@/api/workflow/instance";
import { Collection, Edit } from "@element-plus/icons-vue";

const emits = defineEmits<{ (e: "reload"): void }>();
const formViewRef = ref<VxeFormViewInstance>();
const vxeModalRef = ref<VxeModalInstance>();
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
  activeValue.value = 0;
  modalOptions.modalTitle = title;
  modalOptions.modalValue = true;
  modalOptions.canSubmit = canSubmit ?? true;
};

interface AddInstanceInput {
  definitionId: number | null;
  data: Recordable | null;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = (): AddInstanceInput => {
  return {
    definitionId: null,
    data: null
  };
};
const definitionOptions = ref<Array<any>>([]);
const formData = ref<AddInstanceInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "definitionId",
    title: "流程名称",
    span: 24,
    itemRender: {
      name: "VxeSelect",
      options: definitionOptions.value,
      props: { placeholder: "请选择流程" },
      events: {
        change(value) {
          designConfig.value = JSON.parse(
            definitions.value.find(item => item.id == value.data.definitionId)
              .formContent
          );
        }
      }
    }
  }
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  definitionId: [{ required: true, message: "请选择流程" }]
});

const showAddModal = async () => {
  showModal(`发起流程`);
  formData.value = defaultFormData();
  designConfig.value = null;
  definitionOptions.value = [];
  nextTick(() => {
    formRef.value.clearValidate();
  });
};

const showViewModal = (record: Recordable) => {
  showModal(`查看流程->${record.name}`, false);
  nextTick(() => {
    formRef.value.clearValidate();
    // getSingle(record.id).then((data: any) => {
    //   formData.value = data;
    //   formDesignRef.value.loadConfig(JSON.parse(data.formContent));
    //   flowchartRef.value.renderDesign(JSON.parse(data.designsContent));
    // });
  });
};
const handleSubmit = async () => {
  document.getElementById("submitRef").click();
  startWorkflow(formData.value.definitionId, formData.value.data).then(() => {
    modalOptions.modalValue = false;
    emits("reload");
  });
};
const activeValue = ref(0);
const designConfig = ref();
const handleNext = async () => {
  if (activeValue.value == 0) {
    const validate = await formRef.value.validate();
    if (validate) return false;
  }
  if (activeValue.value++ > 0) activeValue.value = 0;
};
const definitions =
  ref<Array<{ id: number; name: string; formContent: string }>>();

onBeforeMount(async () => {
  definitions.value = await getDefinitions();
  definitions.value.forEach(item => {
    definitionOptions.value.push({
      label: `${item.name}`,
      value: item.id
    });
  });
});
defineExpose({ showAddModal, showViewModal });
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
    <template #default>
      <el-row :span="24">
        <el-col>
          <el-steps
            process-status="finish"
            style="width: 100%"
            :active="activeValue"
            align-center
          >
            <el-step :icon="Edit">
              <template #title>
                <el-button link :type="activeValue > -1 ? `primary` : `info`">
                  流程选择
                </el-button>
              </template>
            </el-step>
            <el-step :icon="Collection">
              <template #title>
                <el-button link :type="activeValue > 0 ? `primary` : `info`">
                  流程数据
                </el-button>
              </template>
            </el-step>
          </el-steps>
        </el-col>
      </el-row>
      <el-row v-show="activeValue == 0">
        <el-col>
          <el-col>
            <vxe-form
              ref="formRef"
              :data="formData"
              :items="formItems"
              :rules="formRules"
              :titleWidth="100"
              :titleColon="true"
              :titleAlign="`right`"
            />
          </el-col>
        </el-col>
      </el-row>
      <el-row v-show="activeValue == 1">
        <el-col>
          <vxe-form-view
            ref="formViewRef"
            v-model="formData.data"
            :config="designConfig"
          >
            <template #footer>
              <div v-show="false">
                <vxe-button
                  id="submitRef"
                  type="submit"
                  status="primary"
                  content="提交"
                ></vxe-button>
              </div>
            </template>
          </vxe-form-view>
        </el-col>
      </el-row>
    </template>
    <template #footer>
      <div>
        <vxe-button content="关闭" @click="modalOptions.modalValue = false" />
        <vxe-button
          :content="activeValue == 1 ? `上一步` : `下一步`"
          status="warning"
          @click="handleNext"
        />
        <vxe-button
          v-if="modalOptions.canSubmit && activeValue == 1"
          status="primary"
          content="确定"
          @click="handleSubmit"
        />
      </div>
    </template>
  </vxe-modal>
</template>
