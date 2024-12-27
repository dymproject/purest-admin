import { http } from "@/utils/http";

export interface SystemConfigProfile {
  id: number;
  name: string;
  configCode: string;
  configValue: string;
  remark: string;
}

export function getPageList(params: any) {
  return http.request("get", "/system-config/paged-list", { params });
}

export const submitData = (params: any) => {
  return http.request(
    params.id ? "put" : "post",
    `/system-config/${params.id ?? ""}`,
    {
      data: params
    }
  );
};

export const getSingle = (id: number) => {
  return http.request("get", `/system-config/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/system-config/${id}`);
};
