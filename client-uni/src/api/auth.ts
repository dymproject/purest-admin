import { LoginOutput, UserInfoOutput } from "@/dtos/auth";
import { http } from "@/utils/http";

/** 登录 */
export const login = async (data?: object) => {
	return http.post<LoginOutput>("/auth/login", data);
};
/** 获取权限 */
export const getUserPermissions = async () => {
	return http.get<string[]>("/auth/functions");
};
// /** 绑定用户 */
// export const bindUser = (params : any) => {
// 	return http.request("post", "/auth/bind-user", {
// 		data: params
// 	});
// };
// /** 注册用户 */
// export const registerUser = (params : any) => {
// 	return http.request("post", "/auth/register-user", {
// 		data: params
// 	});
// };
// /** 获取组织机构 */
// export const getOrganizationTreeData = () => {
// 	return http.request<OrganizationTreeNode[]>("get", "/auth/organization-tree");
// };

export const getUserInfo = (password: string) => {
	return http.get<UserInfoOutput>("/auth/user-info", { password: password });
};


export const getUnReadNotice = () => {
	return http.get("/auth/unread-notice");
};