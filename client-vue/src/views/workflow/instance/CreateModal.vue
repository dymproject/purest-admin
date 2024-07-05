<script lang="tsx" setup>
import { ref, nextTick, reactive, h } from "vue";
import { VxeFormPropTypes, VxeFormInstance, VxeModalInstance } from "vxe-pc-ui";
import { getSingle, submitData } from "@/api/system/notice";
import { ReDictionary } from "@/components/ReDictionary";
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

interface AddNoticeInput {
  title: string;
  content: string;
  noticeType: number | null;
  level: number | null;
  remark: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    title: "",
    content: "",
    noticeType: null,
    level: null,
    remark: ""
  };
};
const formData = ref<AddNoticeInput>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "title",
    title: "标题",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入标题" }
    }
  },
  {
    field: "noticeType",
    title: "类型",
    span: 24,
    slots: {
      default: ({ data }) => {
        return h(ReDictionary, {
          code: "dict_notice_type",
          modelValue: data.noticeType,
          placeholder: "请选择类型",
          onChange({ value }) {
            data.noticeType = value;
            formRef.value.validateField("noticeType");
          }
        });
      }
    }
  },
  {
    field: "level",
    title: "级别",
    span: 24,
    slots: {
      default: ({ data }) => {
        return h(ReDictionary, {
          code: "dict_notice_level",
          modelValue: data.level,
          placeholder: "请选择级别",
          onChange({ value }) {
            data.level = value;
            formRef.value.validateField("level");
          }
        });
      }
    }
  },
  {
    field: "content",
    title: "内容",
    span: 24,
    itemRender: {
      name: "$textarea",
      props: { rows: 4, placeholder: "请输入内容" }
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
  title: [{ required: true, message: "请输入标题" }],
  content: [{ required: true, message: "请输入内容" }],
  noticeType: [{ required: true, message: "请选择通知类型" }],
  level: [{ required: true, message: "请选择通知级别" }]
});

const showAddModal = () => {
  showModal(`添加通知公告`);
  formData.value = defaultFormData();
  nextTick(() => {
    formRef.value.clearValidate();
  });
};
const showEditModal = (record: Recordable) => {
  showModal(`编辑通知公告->${record.title}`);
  nextTick(() => {
    formRef.value.clearValidate();
    getSingle(record.id).then((data: any) => {
      formData.value = data;
    });
  });
};
const showViewModal = (record: Recordable) => {
  showModal(`查看通知公告->${record.title}`, false);
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
