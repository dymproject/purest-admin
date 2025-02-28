import { requestClient } from '#/api/request';
interface Role {
  name: string;
}
export interface UserProfile {
  id: number;
  name: string;
  roleId: number;
  role: Role;
  account: string;
  telephone: string;
  organizationId: number;
  email: string;
  remark: string;
}

/**
 * @description: 获取用户列表
 */
export function getPageList(params: any) {
  return requestClient.get('/user/paged-list', { params });
}

export const submitData = (params: any) => {
  return requestClient.request(`/user/${params.id ?? ''}`, {
    method: params.id ? 'put' : 'post',
    data: params,
  });
};

export const getSingle = (id: number) => {
  return requestClient.get(`/user/${id}`);
};

export const getPassword = (id: number) => {
  return requestClient.get(`/user/${id}/password`);
};

export const deleteData = (id: number) => {
  return requestClient.delete(`/user/${id}`);
};

export const stop = (id: number) => {
  return requestClient.post(`/user/${id}/stop`);
};

export const normal = (id: number) => {
  return requestClient.post(`/user/${id}/normal`);
};
