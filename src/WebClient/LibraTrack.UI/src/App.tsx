// src/App.tsx
import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import RootLayout from './components/layouts/RootLayout';
import Books from './components/Books';

const App: React.FC = () => {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<RootLayout />}>
            <Route path='/books' element={<Books />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;
