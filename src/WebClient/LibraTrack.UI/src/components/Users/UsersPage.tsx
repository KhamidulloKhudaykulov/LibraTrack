import { useEffect, useState } from "react";
import SearchUser from "./SearchUser";
import { activeUser, blockUser, getUsers, type User } from "@/services/userService";
import UserEdit from "./UserEdit";
import UserStatusFilter from "./UserStatusFilter";
import AddUser from "./AddUser";

export const UsersPage = () => {
    const [query, setQuery] = useState('');
    const [status, setStatus] = useState<string>('all');
    const [users, setUsers] = useState<User[]>([]);
    const [loading, setLoading] = useState(true);

    const [selectedUser, setSelectedUser] = useState<User | null>(null);

    const [showEditModal, setShowEditModal] = useState(false);
    useEffect(() => {
        const fetchUsers = async () => {
            const data = await getUsers();
            setUsers(data);
            setLoading(false);
        };

        fetchUsers();
    }, []);

    const filteredUsers = users.filter(user => {
        const matchesStatus = status === 'all' || user.status === status;
        const matchesQuery = user.fullName.toLowerCase().includes(query.toLowerCase()) ||
            user.email.toLowerCase().includes(query.toLowerCase()) ||
            user.passportNumber.toLowerCase().includes(query.toLowerCase());
        return matchesStatus && matchesQuery;
    });

    const handleBlock = async (id: string) => {
        try {
            const success = await blockUser(id);
            if (success) {
                alert('User blocked successfully');
                window.location.reload();
            }
        } catch (error) {
            alert('Failed to block user');
        }
    };

    const handleActive = async (id: string) => {
        try {
            const success = await activeUser(id);
            if (success) {
                alert('User actived successfully');
                window.location.reload();
            }
        } catch (error) {
            alert('Failed to active user');
        }
    };
    // mt-4 ml-4 mr-4 mb-4 rounded-xl overflow-auto bg-white
    return (
        <div className="bg-white w-full rounded-xl flex flex-col h-full flex-1 overflow-auto shadow-">
            <div className="flex flex-row items-center p-4 relative">
                <SearchUser value={query} onChange={setQuery} />
                <UserStatusFilter value={status} onChange={setStatus} />
                <AddUser />
            </div>
            <div className="flex flex-row border-t p-4 border-b border-gray-200 font-bold text-gray-400">
                <h2 className="flex-1 items-center">Passport</h2>
                <h2 className="flex-1 items-center">Fullname</h2>
                <h2 className="flex-1 items-center">Phone</h2>
                <h2 className="flex-1 items-center">Email</h2>
                <h2 className="flex-1 items-center">Status</h2>
                <h2 className="flex-1 items-center">Actions</h2>
            </div>
            {loading &&
                <div className="w-full flex flex-row items-center justify-center flex-1">
                    <div role="status">
                        <svg aria-hidden="true" className="inline w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor" />
                            <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill" />
                        </svg>
                        <span className="sr-only">Loading...</span>
                    </div>
                </div>}
            {filteredUsers.length === 0 && (
                <p className="ml-auto mr-auto text-gray-400 text-sm">No data</p>
            )}
            {filteredUsers.map((user) => (
                <div
                    key={user.id}
                    className="flex flex-row p-4 border-b py-2 hover:bg-gray-50 border-gray-200 text-gray-700">
                    <p className="flex-1 truncate">{user.passportNumber}</p>
                    <p className="flex-1 h-auto">{user.fullName}</p>
                    <p className="flex-1 truncate">{user.phoneNumber}</p>
                    <p className="flex-1 truncate">{user.email}</p>
                    <p className="flex-1 truncate">{user.status}</p>
                    <div className="flex-1">
                        <button
                            className="text-blue-500 hover:underline mr-2 font-bold cursor-pointer"
                            onClick={() => { setShowEditModal(true); setSelectedUser(user); }}>Edit</button>
                        <button className="text-red-500 hover:underline mr-2 font-bold cursor-pointer"
                            onClick={() => handleBlock(user.id)}>Block</button>
                        <button className="text-green-500 hover:underline font-bold cursor-pointer"
                            onClick={() => handleActive(user.id)}>Active</button>
                    </div>
                </div>
            ))}

            {showEditModal && selectedUser && <UserEdit user={selectedUser} onClose={() => setShowEditModal(false)} />}
        </div>
    );
};