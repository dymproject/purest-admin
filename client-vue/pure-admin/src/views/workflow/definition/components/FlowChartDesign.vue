<script setup lang="ts">
import { onMounted, ref, h } from "vue";
import "@logicflow/core/dist/index.css";
import "@logicflow/extension/lib/style/index.css";
import LogicFlow from "@logicflow/core";
import { DndPanel } from "@logicflow/extension";
import { VxeFormPropTypes } from "vxe-pc-ui";
import { ReOrganizationTreeSelect } from "@/components/ReOrganizationTreeSelect";
import { cleanObject } from "@/utils/workflow";
import Start from "./customerNodes/Start";
import End from "./customerNodes/End";
import GeneralAuditing from "./customerNodes/GeneralAuditing";
import Judge from "./customerNodes/Judge";
const props = defineProps<{ widgetData: any[] | null | undefined }>();
const conditionOptions = ref([]);
LogicFlow.use(DndPanel);

interface AuditingDataModel extends Record<string, unknown> {
  auditorType: number;
  auditor?: number;
  auditorName?: string;
  auditingStepType: number;
}
interface JudgedDataModel extends Record<string, unknown> {
  field: string;
  fieldString: string;
  operate: string;
  operateString: string;
  value: string;
}

const lf = ref<LogicFlow>();
const dndPanel = ref<DndPanel>();
onMounted(() => {
  lf.value = new LogicFlow({
    container: document.getElementById("lf-container") as HTMLElement,
    grid: true,
    edgeType: `Judge`,
    snapline: true,
    keyboard: {
      enabled: true
    },
    plugins: [DndPanel]
  });
  lf.value.register(Start);
  lf.value.register(End);
  lf.value.register(GeneralAuditing);
  lf.value.register(Judge);
  lf.value.on("node:click,edge:click,blank:click", args => {
    if (showBusinessPanel.value) {
      showBusinessPanel.value = false;
    }
    const { data } = args;
    if (data) {
      selectedNodeId.value = data.id;
      if (data.type == "GeneralAuditing") {
        showAuditingNode.value = true;
        showBusinessPanel.value = true;
        const nodeProperties = lf.value
          .getNodeModelById(data.id)
          .getProperties() satisfies AuditingDataModel;
        nodeProperties.auditingStepType;
        auditingData.value =
          Object.keys(nodeProperties).length == 0
            ? defaultAuditingData()
            : nodeProperties;
      } else if (data.type == "Judge") {
        showBusinessPanel.value = true;
        showAuditingNode.value = false;
        const edgeProperties = lf.value
          .getEdgeModelById(data.id)
          .getProperties() as JudgedDataModel;
        judgedData.value = edgeProperties;
        conditionOptions.value = [];
        props.widgetData.forEach(value => {
          if (["VxeInput", "VxeNumberInput"].indexOf(value.name) > -1) {
            conditionOptions.value.push({
              title: value.title,
              field: value.field
            });
          }
        });
      }
    }
  });
  dndPanel.value = lf.value.extension.dndPanel as DndPanel;
  dndPanel.value.setPatternItems([
    {
      type: "Start",
      text: "开始",
      label: "开始节点",
      icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAAH6ji2bAAAABGdBTUEAALGPC/xhBQAAAnBJREFUOBGdVL1rU1EcPfdGBddmaZLiEhdx1MHZQXApraCzQ7GKLgoRBxMfcRELuihWKcXFRcEWF8HBf0DdDCKYRZpnl7p0svLe9Zzbd29eQhTbC8nv+9zf130AT63jvooOGS8Vf9Nt5zxba7sXQwODfkWpkbjTQfCGUd9gIp3uuPP8bZ946g56dYQvnBg+b1HB8VIQmMFrazKcKSvFW2dQTxJnJdQ77urmXWOMBCmXM2Rke4S7UAW+/8ywwFoewmBps2tu7mbTdp8VMOkIRAkKfrVawalJTtIliclFbaOBqa0M2xImHeVIfd/nKAfVq/LGnPss5Kh00VEdSzfwnBXPUpmykNss4lUI9C1ga+8PNrBD5YeqRY2Zz8PhjooIbfJXjowvQJBqkmEkVnktWhwu2SM7SMx7Cj0N9IC0oQXRo8xwAGzQms+xrB/nNSUWVveI48ayrFGyC2+E2C+aWrZHXvOuz+CiV6iycWe1Rd1Q6+QUG07nb5SbPrL4426d+9E1axKjY3AoRrlEeSQo2Eu0T6BWAAr6COhTcWjRaYfKG5csnvytvUr/WY4rrPMB53Uo7jZRjXaG6/CFfNMaXEu75nG47X+oepU7PKJvvzGDY1YLSKHJrK7vFUwXKkaxwhCW3u+sDFMVrIju54RYYbFKpALZAo7sB6wcKyyrd+aBMryMT2gPyD6GsQoRFkGHr14TthZni9ck0z+Pnmee460mHXbRAypKNy3nuMdrWgVKj8YVV8E7PSzp1BZ9SJnJAsXdryw/h5ctboUVi4AFiCd+lQaYMw5z3LGTBKjLQOeUF35k89f58Vv/tGh+l+PE/wG0rgfIUbZK5AAAAABJRU5ErkJggg=="
    },
    {
      type: "GeneralAuditing",
      label: "审批节点",
      icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAEFVwZaAAAABGdBTUEAALGPC/xhBQAAAqlJREFUOBF9VM9rE0EUfrMJNUKLihGbpLGtaCOIR8VjQMGDePCgCCIiCNqzCAp2MyYUCXhUtF5E0D+g1t48qAd7CCLqQUQKEWkStcEfVGlLdp/fm3aW2QQdyLzf33zz5m2IsAZ9XhDpyaaIZkTS4ASzK41TFao88GuJ3hsr2pAbipHxuSYyKRugagICGANkfFnNh3HeE2N0b3nN2cgnpcictw5veJIzxmDamSlxxQZicq/mflxhbaH8BLRbuRwNtZp0JAhoplVRUdzmCe/vO27wFuuA3S5qXruGdboy5/PRGFsbFGKo/haRtQHIrM83bVeTrOgNhZReWaYGnE4aUQgTJNvijJFF4jQ8BxJE5xfKatZWmZcTQ+BVgh7s8SgPlCkcec4mGTmieTP4xd7PcpIEg1TX6gdeLW8rTVMVLVvb7ctXoH0Cydl2QOPJBG21STE5OsnbweVYzAnD3A7PVILuY0yiiyDwSm2g441r6rMSgp6iK42yqroI2QoXeJVeA+YeZSa47gZdXaZWQKTrG93rukk/l2Al6Kzh5AZEl7dDQy+JjgFahQjRopSxPbrbvK7GRe9ePWBo1wcU7sYrFZtavXALwGw/7Dnc50urrHJuTPSoO2IMV3gUQGNg87IbSOIY9BpiT9HV7FCZ94nPXb3MSnwHn/FFFE1vG6DTby+r31KAkUktB3Qf6ikUPWxW1BkXSPQeMHHiW0+HAd2GelJsZz1OJegCxqzl+CLVHa/IibuHeJ1HAKzhuDR+ymNaRFM+4jU6UWKXorRmbyqkq/D76FffevwdCp+jN3UAN/C9JRVTDuOxC/oh+EdMnqIOrlYteKSfadVRGLJFJPSB/ti/6K8f0CNymg/iH2gO/f0DwE0yjAFO6l8JaR5j0VPwPwfaYHqOqrCI319WzwhwzNW/aQAAAABJRU5ErkJggg==",
      className: "important-node"
    },
    {
      type: "End",
      text: "结束",
      label: "结束节点",
      icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAAH6ji2bAAAABGdBTUEAALGPC/xhBQAAA1BJREFUOBFtVE1IVUEYPXOf+tq40Y3vPcmFIdSjIorWoRG0ERWUgnb5FwVhYQSl72oUoZAboxKNFtWiwKRN0M+jpfSzqJAQclHo001tKkjl3emc8V69igP3znzfnO/M9zcDcKT67azmjYWTwl9Vn7Vumeqzj1DVb6cleQY4oAVnIOPb+mKAGxQmKI5CWNJ2aLPatxWa3aB9K7/fB+/Z0jUF6TmMlFLQqrkECWQzOZxYGjTlOl8eeKaIY5yHnFn486xBustDjWT6dG7pmjHOJd+33t0iitTPkK6tEvjxq4h2MozQ6WFSX/LkDUGfFwfhEZj1Auz/U4pyAi5Sznd7uKzznXeVHlI/Aywmk6j7fsUsEuCGADrWARXXwjxWQsUbIupDHJI7kF5dRktg0eN81IbiZXiTESic50iwS+t1oJgL83jAiBupLDCQqwziaWSoAFSeIR3P5Xv5az00wyIn35QRYTwdSYbz8pH8fxUUAtxnFvYmEmgI0wYXUXcCCSpeEVpXlsRhBnCEATxWylL9+EKCAYhe1NGstUa6356kS9NVvt3DU2fd+Wtbm/+lSbylJqsqkSm9CRhvoJVlvKPvF1RKY/FcPn5j4UfIMLn8D4UYb54BNsilTDXKnF4CfTobA0FpoW/LSp306wkXM+XaOJhZaFkcNM82ASNAWMrhrUbRfmyeI1FvRBTpN06WKxa9BK0o2E4Pd3zfBBEwPsv9sQBnmLVbLEIZ/Xe9LYwJu/Er17W6HYVBc7vmuk0xUQ+pqxdom5Fnp55SiytXLPYoMXNM4u4SNSCFWnrVIzKG3EGyMXo6n/BQOe+bX3FClY4PwydVhthOZ9NnS+ntiLh0fxtlUJHAuGaFoVmttpVMeum0p3WEXbcll94l1wM/gZ0Ccczop77VvN2I7TlsZCsuXf1WHvWEhjO8DPtyOVg2/mvK9QqboEth+7pD6NUQC1HN/TwvydGBARi9MZSzLE4b8Ru3XhX2PBxf8E1er2A6516o0w4sIA+lwURhAON82Kwe2iDAC1Watq4XHaGQ7skLcFOtI5lDxuM2gZe6WFIotPAhbaeYlU4to5cuarF1QrcZ/lwrLaCJl66JBocYZnrNlvm2+MBCTmUymPrYZVbjdlr/BxlMjmNmNI3SAAAAAElFTkSuQmCC"
    }
  ]);
  lf.value.render(null);
});
const getData = () => {
  return lf.value.getGraphData();
};
const defaultAuditingData = () => {
  return {
    auditor: null,
    auditorType: 0,
    auditorName: "",
    auditingStepType: 0
  };
};
const auditingData = ref<AuditingDataModel>(defaultAuditingData());
const defaultJudgedData = () => {
  return {
    field: "",
    operate: "=",
    value: ""
  } as JudgedDataModel;
};
const judgedData = ref<JudgedDataModel>(defaultJudgedData());
const auditingColumns: VxeFormPropTypes.Items = [
  {
    field: "auditorType",
    titleWidth: 100,
    title: "审批人类型",
    span: 24,
    itemRender: {
      name: "VxeSelect",
      options: [
        { label: "组织机构", value: 0 },
        { label: "用户", value: 1, disabled: true },
        { label: "角色", value: 2, disabled: true }
      ],
      events: {
        change: value => {
          auditingData.value.auditor = value.data.auditor;
        }
      }
    }
  },
  {
    field: "auditor",
    titleWidth: 100,
    title: "审批人",
    span: 24,
    slots: {
      default: ({ data }) => [
        h(ReOrganizationTreeSelect, {
          modelValue: auditingData.value.auditor,
          onNodeClick(nodeData: Recordable) {
            auditingData.value.auditor = nodeData.id;
            auditingData.value.auditorName = nodeData.name;
          }
        })
      ]
    }
  },
  {
    field: "auditingStepType",
    titleWidth: 100,
    title: "节点类型",
    span: 20,
    itemRender: {
      name: "VxeSwitch",
      props: {
        openLabel: "并行节点",
        closeLabel: "串行节点",
        openValue: 1,
        closeValue: 0
      },
      events: {
        change: value => {
          auditingData.value.auditingStepType = value.data.auditingStepType;
        }
      }
    }
  },
  {
    align: "center",
    span: 4,
    itemRender: {
      name: "VxeButtonGroup",
      options: [{ type: "submit", content: "保存", status: "primary" }],
      events: {
        click() {
          setProperties("node");
          showBusinessPanel.value = false;
        }
      }
    }
  }
];
const setProperties = (type: string) => {
  if (type == "node") {
    debugger;

    let cleanedAuditingData = cleanObject(auditingData.value, [
      "auditorType",
      "auditorName",
      "auditingStepType",
      "auditor"
    ]);
    const node = lf.value.getNodeModelById(selectedNodeId.value);
    node.updateText(auditingData.value.auditorName);
    node.setProperties(cleanedAuditingData);
    console.log("cleanedAuditingData", cleanedAuditingData);
  } else {
    const edge = lf.value.getEdgeModelById(selectedNodeId.value);
    const option = conditionOptions.value.find(
      x => x.field == judgedData.value.field
    );
    edge.updateText(
      `${option.title}${judgedData.value.operate}${judgedData.value.value}`
    );
    console.log("judgedData", judgedData.value);
    edge.setProperties(judgedData.value);
  }
  showBusinessPanel.value = false;
};
const showBusinessPanel = ref(false);
const showAuditingNode = ref(false);
const selectedNodeId = ref<string>("");
const renderDesign = data => {
  lf.value.render(data);
  showBusinessPanel.value = false;
};
defineExpose({ getData, renderDesign });
</script>

<template>
  <div>
    <div id="lf-container" style="width: 100%; height: 600px" ref="container" />
    <div class="business-div" v-if="showBusinessPanel">
      <el-card v-if="showAuditingNode">
        <vxe-form :data="auditingData" :items="auditingColumns"></vxe-form>
      </el-card>
      <el-card style="height: 100px" v-else>
        <el-row :span="24">
          <el-col :span="6">
            <vxe-select
              clearable
              filterable
              placement="top"
              :transfer="true"
              v-model="judgedData.field"
              placeholder="请选择"
            >
              <vxe-option
                v-for="c in conditionOptions"
                :key="c.field"
                :value="c.field"
                :label="c.title"
              ></vxe-option>
            </vxe-select>
          </el-col>
          <el-col :span="6">
            <vxe-select
              v-model="judgedData.operate"
              placeholder=""
              placement="top"
              :transfer="true"
              clearable
              filterable
            >
              <vxe-option value="==" label="等于"></vxe-option>
              <vxe-option value=">" label="大于"></vxe-option>
              <vxe-option value="<" label="小于"></vxe-option>
              <vxe-option value=">=" label="大于等于"></vxe-option>
              <vxe-option value="<=" label="小于等于"></vxe-option>
              <vxe-option value="!=" label="不等于"></vxe-option>
            </vxe-select>
          </el-col>
          <el-col :span="6">
            <vxe-input v-model="judgedData.value" placeholder=""></vxe-input>
          </el-col>
          <el-col :span="6">
            <vxe-button
              status="primary"
              @click="setProperties('edge')"
              content="保存"
            ></vxe-button>
          </el-col>
        </el-row>
      </el-card>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.business-div {
  position: absolute;
  bottom: 0px;
  width: 100%;
}
</style>
