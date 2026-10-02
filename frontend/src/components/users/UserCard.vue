<script setup>
defineProps({
  user: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits(["edit", "delete"]);
</script>

<template>
  <article class="rounded-xl bg-white p-5 shadow-sm">
    <div class="flex items-center justify-between gap-4">
      <div>
        <h3 class="font-semibold text-gray-900">
          {{ user.name }}
        </h3>

        <p class="mt-1 text-sm text-gray-500">
          {{ user.email }}
        </p>
      </div>

      <div class="flex gap-2">
        <button
          @click="emit('edit', user)"
          class="rounded-lg px-3 py-1 text-sm text-blue-600 hover:bg-blue-50"
        >
          Edit
        </button>

        <button
          @click="emit('delete', user)"
          class="rounded-lg px-3 py-1 text-sm text-red-600 hover:bg-red-50"
        >
          Delete
        </button>
      </div>
    </div>
  </article>
</template>
