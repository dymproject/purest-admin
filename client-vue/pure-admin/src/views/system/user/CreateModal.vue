<script lang="ts" setup>
import { ref, h, onBeforeMount, nextTick, reactive } from "vue";
import { VxeFormPropTypes, VxeSelect } from "vxe-pc-ui";
import { ReOrganizationTreeSelect } from "@/components/ReOrganizationTreeSelect";
import { getAllList } from "@/api/system/role";
import { getSingle, submitData } from "@/api/system/user";
const emits = defineEmits<{ (e: "reload"): void }>();
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
  roleId: number | null;
  organizationId: number | null;
  telephone: string;
  email: string;
  remark: string;
}
const formRef = ref();
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
const formData = ref<AddUserInput>(defaultFormData());
const roleOptions = ref<any[]>();
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "名称",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入用户名" }
    }
  },
  {
    field: "account",
    title: "帐号",
    span: 24,
    itemRender: { name: "$input", props: { placeholder: "请输入帐号" } }
  },
  {
    field: "roleId",
    title: "角色",
    span: 24,
    slots: {
      default: ({ data }) => [
        h(VxeSelect, {
          options: roleOptions.value,
          optionProps: {
            value: "id",
            label: "name"
          },
          placeholder: "请选择角色",
          modelValue: data.roleId,
          onChange(v) {
            data.roleId = v.value;
          }
        })
      ]
    }
  },
  {
    field: "organizationId",
    title: "组织机构",
    span: 24,
    slots: {
      default: ({ data }) => [
        h(ReOrganizationTreeSelect, {
          modelValue: data.organizationId,
          onNodeClick(nodeData: Recordable) {
            formData.value.organizationId = nodeData.id;
            formRef.value.validateField("organizationId");
          }
        })
      ]
    }
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
  name: [{ required: true, message: "请输入用户名" }],
  account: [
    { required: true, message: "请输入帐号" },
    { min: 3, message: "长度不能低于3个字符" }
  ],
  roleId: [{ required: true, message: "请选择角色" }],
  organizationId: [{ required: true, message: "请选择组织机构" }]
});

const showAddModal = () => {
  showModal(`添加用户`);
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: Recordable) => {
  showModal(`编辑用户->${record.name}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: Recordable) => {
  showModal(`查看用户->${record.name}`, false);
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

onBeforeMount(() => {
  getAllList({ roleName: "" }).then((result: any) => {
    roleOptions.value = result;
  });
});

defineExpose({ showAddModal, showEditModal, showViewModal });
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
