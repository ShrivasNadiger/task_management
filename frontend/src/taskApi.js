import { useAuth } from "./composables/useAuth";

const API_URL = import.meta.env.VITE_API_URL;

const { token } = useAuth();

const reqHeaders = () => {
  return {
    "Content-Type": "application/json",
    ...(token.value && { Authorization: `Bearer ${token.value}` })
  }
    };

// ==============================
// USERS
// ==============================

export async function getUsers() {
  const response = await fetch(`${API_URL}/users`, {
    headers: reqHeaders()
  });

  if (!response.ok) {
    throw new Error("Failed to fetch users");
  }

  return response.json();
}

export async function getUser(userId) {
  const response = await fetch(`${API_URL}/users/${userId}`, {
    headers:reqHeaders
  });

  if (!response.ok) {
    throw new Error("Failed to fetch user");
  }

  return response.json();
}

export async function createUser(user) {
  const response = await fetch(`${API_URL}/users`, {
    method: "POST",
    headers: reqHeaders(),
    body: JSON.stringify(user),
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to create user");
  }

  return response.json();
}

export async function updateUser(userId, user) {
  const response = await fetch(`${API_URL}/users/${userId}`, {
    method: "PUT",
    headers: reqHeaders(),
    body: JSON.stringify(user),
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to update user");
  }
}

export async function deleteUser(userId) {
  const response = await fetch(`${API_URL}/users/${userId}`, {
    method: "DELETE",
    headers:reqHeaders()
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to delete user");
  }
}

// ==============================
// TASKS
// ==============================

export async function getUserTasks(userId) {
  const response = await fetch(
    `${API_URL}/users/${userId}/tasks`, {
      headers:reqHeaders()
    }
  );

  if (!response.ok) {
    throw new Error("Failed to fetch user tasks");
  }

  return response.json();
}

export async function getTask(taskId) {
  const response = await fetch(
    `${API_URL}/tasks/${taskId}`,{
      headers:reqHeaders()
    }
  );

  if (!response.ok) {
    throw new Error("Failed to fetch task");
  }

  return response.json();
}

export async function createTask(task) {
  const response = await fetch(`${API_URL}/tasks`, {
    method: "POST",
    headers: reqHeaders(),
    body: JSON.stringify(task),
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to create task");
  }

  return response.json();
}

export async function updateTask(taskId, task) {
  const response = await fetch(`${API_URL}/tasks/${taskId}`, {
    method: "PUT",
    headers: reqHeaders(),
    body: JSON.stringify(task),
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to update task");
  }
}

export async function deleteTask(taskId) {
  const response = await fetch(`${API_URL}/tasks/${taskId}`, {
    method: "DELETE",
    headers: reqHeaders()
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to delete task");
  }
}