import { useState } from 'react';

type Book = {
  id: number;
  title: string;
  author: string;
  genre: string;
};

const dummyBooks: Book[] = [
  { id: 1, title: 'Clean Code', author: 'Robert C. Martin', genre: 'Programming' },
  { id: 2, title: '1984', author: 'George Orwell', genre: 'Fiction' },
  { id: 3, title: 'The Pragmatic Programmer', author: 'Andrew Hunt', genre: 'Programming' },
  { id: 4, title: 'The Hobbit', author: 'J.R.R. Tolkien', genre: 'Fantasy' },
];

const Books = () => {
  const [search, setSearch] = useState('');
  const [filter, setFilter] = useState('');
  
  const filteredBooks = dummyBooks.filter(book => {
    const matchesSearch = book.title.toLowerCase().includes(search.toLowerCase());
    const matchesFilter = filter ? book.genre === filter : true;
    return matchesSearch && matchesFilter;
  });

  return (
    <div className="mx-auto flex flex-col">
      <h1 className="text-2xl font-bold mb-4">Books</h1>

      <div className="flex gap-4 mb-3">
        <input
          type="text"
          placeholder="Search by title..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          className="border rounded px-3 py-2 w-full"
        />

        <select
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
          className="border rounded px-3 py-2"
        >
          <option value="">All genres</option>
          <option value="Programming">Programming</option>
          <option value="Fiction">Fiction</option>
          <option value="Fantasy">Fantasy</option>
        </select>

        <select
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
          className="border rounded px-3 py-2"
        >
          <option value="">All genres</option>
          <option value="Programming">Programming</option>
          <option value="Fiction">Fiction</option>
          <option value="Fantasy">Fantasy</option>
        </select>
      </div>

      <button type='submit' className='p-2 bg-custom-blue text-white rounded-sm w-34 ml-auto mb-3'>
        Add a book
      </button>

      <ul className="space-y-4">
        {filteredBooks.map((book) => (
          <li key={book.id} className="bg-white p-4 shadow rounded">
            <h2 className="text-xl font-semibold">{book.title}</h2>
            <p className="text-sm text-gray-600">Author: {book.author}</p>
            <p className="text-sm text-gray-500">Genre: {book.genre}</p>
          </li>
        ))}

        {filteredBooks.length === 0 && (
          <p className="text-gray-500">No books found.</p>
        )}
      </ul>
    </div>
  );
};

export default Books;
