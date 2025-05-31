type PaymentFilterProps = {
    value: string;
    onChange: (value: string) => void;
}

const PaymentFilter = ({ value, onChange }: PaymentFilterProps) => {
    return (
        <div>
            <select
                value={value}
                onChange={(e) => onChange(e.target.value)}
                className="cursor-pointer border border-blue-400 p-1 rounded-md bg-inherit border-gray-300 text-gray-300 ml-4 outline-none focus:ring-1 transition-all duration-300 focus:ring-blue-400 hover:border-blue-200"
            >
                <option value="" disabled hidden>
                    Статус платежа
                </option>
                <option value="all">Все</option>
                <option value="true">Оплаченные</option>
                <option value="false">Неоплаченные</option>
            </select>
        </div>
    );
};

export default PaymentFilter;