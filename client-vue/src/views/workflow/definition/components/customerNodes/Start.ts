import { CircleNode, CircleNodeModel } from "@logicflow/core";

class StartModel extends CircleNodeModel { }

class StartView extends CircleNode { }

export default {
  type: "Start",
  view: StartView,
  model: StartModel,
};