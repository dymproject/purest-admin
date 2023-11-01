import { http } from "@/utils/http";

export const getCategoryPageList = (params: any) => {
  return http.request("get", "/dict-category/category-paged-list", { params });
};

export const getPageList = (params: any) => {
  return http.request("get", "/dict-data/paged-list", { params });
};

export const submitData = (params: any) => {
  return http.request(
    params.id ? "put" : "post",
    `/dict-data/${params.id ?? ""}`,
    {
      data: params
    }
  );
};

export const getSingle = (id: number) => {
  return http.request("get", `/dict-data/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/dict-data/${id}`);
};
