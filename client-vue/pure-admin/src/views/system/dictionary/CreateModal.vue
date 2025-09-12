<script lang="ts" setup>
import { ref, nextTick, reactive } from "vue";
import { VxeFormPropTypes, VxeFormInstance, VxeModalInstance } from "vxe-pc-ui";
import { getSingle, submitData } from "@/api/system/dictionary";
const emits = defineEmits<{ (e: "reload"): void }>();
const props = defineProps<{ categoryId?: number }>();
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

interface AddDictDataInput {
  name: string;
  code: string;
  categoryId: null | number;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    name: "",
    code: "",
    categoryId: props.categoryId,
    remark: ""
  };
};
const formData = ref<AddDictDataInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "名称",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入名称" }
    }
  },
  {
    field: "code",
    title: "编码",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入编码" }
    }
  },
  {
    field: "sort",
    title: "排序",
    span: 24,
    itemRender: {
      name: "$input",
      props: { type: "number", min: 0, placeholder: "请输入排序序号" }
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
  name: [{ required: true, message: "请输入字典名称" }],
  code: [{ required: true, message: "请输入字典编码" }]
});

const showAddModal = () => {
  showModal(`添加字典`);
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: Recordable) => {
  showModal(`编辑字典->${record.name}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: Recordable) => {
  showModal(`查看字典->${record.name}`, false);
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
    v-model="modalOptions.modalValue"
    width="600"
    height="400"
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
