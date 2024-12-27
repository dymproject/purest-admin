<script setup lang="ts">
import { ref, onBeforeMount } from "vue";
import { getDictionaryDataByCode } from "@/api/system/dictionary";
const options = ref();
const props = defineProps({
  code: {
    type: String,
    default: ""
  },
  options: {
    type: Array,
    default: () => []
  }
});
onBeforeMount(() => {
  getDictionaryDataByCode(props.code).then((result: any[]) => {
    options.value = result;
  });
});
</script>

<template>
  <div>
    <vxe-select
      v-bind="$attrs"
      :options="options"
      :option-props="{ label: 'name', value: 'id' }"
      :transfer="true"
      clearable
      filterable
    ></vxe-select>
  </div>
</template>
