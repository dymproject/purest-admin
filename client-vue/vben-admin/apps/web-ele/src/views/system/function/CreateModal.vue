<script lang="ts" setup>
import { ref, nextTick, h } from 'vue';
import type { VxeFormPropTypes } from 'vxe-pc-ui';
import { getSingle, submitData, getTreeList } from '#/api/system/function';
import { ElTreeSelect } from 'element-plus';
import { $t } from '@vben/locales';
const emits = defineEmits<{ (e: 'reload'): void }>();
const reModalRef = ref();
const treeSelectData = ref([]);

interface AddFunctionInput {
  name: string;
  code: string;
  parentId: null | number;
  remark: string;
}
const formRef = ref();
const defaultFormData = () => {
  return {
    name: '',
    code: '',
    parentId: null,
    remark: '',
  };
};
const formData = ref<AddFunctionInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: 'name',
    title: $t('function.form.name'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('function.form.placeholder.name') },
    },
  },
  {
    field: 'code',
    title: $t('function.form.code'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('function.form.placeholder.code') },
    },
  },
  {
    field: 'parentId',
    title: $t('function.form.subjection'),
    span: 24,
    slots: {
      default: () => {
        return [
          h(ElTreeSelect, {
            modelValue: formData.value.parentId,
            data: treeSelectData.value,
            checkStrictly: true,
            style: { width: `100%` },
            props: { value: 'id', label: 'name', children: 'children' },
            onChange(v: any) {
              formData.value.parentId = v;
            },
          }),
        ];
      },
    },
  },
  {
    field: "remark",
    title: $t('function.form.remark'),
    span: 24,
    itemRender: {
      name: '$textarea',
      props: { placeholder: $t('function.form.placeholder.remark') },
    },
  },
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: $t('function.form.validate.name') }],
  code: [{ required: true, message: $t('function.form.validate.code') }],
});

const showAddModal = () => {
  reModalRef.value.show($t('function.add'));
  formData.value = defaultFormData();
  nextTick(async () => {
    formRef.value.clearValidate();
    await loadTreeSelectData();
  });
};
const showEditModal = (record: any) => {
  reModalRef.value.show($t('function.edit') + `->${record.name}`);
  nextTick(async () => {
    formRef.value.clearValidate();
    await loadTreeSelectData();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: any) => {
  reModalRef.value.show($t('function.view') + `->${record.name}`, true);
  nextTick(async () => {
    formRef.value.clearValidate();
    await loadTreeSelectData();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const handleSubmit = async () => {
  const validate = await formRef.value.validate();
  if (!validate) {
    submitData(formData.value).then(() => {
      reModalRef.value.close();
      emits('reload');
    });
  }
};
const loadTreeSelectData = async () => {
  const data = await getTreeList();
  treeSelectData.value = data as any;
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
