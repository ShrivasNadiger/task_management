// const API_URL = import.meta.env.VITE_API_URL;

// export async function getUsers() {
//   const response = await fetch(`${API_URL}/users`);

//   if (!response.ok) {
//     throw new Error("Failed to fetch users");
//   }

//   return response.json();
// }

// export async function getUserTasks(userId) {
//   const response = await fetch(
//     `${API_URL}/users/${userId}/tasks`
//   );

//   if (!response.ok) {
//     throw new Error("Failed to fetch user tasks");
//   }

//   return response.json();
// }

// export async function createTask(task) {
//   const response = await fetch(`${API_URL}/tasks`, {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify(task),
//   });

//   if (!response.ok) {
//     const message = await response.text();
//     throw new Error(message || "Failed to create task");
//   }

//   return response.json();
// }


const API_URL = import.meta.env.VITE_API_URL;

// ==============================
// USERS
// ==============================

export async function getUsers() {
  const response = await fetch(`${API_URL}/users`);

  if (!response.ok) {
    throw new Error("Failed to fetch users");
  }

  return response.json();
}

export async function getUser(userId) {
  const response = await fetch(`${API_URL}/users/${userId}`);

  if (!response.ok) {
    throw new Error("Failed to fetch user");
  }

  return response.json();
}

export async function createUser(user) {
  const response = await fetch(`${API_URL}/users`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
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
    headers: {
      "Content-Type": "application/json",
    },
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
    `${API_URL}/users/${userId}/tasks`
  );

  if (!response.ok) {
    throw new Error("Failed to fetch user tasks");
  }

  return response.json();
}

export async function getTask(taskId) {
  const response = await fetch(
    `${API_URL}/tasks/${taskId}`
  );

  if (!response.ok) {
    throw new Error("Failed to fetch task");
  }

  return response.json();
}

export async function createTask(task) {
  const response = await fetch(`${API_URL}/tasks`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
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
    headers: {
      "Content-Type": "application/json",
    },
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
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to delete task");
  }
}