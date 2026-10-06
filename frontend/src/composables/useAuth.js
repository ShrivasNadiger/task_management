import { computed, ref } from "vue";

const token = ref(localStorage.getItem("token"));
const user = ref(
    JSON.parse(localStorage.getItem("user") || "null")
);

export function useAuth() {

    const isAuthenticated = computed(() => {
        return !!token.value;
    });

    const login = (loginResponse) => {
        token.value = loginResponse.token;

        user.value = {
            id: loginResponse.id,
            name: loginResponse.name,
            email: loginResponse.email,
            role: loginResponse.role
        };

        localStorage.setItem("token", loginResponse.token);
        localStorage.setItem(
            "user",
            JSON.stringify(user.value)
        );
    };

    const logout = () => {
        token.value = null;
        user.value = null;

        localStorage.removeItem("token");
        localStorage.removeItem("user");
    };

    return {
        token,
        user,
        isAuthenticated,
        login,
        logout
    };
}