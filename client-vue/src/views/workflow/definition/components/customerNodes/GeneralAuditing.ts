import { RectNode, RectNodeModel } from "@logicflow/core";

class GeneralAuditingModel extends RectNodeModel { }

class GeneralAuditingView extends RectNode { }

export default {
  type: "GeneralAuditing",
  view: GeneralAuditingView,
  model: GeneralAuditingModel,
};