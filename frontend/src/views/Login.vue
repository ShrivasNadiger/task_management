<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { loginUser } from "../authApi";
import { useAuth } from "../composables/useAuth";

const router = useRouter();
const auth = useAuth();

const email = ref("");
const password = ref("");

const errorMessage = ref("");
const loading = ref(false);

const handleLogin = async () => {
  errorMessage.value = "";
  loading.value = true;

  try {
    const response = await loginUser({
      email: email.value,
      password: password.value,
    });

    auth.login(response);

    router.push("/tasks");
  } catch (error) {
    errorMessage.value = error.message;
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
      <h1 class="text-2xl font-bold mb-6 text-center">Login</h1>

      <form @submit.prevent="handleLogin" class="space-y-4">
        <input
          v-model="email"
          type="email"
          placeholder="Email"
          required
          class="w-full border rounded px-3 py-2"
        />

        <input
          v-model="password"
          type="password"
          placeholder="Password"
          required
          class="w-full border rounded px-3 py-2"
        />

        <p v-if="errorMessage" class="text-red-500 text-sm">
          {{ errorMessage }}
        </p>

        <button
          type="submit"
          :disabled="loading"
          class="w-full bg-blue-600 text-white py-2 rounded transition-transform hover:scale-105"
        >
          {{ loading ? "Logging in..." : "Login" }}
        </button>
      </form>

      <p class="text-center mt-4 text-sm">
        Don't have an account?
        <router-link to="/register" class="text-blue-600">
          Register
        </router-link>
      </p>
    </div>
  </div>
</template>
