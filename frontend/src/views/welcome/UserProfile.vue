<script setup lang="ts">
import { ref, onMounted } from "vue";
import { VxeFormPropTypes, VxeFormInstance } from "vxe-table";
import { getUserInfo, editUserInfo } from "@/api/auth";
import { message } from "@/utils/message";
import { useUserStore } from "@/store/modules/user";

interface EditCurrentUser {
  name: string;
  password: string;
  telephone: string;
  email: string;
}
const formRef = ref<VxeFormInstance>();
const defaultFormData = () => {
  return {
    name: "",
    password: "",
    telephone: "",
    email: ""
  };
};
const formData = ref<EditCurrentUser>(defaultFormData());
const formItems = ref<VxeFormPropTypes.Items>([
  {
    field: "name",
    title: "用户名",
    span: 24,
    itemRender: {
      name: "$input",
      props: { placeholder: "请输入用户名" }
    }
  },
  {
    field: "password",
    title: "密码",
    span: 24,
    itemRender: {
      name: "$input",
      props: {
        type: "password",
        placeholder: "",
        onFocus({ _ }) {
          formData.value.password = "";
        }
      }
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
    align: "center",
    span: 24,
    itemRender: {
      name: "$buttons",
      children: [
        { props: { type: "submit", content: "保存", status: "primary" } }
      ]
    }
  }
]);
const formRules = ref<VxeFormPropTypes.Rules>({
  name: [{ required: true, message: "请输入用户名" }]
});

const handleSubmit = async () => {
  const validate = await formRef.value.validate();
  if (!validate) {
    editUserInfo(formData.value).then(() => {
      message("2秒后返回登陆界面，请重新登陆！");
      setTimeout(() => {
        useUserStore().logOut();
      }, 1500);
    });
  }
};
const modalValue = ref(false);
const password = ref("");
const handleConfirm = () => {
  if (password.value.trim() == "") {
    message("请输入密码");
    return;
  }
  getUserInfo(password.value)
    .then((data: any) => {
      modalValue.value = false;
      formData.value = data;
    })
    .catch(error => {
      console.log(error);
    });
};
onMounted(() => {
  setTimeout(() => {
    modalValue.value = true;
  }, 1000);
});
</script>

<template>
  <div>
    <el-card :shadow="`never`">
      <vxe-form
        ref="formRef"
        :data="formData"
        :items="formItems"
        :rules="formRules"
        :titleWidth="100"
        :titleColon="true"
        :titleAlign="`right`"
        @submit="handleSubmit"
      />
    </el-card>
    <vxe-modal :showClose="false" v-model="modalValue" width="600" show-footer>
      <template #title>
        <span style="color: rgb(231, 139, 139)">请输入密码</span>
      </template>

      <template #default>
        <vxe-input
          v-model="password"
          style="width: 100%"
          placeholder="******"
          type="password"
        />
      </template>
      <template #footer>
        <vxe-button status="primary" @click="handleConfirm">确定</vxe-button>
      </template>
    </vxe-modal>
  </div>
</template>

<style lang="scss" scoped></style>
