import { http } from "@/utils/http";
interface Role {
  name: string;
}
export interface UserProfile {
  id: number;
  name: string;
  roleId: number;
  role: Role;
  account: string;
  telephone: string;
  organizationId: number;
  email: string;
  remark: string;
}

/**
 * @description: 获取用户列表
 */
export function getPageList(params) {
  return http.request("get", "/user/paged-list", { params });
}

export const submitData = (params: any) => {
  return http.request(params.id ? "put" : "post", `/user/${params.id ?? ""}`, {
    data: params
  });
};

export const getSingle = (id: number) => {
  return http.request("get", `/user/${id}`);
};

export const getPassword = (id: number) => {
  return http.request("get", `/user/${id}/password`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/user/${id}`);
};

export const stop = (id: number) => {
  return http.request("post", `/user/${id}/stop`);
};

export const normal = (id: number) => {
  return http.request("post", `/user/${id}/normal`);
};
