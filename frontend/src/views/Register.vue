<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { registerUser } from "../authApi";

const router = useRouter();

const name = ref("");
const email = ref("");
const password = ref("");

const error = ref("");
const loading = ref(false);

const handleRegister = async () => {
  error.value = "";
  loading.value = true;

  try {
    // console.log("inside try blolck sending register");
    await registerUser({
      name: name.value,
      email: email.value,
      password: password.value,
    });
    router.push("/login");
  } catch (err) {
    console.log("error in register.vue");
    error.value = err.message;
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
      <h1 class="text-2xl font-bold mb-6 text-center">Register</h1>
      <form @submit.prevent="handleRegister" class="space-y-4">
        <input
          type="text"
          v-model="name"
          required
          placeholder="Enter Name"
          class="w-full border rounded px-3 py-2"
        />
        <input
          type="email"
          v-model="email"
          required
          placeholder="Enter vaild Email"
          class="w-full border rounded px-3 py-2"
        />
        <input
          type="password"
          v-model="password"
          required
          minlength="8"
          placeholder="Enter Password"
          class="w-full border rounded px-3 py-2"
        />

        <p v-if="error" class="text-red-500 text-sm">{{ error }}</p>

        <button
          type="submit"
          :disabled="loading"
          class="w-full bg-green-600 text-white py-2 rounded transition-transform hover:scale-105"
        >
          {{ loading ? "Creating .." : "Register" }}
        </button>
      </form>
      <p>
        Already have Account?
        <router-link to="/login" class="text-blue-600">Login</router-link>
      </p>
    </div>
  </div>
</template>
