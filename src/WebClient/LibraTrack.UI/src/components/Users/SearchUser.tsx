type SearchUserInputsProps = {
    value: string;
    onChange: (value: string) => void;
    placeholder?: string;
}

const SearchUser = ({ value, onChange, placeholder = "Search..." }: SearchUserInputsProps) => {
    return (
        <input
            type="text"
            value={value}
            onChange={(e) => onChange(e.target.value)}
            placeholder={placeholder}
            className="w-72 bg-inherit text-gray-400 h-8 rounded-md px-4 border border-gray-300 transition-all duration-300 focus:outline-none focus:ring-1 focus:ring-blue-400 hover:border-blue-200"
        />
    );
};

export default SearchUser;