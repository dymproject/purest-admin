import { http } from "@/utils/http";
export function getPageList(params: any) {
  return http.request("get", "/definition/paged-list", { params });
}

export const submitData = (params: any) => {
  return http.request(
    params.id ? "put" : "post",
    `/definition/${params.id ?? ""}`,
    {
      data: params
    }
  );
};

export const getSingle = (id: number) => {
  return http.request("get", `/definition/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/definition/${id}`);
};

export const lock = (id: number) => {
  return http.request("post", `/definition/${id}/lock`);
};

export const unlock = (id: number) => {
  return http.request("post", `/definition/${id}/unlock`);
};