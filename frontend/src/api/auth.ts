import { UserInfoType } from "@/store/modules/types";
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

/** 登录 */
export const login = async (data?: object) => {
  return http.request<UserInfoType>("post", "/auth/login", { data });
};
/** 获取权限 */
export const getUserPermissions = async () => {
  return http.request<string[]>("get", "/auth/functions");
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
