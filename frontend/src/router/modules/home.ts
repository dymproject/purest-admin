const { VITE_HIDE_HOME } = import.meta.env;
const Layout = () => import("@/layout/index.vue");
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import House from "@iconify-icons/ep/house";

export default {
  path: "/",
  name: "Home",
  component: Layout,
  redirect: "/welcome",
  meta: {
    icon: useRenderIcon(House),
    title: "首页",
    rank: 0
  },
  children: [
    {
      path: "/welcome",
      name: "Welcome",
      component: () => import("@/views/welcome/index.vue"),
      meta: {
        title: "首页",
        showLink: VITE_HIDE_HOME === "true" ? false : true
      }
    },
    {
      path: "/userprofile",
      name: "UserProfile",
      component: () => import("@/views/welcome/UserProfile.vue"),
      meta: {
        title: "个人信息",
        showLink: false
      }
    }
  ]
} as RouteConfigsTable;
