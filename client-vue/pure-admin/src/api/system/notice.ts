import { http } from "@/utils/http";
export function getPageList(params: any) {
  return http.request("get", "/notice/paged-list", { params });
}

export const submitData = (params: any) => {
  return http.request(
    params.id ? "put" : "post",
    `/notice/${params.id ?? ""}`,
    {
      data: params
    }
  );
};

export const getSingle = (id: number) => {
  return http.request("get", `/notice/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/notice/${id}`);
};
