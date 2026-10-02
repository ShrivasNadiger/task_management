<script setup>
defineProps({
  modelValue: {
    type: String,
    required: true,
  },
});

const emit = defineEmits(["update:modelValue"]);
</script>

<template>
  <div class="flex flex-wrap gap-2">
    <button
      v-for="status in ['ALL', 'PENDING', 'IN_PROGRESS', 'COMPLETED']"
      :key="status"
      @click="emit('update:modelValue', status)"
      class="rounded-lg px-4 py-2 text-sm font-medium"
      :class="
        modelValue === status
          ? 'bg-blue-600 text-white'
          : 'bg-white text-gray-700 hover:bg-gray-50'
      "
    >
      {{ status === "IN_PROGRESS" ? "In Progress" : status }}
    </button>
  </div>
</template>
