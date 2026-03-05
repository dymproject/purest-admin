import { ReVxeGrid } from '#/components/grid';
import { ReModal } from '#/components/modal';
import { ReDictionary } from '#/components/dictionary';

declare module 'vue' {
  export interface GlobalComponents {
    ReVxeGrid: typeof ReVxeGrid;
    ReModal: typeof ReModal;
    ReDictionary: typeof ReDictionary;
  }
}

export {};
