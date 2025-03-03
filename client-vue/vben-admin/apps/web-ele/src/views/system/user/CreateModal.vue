<script lang="ts" setup>
import { ref, h, onBeforeMount, nextTick } from 'vue';
import { VxeSelect, type VxeFormPropTypes } from 'vxe-pc-ui';
import { ReOrganizationTreeSelect } from '#/components/organization';
import { getAllList } from '#/api/system/role';
import { getSingle, submitData } from '#/api/system/user';
import { $t } from '#/locales';
const emits = defineEmits<{ (e: 'reload'): void }>();
const reModalRef = ref();
interface AddUserInput {
  name: string;
  account: string;
  password: string;
  roleId: number | null;
  organizationId: number | null;
  telephone: string;
  email: string;
  remark: string;
}
const formRef = ref();
const defaultFormData = () => {
  return {
    name: '',
    account: '',
    password: '',
    roleId: null,
    organizationId: null,
    telephone: '',
    email: '',
    remark: '',
  };
};
const formData = ref<AddUserInput>(defaultFormData());
const roleOptions = ref<any[]>();
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: 'name',
    title: $t('user.form.name'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('user.form.placeholder.name') },
    },
  },
  {
    field: 'account',
    title: $t('user.form.account'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('user.form.placeholder.account') },
    },
  },
  {
    field: 'roleId',
    title: $t('user.form.role'),
    span: 12,
    slots: {
      default: ({ data }) => [
        h(VxeSelect, {
          options: roleOptions.value,
          optionProps: {
            value: 'id',
            label: 'name',
          },
          placeholder: $t('user.form.placeholder.role'),
          modelValue: data.roleId,
          onChange(v) {
            data.roleId = v.value;
          },
        }),
      ],
    },
  },
  {
    field: 'organizationId',
    title: $t('user.form.organization'),
    span: 12,
    slots: {
      default: ({ data }) => [
        h(ReOrganizationTreeSelect, {
          modelValue: data.organizationId,
          placeholder: $t('user.form.placeholder.organization'),
          onNodeClick(nodeData: any) {
            formData.value.organizationId = nodeData.id;
            formRef.value.validateField('organizationId');
          },
        }),
      ],
    },
  },
  {
    field: 'telephone',
    title: $t('user.form.phone'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('user.form.placeholder.phone') },
    },
  },
  {
    field: 'email',
    title: $t('user.form.email'),
    span: 24,
    itemRender: {
      name: '$input',
      props: { placeholder: $t('user.form.placeholder.email') },
    },
  },
  {
    field: 'remark',
    title: $t('user.form.remark'),
    span: 24,
    itemRender: {
      name: '$textarea',
      props: { placeholder: $t('user.form.placeholder.remark') },
    },
  },
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: $t('user.form.validate.name') }],
  account: [
    { required: true, message: $t('user.form.validate.account') },
    { min: 3, message: $t('user.form.validate.min') },
  ],
  roleId: [{ required: true, message: $t('user.form.validate.role') }],
  organizationId: [
    { required: true, message: $t('user.form.validate.organization') },
  ],
});

const showAddModal = () => {
  reModalRef.value.show($t('user.add'));
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: any) => {
  reModalRef.value.show(`${$t('user.edit')}->${record.name}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: any) => {
  reModalRef.value.show(`${$t('user.view')}->${record.name}`, true);
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
      emits('reload');
      reModalRef.value.close();
    });
  }
};

onBeforeMount(() => {
  getAllList({ roleName: '' }).then((result: any) => {
    roleOptions.value = result;
  });
});

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
