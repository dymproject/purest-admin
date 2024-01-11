import { http } from "@/utils/http";

export const getTreeList = () => {
  return http.request("get", "/interface/tree");
};

export const getPageList = params => {
  return http.request("get", "/interface/paged-list", { params });
};

export const submitData = (params: any) => {
  return http.request(
    params.id ? "put" : "post",
    `/interface/${params.id ?? ""}`,
    {
      data: params
    }
  );
};

export const getSingle = (id: number) => {
  return http.request("get", `/interface/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/interface/${id}`);
};
