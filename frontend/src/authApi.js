const API_URL = "http://localhost:5062/api/auth";

export async function registerUser(userData) {
    const response = await fetch(`${API_URL}/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(userData)
    });

    if (!response.ok) {
        const error = await response.text();
        throw new Error(error || "Registration failed");
    }

    return response.json();
}

export async function loginUser(credentials) {
    const response = await fetch(`${API_URL}/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(credentials)
    });

    if (!response.ok) {
        const error = await response.text();
        throw new Error(error || "Login failed");
    }

    return response.json();
}