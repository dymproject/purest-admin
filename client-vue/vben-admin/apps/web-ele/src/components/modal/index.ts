import { $t } from '@vben/locales';
import { VxeUI } from 'vxe-pc-ui';

export { default as ReModal } from './src/ReModal.vue';
export const deleteConfirm = async () => {
  const type = await VxeUI.modal.confirm(
    $t('common.tooltip.delete'),
    $t('common.tooltip.title'),
    {
      size: 'small',
      confirmButtonText: $t('common.confirm'),
      cancelButtonText: $t('common.cancel'),
    },
  );
  return type === 'confirm';
};
