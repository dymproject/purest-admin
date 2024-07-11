import { http } from "@/utils/http";
export function getSelfPageList(params: any) {
  return http.request("get", "/instance/self-paged-list", { params });
}

export function getAuditingPageList(params: any) {
  return http.request("get", "/instance/auditing-paged-list", { params });
}

export const startWorkflow = (id: number, params: any) => {
  return http.request("post", `/instance/${id}/start`, { data: params });
};

export const auditingWorkflow = (id: number, params: any) => {
  return http.request("post", `/instance/${id}/auditing`, { data: params });
};

export const getAuditingDetail = (id: number) => {
  return http.request("get", `/instance/${id}/auditing-detail`);
};
