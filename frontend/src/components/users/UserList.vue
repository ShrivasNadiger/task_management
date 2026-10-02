<script setup>
import UserCard from "./UserCard.vue";

defineProps({
  users: {
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
    Loading users...
  </div>

  <div
    v-else-if="users.length === 0"
    class="rounded-xl bg-white p-10 text-center shadow-sm"
  >
    <h3 class="font-semibold text-gray-800">No users found</h3>

    <p class="mt-2 text-sm text-gray-500">Create your first user.</p>
  </div>

  <div v-else class="space-y-4">
    <UserCard
      v-for="user in users"
      :key="user.id"
      :user="user"
      @edit="emit('edit', $event)"
      @delete="emit('delete', $event)"
    />
  </div>
</template>
