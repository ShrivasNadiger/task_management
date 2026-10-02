<script setup>
defineProps({
  task: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits(["edit", "delete"]);
</script>

<template>
  <article class="rounded-xl bg-white p-5 shadow-sm">
    <div class="flex items-start justify-between gap-4">
      <div>
        <h3 class="font-semibold text-gray-900">
          {{ task.title }}
        </h3>

        <p class="mt-1 text-sm text-gray-500">
          {{ task.description }}
        </p>
      </div>

      <div class="flex gap-2">
        <button
          @click="emit('edit', task)"
          class="rounded-lg px-3 py-1 text-sm text-blue-600 hover:bg-blue-50"
        >
          Edit
        </button>

        <button
          @click="emit('delete', task)"
          class="rounded-lg px-3 py-1 text-sm text-red-600 hover:bg-red-50"
        >
          Delete
        </button>
      </div>
    </div>

    <div class="mt-4 flex flex-wrap gap-2">
      <span
        class="rounded-full bg-yellow-100 px-3 py-1 text-xs font-medium text-yellow-700"
      >
        {{ task.status }}
      </span>

      <span
        class="rounded-full bg-red-100 px-3 py-1 text-xs font-medium text-red-700"
      >
        {{ task.priority }}
      </span>

      <span
        class="rounded-full bg-gray-100 px-3 py-1 text-xs font-medium text-gray-600"
      >
        👤 {{ task.userName }}
      </span>
    </div>
  </article>
</template>
