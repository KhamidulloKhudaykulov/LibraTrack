const RentPayments = () => {
    return (
        <div className="flex flex-row gap-4 items-center justify-center">
            <div className="flex-1 h-20 bg-white rounded-lg flex flex-col gap-1 justify-center pl-4">
                <p className="text-gray-600 text-sm">Yozuvlar soni</p>
                <p className="text-gray-600 text-lg">260456</p>
            </div>
            <div className="flex-1 h-20 bg-white rounded-lg flex flex-col gap-1 justify-center pl-4">
                <p className="text-gray-600 text-sm">Umumiy qiymat</p>
                <p className="text-gray-600 text-lg">388 106 245</p>
            </div>
            <div className="flex-1 h-20 bg-white rounded-lg flex flex-col gap-1 justify-center pl-4">
                <p className="text-gray-600 text-sm">To'langan</p>
                <p className="text-gray-600 text-lg">260 456 156</p>
            </div>
        </div>
    )
};

export default RentPayments;