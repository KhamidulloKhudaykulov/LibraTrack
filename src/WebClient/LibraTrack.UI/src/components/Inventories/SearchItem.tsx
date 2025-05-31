type SearchItemProps = {
    value: string;
    onChange: (value: string) => void;
}

const SearchItem = (props: SearchItemProps) => {
    return (
        <input
            type="text"
            value={props.value}
            onChange={(e) => props.onChange(e.target.value)}
            placeholder={'Искать по названию'}
            className={`w-72 bg-inherit h-8 rounded-md px-4 border border-gray-300 transition-all 
                duration-300 focus:outline-none focus:ring-1 focus:ring-blue-400 hover:border-blue-200
                ${props.value.length !== 0 ? "text-gray-800" : "text-gray-400"}`}
        />
    );
};

export default SearchItem;