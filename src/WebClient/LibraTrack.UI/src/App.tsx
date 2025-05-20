// src/App.tsx
import React from 'react';
import RootLayout from './components/layouts/RootLayout';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Dashboard } from './components/Dashboard';
import { UsersPage } from './components/Users/UsersPage';
import { BooksPage } from './components/Books/BooksPage';

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<RootLayout />}>
          <Route path='/' element={<Dashboard />} />
          <Route path='/users' element={<UsersPage />} />
          <Route path='/books' element={<BooksPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
