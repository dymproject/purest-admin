import { http } from "@/utils/http";

export function getPageList(params: any) {
  return http.request("get", "/request-log/paged-list", { params });
}

export function getLogCount(params: any) {
  return http.request("get", "/request-log/request-log-chart", { params });
}
