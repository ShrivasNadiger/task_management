<script setup>
import { ref, watch } from "vue";

const props = defineProps({
  userId: {
    type: Number,
    default: null,
  },

  editingTask: {
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
  title: "",
  description: "",
  status: "PENDING",
  priority: "MEDIUM",
});

watch(
  () => props.editingTask,
  (task) => {
    if (task) {
      form.value = {
        title: task.title,
        description: task.description ?? "",
        status: task.status,
        priority: task.priority,
      };
    } else {
      resetForm();
    }
  },
  { immediate: true },
);

function resetForm() {
  form.value = {
    title: "",
    description: "",
    status: "PENDING",
    priority: "MEDIUM",
  };
}

function handleSubmit() {
  if (!form.value.title.trim()) {
    return;
  }

  emit("submit", {
    ...form.value,
    userId: props.userId,
  });
}

defineExpose({
  resetForm,
});
</script>

<template>
  <section class="rounded-xl bg-white p-5 shadow-sm">
    <h2 class="mb-4 text-lg font-semibold text-gray-900">
      {{ editingTask ? "Edit Task" : "Create Task" }}
    </h2>

    <form @submit.prevent="handleSubmit" class="space-y-4">
      <input
        v-model="form.title"
        type="text"
        placeholder="Task title"
        class="w-full rounded-lg border border-gray-300 px-3 py-2"
      />

      <textarea
        v-model="form.description"
        placeholder="Task description"
        rows="3"
        class="w-full rounded-lg border border-gray-300 px-3 py-2"
      ></textarea>

      <select
        v-model="form.status"
        class="w-full rounded-lg border border-gray-300 px-3 py-2"
      >
        <option value="PENDING">Pending</option>
        <option value="IN_PROGRESS">In Progress</option>
        <option value="COMPLETED">Completed</option>
      </select>

      <select
        v-model="form.priority"
        class="w-full rounded-lg border border-gray-300 px-3 py-2"
      >
        <option value="LOW">Low</option>
        <option value="MEDIUM">Medium</option>
        <option value="HIGH">High</option>
      </select>

      <div class="flex gap-2">
        <button
          type="submit"
          :disabled="loading || !userId"
          class="rounded-lg bg-blue-600 px-5 py-2 font-medium text-white disabled:opacity-50"
        >
          {{
            loading ? "Saving..." : editingTask ? "Update Task" : "Create Task"
          }}
        </button>

        <button
          v-if="editingTask"
          type="button"
          @click="emit('cancel')"
          class="rounded-lg bg-gray-200 px-5 py-2 font-medium text-gray-700"
        >
          Cancel
        </button>
      </div>
    </form>
  </section>
</template>
