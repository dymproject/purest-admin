import { requestClient as http } from '#/api/request';

export const getTreeList = () => {
  return http.get('/function/tree');
};

export function getPageList(params: any) {
  return http.get('/function/paged-list', { params });
}

export const submitData = (params: any) => {
  return http.request(`/function/${params.id ?? ''}`, {
    method: params.id ? 'PUT' : 'POST',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return http.get(`/function/${id}`);
};

export const deleteData = (id: number) => {
  return http.delete(`/function/${id}`);
};

export const getFunInterface = (id: number): any => {
  return http.get(`/function/${id}/interfaces`);
};

export const assignInterface = (params: any) => {
  return http.post(`/function/assign-interface`, params);
};

export const removeInterface = (id: number) => {
  return http.delete(`/function/${id}/function-interface`);
};
