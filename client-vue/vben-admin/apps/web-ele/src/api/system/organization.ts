import { requestClient as http } from '#/api/request';

export function getPageList(params: any) {
  return http.get('/organization/paged-list', { params });
}

export const submitData = (params: any) => {
  return http.request(`/organization/${params.id ?? ''}`, {
    method: params.id ? 'put' : 'post',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return http.get(`/organization/${id}`);
};

export const deleteData = (id: number) => {
  return http.delete(`/organization/${id}`);
};
