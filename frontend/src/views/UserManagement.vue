<script setup>
import { ref, onMounted } from "vue";

import UserForm from "../components/users/UserForm.vue";
import UserList from "../components/users/UserList.vue";

import { getUsers, createUser, updateUser, deleteUser } from "../taskApi";

const users = ref([]);

const editingUser = ref(null);

const loadingUsers = ref(false);
const savingUser = ref(false);

const error = ref("");

const UserFormRef = ref(null);

async function loadUsers() {
  loadingUsers.value = true;
  error.value = "";

  try {
    users.value = await getUsers();
  } catch (err) {
    error.value = err.message;
  } finally {
    loadingUsers.value = false;
  }
}

function startEditing(user) {
  editingUser.value = user;

  window.scrollTo({
    top: 0,
    behavior: "smooth",
  });
}

function cancelEditing() {
  editingUser.value = null;
}

async function handleUserSubmit(userData) {
  savingUser.value = true;
  error.value = "";

  try {
    if (editingUser.value) {
      await updateUser(editingUser.value.id, userData);
    } else {
      await createUser(userData);

      UserFormRef.value?.resetForm();
    }

    editingUser.value = null;

    await loadUsers();
  } catch (err) {
    error.value = err.message;
  } finally {
    savingUser.value = false;
  }
}

async function handleDeleteUser(user) {
  const confirmed = window.confirm(`Delete "${user.name}"?`);

  if (!confirmed) {
    return;
  }

  error.value = "";
  try {
    await deleteUser(user.id);

    if (editingUser.value?.id === user.id) {
      editingUser.value = null;
    }

    await loadUsers();
  } catch (err) {
    error.value = err.message;
  }
}

onMounted(loadUsers);
</script>

<template>
  <main class="mx-auto max-w-5xl space-y-6 px-4 py-6">
    <div>
      <h1 class="text-2xl font-bold text-gray-900">User Management</h1>

      <p class="mt-1 text-sm text-gray-500">
        Create and manage users in the system.
      </p>
    </div>

    <div v-if="error" class="rounded-lg bg-red-50 p-4 text-sm text-red-600">
      {{ error }}
    </div>

    <UserForm
      :editing-user="editingUser"
      :loading="savingUser"
      @submit="handleUserSubmit"
      @cancel="cancelEditing"
      ref="UserFormRef"
    />

    <section>
      <div class="mb-4 flex items-center justify-between">
        <h2 class="text-lg font-semibold">Users</h2>

        <span class="text-sm text-gray-500"> {{ users.length }} user(s) </span>
      </div>

      <UserList
        :users="users"
        :loading="loadingUsers"
        @edit="startEditing"
        @delete="handleDeleteUser"
      />
    </section>
  </main>
</template>
