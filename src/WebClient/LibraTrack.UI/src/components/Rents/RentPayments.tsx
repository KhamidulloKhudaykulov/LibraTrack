import { type Rent } from "@/services/rentService";
import { useEffect, useState } from "react";

type Payments = {
    rent: Rent[];
}

const RentPayments = (rents: Payments) => {
    const [totalPrice, setTotalPrice] = useState('');
    const [records, setRecords] = useState(0);
    const [payedRents, setPayedRents] = useState(0);

    useEffect(() => {
        setRecords(rents.rent.length);
        const data = rents.rent.reduce((sum, rent) => sum + Number(rent.price), 0).toString();
        setTotalPrice(data);
        const totalPayed = rents.rent
            .filter(rent => rent.isPayed)
            .reduce((sum, rent) => sum + Number(rent.price), 0);

        setPayedRents(totalPayed);
    })

    return (
        <div className="flex flex-row gap-4 items-center justify-center">
            <div className="flex-1 h-20 bg-white rounded-lg flex flex-col gap-1 justify-center pl-4">
                <p className="text-gray-600 text-sm">Yozuvlar soni</p>
                <p className="text-gray-600 text-lg">{records}</p>
            </div>
            <div className="flex-1 h-20 bg-white rounded-lg flex flex-col gap-1 justify-center pl-4">
                <p className="text-gray-600 text-sm">Umumiy qiymat</p>
                <p className="text-gray-600 text-lg">{totalPrice}</p>
            </div>
            <div className="flex-1 h-20 bg-white rounded-lg flex flex-col gap-1 justify-center pl-4">
                <p className="text-gray-600 text-sm">To'langan</p>
                <p className="text-gray-600 text-lg">{payedRents}</p>
            </div>
        </div>
    )
};

export default RentPayments;