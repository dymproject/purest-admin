import { useUserStore } from "@/store/user";

export const hasPermission = (permissionCodes: string | string[]): boolean => {
    const userStore = useUserStore();
    const permissions = Array.isArray(permissionCodes) ? permissionCodes : [permissionCodes];

    const storePermissions = userStore.permissions;

    return storePermissions.some(permission => permissions.includes(permission));
}