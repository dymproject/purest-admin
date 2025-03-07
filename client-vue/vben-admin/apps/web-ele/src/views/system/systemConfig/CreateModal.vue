<script lang="ts" setup>
import { ref, nextTick } from 'vue';
import type { VxeFormPropTypes, VxeFormInstance } from 'vxe-pc-ui';
import { getSingle, submitData } from '#/api/system/systemConfig';
import { $t } from '@vben/locales';
const emits = defineEmits<{ (e: 'reload'): void }>();
const reModalRef = ref();
interface AddSystemConfigInput {
  name: string;
  configCode: string;
  configValue: string;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    name: '',
    configCode: '',
    configValue: '',
    remark: '',
  };
};
const formData = ref<AddSystemConfigInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: 'name',
    title: $t('systemConfig.form.name'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('systemConfig.form.placeholder.name') },
    },
  },
  {
    field: 'configCode',
    title: $t('systemConfig.form.configCode'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('systemConfig.form.placeholder.configCode') },
    },
  },
  {
    field: 'configValue',
    title: $t('systemConfig.form.configValue'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('systemConfig.form.placeholder.configValue') },
    },
  },
  {
    field: 'remark',
    title: $t('systemConfig.form.remark'),
    span: 24,
    itemRender: {
      name: '$textarea',
      props: { placeholder: $t('systemConfig.form.placeholder.remark') },
    },
  },
]);

const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: $t('systemConfig.form.validate.name') }],
  configCode: [
    { required: true, message: $t('systemConfig.form.validate.configCode') },
  ],
  configValue: [
    { required: true, message: $t('systemConfig.form.validate.configValue') },
  ],
});

const showAddModal = () => {
  reModalRef.value.show($t('systemConfig.add'));
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value?.clearValidate();
  });
};
const showEditModal = (record: any) => {
  reModalRef.value.show($t('systemConfig.edit') + `->${record.name}`);
  nextTick(() => {
    formRef.value?.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: any) => {
  reModalRef.value.show(`${$t('systemConfig.view')}->${record.name}`, true);
  nextTick(() => {
    formRef.value?.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const handleSubmit = async () => {
  const validate = await formRef.value?.validate();
  if (!validate) {
    submitData(formData.value).then(() => {
      emits('reload');
      reModalRef.value.close();
    });
  }
};

defineExpose({ showAddModal, showEditModal, showViewModal });
</script>
<template>
  <re-modal ref="reModalRef" @submit="handleSubmit">
    <vxe-form
      ref="formRef"
      :data="formData"
      :items="formItems"
      :rules="formRules"
      :titleWidth="100"
      :titleColon="true"
      :titleAlign="`right`"
    />
  </re-modal>
</template>
