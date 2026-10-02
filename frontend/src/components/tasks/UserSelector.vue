<script setup>
defineProps({
  users: {
    type: Array,
    required: true,
  },

  selectedUserId: {
    type: Number,
    default: null,
  },
});

const emit = defineEmits(["select"]);
</script>

<template>
  <section class="rounded-xl bg-white p-5 shadow-sm">
    <h2 class="mb-4 text-lg font-semibold text-gray-900">Select User</h2>

    <select
      :value="selectedUserId"
      @change="emit('select', Number($event.target.value))"
      class="w-full rounded-lg border border-gray-300 px-3 py-2 outline-none focus:border-blue-500"
    >
      <option disabled value="">Select a user</option>

      <option v-for="user in users" :key="user.id" :value="user.id">
        {{ user.name }} - {{ user.email }}
      </option>
    </select>
  </section>
</template>
