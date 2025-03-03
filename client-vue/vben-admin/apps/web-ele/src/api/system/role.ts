import { requestClient as http } from '#/api/request';

export const getAllList = (params: any) => {
  return http.request('/role/roles', { method: 'GET', params });
};

export function getPageList(params: any) {
  return http.request('/role/paged-list', { method: 'GET', params });
}

export const submitData = (params: any) => {
  return http.request(`/role/${params.id ?? ''}`, {
    method: params.id ? 'PUT' : 'POST',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return http.request(`/role/${id}`, { method: 'GET' });
};

export const deleteData = (id: number) => {
  return http.request(`/role/${id}`, { method: 'DELETE' });
};

export function getFunctions(roleId: number) {
  return http.request(`/role/${roleId}/functions`, { method: 'GET' });
}

export function assignFunction(roleId: number, data: any) {
  return http.request(`/role/${roleId}/assign-function`, {
    method: 'POST',
    data,
  });
}
