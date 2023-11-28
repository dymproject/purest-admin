import { UserInfoType } from "@/store/modules/types";
import { http } from "@/utils/http";

/** 登录 */
export const login = async (data?: object) => {
  return http.request<UserInfoType>("post", "/auth/login", { data });
};

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

/** 获取组织机构 */
export const getOrganizationTreeData = () => {
  return http.request<OrganizationTreeNode[]>("get", "/auth/organization-tree");
};

export const getUserInfo = (password: string) => {
  return http.request<UserInfo>("get", "/auth/userinfo", {
    params: { password: password }
  });
};
export const editUserInfo = (params: any) => {
  return http.request<UserInfo>("put", "/auth/userinfo", {
    data: params
  });
};
