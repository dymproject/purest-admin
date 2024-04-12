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
      <el-descriptions :column="2" border>
        <el-descriptions-item
          label="机器名称"
          label-align="right"
          align="center"
          width="150"
        >
          {{ systemPlatformInfo.machineName }}
        </el-descriptions-item>
        <el-descriptions-item
          label="操作系统"
          label-align="right"
          align="center"
          width="150"
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
          label="项目地址"
          label-align="right"
          :span="2"
          align="center"
        >
          如果该项目帮助了您，希望能点个 Star 鼓励一下呦！
          &nbsp;&nbsp;&nbsp;&nbsp;
          <el-link
            href="https://gitee.com/dymproject/purest-admin"
            target="_blank"
            type="success"
          >
            gitee地址
          </el-link>
          &nbsp;&nbsp;&nbsp;&nbsp;
          <el-link
            href="https://github.com/dymproject/purest-admin"
            target="_blank"
            type="danger"
          >
            github地址
          </el-link>
          &nbsp;&nbsp;&nbsp;&nbsp;
        </el-descriptions-item>
      </el-descriptions>
    </el-card>
    <el-card :shadow="`never`" class="table-card">
      <template #header>系统访问曲线</template>
      <div ref="chartRef" style="width: 100%; height: 35vh" />
    </el-card>
  </div>
</template>
