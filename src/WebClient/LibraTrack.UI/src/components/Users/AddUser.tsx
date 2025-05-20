import { addUser } from "@/services/userService";
import { useState } from "react";

const AddUser = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [passportNumber, setPassportNumber] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');

    const [showAddModal, setShowAddModal] = useState(false);
    const handleAdd = async () => {
        try {
            await addUser({ firstName, lastName, email, passportNumber, phoneNumber });
        }
        catch (error) {
            alert(error);
        }
        setShowAddModal(false);
        window.location.reload();
    }
    return (
        <div className="right-0 absolute">
            <button className="p-1 pl-4 pr-4 text-white bg-blue-500 rounded-md cursor-pointer shadow-sm hover:rounded-lg transition-all duration-300"
                onClick={() => setShowAddModal(true)}
            >
                Add User
            </button>

            {showAddModal && (
                <div className="fixed inset-0 bg-opacity-40 flex justify-center items-center z-50">
                    <div className="bg-white p-6 rounded-md shadow-lg p-12">
                        <h2 className="text-xl font-bold mb-4">Add New User</h2>
                        <div className="flex flex-col gap-1">
                            <input
                                type="text"
                                placeholder="First name"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                            <input
                                type="text"
                                placeholder="Last name"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" /> <input type="text"
                                    placeholder="Email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                            <input type="text"
                                placeholder="Phone number"
                                value={phoneNumber}
                                onChange={(e) => setPhoneNumber(e.target.value)}
                                className="p-2 bg-gray-100 rounded-md w-full pl-4 focus:outline-none focus:ring-2 focus:ring-blue-400" />
                            <input type="text"
                                placeholder="Passport number"
                                onChange={(e) => setPassportNumber(e.target.value)}
                                value={passportNumber}
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

export default AddUser;