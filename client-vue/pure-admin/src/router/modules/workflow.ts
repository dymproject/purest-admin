import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import FlowChart from "~icons/ri/flow-chart";

const Layout = () => import("@/layout/index.vue");

export default {
  path: "/workflow",
  name: "workflow",
  component: Layout,
  meta: {
    icon: useRenderIcon(FlowChart),
    title: "工作流程",
    permissions: ["workflow"],
    rank: 0
  },
  children: [
    {
      path: "/workflow/definition",
      name: "workflow_definition",
      meta: {
        title: "流程模板",
        permissions: ["workflow.definition"]
      },
      component: () => import("@/views/workflow/definition/index.vue")
    },
    {
      path: "/workflow/my",
      name: "workflow_my",
      meta: {
        title: "我的流程",
        permissions: ["workflow.my"]
      },
      component: () => import("@/views/workflow/instance/index.vue")
    },
    {
      path: "/workflow/auditing",
      name: "workflow_auditing",
      meta: {
        title: "待办事项",
        permissions: ["workflow.auditing"]
      },
      component: () => import("@/views/workflow/instance/Auditing.vue")
    },
  ]
} satisfies RouteConfigsTable;
