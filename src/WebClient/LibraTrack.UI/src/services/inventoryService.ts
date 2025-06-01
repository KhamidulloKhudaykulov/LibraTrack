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
        const response = await fetch('https://localhost:7096/api/inventory', {
            method: 'GET',
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
        const response = await fetch('https://localhost:7096/api/inventory/stockBalances', {
            method: 'GET',
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
        const response = await fetch("https://localhost:7096/api/inventory", {
            method: "POST",
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