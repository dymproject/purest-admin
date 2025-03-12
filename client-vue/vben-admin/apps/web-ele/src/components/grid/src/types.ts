import type { VxeComponentSizeType } from 'vxe-pc-ui';
import type { VxeGridPropTypes } from 'vxe-table';

export interface PurestGridProps {
  height?: number;
  columns: VxeGridPropTypes.Columns<any> | Array<any>;
  customToolbarActions?: VxeGridPropTypes.ToolbarConfig;
  commonOperation?: CommonOperationType | undefined;
  request: (params: any) => Promise<any>;
  rowKey?: string;
  size?: VxeComponentSizeType | undefined;
  treeConfig?: any;
  customePager?: {
    total: number;
    pageIndex: number;
    pageSize: number;
  };
  searchOptions?: {
    formData: any;
    formItems: Array<any>;
    submit: (params?: any) => void;
    reset: () => void;
  };
}

export type CommonOperationType = {
  view?: {
    permissionCode: string;
    params?: any;
    handleClick: (params?: any) => void;
  };
  edit?: {
    permissionCode: string;
    params?: any;
    handleClick: (params?: any) => void;
  };
  delete?: {
    permissionCode: string;
    params?: any;
    handleClick: (params?: any) => void;
  };
  add?: {
    permissionCode: string;
    params?: any;
    handleClick: (params?: any) => void;
  };
  import?: {
    permissionCode: string;
    params?: any;
    handleClick: (params?: any) => void;
  };
  export?: {
    permissionCode: string;
    params?: any;
    handleClick: (params?: any) => void;
  };
};
