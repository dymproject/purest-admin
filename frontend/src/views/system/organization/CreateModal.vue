<script lang="ts" setup>
import { ref, h, nextTick, reactive } from "vue";
import { VxeFormPropTypes, VxeFormInstance, VxeModalInstance } from "vxe-table";
import { ReOrganizationTreeSelect } from "@/components/ReOrganizationTreeSelect";
import { getSingle, submitData } from "@/api/system/organization";
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
    name: "",
    leader: "",
    parentId: null,
    telephone: "",
    sort: 0,
    remark: ""
  };
};
const formData = ref<AddOrganizationInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "组织机构名称",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入组织机构名称" }
    }
  },
  {
    field: "leader",
    title: "负责人",
    span: 24,
    itemRender: { name: "$input", props: { placeholder: "请输入负责人" } }
  },
  {
    field: "parentId",
    title: "父级组织机构",
    span: 24,
    slots: {
      default: ({ data }) => [
        h(ReOrganizationTreeSelect, {
          modelValue: data.parentId,
          onNodeClick(nodeData: Recordable) {
            formData.value.parentId = nodeData.id;
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
    field: "sort",
    title: "排序",
    span: 24,
    itemRender: {
      name: "$input",
      props: { type: "number", placeholder: "请输入电话号码" }
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
  name: [{ required: true, message: "请输入组织机构名称" }],

  organizationId: [{ required: true, message: "请选择组织机构" }]
});

const showAddModal = () => {
  showModal(`添加组织机构`);
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: Recordable) => {
  showModal(`编辑组织机构->${record.name}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: Recordable) => {
  showModal(`查看组织机构->${record.name}`, false);
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
    height="450"
    showFooter
    :destroy-on-close="true"
    v-model="modalOptions.modalValue"
    :title="modalOptions.modalTitle"
  >
    <template #default>
      <vxe-form
        ref="formRef"
        :data="formData"
        :items="formItems"
        :rules="formRules"
        :titleWidth="130"
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
