<script setup>
import { ref, computed, onMounted } from "vue";

import UserSelector from "../components/tasks/UserSelector.vue";
import TaskForm from "../components/tasks/TaskForm.vue";
import TaskList from "../components/tasks/TaskList.vue";
import StatusFilter from "../components/tasks/StatusFilter.vue";

import {
  getUsers,
  getUserTasks,
  createTask,
  updateTask,
  deleteTask,
} from "../taskApi";

const users = ref([]);
const tasks = ref([]);

const selectedUserId = ref(null);
const editingTask = ref(null);

const selectedStatus = ref("ALL");

const loadingUsers = ref(false);
const loadingTasks = ref(false);
const savingTask = ref(false);

const error = ref("");

const taskFormRef = ref(null);

// ========================================
// LOAD USERS
// ========================================

async function loadUsers() {
  loadingUsers.value = true;
  error.value = "";

  try {
    users.value = await getUsers();

    if (users.value.length > 0) {
      selectedUserId.value = users.value[0].id;

      await loadUserTasks(selectedUserId.value);
    }
  } catch (err) {
    error.value = err.message;
  } finally {
    loadingUsers.value = false;
  }
}

// ========================================
// LOAD USER TASKS
// ========================================

async function loadUserTasks(userId) {
  if (!userId) return;

  loadingTasks.value = true;
  error.value = "";

  try {
    selectedUserId.value = userId;

    tasks.value = await getUserTasks(userId);

    editingTask.value = null;
  } catch (err) {
    error.value = err.message;
  } finally {
    loadingTasks.value = false;
  }
}

// ========================================
// CREATE / UPDATE TASK
// ========================================

async function handleTaskSubmit(taskData) {
  savingTask.value = true;
  error.value = "";

  try {
    if (editingTask.value) {
      await updateTask(editingTask.value.id, taskData);
    } else {
      await createTask(taskData);
      taskFormRef.value?.resetForm();
    }

    editingTask.value = null;

    await loadUserTasks(selectedUserId.value);
  } catch (err) {
    error.value = err.message;
  } finally {
    savingTask.value = false;
  }
}

// ========================================
// START EDITING
// ========================================

function startEditing(task) {
  editingTask.value = task;

  window.scrollTo({
    top: 0,
    behavior: "smooth",
  });
}

// ========================================
// CANCEL EDITING
// ========================================

function cancelEditing() {
  editingTask.value = null;
}

// ========================================
// DELETE TASK
// ========================================

async function handleDeleteTask(task) {
  const confirmed = window.confirm(`Delete "${task.title}"?`);

  if (!confirmed) return;

  try {
    await deleteTask(task.id);

    await loadUserTasks(selectedUserId.value);
  } catch (err) {
    error.value = err.message;
  }
}

// ========================================
// FILTER TASKS
// ========================================

const filteredTasks = computed(() => {
  if (selectedStatus.value === "ALL") {
    return tasks.value;
  }

  return tasks.value.filter((task) => task.status === selectedStatus.value);
});

// ========================================
// INITIAL LOAD
// ========================================

onMounted(loadUsers);
</script>

<template>
  <div class="min-h-screen bg-gray-100">
    <main class="mx-auto max-w-5xl space-y-6 px-4 py-6">
      <!-- ERROR -->

      <div v-if="error" class="rounded-lg bg-red-50 p-4 text-sm text-red-600">
        {{ error }}
      </div>

      <!-- USER SELECTOR -->

      <UserSelector
        :users="users"
        :selected-user-id="selectedUserId"
        @select="loadUserTasks"
      />

      <!-- TASK FORM -->

      <TaskForm
        :user-id="selectedUserId"
        :editing-task="editingTask"
        :loading="savingTask"
        @submit="handleTaskSubmit"
        @cancel="cancelEditing"
        ref="taskFormRef"
      />

      <!-- TASK SECTION -->

      <section>
        <div class="mb-4 flex items-center justify-between">
          <h2 class="text-lg font-semibold">Tasks</h2>

          <span class="text-sm text-gray-500">
            {{ filteredTasks.length }} task(s)
          </span>
        </div>

        <!-- STATUS FILTER -->

        <StatusFilter v-model="selectedStatus" class="mb-4" />

        <!-- TASK LIST -->

        <TaskList
          :tasks="filteredTasks"
          :loading="loadingUsers || loadingTasks"
          @edit="startEditing"
          @delete="handleDeleteTask"
        />
      </section>
    </main>
  </div>
</template>
