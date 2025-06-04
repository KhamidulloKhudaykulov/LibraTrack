export interface Rent {
  id: string;
  userName: string;
  bookTitle: string;
  startDate: string;
  endDate: string;
  price: string;
  isPayed: string;
  isReturned: string;
  isDeleted: string;
}

export type AddRentCommand = {
  userId: string;
  bookId: string;
  price: number;
  startDate: Date;
  endDate: Date;
}

export async function getRents(): Promise<Rent[]> {
  try {
    const response = await fetch('http://localhost:5203/rentals-api/api/rents', {
      method: 'GET',
      credentials: "include",
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }

    const data: Rent[] = await response.json();
    return data;
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  }
};

export async function getPriceRents(): Promise<string> {
  try {
    const response = await fetch('http://localhost:5203/rentals-api/api/rents', {
      method: 'GET',
      credentials: "include",
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }

    const data: Rent[] = await response.json();
    const totalPrice: number = data.reduce((sum, rent) => sum + Number(rent.price), 0);
    return totalPrice.toString();

  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  }
};

export async function addRent(command: AddRentCommand) {
  try {
    const data = {
      userId: command.userId,
      bookId: command.bookId,
      price: command.price,
      startDate: command.startDate.toISOString(),
      endDate: command.endDate.toISOString()
    };
    const response = await fetch("http://localhost:5203/rentals-api/api/rents", {
      method: "POST",
      credentials: "include",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });
    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(`${errorData.message}`);
    }
    const result: Rent[] = await response.json();
    return result;
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  };
};

export async function closeRent(id: string) {
  try {
    const response = await fetch(`http://localhost:5203/rentals-api/api/rents/close?rentId=${id}`, {
      method: "POST",
      credentials: "include",
      headers: {
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Server error: ${response.status} - ${errorText}`);
    }
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  }
};

export async function cancelRent(id: string) {
  try {
    const response = await fetch(`http://localhost:5203/rentals-api/api/rents/cancel?rentId=${id}`, {
      method: "PUT",
      credentials: "include",
      headers: {
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Server error: ${response.status} - ${errorText}`);
    }
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  }
}

export async function payRent(id: string) {
  try {
    const response = await fetch(`http://localhost:5203/rentals-api/api/rents/pay?rentId=${id}`, {
      method: "PUT",
      credentials: "include",
      headers: {
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      const errorText = await response.json();
      throw new Error(`Server error: ${response.status}\nMessage: ${errorText.message}`);
    }
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error;
  }
}