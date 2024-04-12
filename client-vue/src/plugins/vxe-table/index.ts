import type { App } from "vue";
import VXETable from "vxe-table";
import "vxe-table/lib/style.css";

export function useTable(app: App) {
  app.use(VXETable);
}
