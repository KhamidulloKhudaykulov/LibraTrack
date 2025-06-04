export async function authenticate(login: string, password: string): Promise<string> {
    try {
        const response = await fetch(`http://localhost:5203/admins-api/api/admins/sign-in?login=${encodeURIComponent(login)}&password=${encodeURIComponent(password)}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error(`Server error: ${response.status}`);
        }

        const data = await response.text();
        return data;
    } catch (error) {
        alert(error);
        throw error;
    }
}


export async function logout() {
    try {
        const response = await fetch(`http://localhost:5203/admins-api/api/admins/logout`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error(`Server error: ${response.status}`);
        }

        const data = await response.text();
        return data;
    } catch (error) {
        alert(error);
        throw error;
    }
}
