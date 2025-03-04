import { requestClient as http } from '#/api/request';

export const getTreeList = () => {
  return http.get('/function/tree');
};

export function getPageList(params: any) {
  return http.request('/function/paged-list', { method: 'GET', params });
}

export const submitData = (params: any) => {
  return http.post(`/function/${params.id ?? ''}`, {
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
  return http.request(`/function/assign-interface`, {
    method: 'POST',
    data: params,
  });
};

export const removeInterface = (id: number) => {
  return http.delete(`/function/${id}/function-interface`);
};
