<script lang="ts" setup>
import { ref, nextTick, reactive } from "vue";
import { VxeFormPropTypes, VxeFormInstance, VxeModalInstance } from "vxe-table";
import { getSingle, submitData } from "@/api/system/systemconfig";
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
  account: string;
  password: string;
  roleId: number | null;
  organizationId: number | null;
  telephone: string;
  email: string;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    name: "",
    account: "",
    password: "",
    roleId: null,
    organizationId: null,
    telephone: "",
    email: "",
    remark: ""
  };
};
const formData = ref<AddSystemConfigInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "配置名称",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入配置名称" }
    }
  },
  {
    field: "configCode",
    title: "配置编码",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入配置编码" }
    }
  },
  {
    field: "configValue",
    title: "配置值",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入配置值" }
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
  name: [{ required: true, message: "请输入系统配置名" }],
  configCode: [{ required: true, message: "请输入配置编码" }],
  configValue: [{ required: true, message: "请输入配置值" }]
});

const showAddModal = () => {
  showModal(`添加系统配置`);
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: Recordable) => {
  showModal(`编辑系统配置->${record.name}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: Recordable) => {
  showModal(`查看系统配置->${record.name}`, false);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
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

defineExpose({ showAddModal, showEditModal, showViewModal });
</script>
<template>
  <vxe-modal
    ref="vxeModalRef"
    width="600"
    height="500"
    showFooter
    v-model="modalOptions.modalValue"
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
        status="primary"
        content="确定"
        v-if="modalOptions.canSubmit"
        @click="handleSubmit"
      />
    </template>
  </vxe-modal>
</template>
