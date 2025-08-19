import { defineStore } from "pinia";
import { createConnection } from "@/utils/signalr";
import { HubConnection } from "uniapp-signalr";
export const useOnlineUserStore = defineStore("online-user", {
    state: () => ({
        connection: {} as HubConnection
    }),
    persist: true,
    actions: {
        async createConnection() {
            const connection = await createConnection(`/online-user`);
            this.connection = connection;
            return connection;
        }
    }
});                