import { updateBook, type Book } from "@/services/bookService";
import { useState } from "react";

type BookEditProps = {
  book: Book;
  onClose: () => void;
};

const BookEdit = ({ book, onClose }: BookEditProps) => {
  const [id, setId] = useState(book.id);
  const [title, setTitle] = useState(book.title);
  const [description, setDescription] = useState(book.description);
  const [author, setAuthor] = useState(book.author);
  const [publisher, setPublisher] = useState(book.publisher);
  const [price, setPrice] = useState(book.price);

  const onSave = async () => {
    const updatedBook: Book = {
      id,
      title,
      description,
      author,
      publisher,
      price
    };

    try {
      const response = await updateBook(updatedBook);
      console.log("Book updated:", response);
      onClose();
      window.location.reload();
    } catch (error) {
      console.error("Error saving user:", error);
    }
  }

  return (
    <div className="fixed inset-0 bg-opacity-10 flex justify-center items-center z-50">
      <div className="bg-white rounded-lg p-6 shadow-2xl w-[400px]">
        <h2 className="text-xl font-bold mb-4">Edit User</h2>
        <div className="flex flex-col gap-1">
          <input
            type="text"
            placeholder="id"
            value={id}
            onChange={(e) => setId(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
            disabled
          />
          <input
            type="text"
            placeholder="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="text"
            placeholder="Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="text"
            placeholder="Author"
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="text"
            placeholder="Publisher"
            value={publisher}
            onChange={(e) => setPublisher(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="number"
            placeholder="Price"
            value={price}
            onChange={(e) => setPrice(e.target.valueAsNumber)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
        </div>
        <button
          className="mt-4 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
          onClick={onClose}
        >
          Close
        </button>
        <button
          className="mt-4 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 ml-1"
          onClick={onSave}
        >
          Save
        </button>
      </div>
    </div>
  );
};

export default BookEdit;
