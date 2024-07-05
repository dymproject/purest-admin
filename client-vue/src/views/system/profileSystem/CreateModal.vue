<script lang="ts" setup>
import { ref, nextTick, reactive, h } from "vue";
import {
  VxeFormPropTypes,
  VxeFormInstance,
  VxeModalInstance,
  VxeInput,
  VxeButton,
  VxeUI
} from "vxe-pc-ui";
import { submitData } from "@/api/system/profileSystem";
const emits = defineEmits<{ (e: "reload"): void }>();
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
  modalOptions.modalTitle = title;
  modalOptions.modalValue = true;
  modalOptions.canSubmit = canSubmit ?? true;
};

interface AddSystemConfigInput {
  name: string;
  code: string;
  file: File;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  fileName.value = "";
  return {
    name: "",
    code: "",
    file: null,
    remark: ""
  };
};
const fileName = ref("");
const formData = ref<AddSystemConfigInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "名称",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入名称" }
    }
  },
  {
    field: "code",
    title: "编码",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入编码" }
    }
  },
  {
    field: "file",
    title: "配置值",
    span: 24,
    slots: {
      default: ({ data }) => {
        return [
          h(VxeInput, {
            style: { width: `60%` },
            readonly: true,
            placeholder: "请选择文件",
            modelValue: fileName.value
          }),
          h(
            VxeButton,
            {
              style: { width: `30%` },
              async onClick() {
                try {
                  const { file } = await VxeUI.readFile();
                  data.file = file;
                  fileName.value = file.name;
                  formRef.value.validate();
                } catch (e) {}
              }
            },
            { default: () => "选择文件" }
          )
        ];
      }
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
  name: [{ required: true, message: "请输入系统文件名称" }],
  code: [{ required: true, message: "请输入系统文件编码" }],
  file: [{ required: true, message: "请选择文件" }]
});

const showAddModal = () => {
  showModal(`添加系统文件`);
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const handleSubmit = async () => {
  const validate = await formRef.value.validate();
  if (!validate) {
    submitData(formData.value).then(() => {
      modalOptions.modalValue = false;
      emits("reload");
    });
  }
};

defineExpose({ showAddModal });
</script>
<template>
  <vxe-modal
    ref="vxeModalRef"
    v-model="modalOptions.modalValue"
    width="600"
    height="500"
    showFooter
    :title="modalOptions.modalTitle"
  >
    <template #default>
      <vxe-form
        ref="formRef"
        :data="formData"
        :items="formItems"
        :rules="formRules"
        :titleWidth="100"
        :titleColon="true"
        :titleAlign="`right`"
      />
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
