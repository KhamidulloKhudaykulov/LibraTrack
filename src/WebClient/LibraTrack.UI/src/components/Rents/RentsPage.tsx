import { useEffect, useState } from "react";
import SearchUser from "../Users/SearchUser";
import RentPayments from "./RentPayments";
import { cancelRent, closeRent, getRents, payRent, type Rent } from "@/services/rentService";
import AddRent from "./AddRent";
import PaymentFilter from "./PaymetFilter";

const RentsPage = () => {
    const [query, setQuery] = useState('');
    const [payedQuery, setPayedQuery] = useState('');
    const [rents, setRents] = useState<Rent[]>([]);
    const [loading, setLoading] = useState(true);

    const filteredRents = rents.filter(rent => {
        const matchesQuery = rent.bookTitle.toLowerCase().includes(query.toLowerCase()) ||
            rent.userName.toLowerCase().includes(query.toLowerCase());

        if (payedQuery === "true") return matchesQuery && Boolean(rent.isPayed) === true;
        if (payedQuery === "false") return matchesQuery && Boolean(rent.isPayed) === false;
        return matchesQuery;
    });

    useEffect(() => {
        const fetchUsers = async () => {
            const data = await getRents();
            setRents(data);
            setLoading(false);
        };

        fetchUsers();
    }, []);

    const handleClose = async (id: string) => {
        try {
            await closeRent(id);
        } catch (error) {
            alert(error);
        }

        window.location.reload();
    }

    const handlePay = async (id: string) => {
        try {
            await payRent(id);
        } catch (error) {
            alert(error);
        }

        window.location.reload();
    }

    const handleCancel = async (id: string) => {
        try {
            await cancelRent(id);
        } catch (error) {
            alert(error);
        }

        window.location.reload();
    }

    return (
        <div className="flex flex-col gap-4">
            <div className="h-auto">
                <RentPayments rent={filteredRents} />
            </div>
            <div className="bg-white w-full rounded-xl flex flex-col h-full">
                <div className="flex flex-row items-center relative p-4">
                    {/* filter */}
                    <SearchUser value={query} placeholder="Искать по ФИО или по названию книги" onChange={setQuery} />
                    <PaymentFilter value={payedQuery} onChange={setPayedQuery} />
                    <AddRent />
                </div>
                <div className="flex flex-row p-4 border-t border-b border-gray-200 font-bold text-gray-400">
                    <h2 className="flex-1 items-center">Пользователь</h2>
                    <h2 className="flex-1 items-center pl-1">Книга</h2>
                    <h2 className="flex-1 items-center pl-1">Начало</h2>
                    <h2 className="flex-1 items-center pl-1">Конец</h2>
                    <h2 className="flex-1 items-center pl-1">Статус платежа</h2>
                    <h2 className="flex-1 items-center pl-1">Статус аренды</h2>
                    <h2 className="flex-1 items-center pl-1">Цена</h2>
                    <h2 className="flex-1 items-center pl-1">Действии</h2>
                </div>
                {loading &&
                    <div className="w-full flex flex-row items-center justify-center flex-1">
                        <div role="status">
                            <svg aria-hidden="true" className="inline w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor" />
                                <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill" />
                            </svg>
                            <span className="sr-only">Загружается...</span>
                        </div>
                    </div>}
                {filteredRents.length === 0 && (
                    <p className="ml-auto mr-auto text-gray-400 text-sm p-4">Нет данных</p>
                )}

                {filteredRents.map((rent) => {
                    // const [day, month, year] = rent.endDate.split(".");
                    // const endDateObj = new Date(`${year}-${month}-${day}`);
                    // const isExpired = endDateObj < new Date();

                    return (
                        <div
                            key={rent.id}
                            className={`flex flex-row p-4 items-start py-2 ${rent.isDeleted ? "bg-red-50" : "border-b hover:bg-gray-50 border-gray-200 text-gray-700"}`}
                        >
                            <p className="flex-1 pl-1 min-w-0  break-words whitespace-normal self-center">{rent.userName}</p>
                            <p className="flex-1 pl-1 min-w-0 break-words whitespace-normal self-center">{rent.bookTitle}</p>
                            <p className="flex-1 pl-1 min-w-0 self-center">{rent.startDate}</p>
                            <p className="flex-1 pl-1 min-w-0 self-center">{rent.endDate}</p>
                            <div className={`flex-1 pl-1 min-w-0 self-center w-10 text-center`}>
                                <p className={`w-32 rounded-sm px-2 py-1 font-bold cursor-default ${rent.isPayed || rent.isReturned ? "bg-green-200 text-green-400" : "bg-red-200 text-red-400"}`}>
                                    {rent.isPayed || rent.isReturned ? "Оплачено" : "Не оплачено"}
                                </p>
                            </div>
                            <p className={`flex-1 pl-1 font-semibold min-w-0 self-center cursor-default ${rent.isReturned ? "text-gray-200" : rent.isDeleted ? "text-red-300" : "text-green-500"}`}>
                                {rent.isReturned ? "Возвращено" : rent.isDeleted ? "Отменено" : "Активный"}
                            </p>
                            <p className="flex-1 pl-1 min-w-0 self-center">{rent.price}</p>
                            <div className="flex-1 flex flex-col gap-1 font-bold">
                                <button
                                    className={`px-1 py-1 ${rent.isPayed || rent.isDeleted ? "cursor-not-allowed bg-blue-200 text-white" : "border-blue-500 border-1 text-blue-500 rounded-sm cursor-pointer hover:bg-blue-500 hover:text-white duration-150"}`}
                                    onClick={() => {
                                        if (!rent.isPayed || rent.isDeleted){
                                            handlePay(rent.id)
                                        }
                                    }}
                                >
                                    Оплатить
                                </button>
                                <button
                                    className={`px-1 py-1 pl-1 ${rent.isReturned || (rent.isDeleted) ? "cursor-not-allowed bg-green-200 text-white" : "border-green-500 border-1 text-green-500 rounded-sm cursor-pointer hover:bg-green-500 hover:text-white duration-150"}`}
                                    onClick={() => handleClose(rent.id)}>
                                    Возврат
                                </button>
                                <button
                                    className={`px-1 py-1 pl-1 ${rent.isDeleted || (rent.isPayed && rent.isReturned) ? "cursor-not-allowed bg-red-200 text-white" : "border-red-500 border-1 text-red-500 rounded-sm cursor-pointer hover:bg-red-500 hover:text-white duration-150"}`}
                                    onClick={() => {
                                        if (!rent.isDeleted){
                                            handleCancel(rent.id)
                                        }
                                    }}>
                                    Отменить
                                </button>
                                
                            </div>
                        </div>

                    );
                })}

            </div>
        </div>
    )
};

export default RentsPage;