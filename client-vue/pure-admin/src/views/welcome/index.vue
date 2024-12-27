<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useECharts, useDark } from "@pureadmin/utils";
import { getSystemPlatformInfo, SystemPlatformInfo } from "@/api/auth";
import { getLogCount } from "@/api/system/requestLog";
const defValue = (): SystemPlatformInfo => {
  return {
    frameworkDescription: "",
    osDescription: "",
    osVersion: "",
    osArchitecture: "",
    machineName: "",
    version: ""
  };
};
const systemPlatformInfo = ref<SystemPlatformInfo>(defValue());
const chartRef = ref();
// 兼容dark主题
const { isDark } = useDark();
const { setOptions } = useECharts(chartRef, {
  theme: isDark.value ? "dark" : "default"
});
const chartOptions = {
  tooltip: {
    trigger: "axis",
    axisPointer: {
      type: "shadow"
    }
  },
  yAxis: {
    type: "value"
  }
};
const showRequestCount = () => {
  getLogCount({}).then((result: any) => {
    const aa = { ...chartOptions, ...result };
    setOptions(aa);
  });
};

onMounted(() => {
  getSystemPlatformInfo().then((result: SystemPlatformInfo) => {
    systemPlatformInfo.value = result;
  });
  showRequestCount();
});
</script>

<template>
  <div>
    <el-card :shadow="`never`">
      <template #header> 系统信息 </template>
      <div class="descriptions-text" style="font-weight: 600">
        开源之路充满挑战，但每一步都凝结着作者的汗水与智慧。
        如果您觉得这个项目对您有帮助，不妨给它点个Star，给予一点小小的支持。您的每一个鼓励，都是我继续前行的动力，
        项目持续更新中，如果您有任何问题，可通过文档中的联系方式，提出宝贵意见。
        让我有更多的热情和信心去完善和优化这个项目。感谢您的支持与关注！
      </div>
      <el-descriptions :column="3" border>
        <el-descriptions-item
          label="机器名称"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.machineName }}
        </el-descriptions-item>
        <el-descriptions-item
          label="操作系统"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.osDescription }}
        </el-descriptions-item>
        <el-descriptions-item
          label="操作系统版本"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.osVersion }}
        </el-descriptions-item>
        <el-descriptions-item
          label="平台架构"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.osArchitecture }}
        </el-descriptions-item>

        >
        <el-descriptions-item
          label="程序核心框架版本"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.version }}
        </el-descriptions-item>
        <el-descriptions-item
          label="运行框架"
          label-align="right"
          align="center"
        >
          {{ systemPlatformInfo.frameworkDescription }}
        </el-descriptions-item>
        <el-descriptions-item
          label="gitee地址"
          label-align="right"
          align="center"
        >
          <el-link
            href="https://gitee.com/dymproject/purest-admin"
            target="_blank"
            type="success"
            class="descriptions-text"
          >
            https://gitee.com/dymproject/purest-admin
          </el-link>
        </el-descriptions-item>
        <el-descriptions-item
          label="github地址"
          label-align="right"
          align="center"
        >
          <el-link
            href="https://github.com/dymproject/purest-admin"
            target="_blank"
            type="danger"
            class="descriptions-text"
          >
            https://github.com/dymproject/purest-admin
          </el-link>
        </el-descriptions-item>
        <el-descriptions-item
          label="文档地址"
          label-align="right"
          align="center"
        >
          <el-link
            href="http://docs.purestadmin.com"
            target="_blank"
            type="danger"
            class="descriptions-text"
          >
            http://docs.purestadmin.com
          </el-link>
        </el-descriptions-item>
      </el-descriptions>
    </el-card>
    <el-card :shadow="`never`" class="table-card">
      <template #header>系统访问曲线</template>
      <div ref="chartRef" style="width: 100%; height: 35vh" />
    </el-card>
  </div>
</template>
<style scoped>
.descriptions-text {
  background: linear-gradient(
    to right,
    #ff0000,
    #0010f3
  ); /*设置渐变的方向从左到右 颜色从ff0000到ffff00*/
  background-clip: border-box;
  -webkit-background-clip: text; /*将设置的背景颜色限制在文字中*/
  -webkit-text-fill-color: transparent; /*给文字设置成透明*/
}
</style>
