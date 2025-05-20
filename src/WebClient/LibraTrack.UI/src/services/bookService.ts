export interface Book {
  id: string;           // GUID string ko‘rinishida
  title: string;
  author: string;
  publisher: string;
  price: number;        // oddiy number bo‘lsin
  description: string;  // Yangi qo‘shildi
}

export async function getBooks(): Promise<Book[]> {
  try {
    const response = await fetch('https://localhost:7202/api/books', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }

    const data: Book[] = await response.json();
    return data;
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  }
};


export async function addBook(book: Omit<Book, 'id'>): Promise<Book> {
  try {
    const response = await fetch('https://localhost:7202/api/books', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book),
    });

    if (!response.ok) {
      const errorText = await response.text();  // backenddan xato matnini o‘qiymiz
      throw new Error(`Failed to add book: ${response.status} - ${errorText}`);
    }

    const createdBook: Book = await response.json();
    return createdBook;
  } catch (error: any) {
    console.error('Error adding book:', error.message);
    throw error;
  }
}

export async function deleteBook(id: string): Promise<void> {
  try {
    const response = await fetch(`https://localhost:7202/api/books?id=${id}`, {
      method: 'DELETE',
    });

    if (!response.ok) {
      throw new Error(`Failed to delete book: ${response.status}`);
    }
  } catch (error) {
    console.error('Error deleting book:', error);
    throw error;
  }
}

export async function updateBook(book: Book): Promise<Book> {
  const params = new URLSearchParams({
    id: book.id,
    title: book.title,
    description: book.description,
    author: book.author,
    publisher: book.publisher,
    price: book.price.toString()
  });

  const response = await fetch(`https://localhost:7202/api/books?${params.toString()}`, {
    method: 'PUT'
  });

  if (!response.ok) {
    throw new Error('Failed to update book');
  }

  return await response.json();
}
