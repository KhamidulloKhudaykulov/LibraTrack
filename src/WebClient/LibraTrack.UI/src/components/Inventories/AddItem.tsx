import { getBooks } from "@/services/bookService";
import { useEffect, useRef, useState } from "react";
import type { Book } from "@/services/bookService";
import { addItemToInventory } from "@/services/inventoryService";

const AddItem = () => {
    const [showAddModal, setShowAddModal] = useState(false);
    const [booksList, setBooksList] = useState<Book[]>([]);
    const [book, setBook] = useState('');
    const [price, setPrice] = useState('');
    const [quantity, setQuantity] = useState('');

    const [bookId, setBookId] = useState('');

    const [showBookDropdown, setShowBookDropdown] = useState(false);


    const wrapperRef = useRef<HTMLDivElement>(null)

    const handleClickOutside = (event: MouseEvent) => {
        if (wrapperRef.current && !wrapperRef.current.contains(event.target as Node)) {
            setShowBookDropdown(false);
        }
    };

    useEffect(() => {
        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    const fetchBooks = async () => {
        try {
            const response = await getBooks();
            setBook('');
            setBooksList(response);
            setShowBookDropdown(true);
        } catch (error) {
            alert(error);
        }
    }

    const handleSelectBook = (book: Book) => {
        setBookId(book.id);
        setBook(book.title);
        setShowBookDropdown(false);
    };

    const handleAdd = async () => {
        try {
            setShowAddModal(false);
            await addItemToInventory({
                productId: bookId,
                amount: Number(quantity),
                price: Number(price),
            });
        } catch (error) {
            alert(error)
        }

        window.location.reload();
    };


    return (
        <div className="absolute right-0 pr-4">
            <button
                onClick={() => setShowAddModal(true)}
                className="bg-blue-500 text-white rounded-lg py-1 px-4 shadow-sm hover:bg-blue-600 cursor-pointer duration-150">
                Добавить приход
            </button>
            {showAddModal && (
                <div className="bg-opacity fixed inset-0 bg-opacity-40 flex justify-center items-center z-50">
                    <div className="bg-white rounded-md shadow-lg p-8">
                        <h2 className="text-xl font-bold mb-4 w-100">Создать приход</h2>
                        <div className="flex flex-col gap-3" ref={wrapperRef}>
                            <input
                                type="text"
                                placeholder="Продукт"
                                value={book}
                                onChange={(e) => setBook(e.target.value)}
                                onFocus={fetchBooks}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />
                            {showBookDropdown && booksList.filter((b) =>
                                b.title.toLowerCase().includes(book.toLowerCase())
                            ).length > 0 && (
                                    <ul className="absolute z-10 max-w-72 bg-white border mt-12 border-gray-300 rounded-md w-full max-h-60 overflow-y-auto shadow-lg">
                                        {booksList
                                            .filter((b) => b.title.toLowerCase().includes(book.toLowerCase()))
                                            .map((b, index) => (
                                                <li
                                                    key={index}
                                                    onClick={() => handleSelectBook(b)}
                                                    className="p-2 hover:bg-blue-100 cursor-pointer"
                                                >
                                                    {b.title}
                                                </li>
                                            ))}
                                    </ul>
                                )}
                            <input
                                type="text"
                                placeholder="Количество"
                                value={quantity}
                                onChange={(e) => setQuantity(e.target.value)}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />

                            <input
                                type="number"
                                placeholder="Цена за единицу"
                                value={price}
                                onChange={(e) => setPrice(e.target.value)}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />

                            <input
                                type="number"
                                placeholder="Итоговая сумма"
                                value={`${Number(price) * Number(quantity)}`}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />

                            <div className="flex flex-row gap-2">
                                <button
                                    onClick={handleAdd}
                                    className="flex-1 bg-blue-500 rounded-lg py-2 px-1 ml-5 text-white mt-4 hover:bg-blue-600 duration-150">
                                    Добавить приход
                                </button>
                                <button
                                    onClick={() => setShowAddModal(false)}
                                    className="flex-1 bg-red-500 rounded-lg py-2 px-1 mr-5 text-white mt-4 hover:bg-red-600 duration-150">
                                    Закрыть
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    )
};

export default AddItem;