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
/** 获取组织机构 */
export const getOrganizationTreeData = () => {
  return http.request<OrganizationTreeNode[]>("get", "/auth/organization-tree");
};
