export interface Book {
  id: string;           // GUID string ko‘rinishida
  title: string;
  author: string;
  publisher: string;
  price: number;        // oddiy number bo‘lsin
  description: string;  // Yangi qo‘shildi
}

export type AddBookCommand = {
  title: string;
  description: string; 
  author: string;
  publisher: string;
  price: number;
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

export async function addBook(command: AddBookCommand) {
  try{
    const response = await fetch("https://localhost:7202/api/books", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(command)
    });
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Server error: ${response.status} - ${errorText}`);
    }

    const createdBook = await response.json();
    return createdBook;
  } catch (error) {
    console.error("Error adding book:", error);
    throw error;
  }
}