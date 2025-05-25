import { getBooks, type Book } from "@/services/bookService";
import { addRent } from "@/services/rentService";
import { getUsers } from "@/services/userService";
import type { User } from "@/services/userService";
import { useEffect, useRef, useState } from "react";

const AddRent = () => {
    const [user, setUser] = useState('');
    const [book, setBook] = useState('');
    const [price, setPrice] = useState<number>(0);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

    const [userId, setUserId] = useState('');
    const [bookId, setBookId] = useState('');


    const [userList, setUserList] = useState<User[]>([]);
    const [bookList, setBookList] = useState<Book[]>([]);
    const [showUserDropdown, setShowUserDropdown] = useState(false);
    const [showBookDropdown, setShowBookDropdown] = useState(false);

    const [showAddModal, setShowAddModal] = useState(false);
    const wrapperRef = useRef<HTMLDivElement>(null)

    const handleClickOutside = (event: MouseEvent) => {
        if (wrapperRef.current && !wrapperRef.current.contains(event.target as Node)) {
            setShowUserDropdown(false);
            setShowBookDropdown(false);
        }
    };

    useEffect(() => {
        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);


    const fetchUsers = async () => {
        try {
            const response = await getUsers();
            setUserList(response);
            setShowUserDropdown(true);
            setShowBookDropdown(false);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };

    const fetchBooks = async () => {
        try {
            const response = await getBooks();
            setBookList(response);
            setShowBookDropdown(true);
            setShowUserDropdown(false);
        } catch (error) {
            console.error('Error fetching books:', error);
        }
    };

    const handleSelectUser = (user: User) => {
        setUserId(user.id);
        setUser(user.fullName);
        setShowUserDropdown(false);
    };

    const handleSelectBook = (book: Book) => {
        setBookId(book.id);
        setBook(book.title);
        setShowBookDropdown(false);
    };

    const handleAdd = async () => {
        try {
            await addRent({
                userId: userId,
                bookId: bookId,
                price: price,
                startDate: new Date(startDate),
                endDate: new Date(endDate)
            });
        }
        catch (error) {
            alert(error);
        }
        setShowAddModal(false);
        window.location.reload();
    }

    return (
        <div className="flex-1 absolute right-0 p-4">
            <button
                className="pr-8 pl-8 p-1 text-white bg-blue-500 rounded-md cursor-pointer shadow-sm"
                onClick={() => setShowAddModal(true)}>Create rent</button>
            {showAddModal && (
                <div className="bg-opacity fixed inset-0 bg-opacity-40 flex justify-center items-center z-50">
                    <div className="bg-white rounded-md shadow-lg  p-8">
                        <h2 className="text-xl font-bold mb-4 w-100">Generate rent</h2>
                        <div className="flex flex-col gap-3" ref={wrapperRef}>
                            <input
                                type="text"
                                placeholder="User"
                                value={user}
                                onChange={(e) => setUser(e.target.value)}
                                onFocus={fetchUsers}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />

                            {showUserDropdown && userList.filter((u) =>
                                u.fullName.toLowerCase().includes(user.toLowerCase())
                            ).length > 0 && (
                                    <ul className="absolute z-10 max-w-72 bg-white border border-gray-300 rounded-md mt-12 w-full max-h-60 overflow-y-auto shadow-lg">
                                        {userList
                                            .filter((u) => u.fullName.toLowerCase().includes(user.toLowerCase()))
                                            .map((u, index) => (
                                                <li
                                                    key={index}
                                                    onClick={() => handleSelectUser(u)}
                                                    className="p-2 hover:bg-blue-100 cursor-pointer"
                                                >
                                                    {u.fullName}
                                                </li>
                                            ))}
                                    </ul>
                                )}

                            <input
                                type="text"
                                placeholder="Book"
                                value={book}
                                onChange={(e) => setBook(e.target.value)}
                                onFocus={fetchBooks}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />
                            {showBookDropdown && bookList.filter((b) =>
                                b.title.toLowerCase().includes(book.toLowerCase())
                            ).length > 0 && (
                                    <ul className="absolute z-10 max-w-72 bg-white border mt-23 border-gray-300 rounded-md w-full max-h-60 overflow-y-auto shadow-lg">
                                        {bookList
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

                            <input type="number"
                                placeholder="Price"
                                value={price}
                                onChange={(e) => setPrice(e.target.valueAsNumber)}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />

                            <input type="date"
                                placeholder="Start date"
                                value={startDate}
                                onChange={(e) => setStartDate(e.target.value)}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />
                            <input type="date"
                                placeholder="End date"
                                value={endDate}
                                onChange={(e) => setEndDate(e.target.value)}
                                className="p-2 bg-gray-50 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400 transition-all duration-300" />

                        </div>
                        <button
                            className="mt-4 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600 transition-all duration-300"
                            onClick={() => setShowAddModal(false)}
                        >
                            Close
                        </button>
                        <button
                            className="mt-4 ml-2 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition-all duration-300"
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

export default AddRent;