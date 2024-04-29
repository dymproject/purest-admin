import { defineStore } from "pinia";
import { store } from "@/store";
import { routerArrays } from "@/layout/types";
import { router, resetRouter } from "@/router";
import { storageSession } from "@pureadmin/utils";
import { getUserPermissions, login } from "@/api/auth";
import { useMultiTagsStoreHook } from "@/store/modules/multiTags";
import type { UserInfoType } from "./types";

interface IUserStore {
  currentUser: UserInfoType;
  accessToken: string;
}
const CURRENT_USER = "current-user";
const ACCESS_TOKEN = "access-token";
export const useUserStore = defineStore({
  id: "pure-user",
  state: (): IUserStore => ({
    currentUser: storageSession().getItem<UserInfoType>(CURRENT_USER),
    accessToken: storageSession().getItem<string>(ACCESS_TOKEN)
  }),
  getters: {
    getToken(): string {
      return this.accessToken;
    },
    getCurrentUser(): UserInfoType {
      return this.currentUser;
    }
  },
  actions: {
    setToken(accessToken: string) {
      this.accessToken = accessToken;
      storageSession().setItem(ACCESS_TOKEN, accessToken);
    },
    setCurrentUser(currentUser: UserInfoType) {
      this.currentUser = currentUser;
      storageSession().setItem(CURRENT_USER, currentUser);
    },
    /** 登入 */
    async login(data: any) {
      const user = await login(data);
      if (user) {
        const permissions = await getUserPermissions();
        user.permissions = permissions ?? [];
        this.setCurrentUser(user);
        return Promise.resolve(user);
      } else {
        return Promise.reject("登陆失败");
      }
    },
    /** 前端登出（不调用接口） */
    logOut() {
      this.currentUser = null;
      this.accessToken = null;
      storageSession().setItem(ACCESS_TOKEN, null);
      storageSession().setItem(CURRENT_USER, null);
      useMultiTagsStoreHook().handleTags("equal", [...routerArrays]);
      resetRouter();
      router.push("/login");
    }
  }
});

export function useUserStoreHook() {
  return useUserStore(store);
}
