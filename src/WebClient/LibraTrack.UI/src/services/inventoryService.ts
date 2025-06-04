export interface Item {
    id: string;
    productName: string;
    amount: number;
    availableQuantity: number;
    price: number;
    totalPrice: number;
    createdDate: string;
}

export type AddItemCommand = {
    productId: string;
    amount: number;
    price: number;
}

export interface StockBalance {
    id : string;
    productName: string;
    availableQuantity: number;
}

export async function getInventoryItems() {
    try {
        const response = await fetch('http://localhost:5203/inventory-api/api/inventory', {
            method: 'GET',
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error(`Server error: ${response.status}`);
        }

        const data: Item[] = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching items:', error);
        throw error;
    }
};

export async function getInventoryStockBalances() {
    try {
        const response = await fetch('http://localhost:5203/inventory-api/api/inventory/stockBalances', {
            method: 'GET',
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error(`Server error: ${response.status}`);
        }

        const data: StockBalance[] = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching items:', error);
        throw error;
    }
}

export async function addItemToInventory(command: AddItemCommand) {
    try {
        const response = await fetch("http://localhost:5203/inventory-api/api/inventory", {
            method: "POST",
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        });
        if (!response.ok) {
            throw new Error(`Server error: ${response.status}`);
        }

        const data: Item[] = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching books:', error);
        throw error;
    }
}