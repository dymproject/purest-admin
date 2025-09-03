import { type HubConnection, HubConnectionBuilder } from "uniapp-signalr";
import { useUserStore } from "@/store/user";
import { getSignalRUrl } from "@/utils/core";
/**
 * 返回signalr链接
 * @param url Url地址,开头要带/
 * @param directStart 直接启动
 * @returns HubConnection
 */
export const createConnection = (
    url: string,
    directStart?: boolean | undefined
): HubConnection => {
    const hubConnectionBuilder = new HubConnectionBuilder();
    const connection = hubConnectionBuilder
        .withUrl(getSignalRUrl(url), {
            accessTokenFactory: () => useUserStore().token
        })
        .withAutomaticReconnect()
        .build();
    if (!directStart) {
        connection.start()
            .catch(err => {
                console.error(err);
                useUserStore().logout()
            });
    }
    return connection;
};
/**
 * 返回signalr链接
 * @param url Url地址,开头要带/
 * @param directStart 直接启动
 * @returns HubConnection
 */
export const createConnectionAsync = async (
    url: string,
    directStart?: boolean | undefined
): Promise<HubConnection> => {
    const hubConnectionBuilder = new HubConnectionBuilder();
    const connection = hubConnectionBuilder
        .withUrl(getSignalRUrl(url), {
            accessTokenFactory: () => useUserStore().token
        })
        .withAutomaticReconnect()
        .build();
    if (!directStart) {
        await connection.start();
    }    
    return connection;
};
