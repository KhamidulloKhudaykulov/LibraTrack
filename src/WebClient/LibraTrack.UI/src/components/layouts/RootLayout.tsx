// src/layouts/RootLayout.tsx
import { Link, Outlet } from 'react-router-dom';

const RootLayout = () => {
  return (
    <div className="flex min-h-screen">
      {/* Sidebar */}
      <div className="w-56 bg-custom-blue text-white flex flex-col max-h-screen overflow-auto">
        <Link to="/" className="bg-yellow-300 text-black font-bold text-center p-4">Logo</Link>
        <Link to="/home" className="hover:bg-blue-200 p-4">Dashboard</Link>
        <Link to="/books" className="hover:bg-blue-200 p-4">Users</Link>
        <Link to="/books" className="hover:bg-blue-200 p-4">Books</Link>
        <Link to="/" className="hover:bg-blue-200 p-4">Rents</Link>
        <Link to="/" className="hover:bg-blue-200 p-4">Inventory</Link>
      </div>

      {/* Main content */}
      <main className="flex-grow p-4 bg-gray-50 max-h-screen overflow-auto">
        <Outlet />
      </main>
    </div>
  );
};

export default RootLayout;
