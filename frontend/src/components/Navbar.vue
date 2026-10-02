<script setup>
import { RouterLink } from "vue-router";
</script>

<template>
  <nav class="border-b bg-white">
    <div class="mx-auto flex max-w-5xl items-center justify-between px-4 py-4">
      <RouterLink to="/tasks" class="text-xl font-bold text-gray-900">
        TaskFlow
      </RouterLink>

      <div class="flex items-center gap-2">
        <RouterLink
          to="/tasks"
          class="rounded-lg px-4 py-2 text-sm font-medium text-gray-600 hover:bg-gray-100"
          active-class="!bg-blue-600 !text-white"
        >
          Tasks
        </RouterLink>

        <RouterLink
          to="/users"
          class="rounded-lg px-4 py-2 text-sm font-medium text-gray-600 hover:bg-gray-100"
          active-class="!bg-blue-600 !text-white"
        >
          Users
        </RouterLink>
      </div>
    </div>
  </nav>
</template>
