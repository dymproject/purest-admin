import { requestClient as http } from '#/api/request';

export interface SystemConfigProfile {
  id: number;
  name: string;
  configCode: string;
  configValue: string;
  remark: string;
}

export function getPageList(params: any) {
  return http.request('/system-config/paged-list', { method: 'get', params });
}

export const submitData = (params: any) => {
  return http.request(`/system-config/${params.id ?? ''}`, {
    method: params.id ? 'put' : 'post',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return http.get(`/system-config/${id}`);
};

export const deleteData = (id: number) => {
  return http.delete(`/system-config/${id}`);
};
