import { addBook } from "@/services/bookService";
import { useState } from "react";

const AddBook = () => {
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [publisher, setPublisher] = useState('');
    const [price, setPrice] = useState(0);
    const [description, setDescription] = useState('');

    const [showAddModal, setShowAddModal] = useState(false);

    const handleAdd = async () => {
        try {
            await addBook({ title, description, author, publisher, price });
        }
        catch (error) {
            alert(error);
        }
        setShowAddModal(false);
        window.location.reload();
    }

    return (
        <div className="right-0 absolute p-4">
            <button className="pr-8 pl-8 p-1 text-white bg-blue-500 rounded-md cursor-pointer shadow-sm hover:rounded-lg transition-all duration-300"
                onClick={() => setShowAddModal(true)}
            >
                Add Book
            </button>
            {showAddModal && (
                <div className="fixed inset-0 bg-opacity-40 flex justify-center items-center z-50">
                    <div className="bg-white p-6 rounded-md shadow-lg p-12">
                        <h2 className="text-xl font-bold mb-4">Add New User</h2>
                        <div className="flex flex-col gap-1">
                            <input
                                type="text"
                                placeholder="Title"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                            <input
                                type="text"
                                placeholder="Description"
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" /> <input type="text"
                                    placeholder="Author"
                                    value={author}
                                    onChange={(e) => setAuthor(e.target.value)}
                                    className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                            <input type="text"
                                placeholder="Publisher"
                                value={publisher}
                                onChange={(e) => setPublisher(e.target.value)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                            <input type="number"
                                placeholder="Price"
                                value={price}
                                onChange={(e) => setPrice(e.target.valueAsNumber)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                        </div>
                        <button
                            className="mt-4 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
                            onClick={() => setShowAddModal(false)}
                        >
                            Close
                        </button>
                        <button
                            className="mt-4 ml-2 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                            onClick={() => handleAdd()}
                        >
                            Save
                        </button>
                    </div>
                </div>
            )}
        </div>
    )
};

export default AddBook;