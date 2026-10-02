<script setup>
import TaskCard from "./TaskCard.vue";

defineProps({
  tasks: {
    type: Array,
    required: true,
  },

  loading: {
    type: Boolean,
    default: false,
  },
});

const emit = defineEmits(["edit", "delete"]);
</script>

<template>
  <div
    v-if="loading"
    class="rounded-xl bg-white p-10 text-center text-gray-500 shadow-sm"
  >
    Loading tasks...
  </div>

  <div
    v-else-if="tasks.length === 0"
    class="rounded-xl bg-white p-10 text-center shadow-sm"
  >
    <h3 class="font-semibold text-gray-800">No tasks found</h3>

    <p class="mt-2 text-sm text-gray-500">Create a task to get started.</p>
  </div>

  <div v-else class="space-y-4">
    <TaskCard
      v-for="task in tasks"
      :key="task.id"
      :task="task"
      @edit="emit('edit', $event)"
      @delete="emit('delete', $event)"
    />
  </div>
</template>
