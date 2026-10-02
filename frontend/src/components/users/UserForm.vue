<script setup>
import { ref, watch } from "vue";

const props = defineProps({
  editingUser: {
    type: Object,
    default: null,
  },
  loading: {
    type: Boolean,
    default: false,
  },
});

const emit = defineEmits(["submit", "cancel"]);

const form = ref({
  name: "",
  email: "",
});

const error = ref("");

watch(
  () => props.editingUser,
  (user) => {
    if (user) {
      form.value = {
        name: user.name,
        email: user.email,
      };
    } else {
      resetForm();
    }
  },
  { immediate: true },
);

function resetForm() {
  form.value = {
    name: "",
    email: "",
  };

  error.value = "";
}

function handleSubmit() {
  error.value = "";

  if (!form.value.name.trim()) {
    error.value = "Name is required.";
    return;
  }

  if (!form.value.email.trim()) {
    error.value = "Email is required.";
    return;
  }

  emit("submit", {
    name: form.value.name.trim(),
    email: form.value.email.trim(),
  });
}

defineExpose({ resetForm });
</script>

<template>
  <section class="rounded-xl bg-white p-5 shadow-sm">
    <div class="mb-4 flex items-center justify-between">
      <h2 class="text-lg font-semibold text-gray-900">
        {{ editingUser ? "Edit User" : "Create User" }}
      </h2>
    </div>

    <form @submit.prevent="handleSubmit" class="space-y-4">
      <div>
        <label class="mb-1 block text-sm font-medium text-gray-700">
          Name
        </label>

        <input
          v-model="form.name"
          type="text"
          placeholder="Enter user name"
          class="w-full rounded-lg border border-gray-300 px-3 py-2 outline-none focus:border-blue-500"
        />
      </div>

      <div>
        <label class="mb-1 block text-sm font-medium text-gray-700">
          Email
        </label>

        <input
          v-model="form.email"
          type="email"
          placeholder="Enter email"
          class="w-full rounded-lg border border-gray-300 px-3 py-2 outline-none focus:border-blue-500"
        />
      </div>

      <div v-if="error" class="rounded-lg bg-red-50 p-3 text-sm text-red-600">
        {{ error }}
      </div>

      <div class="flex gap-2">
        <button
          type="submit"
          :disabled="loading"
          class="rounded-lg bg-blue-600 px-5 py-2 font-medium text-white hover:bg-blue-700 disabled:opacity-50"
        >
          {{
            loading ? "Saving..." : editingUser ? "Update User" : "Create User"
          }}
        </button>

        <button
          v-if="editingUser"
          type="button"
          @click="emit('cancel')"
          class="rounded-lg bg-gray-200 px-5 py-2 font-medium text-gray-700 hover:bg-gray-300"
        >
          Cancel
        </button>
      </div>
    </form>
  </section>
</template>
