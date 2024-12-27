import { PolylineEdge, PolylineEdgeModel } from "@logicflow/core";

class JudgeModel extends PolylineEdgeModel { }

export default {
  type: "Judge",
  view: PolylineEdge,
  model: JudgeModel,
};