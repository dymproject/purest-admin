import type { UserInfoType } from "@/store/modules/types";
import { http } from "@/utils/http";

export interface OrganizationTreeNode {
  id: number;
  name: string;
  children?: OrganizationTreeNode[];
  remark: string;
}
export interface UserInfo {
  name: string;
  telephone: string;
  email: string;
}
export interface SystemPlatformInfo {
  frameworkDescription: string;
  osDescription: string;
  osVersion: string;
  osArchitecture: string;
  machineName: string;
  version: string;
}
/** 登录 */
export const login = async (data?: object) => {
  return http.request<UserInfoType>("post", "/auth/login", { data });
};
/** 获取权限 */
export const getUserPermissions = async () => {
  return http.request<string[]>("get", "/auth/functions");
};
/** 绑定用户 */
export const bindUser = (params: any) => {
  return http.request("post", "/auth/bind-user", {
    data: params
  });
};
/** 注册用户 */
export const registerUser = (params: any) => {
  return http.request("post", "/auth/register-user", {
    data: params
  });
};
/** 获取组织机构 */
export const getOrganizationTreeData = () => {
  return http.request<OrganizationTreeNode[]>("get", "/auth/organization-tree");
};

export const getUserInfo = (password: string) => {
  return http.request<UserInfo>("get", "/auth/user-info", {
    params: { password: password }
  });
};
export const editUserInfo = (params: any) => {
  return http.request<UserInfo>("put", "/auth/user-info", {
    data: params
  });
};
export const getSystemPlatformInfo = () => {
  return http.request<SystemPlatformInfo>(
    "get",
    "/auth/system-platform-info",
    {}
  );
};

export const getUnReadNotice = () => {
  return http.request("get", "/auth/unread-notice");
};