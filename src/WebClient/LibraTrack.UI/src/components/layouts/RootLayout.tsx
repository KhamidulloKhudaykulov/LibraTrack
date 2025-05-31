import { NavLink, Outlet } from "react-router-dom";
import UserAccount from "../Users/UserAccount";
import { useState } from "react";

const RootLayout = () => {
  const [openSubMenu, setOpenSubMenu] = useState<string | null>(null);

  const toggleSubMenu = (menu: string) => {
    setOpenSubMenu((prev) => (prev === menu ? null : menu))
  };

  return (
    <div className="h-screen flex bg-gray-100 overflow-hidden opacity-90">
      <nav className="w-60 bg-white shadow-md flex flex-col">
        <h1 className="flex justify-center items-center mb-4 h-16 w-full font-bold text-xl">
          LibraTrack Platform
        </h1>
        <NavLink
          to="/"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-bold" : "text-gray-800"
            }`
          }
        >
          Панель управления
        </NavLink>
        <NavLink
          to="/users"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-bold" : "text-gray-800"
            }`
          }>Пользователи</NavLink>
        <NavLink
          to="/books"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-bold" : "text-gray-800"
            }`
          }>Книги</NavLink>
        <NavLink
          to="/rents"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-bold" : "text-gray-800"
            }`
          }>Аренды</NavLink>
        <button
          onClick={() => toggleSubMenu("inventory")}
          className="px-4 py-3 text-left hover:bg-blue-100 duration-300 text-gray-800"
        >
          Склад
        </button>
        {openSubMenu === "inventory" && (
          <div className="pl-5">
            <NavLink
              to="/inventory/arrivals"
              className={({ isActive }) =>
                `block py-2 hover:text-blue-500 ${isActive ? "text-blue-500 font-semibold" : "text-gray-700"}`
              }
            >
              Приходы
            </NavLink>
            <NavLink
              to="/inventory/stocks"
              className={({ isActive }) =>
                `block py-2 hover:text-blue-500 ${isActive ? "text-blue-500 font-semibold" : "text-gray-700"}`
              }
            >
              Остатки
            </NavLink>
          </div>
        )}
        {/* <NavLink
          to="/inventory"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-bold" : "text-gray-800"
            }`
          }>Склад</NavLink> */}
        <NavLink
          to="/settings"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-bold" : "text-gray-800"
            }`
          }>Настройки</NavLink>
      </nav>

      <div className="flex flex-col flex-1 relative">
        <div className="h-18 bg-white shadow-sm flex items-center px-4 ml-4 mr-4 mt-4 rounded-xl">
          <input
            type="text"
            placeholder="Искать по ФИО"
            className="w-72 rounded-lg h-8 px-4 border border-gray-200 text-gray-400 focus:outline-none focus:ring-1 focus:ring-blue-400 hover:border-blue-200 duration-150"
          />
          <UserAccount />
        </div>

        <main className="m-4 overflow-y-scroll flex-1">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default RootLayout;
