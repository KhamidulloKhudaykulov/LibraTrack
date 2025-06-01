import { useEffect, useState } from "react";
import SearchItem from "./SearchItem"
import { getInventoryStockBalances, type StockBalance } from "@/services/inventoryService";

const StockBalancePage = () => {
    const [items, setItems] = useState<StockBalance[]>([]);
    const [query, setQuery] = useState('');

    useEffect(() => {
        const fetchItems = async () => {
            const data = await getInventoryStockBalances();
            setItems(data);
        };

        fetchItems();
    }, []);
    const filteredItems = items.filter(item => {
        const matchesQuery = item.productName.toLowerCase().includes(query.toLowerCase());

        return matchesQuery;
    });

    return (
        <div className="bg-white rounded-xl ">
            <div className="flex flex-row p-4 relative ">
                <SearchItem value={query} onChange={setQuery} />
            </div>
            <div className="flex flex-row border-t p-4 border-b border-gray-200 font-bold text-gray-400 cursor-default">
                <h2 className="flex-1">Название</h2>
                <h2 className="flex-1">Общее количество</h2>
                <h2 className="flex-1">Действии</h2>
            </div>
            {filteredItems.map(item => (
                <div key={item.id}
                    className="flex flex-row p-4 border-b py-6 hover:bg-gray-50 border-gray-200 text-gray-700 cursor-default">
                    <p className="flex-1 h-auto">{item.productName}</p>
                    <p className="flex-1 truncate">{item.availableQuantity}</p>
                    <div className="flex-1">
                        <button
                            className="text-blue-500 hover:bg-blue-100 mr-2 font-bold bg-blue-50 py-1 px-4 border border-blue-300 rounded-md duration-150 text-sm">
                            Просмотр
                        </button>
                    </div>
                </div>
            ))}
        </div>
    )
};

export default StockBalancePage;