<script lang="ts" setup>
import { ref, h, nextTick } from 'vue';
import type { VxeFormPropTypes, VxeFormInstance } from 'vxe-pc-ui';
import { ReOrganizationTreeSelect } from '#/components/organization';
import { getSingle, submitData } from '#/api/system/organization';
import { $t } from '@vben/locales';
const emits = defineEmits<{ (e: 'reload'): void }>();
const reModalRef = ref();
interface AddOrganizationInput {
  name: string;
  leader: string;
  parentId: number | null;
  telephone: string;
  sort: number;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    name: '',
    leader: '',
    parentId: null,
    telephone: '',
    sort: 0,
    remark: '',
  };
};
const formData = ref<AddOrganizationInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: 'name',
    title: $t('organization.form.name'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('organization.form.placeholder.name') },
    },
  },
  {
    field: 'leader',
    title: $t('organization.form.leader'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('organization.form.placeholder.leader') },
    },
  },
  {
    field: 'parentId',
    title: $t('organization.form.parentId'),
    span: 24,
    slots: {
      default: ({ data }) => [
        h(ReOrganizationTreeSelect, {
          modelValue: data.parentId,
          placeholder: $t('organization.form.placeholder.parentId'),
          onNodeClick(nodeData: any) {
            formData.value.parentId = nodeData.id;
            formRef.value?.validateField('parentId');
          },
        }),
      ],
    },
  },
  {
    field: 'telephone',
    title: $t('organization.form.telephone'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('organization.form.placeholder.telephone') },
    },
  },
  {
    field: 'sort',
    title: $t('organization.form.sort'),
    span: 24,
    itemRender: {
      name: '$input',
      props: {
        type: 'number',
        placeholder: $t('organization.form.placeholder.sort'),
      },
    },
  },
  {
    field: 'remark',
    title: $t('organization.form.remark'),
    span: 24,
    itemRender: {
      name: '$textarea',
      props: { placeholder: $t('organization.form.placeholder.remark') },
    },
  },
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: $t('organization.form.validate.name') }],
  parentId: [
    { required: true, message: $t('organization.form.validate.parentId') },
  ],
});

const showAddModal = () => {
  reModalRef.value.show($t('organization.add'));
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value?.clearValidate();
  });
};
const showEditModal = (record: any) => {
  reModalRef.value.show(`${$t('organization.edit')}->${record.name}`);
  nextTick(() => {
    formRef.value?.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: any) => {
  reModalRef.value.show(`${$t('organization.view')}->${record.name}`, true);
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
