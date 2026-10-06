import { createRouter, createWebHistory } from "vue-router";
import { useAuth } from "../composables/useAuth";

const Routes=[
    {
      path: "/",
      redirect: "/login",
    },

    {
      path: "/login",
      name:"Login",
      component: () => import("../views/Login.vue"),
      meta: {
        guest:true,
      }
    },

    {
      path: "/register",
      name: "Register",
      component: () => import("../views/Register.vue"),
      meta: {
        guest:true,
      }
    },
    
    {
      path: "/tasks",
      name:"Tasks",
      component: () => import("../views/TaskManagement.vue"),
      meta: {
        requiresAuth:true,
      }
    },

    {
      path: "/users",
      name:"Users",
      component: () => import("../views/UserManagement.vue"),
      meta: {
        requiresAuth: true,
        requiresAdmin:true
      }
    },
  ]

const router = createRouter({
  history: createWebHistory(),

  routes: Routes,
});

const auth = useAuth();

router.beforeEach(
  (to) => {

    console.log("----- ROUTER GUARD -----");
    console.log("Going to:", to.path);
    console.log("Token ref:", auth.token);
    console.log("Token value:", auth.token.value);
    console.log("User:", auth.user.value);
    console.log("requiresAuth:", to.meta.requiresAuth);
    console.log("guest:", to.meta.guest);
    console.log("requiresAdmin:", to.meta.requiresAdmin);
    

    if (to.meta.requiresAuth && !auth.token.value) {
      return "/login";
    }

    if (to.meta.guest && auth.token.value) {
      return "/tasks";
    }

    if (to.meta.requiresAdmin && auth.user.value?.role !== "Admin") {
      return "/tasks";
    }
  }
);

export default router;