// src/App.tsx
import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Dashboard } from './components/Dashboard';
import { UsersPage } from './components/Users/UsersPage';
import { BooksPage } from './components/Books/BooksPage';
import RentsPage from './components/Rents/RentsPage';
import InventoryArrivalsPage from './components/Inventories/InventoryPage';
import RootLayout from './components/layouts/RootLayout';
import StockBalancePage from './components/Inventories/StockBalancePage';
import LoginPage from './components/Login/LoginPage';

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/login' element={<LoginPage />} />
        <Route element={<RootLayout />}>
          <Route path='/' element={<Dashboard />} />
          <Route path='/users' element={<UsersPage />} />
          <Route path='/books' element={<BooksPage />} />
          <Route path='/rents' element={<RentsPage />} />
          <Route path='/inventory/arrivals' element={<InventoryArrivalsPage />} />
          <Route path='/inventory/stocks' element={<StockBalancePage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
