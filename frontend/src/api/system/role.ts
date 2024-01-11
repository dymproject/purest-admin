import { http } from "@/utils/http";

export const getAllList = params => {
  return http.request("get", "/role/all", params);
};

export function getPageList(params) {
  return http.request("get", "/role/paged-list", { params });
}

export const submitData = (params: any) => {
  return http.request(params.id ? "put" : "post", `/role/${params.id ?? ""}`, {
    data: params
  });
};

export const getSingle = (id: number) => {
  return http.request("get", `/role/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/role/${id}`);
};

export function getFunctions(roleId: number) {
  return http.request("get", `/role/${roleId}/functions`);
}

export function assignFunction(roleId: number, data: any) {
  return http.request("post", `/role/${roleId}/assign-function`, { data });
}
