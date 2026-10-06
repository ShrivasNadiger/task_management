<!-- <script setup>
import { RouterLink } from "vue-router";
</script> -->

<!-- <template>
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
</template> -->

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useAuth } from "../composables/useAuth";

const router = useRouter();
const auth = useAuth();

const showProfile = ref(false);

const toggleProfile = () => {
  showProfile.value = !showProfile.value;
};

const handleLogout = () => {
  auth.logout();
  showProfile.value = false;
  router.push("/login");
};
</script>

<template>
  <nav class="bg-white border-b border-gray-200 px-6 py-4">
    <div class="max-w-6xl mx-auto flex items-center justify-between">
      <!-- Logo -->
      <router-link to="/tasks" class="font-bold text-lg">
        TaskFlow
      </router-link>

      <!-- Navigation -->
      <div class="flex items-center gap-6">
        <router-link
          v-if="auth.isAuthenticated.value"
          to="/tasks"
          class="text-sm"
        >
          Tasks
        </router-link>

        <router-link
          v-if="auth.user.value?.role === 'Admin'"
          to="/users"
          class="text-sm"
        >
          Users
        </router-link>

        <!-- Profile -->
        <div class="relative">
          <!-- Profile button -->
          <button
            @click="toggleProfile"
            class="flex items-center gap-2 text-sm"
            v-if="auth.isAuthenticated.value"
          >
            <span>
              {{ auth.user.value?.name }}
            </span>
            <span v-if="showProfile" class="text-xs">&#x274C;</span>
            <span v-else class="text-xs"> ▼ </span>
          </button>

          <!-- Dropdown -->
          <div
            v-if="showProfile"
            class="absolute right-0 mt-2 w-64 bg-white border border-gray-200 rounded-lg shadow-lg z-50"
          >
            <!-- User information -->
            <div class="px-4 py-4 border-b border-gray-200">
              <p class="font-semibold text-gray-800">
                {{ auth.user.value?.name }}
              </p>

              <p class="text-sm text-gray-500 mt-1">
                {{ auth.user.value?.email }}
              </p>
            </div>

            <!-- Logout -->
            <div class="p-2">
              <button
                @click="handleLogout"
                class="w-full text-left px-3 py-2 rounded-md text-red-600 hover:bg-red-50"
              >
                Logout
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>
