// pages/LoginPage.tsx
// import { useAuth } from "../Authentication/AuthProvider";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { authenticate } from "@/services/authenticationService";
// import { authenticate } from "@/services/authenticationService";

const LoginPage = () => {
    // const { login } = useAuth();
    const navigate = useNavigate();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const handleLogin = async () => {
        // Bu yerda backend API bilan tekshirish kiritiladi
        try {
            await authenticate(username, password);

            navigate('/users')
        }
        catch (error) {
            alert("error");
        }
    };

    return (
        <div className="h-screen flex flex-col justify-center items-center">
            <div className="w-96 bg-gray-50 p-6 rounded-2xl shadow-lg flex flex-col gap-5 justify-center items-center">
                <h1 className="text-center text-2xl text-blue-700 font-bold border-b pb-4">Добро пожаловать в LibraTrack</h1>
                <input
                    type="text"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    className="w-full bg-white rounded-lg py-3 px-2 focus:ring-2 focus:ring-blue-700 focus:outline-none duration-150"
                    placeholder="Логин"
                />

                <input
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="w-full bg-white rounded-lg py-3 px-2 focus:ring-2 focus:ring-blue-700 focus:outline-none duration-150"
                    placeholder="Пароль"
                />

                <button
                    onClick={handleLogin}
                    className="bg-blue-700 text-white px-8 py-2 rounded-lg cursor-pointer hover:bg-blue-800 duration-150">
                    Войти
                </button>
            </div>
        </div>
    );
};

export default LoginPage;
