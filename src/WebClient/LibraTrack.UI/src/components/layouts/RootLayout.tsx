import { NavLink, Outlet } from "react-router-dom";
import UserAccount from "../Users/UserAccount";

const RootLayout = () => {
  return (
    <div className="h-screen flex bg-gray-100 overflow-hidden opacity-90">
      <nav className="w-60 bg-white shadow-md flex flex-col">
        <h1 className="flex justify-center items-center mb-4 h-16 w-full font-bold text-xl">
          LibraTrack Platform
        </h1>
        <NavLink
          to="/"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-semibold" : "text-gray-800"
            }`
          }
        >
          Dashboard
        </NavLink>
        <NavLink
          to="/users"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-semibold" : "text-gray-800"
            }`
          }>Users</NavLink>
        <NavLink
          to="/books"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-semibold" : "text-gray-800"
            }`
          }>Books</NavLink>
        <NavLink
          to="/rents"
          className={({ isActive }) =>
            `px-4 py-3 hover:bg-blue-100 duration-300 ${isActive ? "text-blue-500 font-semibold" : "text-gray-800"
            }`
          }>Rents</NavLink>
      </nav>

      <div className="flex flex-col flex-1 overflow-hidden relative">
        <div className="h-14 bg-white shadow-sm flex items-center px-4 ml-4 mr-4 mt-4 rounded-xl">
          <input
            type="text"
            placeholder="Search"
            className="w-72 bg-gray-100 h-8 rounded-sm px-4 border border-gray-200 text-gray-600"
          />
          <UserAccount />
        </div>

        <main className="m-4 overflow-auto ">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default RootLayout;
