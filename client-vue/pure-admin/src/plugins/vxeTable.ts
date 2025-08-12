import type { App } from "vue";
// ...纯表格
import VxeTable from 'vxe-table'
import 'vxe-table/lib/style.css'
// ...

// ...可选 UI
import VxeUI from 'vxe-pc-ui'
import 'vxe-pc-ui/lib/style.css'
// ...

// ...
import VxeUIDesign from 'vxe-design'
import 'vxe-design/lib/style.css'
// ...

export function useVxeTable(app: App) {
  app.use(VxeUI).use(VxeTable).use(VxeUIDesign);
}
