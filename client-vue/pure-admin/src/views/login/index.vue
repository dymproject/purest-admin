<script setup lang="ts">
import Motion from "./utils/motion";
import { useRouter } from "vue-router";
import { message } from "@/utils/message";
import { ElMessageBox } from "element-plus";
import { loginRules } from "./utils/rule";
import { useNav } from "@/layout/hooks/useNav";
import type { FormInstance } from "element-plus";
import { useLayout } from "@/layout/hooks/useLayout";
import { bg, avatar, illustration } from "./utils/static";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { ref, reactive, toRaw, onMounted, onBeforeUnmount } from "vue";
import { useDataThemeChange } from "@/layout/hooks/useDataThemeChange";
import { addPathMatch } from "@/router/utils";
import { usePermissionStoreHook } from "@/store/modules/permission";

import dayIcon from "@/assets/svg/day.svg?component";
import darkIcon from "@/assets/svg/dark.svg?component";
import Lock from "~icons/ri/lock-fill";
import User from "~icons/ri/user-3-fill";
import Github from "~icons/simple-icons/github";
import Gitee from "~icons/simple-icons/gitee";
import { useUserStoreHook } from "@/store/modules/user";
import { createConnectionAsync } from "@/utils/signalr";
import { HubConnection, HubConnectionState } from "@microsoft/signalr";
import Register from "./Register.vue";
import Binding from "./Binding.vue";
const registerModalRef = ref();
const bindingModalRef = ref();
defineOptions({
  name: "Login"
});
const router = useRouter();
const loading = ref(false);
const ruleFormRef = ref<FormInstance>();

const { initStorage } = useLayout();
initStorage();

const { dataTheme, dataThemeChange } = useDataThemeChange();
dataThemeChange();
const { title } = useNav();

const ruleForm = reactive({
  account: "",
  password: ""
});

const onLogin = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate(async (valid, fields) => {
    if (valid) {
      loading.value = true;
      try {
        const user = await useUserStoreHook().login(ruleForm);
        if (user) {
          usePermissionStoreHook().handleWholeMenus([]);
          addPathMatch();
          router.push("/");
          message("登录成功", { type: "success" });
        } else {
          message("登录失败");
        }
      } catch (error) {
        message(error.message, { type: "warning" });
      } finally {
        loading.value = false;
      }
    }
  });
};

/** 使用公共函数，避免`removeEventListener`失效 */
function onkeypress({ code }: KeyboardEvent) {
  if (code === "Enter") {
    onLogin(ruleFormRef.value);
  }
}
const persistenceId = ref<number>(0);
const connectionId = ref<string>("");
const connection = ref<HubConnection>();
const createAuthorizationConnection = async () => {
  connection.value = await createConnectionAsync(`/authorization`);
  connectionId.value = connection.value.connectionId;
  connection.value.on("NoticeOpenAuthorizationPage", (url: string) => {
    window.open(url, "_blank");
  });
  connection.value.on("NoticeRegister", (oAuth2UserId: number) => {
    persistenceId.value = oAuth2UserId;
    ElMessageBox.confirm("未检测到系统内相关联的用户信息！", "温馨提示", {
      confirmButtonText: "有账号，去绑定",
      cancelButtonText: "无账号，去注册"
    })
      .then(() => {
        bindingModalRef.value.showAddModal();
      })
      .catch(() => {
        registerModalRef.value.showAddModal();
      });
  });
  connection.value.on("NoticeRedirect", (accessToken, userInfo) => {
    useUserStoreHook().setToken(accessToken);
    useUserStoreHook().setCurrentUser(userInfo);
    usePermissionStoreHook().handleWholeMenus([]);
    addPathMatch();
    router.push("/");
  });
};

const toAuthorize = (type: string) => {
  if (
    connection.value &&
    connection.value.state === HubConnectionState.Connected
  ) {
    connection.value.invoke("Authorize", type);
  } else {
    message("连接服务器失败，请刷新后重试");
  }
};

onMounted(() => {
  window.document.addEventListener("keypress", onkeypress);
  createAuthorizationConnection();
});

onBeforeUnmount(() => {
  window.document.removeEventListener("keypress", onkeypress);
});
</script>

<template>
  <div class="select-none">
    <img :src="bg" class="wave" />
    <div class="absolute flex-c right-5 top-3">
      <!-- 主题 -->
      <el-switch
        v-model="dataTheme"
        inline-prompt
        :active-icon="dayIcon"
        :inactive-icon="darkIcon"
        @change="dataThemeChange"
      />
    </div>
    <div class="login-container">
      <div class="img">
        <component :is="toRaw(illustration)" />
      </div>
      <div class="login-box">
        <div class="login-form">
          <avatar class="avatar" />
          <Motion>
            <h2 class="outline-none">{{ title }}</h2>
          </Motion>

          <el-form
            ref="ruleFormRef"
            :model="ruleForm"
            :rules="loginRules"
            size="large"
          >
            <Motion :delay="100">
              <el-form-item
                :rules="[
                  {
                    required: true,
                    message: '请输入账号',
                    trigger: 'blur'
                  }
                ]"
                prop="account"
              >
                <el-input
                  v-model="ruleForm.account"
                  clearable
                  placeholder="账号"
                  :prefix-icon="useRenderIcon(User)"
                />
              </el-form-item>
            </Motion>

            <Motion :delay="150">
              <el-form-item prop="password">
                <el-input
                  v-model="ruleForm.password"
                  clearable
                  show-password
                  placeholder="密码"
                  :prefix-icon="useRenderIcon(Lock)"
                />
              </el-form-item>
            </Motion>

            <Motion :delay="250">
              <el-button
                class="w-full mt-4"
                size="default"
                type="primary"
                :loading="loading"
                @click="onLogin(ruleFormRef)"
              >
                登录
              </el-button>
            </Motion>
          </el-form>
          <el-divider> 第三方登录 </el-divider>
          <div class="button-container">
            <el-button
              type="primary"
              color="#4F4F4F"
              plain
              circle
              :icon="useRenderIcon(Github)"
              @click="toAuthorize('github')"
            />
            <el-button
              type="primary"
              color="#FF2F00"
              plain
              circle
              :icon="useRenderIcon(Gitee)"
              @click="toAuthorize('gitee')"
            />
          </div>
        </div>
      </div>
    </div>
    <Register
      ref="registerModalRef"
      :connection-id="connectionId"
      :o-auth2-user-id="persistenceId"
    />
    <Binding
      ref="bindingModalRef"
      :connection-id="connectionId"
      :o-auth2-user-id="persistenceId"
    />
  </div>
</template>

<style scoped>
@import url("@/style/login.css");
</style>

<style lang="scss" scoped>
.button-container {
  display: flex;
  justify-content: center;
}
:deep(.el-input-group__append, .el-input-group__prepend) {
  padding: 0;
}
</style>
