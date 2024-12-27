<script setup lang="ts">
import { computed } from "vue";
import { isUrl } from "@pureadmin/utils";
import { menuType } from "@/layout/types";

const props = defineProps<{
  to: menuType;
}>();

const isExternalLink = computed(() => isUrl(props.to.name));
const getLinkProps = (item: menuType) => {
  if (isExternalLink.value) {
    return {
      href: item.name,
      target: "_blank",
      rel: "noopener"
    };
  }
  if (item.children?.length == 1) {
    return {
      to: item.children[0]
    };
  }
  return {
    to: item
  };
};
</script>

<template>
  <component
    :is="isExternalLink ? 'a' : 'router-link'"
    v-bind="getLinkProps(to)"
  >
    <slot />
  </component>
</template>
