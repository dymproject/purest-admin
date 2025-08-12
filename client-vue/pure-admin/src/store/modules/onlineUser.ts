import type { HubConnection } from "@microsoft/signalr";
import { defineStore } from "pinia";
import { createConnection } from "@/utils/signalr";

interface ISignalrState {
  connection?: HubConnection;
}
export const useOnlineUserStore = defineStore('purest-onlineUser', {
  state: (): ISignalrState => ({}),
  getters: {
    getConnection(state) {
      return state.connection;
    }
  },
  actions: {
    createConnection() {
      const connection = createConnection(`/online-user`);
      this.connection = connection;
      return connection;
    }
  }
});
