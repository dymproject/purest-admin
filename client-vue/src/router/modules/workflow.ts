import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import Collection from "@iconify-icons/ep/collection";

const Layout = () => import("@/layout/index.vue");

export default {
  path: "/workflow",
  name: "workflow",
  component: Layout,
  redirect: "/workflow",
  meta: {
    icon: useRenderIcon(Collection),
    title: "工作流程",
    permissions: ["system"],
    rank: 0
  },
  children: [
    {
      path: "/definition",
      name: "workflow_definition",
      meta: {
        title: "流程模板",
        // permissions: ["system.user"]
      },
      component: () => import("@/views/workflow/definition/index.vue")
    },
    {
      path: "/self",
      name: "workflow_self",
      meta: {
        title: "我的流程",
        // permissions: ["system.user"]
      },
      component: () => import("@/views/workflow/instance/index.vue")
    },
    {
      path: "/auditing",
      name: "workflow_auditing",
      meta: {
        title: "待办事项",
        // permissions: ["system.user"]
      },
      component: () => import("@/views/workflow/instance/Auditing.vue")
    },
  ]
} as RouteConfigsTable;
