import { CircleNode, CircleNodeModel } from "@logicflow/core";

class EndModel extends CircleNodeModel { }

class EndView extends CircleNode { }

export default {
  type: "End",
  view: EndView,
  model: EndModel,
};