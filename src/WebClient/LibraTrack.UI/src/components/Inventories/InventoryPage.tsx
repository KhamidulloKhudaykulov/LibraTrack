import { useEffect, useState } from "react";
import AddItem from "./AddItem";
import SearchItem from "./SearchItem";
import { getInventoryItems, type Item } from "@/services/inventoryService";

const InventoryArrivalsPage = () => {
    const [items, setItems] = useState<Item[]>([]);

    const [query, setQuery] = useState('');

    useEffect(() => {
        const fetchItems = async () => {
            const data = await getInventoryItems();
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
            <div className="w-auto bg-gray-100 h-auto p-1 absolute bottom-4 right-10">
                <p className="text-sm">{`Итого: ${filteredItems.reduce((sum, item) => sum + Number(item.totalPrice), 0)}`}</p>
            </div>
            <div className="flex flex-row p-4 relative ">
                <SearchItem value={query} onChange={setQuery}/>
                <AddItem />
            </div>
            {/* <div className="absolute w-full h-24 bg-gray-200 bottom-0"></div> */}
            <div className="flex flex-row border-t p-4 border-b border-gray-200 font-bold text-gray-400 cursor-default">
                <h2 className="flex-1">Название</h2>
                <h2 className="flex-1">Дата прихода</h2>
                <h2 className="flex-1">Общее количество</h2>
                <h2 className="flex-1">Сумма за ед.</h2>
                <h2 className="flex-1">Итоговая сумма</h2>
                <h2 className="flex-1">Действии</h2>
            </div>
            {filteredItems.map(item => (
                <div key={item.id}
                    className="flex flex-row p-4 border-b py-6 hover:bg-gray-50 border-gray-200 text-gray-700 cursor-default">
                    <p className="flex-1 h-auto">{item.productName}</p>
                    <p className="flex-1 truncate">{item.createdDate}</p>
                    <p className="flex-1 truncate">{item.amount}</p>
                    <p className="flex-1 truncate">{item.price}</p>
                    <p className="flex-1 truncate">{item.price * item.amount}</p>
                    <div className="flex-1">
                        <button
                            className="text-blue-500 hover:bg-blue-100 mr-2 font-bold bg-blue-50 py-1 px-4 border border-blue-300 rounded-md duration-150 text-sm">
                            Просмотр
                        </button>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default InventoryArrivalsPage;