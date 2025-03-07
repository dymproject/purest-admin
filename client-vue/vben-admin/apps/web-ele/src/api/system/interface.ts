import { requestClient as http } from '#/api/request';

export const getTreeList = () => {
  return http.get('/interface/tree');
};

export const getPageList = (params: any) => {
  return http.get('/interface/paged-list', { params });
};

export const submitData = (params: any) => {
  return http.request(`/interface/${params.id ?? ''}`, {
    method: params.id ? 'PUT' : 'POST',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return http.get(`/interface/${id}`);
};

export const deleteData = (id: number) => {
  return http.delete(`/interface/${id}`);
};

export const asyncApi = () => {
  return http.post('/interface/async-api');
};
