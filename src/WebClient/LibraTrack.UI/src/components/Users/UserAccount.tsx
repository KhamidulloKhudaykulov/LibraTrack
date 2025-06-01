import { logout } from "@/services/authenticationService";
import { useState } from "react";
// import { useAuth } from "../Authentication/AuthProvider";
import { useNavigate } from "react-router-dom";


const UserAccount = () => {
    const [userAccountMenu, setUserAccountMenu] = useState(false);
    const [logOutModal, setLogoutModal] = useState(false);
    // const { logout } = useAuth();

    const navigate = useNavigate();

    const toggleMenu = () => {
        setUserAccountMenu(prev => !prev);
    };

    const handleLogout = () => {
        logout();
        navigate("/login");
    }

    return (
        <div className="right-8 absolute">
            {userAccountMenu && (
                <div
                    className="bg-gray-500 shadow-lg border-b border-white border-2 rounded-lg w-50 h-auto fixed rounded-lg z-100 right-8 top-20 text-white"
                    onMouseLeave={() => setUserAccountMenu(false)}
                >
                    <nav className="flex flex-col">
                        <h2 className="text-white p-2">Xudaykulov Xamidullo</h2>
                        <p
                            onClick={() => { setLogoutModal(true); setUserAccountMenu(false) }}
                            className={"text-white rounded-b-lg hover:bg-gray-600 px-2 py-4 cursor-pointer"}>Выйти</p>
                    </nav>
                </div>
            )}
            {logOutModal && (
                <div className="fixed inset-0 z-50 flex justify-center items-center backdrop-blur-sm bg-black/30">
                    <div className="p-6 w-[800px] h-[300px] bg-gray-50 shadow-lg rounded-lg flex flex-col justify-center items-center">
                        <h1 className="font-bold text-xl mb-4">Вы действительно хотите выйти из аккаунта?</h1>
                        <div className="mt-4">
                            <button 
                                className="px-8 py-2 bg-blue-500 text-white rounded mr-2"
                                onClick={handleLogout}>Да</button>
                            <button className="px-8 py-2 bg-red-600 text-white rounded" onClick={() => setLogoutModal(false)}>Нет</button>
                        </div>
                    </div>
                </div>
            )}

            <div
                className={`w-12 h-12 bg-gray-50 border border-blue-200 duration-150 rounded-full 
                    flex items-center justify-center cursor-pointer
                    ${userAccountMenu ? "border-3 border-blue-200" : ""}`}
                onClick={toggleMenu}
            >
                {/* User Icon */}
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-6 w-6 text-blue-500"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                    strokeWidth={2}
                >
                    <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M5.121 17.804A9 9 0 0112 15c2.065 0 3.948.7 5.38 1.877M15 11a3 3 0 11-6 0 3 3 0 016 0z"
                    />
                </svg>
            </div>
        </div>
    );
};

export default UserAccount;
