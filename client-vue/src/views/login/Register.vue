<script lang="ts" setup>
import { ref, nextTick, reactive } from "vue";
import { VxeFormPropTypes } from "vxe-pc-ui";
import { getSingle, submitData } from "@/api/system/user";
const vxeModalRef = ref();
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

interface AddUserInput {
  name: string;
  account: string;
  password: string;
  telephone: string;
  email: string;
}
const formRef = ref();
const defaultFormData = () => {
  return {
    name: "",
    account: "",
    password: "",
    telephone: "",
    email: ""
  };
};
const formData = ref<AddUserInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "名称",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入用户名称" }
    }
  },
  {
    field: "account",
    title: "帐号",
    span: 24,
    itemRender: { name: "$input", props: { placeholder: "请输入帐号" } }
  },
  {
    field: "telephone",
    title: "电话号码",
    span: 24,
    itemRender: { name: "$input", props: { placeholder: "请输入电话号码" } }
  },
  {
    field: "email",
    title: "邮箱",
    span: 24,
    itemRender: { name: "$input", props: { placeholder: "请输入邮箱" } }
  }
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: "请输入用户名" }],
  account: [
    { required: true, message: "请输入帐号" },
    { min: 3, message: "长度不能低于3个字符" }
  ]
});

const showAddModal = () => {
  showModal(`绑定用户`);
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
