<script setup lang="ts">
import { onMounted, ref, h } from "vue";
import "@logicflow/core/dist/index.css";
import "@logicflow/extension/lib/style/index.css";
import LogicFlow from "@logicflow/core";
import { DndPanel } from "@logicflow/extension";
import { VxeFormPropTypes } from "vxe-pc-ui";
import { ReOrganizationTreeSelect } from "@/components/ReOrganizationTreeSelect";
const props = defineProps<{ widgetData: any[] | null | undefined }>();
const conditionOptions = ref([]);
LogicFlow.use(DndPanel);

interface AuditingDataModel {
  auditorType: number;
  auditor?: number;
  isCountersign: number;
}

const lf = ref<LogicFlow>();
const dndPanel = ref<DndPanel>();
onMounted(() => {
  lf.value = new LogicFlow({
    container: document.getElementById("lf-container") as HTMLElement,
    grid: true,
    snapline: true,
    keyboard: {
      enabled: true
    },
    plugins: [DndPanel]
  });
  lf.value.on("node:click,edge:click,blank:click", args => {
    if (showBusinessPanel.value) {
      showBusinessPanel.value = false;
    }
    const { data } = args;
    if (data) {
      selectedNodeId.value = data.id;
      showBusinessPanel.value = true;
      if (data.type == "rect") {
        showAuditingNode.value = true;
        setAuditingPanelData(data.id);
      } else if (data.type == "diamond") {
        showAuditingNode.value = false;

        props.widgetData.forEach(value => {
          if (value.name == "VxeInput") {
            conditionOptions.value.push({
              title: value.title,
              field: value.field
            });
          }
        });
        console.log(props.widgetData);
      }
    }
  });
  dndPanel.value = lf.value.extension.dndPanel as DndPanel;
  dndPanel.value.setPatternItems([
    {
      type: "circle",
      text: "开始",
      label: "开始节点",
      icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAAH6ji2bAAAABGdBTUEAALGPC/xhBQAAAnBJREFUOBGdVL1rU1EcPfdGBddmaZLiEhdx1MHZQXApraCzQ7GKLgoRBxMfcRELuihWKcXFRcEWF8HBf0DdDCKYRZpnl7p0svLe9Zzbd29eQhTbC8nv+9zf130AT63jvooOGS8Vf9Nt5zxba7sXQwODfkWpkbjTQfCGUd9gIp3uuPP8bZ946g56dYQvnBg+b1HB8VIQmMFrazKcKSvFW2dQTxJnJdQ77urmXWOMBCmXM2Rke4S7UAW+/8ywwFoewmBps2tu7mbTdp8VMOkIRAkKfrVawalJTtIliclFbaOBqa0M2xImHeVIfd/nKAfVq/LGnPss5Kh00VEdSzfwnBXPUpmykNss4lUI9C1ga+8PNrBD5YeqRY2Zz8PhjooIbfJXjowvQJBqkmEkVnktWhwu2SM7SMx7Cj0N9IC0oQXRo8xwAGzQms+xrB/nNSUWVveI48ayrFGyC2+E2C+aWrZHXvOuz+CiV6iycWe1Rd1Q6+QUG07nb5SbPrL4426d+9E1axKjY3AoRrlEeSQo2Eu0T6BWAAr6COhTcWjRaYfKG5csnvytvUr/WY4rrPMB53Uo7jZRjXaG6/CFfNMaXEu75nG47X+oepU7PKJvvzGDY1YLSKHJrK7vFUwXKkaxwhCW3u+sDFMVrIju54RYYbFKpALZAo7sB6wcKyyrd+aBMryMT2gPyD6GsQoRFkGHr14TthZni9ck0z+Pnmee460mHXbRAypKNy3nuMdrWgVKj8YVV8E7PSzp1BZ9SJnJAsXdryw/h5ctboUVi4AFiCd+lQaYMw5z3LGTBKjLQOeUF35k89f58Vv/tGh+l+PE/wG0rgfIUbZK5AAAAABJRU5ErkJggg=="
    },
    {
      type: "rect",
      label: "审批节点",
      icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAEFVwZaAAAABGdBTUEAALGPC/xhBQAAAqlJREFUOBF9VM9rE0EUfrMJNUKLihGbpLGtaCOIR8VjQMGDePCgCCIiCNqzCAp2MyYUCXhUtF5E0D+g1t48qAd7CCLqQUQKEWkStcEfVGlLdp/fm3aW2QQdyLzf33zz5m2IsAZ9XhDpyaaIZkTS4ASzK41TFao88GuJ3hsr2pAbipHxuSYyKRugagICGANkfFnNh3HeE2N0b3nN2cgnpcictw5veJIzxmDamSlxxQZicq/mflxhbaH8BLRbuRwNtZp0JAhoplVRUdzmCe/vO27wFuuA3S5qXruGdboy5/PRGFsbFGKo/haRtQHIrM83bVeTrOgNhZReWaYGnE4aUQgTJNvijJFF4jQ8BxJE5xfKatZWmZcTQ+BVgh7s8SgPlCkcec4mGTmieTP4xd7PcpIEg1TX6gdeLW8rTVMVLVvb7ctXoH0Cydl2QOPJBG21STE5OsnbweVYzAnD3A7PVILuY0yiiyDwSm2g441r6rMSgp6iK42yqroI2QoXeJVeA+YeZSa47gZdXaZWQKTrG93rukk/l2Al6Kzh5AZEl7dDQy+JjgFahQjRopSxPbrbvK7GRe9ePWBo1wcU7sYrFZtavXALwGw/7Dnc50urrHJuTPSoO2IMV3gUQGNg87IbSOIY9BpiT9HV7FCZ94nPXb3MSnwHn/FFFE1vG6DTby+r31KAkUktB3Qf6ikUPWxW1BkXSPQeMHHiW0+HAd2GelJsZz1OJegCxqzl+CLVHa/IibuHeJ1HAKzhuDR+ymNaRFM+4jU6UWKXorRmbyqkq/D76FffevwdCp+jN3UAN/C9JRVTDuOxC/oh+EdMnqIOrlYteKSfadVRGLJFJPSB/ti/6K8f0CNymg/iH2gO/f0DwE0yjAFO6l8JaR5j0VPwPwfaYHqOqrCI319WzwhwzNW/aQAAAABJRU5ErkJggg==",
      className: "important-node"
    },
    {
      type: "diamond",
      label: "条件判断",
      icon: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABUAAAAVCAYAAAHeEJUAAAAABGdBTUEAALGPC/xhBQAAAvVJREFUOBGNVEFrE0EU/mY3bQoiFlOkaUJrQUQoWMGePLX24EH0IIoHKQiCV0G8iE1covgLiqA/QTzVm1JPogc9tIJYFaQtlhQxqYjSpunu+L7JvmUTU3AgmTfvffPNN++9WSA1DO182f6xwILzD5btfAoQmwL5KJEwiQyVbSVZ0IgRyV6PTpIJ81E5ZvqfHQR0HUOBHW4L5Et2kQ6Zf7iAOhTFAA8s0pEP7AXO1uAA52SbqGk6h/6J45LaLhO64ByfcUzM39V7ZiAdS2yCePPEIQYvTUHqM/n7dgQNfBKWPjpF4ISk8q3J4nB11qw6X8l+FsF3EhlkEMfrjIer3wJTLwS2aCNcj4DbGxXTw00JmAuO+Ni6bBxVUCvS5d9aa04+so4pHW5jLTywuXAL7jJ+D06sl82Sgl2JuVBQn498zkc2bGKxULHjCnSMadBKYDYYHAtsby1EQ5lNGrQd4Y3v4Zo0XdGEmDno46yCM9Tk+RiJmUYHS/aXHPNTcjxcbTFna000PFJHIVZ5lFRqRpJWk9/+QtlOUYJj9HG5pVFEU7zqIYDVsw2s+AJaD8wTd2umgSCCyUxgGsS1Y6TBwXQQTFuZaHcd8gAGioE90hlsY+wMcs30RduYtxanjMGal8H5dMW67dmT1JFtYUEe8LiQLRsPZ6IIc7A4J5tqco3T0pnv/4u0kyzrYUq7gASuEyI8VXKvB9Odytv6jS/PNaZBln0nioJG/AVQRZvApOdhjj3Jt8QC8Im09SafwdBdvIpztpxWxpeKCC+EsFdS8DCyuCn2munFpL7ctHKp+Xc5cMybeIyMAN33SPL3ZR9QV1XVwLyzHm6Iv0/yeUuUb7PPlZC4D4HZkeu6dpF4v9j9MreGtMbxMMRLIcjJic9yHi7WQ3yVKzZVWUr5UrViJvn1FfUlwe/KYVfYyWRLSGNu16hR01U9IacajXPei0wx/5BqgInvJN+MMNtNme7ReU9SBbgntovn0kKHpFg7UogZvaZiOue/q1SBo9ktHzQAAAAASUVORK5CYII="
    },
    {
      type: "circle",
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
const auditingData = ref<AuditingDataModel>({
  auditor: null,
  auditorType: 0,
  isCountersign: 0
});
const auditingColumns: VxeFormPropTypes.Items = [
  {
    field: "auditorType",
    titleWidth: 100,
    title: "审核人类型",
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
    title: "审核人",
    span: 24,
    slots: {
      default: ({ data }) => [
        h(ReOrganizationTreeSelect, {
          modelValue: auditingData.value.auditor,
          onNodeClick(nodeData: Recordable) {
            auditingData.value.auditor = nodeData.id;
          }
        })
      ]
    }
  },
  {
    field: "isCountersign",
    titleWidth: 100,
    title: "是否会签",
    span: 20,
    itemRender: {
      name: "VxeSwitch",
      props: {
        openLabel: "是",
        closeLabel: "否",
        openValue: 1,
        closeValue: 0
      },
      events: {
        change: value => {
          auditingData.value.isCountersign = value.data.isCountersign;
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
          setAuditingNodeProperties(auditingData.value);
          showBusinessPanel.value = false;
        }
      }
    }
  }
];
const setAuditingNodeProperties = (data: AuditingDataModel) => {
  lf.value.getNodeModelById(selectedNodeId.value).setProperties(data);
};
const setAuditingPanelData = (nodeId: string) => {
  const currentNodeProperties = lf.value
    .getNodeModelById(nodeId)
    .getProperties() as any;
  auditingData.value = currentNodeProperties;
};
const showBusinessPanel = ref(false);
const showAuditingNode = ref(false);
const selectedNodeId = ref<string>("");
defineExpose({ getData });
</script>

<template>
  <div>
    <div id="lf-container" style="width: 100%; height: 600px" ref="container" />
    <div class="business-div" v-if="showBusinessPanel">
      <el-card v-if="showAuditingNode">
        <vxe-form :data="auditingData" :items="auditingColumns"></vxe-form>
      </el-card>
      <el-card style="height: 200px" v-else>
        <vxe-select placeholder="默认尺寸">
          <vxe-option
            v-for="c in conditionOptions"
            :key="c.field"
            :value="c.field"
            :label="c.title"
          ></vxe-option>
        </vxe-select>
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
