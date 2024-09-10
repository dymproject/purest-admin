<script lang="ts" setup>
import { ref, nextTick, reactive } from "vue";
import { VxeFormPropTypes } from "vxe-pc-ui";
import { bindUser } from "@/api/auth";
const props = defineProps<{ oAuth2UserId: number; connectionId: string }>();
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

interface LoginInput {
  account: string;
  password: string;
  oAuth2UserId: number | null;
}
const formRef = ref();
const defaultFormData = () => {
  return {
    account: "",
    password: "",
    oAuth2UserId: props.oAuth2UserId,
    connectionId: props.connectionId
  };
};
const formData = ref<LoginInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "account",
    title: "帐号",
    span: 24,
    itemRender: { name: "$input", props: { placeholder: "请输入帐号" } }
  },
  {
    field: "password",
    title: "密码",
    span: 24,
    itemRender: {
      name: "$input",
      props: { type: "password", placeholder: "请输入密码" }
    }
  }
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  account: [
    { required: true, message: "请输入帐号" },
    { min: 3, message: "长度不能低于3个字符" }
  ],
  password: [
    { required: true, message: "请输入密码" },
    { min: 6, message: "长度不能低于3个字符" }
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
    bindUser(formData.value).then(() => {
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
    height="240"
    showFooter
    :title="modalOptions.modalTitle"
  >
    <template #default>
      <vxe-form
        ref="formRef"
        :data="formData"
        :items="formItems"
        :rules="formRules"
        :titleWidth="80"
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
