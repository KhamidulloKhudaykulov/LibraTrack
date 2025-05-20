type UserStatusFilterProps = {
    value: string;
    onChange: (value: string) => void;
}

const UserStatusFilter = ({ value, onChange }: UserStatusFilterProps) => {
    return (
        <div className="">
            <select
                value={value}
                onChange={(e) => onChange(e.target.value)}
                className="cursor-pointer border border-blue-400 p-1 rounded-md bg-inherit border-gray-300 text-gray-300 ml-4 outline-none focus:ring-1 transition-all duration-300 focus:ring-blue-400 hover:border-blue-200"
            >
                <option value="all">All Statuses</option>
                <option value="Active">Active</option>
                <option value="Blocked">Blocked</option>
            </select>
        </div>
    )
};

export default UserStatusFilter;