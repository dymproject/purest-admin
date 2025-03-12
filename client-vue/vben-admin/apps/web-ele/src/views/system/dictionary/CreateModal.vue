<script lang="ts" setup>
import { ref, nextTick } from 'vue';
import type { VxeFormPropTypes, VxeFormInstance } from 'vxe-pc-ui';
import { getSingle, submitData } from '#/api/system/dictionary';
import { $t } from '@vben/locales';
const emits = defineEmits<{ (e: 'reload'): void }>();
const props = defineProps<{ categoryId?: number }>();
const reModalRef = ref();
interface AddDictDataInput {
  name: string;
  code: string;
  categoryId: null | number | undefined;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    name: '',
    code: '',
    categoryId: props.categoryId,
    remark: '',
  };
};
const formData = ref<AddDictDataInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: 'name',
    title: $t('dictionary.form.itemName'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('dictionary.form.placeholder.itemName') },
    },
  },
  {
    field: 'sort',
    title: $t('dictionary.form.sort'),
    span: 24,
    itemRender: {
      name: '$input',
      props: {
        type: 'number',
        min: 0,
        placeholder: $t('dictionary.form.placeholder.sort'),
      },
    },
  },
  {
    field: 'remark',
    title: $t('dictionary.form.remark'),
    span: 24,
    itemRender: {
      name: '$textarea',
      props: { placeholder: $t('dictionary.form.placeholder.remark') },
    },
  },
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: $t('dictionary.form.validate.itemName') }],
});

const showAddModal = () => {
  reModalRef.value.show($t('dictionary.add'));
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value?.clearValidate();
  });
};
const showEditModal = (record: any) => {
  reModalRef.value.show($t('dictionary.edit') + `->${record.name}`);
  nextTick(() => {
    formRef.value?.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: any) => {
  reModalRef.value.show(`${$t('dictionary.view')}>${record.name}`, true);
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
      :titleWidth="130"
      :titleColon="true"
      :titleAlign="`right`"
    />
  </re-modal>
</template>
