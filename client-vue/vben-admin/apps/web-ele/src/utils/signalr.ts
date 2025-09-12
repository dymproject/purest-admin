import { type HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { useAccessStore } from '@vben/stores';
/**
 * 返回signalr链接
 * @param url Url地址,开头要带/
 * @param directStart 直接启动
 * @returns HubConnection
 */
export const createConnection = (
  url: string,
  directStart?: boolean | undefined,
): HubConnection => {
  const hubConnectionBuilder = new HubConnectionBuilder();
  const connection = hubConnectionBuilder
    .withUrl(`/signalr-hubs${url}`, {
      accessTokenFactory: () => useAccessStore().accessToken ?? '',
    })
    .withAutomaticReconnect()
    .build();
  if (!directStart) {
    connection.start();
  }
  connection.onclose(() => {
    console.log('connection closed');
  });
  return connection;
};

export const createConnectionAsync = async (
  url: string,
  directStart?: boolean | undefined,
): Promise<HubConnection> => {
  const hubConnectionBuilder = new HubConnectionBuilder();
  const connection = hubConnectionBuilder
    .withUrl(`/signalr-hubs${url}`, {
      accessTokenFactory: () => useAccessStore().accessToken ?? '',
    })
    .withAutomaticReconnect()
    .build();
  if (!directStart) {
    await connection.start();
  }
  return connection;
};
