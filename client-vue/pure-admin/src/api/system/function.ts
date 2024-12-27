import { http } from "@/utils/http";

export const getTreeList = () => {
  return http.request("get", "/function/tree");
};

export function getPageList(params) {
  return http.request("get", "/function/paged-list", { params });
}

export const submitData = (params: any) => {
  return http.request(
    params.id ? "put" : "post",
    `/function/${params.id ?? ""}`,
    {
      data: params
    }
  );
};

export const getSingle = (id: number) => {
  return http.request("get", `/function/${id}`);
};

export const deleteData = (id: number) => {
  return http.request("delete", `/function/${id}`);
};

export const getFunInterface = (id: number): any => {
  return http.request("get", `/function/${id}/interfaces`);
};

export const assignInterface = (params: any) => {
  return http.request("post", `/function/assign-interface`, {
    data: params
  });
};

export const removeInterface = (id: number) => {
  return http.request("delete", `/function/${id}/function-interface`);
};
