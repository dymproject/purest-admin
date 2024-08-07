<script lang="ts" setup>
import { ref, nextTick, reactive } from "vue";
import { VxeFormPropTypes, VxeFormInstance, VxeModalInstance } from "vxe-pc-ui";
import { getSingle, submitData } from "@/api/workflow/definition";
import { Picture, Collection, Edit } from "@element-plus/icons-vue";
import { VxeFormDesignPropTypes, VxeFormDesignInstance } from "vxe-pc-ui";
import { message } from "@/utils/message";
import FlowChartDesign from "./components/FlowChartDesign.vue";

const emits = defineEmits<{ (e: "reload"): void }>();
const formDesignRef = ref<VxeFormDesignInstance>();
const formDesignWidgets = ref<VxeFormDesignPropTypes.Widgets>([
  // {
  //   group: "layout",
  //   children: ["row"]
  // },
  {
    group: "base",
    children: [
      "VxeNumberInput",
      "VxeTextarea"
      // "VxeSelect",
      // "VxeSwitch",
      // "VxeRadioGroup",
      // "VxeCheckboxGroup",
      //  "VxeInput",
    ]
  }
]);

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

interface AddDefinitionInput {
  name: string;
  version: number;
  remark: string;
  designsContent: string;
  formContent: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = (): AddDefinitionInput => {
  return {
    name: "",
    version: 1,
    designsContent: "",
    formContent: "",
    remark: ""
  };
};
const formData = ref<AddDefinitionInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "模板名",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入模板名" }
    }
  },
  {
    field: "version",
    title: "版本",
    span: 24,
    itemRender: {
      name: "$input",
      props: { type: "number", placeholder: "版本" }
    }
  },
  {
    field: "remark",
    title: "备注",
    span: 24,
    itemRender: {
      name: "$textarea",
      props: { placeholder: "请输入备注" }
    }
  }
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: "请输入模板名" }]
});

const showAddModal = () => {
  showModal(`添加模板`);
  formData.value = defaultFormData();
  nextTick(() => {
    formDesignRef.value.clearConfig();
    flowchartRef.value.renderDesign(null);
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: Recordable) => {
  showModal(`编辑模板->${record.name}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
      formDesignRef.value.loadConfig(JSON.parse(data.formContent));
      flowchartRef.value.renderDesign(JSON.parse(data.designsContent));
    });
  });
};
const showViewModal = (record: Recordable) => {
  showModal(`查看模板->${record.name}`, false);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
      formDesignRef.value.loadConfig(JSON.parse(data.formContent));
      flowchartRef.value.renderDesign(JSON.parse(data.designsContent));
    });
  });
};
const flowchartRef = ref();
const handleSubmit = async () => {
  formData.value.designsContent = JSON.stringify(flowchartRef.value.getData());
  submitData(formData.value).then(() => {
    modalOptions.modalValue = false;
    emits("reload");
  });
};
const activeValue = ref(0);
const widgetData = ref();
const handleNext = async () => {
  if (activeValue.value == 0) {
    const validate = await formRef.value.validate();
    if (validate) return false;
  } else if (activeValue.value == 1) {
    var formDesign = formDesignRef.value.getConfig();
    if (formDesign.widgetData.length == 0) {
      message("表单内容不能为空", { type: "warning" });
      return false;
    }
    widgetData.value = formDesign.widgetData;
    formData.value.formContent = JSON.stringify(formDesign);
  }
  if (activeValue.value++ > 1) activeValue.value = 0;
};
defineExpose({ showAddModal, showEditModal, showViewModal });
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
                  基本信息
                </el-button>
              </template>
            </el-step>
            <el-step :icon="Collection">
              <template #title>
                <el-button link :type="activeValue > 0 ? `primary` : `info`">
                  表单设计
                </el-button>
              </template>
            </el-step>
            <el-step :icon="Picture">
              <template #title>
                <el-button link :type="activeValue > 0 ? `primary` : `info`">
                  流程设计
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
          <vxe-form-design
            ref="formDesignRef"
            :widgets="formDesignWidgets"
            :height="600"
          />
        </el-col>
      </el-row>
      <el-row v-show="activeValue == 2">
        <FlowChartDesign
          ref="flowchartRef"
          :widgetData="widgetData"
          style="width: 100%"
        />
      </el-row>
    </template>
    <template #footer>
      <div>
        <vxe-button content="关闭" @click="modalOptions.modalValue = false" />
        <vxe-button
          :content="activeValue == 2 ? `重新开始` : `下一步`"
          status="warning"
          @click="handleNext"
        />
        <vxe-button
          v-if="modalOptions.canSubmit && activeValue == 2"
          status="primary"
          content="确定"
          @click="handleSubmit"
        />
      </div>
    </template>
  </vxe-modal>
</template>
