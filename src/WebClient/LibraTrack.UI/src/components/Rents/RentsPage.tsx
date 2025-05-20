import { useState } from "react";
import SearchUser from "../Users/SearchUser";
import type { User } from "@/services/userService";
import RentPayments from "./RentPayments";

const RentsPage = () => {
    const [query, setQuery] = useState('');
    const [users, setUsers] = useState<User[]>([]);

    const filteredUsers = users.filter(user => {
        const matchesQuery = user.fullName.toLowerCase().includes(query.toLowerCase()) ||
            user.email.toLowerCase().includes(query.toLowerCase()) ||
            user.passportNumber.toLowerCase().includes(query.toLowerCase());
        return matchesQuery;
    });

    return (
        <div className="flex flex-col gap-4">
            <div className="h-auto">
                <RentPayments />
            </div>
            <div className="bg-white w-full rounded-xl flex flex-col h-full">
                <div className="flex flex-row items-center relative p-4">
                    {/* filter */}
                    <SearchUser value={query} onChange={setQuery} />
                </div>
                <div className="flex flex-row p-4 border-t border-b border-gray-200 font-bold text-gray-400">
                    <h2 className="flex-1 items-center">User</h2>
                    <h2 className="flex-1 items-center">Book</h2>
                    <h2 className="flex-1 items-center">Start date</h2>
                    <h2 className="flex-1 items-center">End date</h2>
                    <h2 className="flex-1 items-center">Payment Status</h2>
                    <h2 className="flex-1 items-center">Status</h2>
                    <h2 className="flex-1 items-center">Price</h2>
                </div>
                {filteredUsers.length === 0 && (
                    <p className="ml-auto mr-auto text-gray-400 text-sm p-4">No data</p>
                )}
            </div>
        </div>
    )
};

export default RentsPage;