import React, { useState } from 'react';

const Login: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Bu yerda autentifikatsiya logikasini yozasiz
    alert(`Email: ${email}, Password: ${password}`);
  };

  return (
    <div className="container mt-5" style={{ maxWidth: '400px' }}>
      <h2 className="mb-4 text-center">Login</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="email" className="form-label">Email manzilingiz</label>
          <input
            type="email"
            id="email"
            className="form-control"
            placeholder="Email kiriting"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">Parol</label>
          <input
            type="password"
            id="password"
            className="form-control"
            placeholder="Parolingizni kiriting"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary w-100">Kirish</button>
      </form>
    </div>
  );
};

export default Login;
