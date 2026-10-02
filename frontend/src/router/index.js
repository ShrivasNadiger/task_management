import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(),

  routes: [
    {
      path: "/",
      redirect: "/tasks",
    },

    {
      path: "/tasks",
      component: () => import("../views/TaskManagement.vue"),
    },

    {
      path: "/users",
      component: () => import("../views/UserManagement.vue"),
    },
  ],
});

export default router;