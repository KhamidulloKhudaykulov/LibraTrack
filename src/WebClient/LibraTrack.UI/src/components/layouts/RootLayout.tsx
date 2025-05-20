import { Link, Outlet } from "react-router-dom";
import UserAccount from "../Users/UserAccount";

const RootLayout = () => {
  return (
    <div className="h-screen flex bg-gray-200 overflow-hidden">
      <nav className="w-60 bg-white shadow-md flex flex-col transition-all duration-300">
        <h1 className="flex justify-center items-center h-16 w-full bg-gray-200 font-bold text-xl">
          LibraTrack Platform
        </h1>
        <Link to="/" className="bg-red-200 px-4 py-2 hover:bg-red-300 duration-150">Dashboard</Link>
        <Link to="/users" className="bg-red-200 px-4 py-2 hover:bg-red-300 duration-150">Users</Link>
        <Link to="/books" className="bg-red-200 px-4 py-2 hover:bg-red-300 duration-150">Books</Link>
      </nav>

      <div className="flex flex-col flex-1 overflow-hidden relative">
        <div className="h-16 bg-white shadow-md flex items-center px-4 ml-4 mr-4 mt-4 rounded-xl">
          <input
            type="text"
            placeholder="Search"
            className="w-72 bg-gray-100 h-8 rounded-sm px-4 border border-gray-200 text-gray-600"
          />
          <UserAccount />
        </div>

        <main className="flex-1 mt-4 ml-4 mr-4 mb-4 rounded-xl p-4 overflow-auto bg-white">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default RootLayout;
