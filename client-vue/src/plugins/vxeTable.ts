import type { App } from "vue";
import VXETable from "vxe-table";
import "vxe-table/lib/style.css";

export function useVxeTable(app: App) {
  app.use(VXETable);
}
