import { type HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useUserStoreHook } from "@/store/modules/user";
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
    .withUrl(`/signalr-hubs${url}`, {
      accessTokenFactory: () => useUserStoreHook().getToken
    })
    .withAutomaticReconnect()
    .build();
  if (!directStart) {
    connection.start();
  }
  return connection;
};

export const createConnectionAsync = async (
  url: string,
  directStart?: boolean | undefined
): Promise<HubConnection> => {
  const hubConnectionBuilder = new HubConnectionBuilder();
  const connection = hubConnectionBuilder
    .withUrl(`/signalr-hubs${url}`, {
      accessTokenFactory: () => useUserStoreHook().getToken
    })
    .withAutomaticReconnect()
    .build();
  if (!directStart) {
    await connection.start();
  }
  return connection;
};
