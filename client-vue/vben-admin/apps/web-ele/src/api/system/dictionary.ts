import { requestClient as http } from '#/api/request';

export const getCategoryPageList = (params: any) => {
  return http.request('/dict-category/paged-list', { params, method: 'get' });
};

export const getPageList = (params: any) => {
  return http.request('/dict-data/paged-list', { params, method: 'get' });
};

export const submitData = (params: any) => {
  return http.request(`/dict-data/${params.id ?? ''}`, {
    method: params.id ? 'put' : 'post',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return http.get(`/dict-data/${id}`);
};

export const deleteData = (id: number) => {
  return http.delete(`/dict-data/${id}`);
};

export const getDictionaryDataByCode = (code: string) => {
  return http.get(`/dict-data`, { params: { categoryCode: code } });
};
