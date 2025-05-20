export interface User{
    id: string;
    fullName: string;
    email: string;
    phoneNumber: string;
    passportNumber: string;
    status: string;
}

export type AddUserCommand = {
  firstName: string;
  lastName: string;
  email: string;
  passportNumber: string;
  phoneNumber: string;
};

export async function getUsers() : Promise<User[]> {
    try {
        const response = await fetch('https://localhost:7287/api/users', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
    
        if (!response.ok) {
          throw new Error(`Server error: ${response.status}`);
        }
    
        const data: User[] = await response.json();
        return data;
      } catch (error) {
        console.error('Error fetching books:', error);
        throw error;
      }
}

export async function blockUser(id: string) : Promise<boolean> {
    try {
        const response = await fetch(`https://localhost:7287/api/users/block?id=${id}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
          },
        });
    
        if (!response.ok) {
          throw new Error(`Server error: ${response.status}`);
        }
    
        const data: boolean = await response.json();
        return data;
      } catch (error) {
        console.error('Error fetching books:', error);
        throw error;
      }
}

export async function activeUser(id: string) : Promise<boolean> {
    try {
        const response = await fetch(`https://localhost:7287/api/users/active?id=${id}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
          },
        });
    
        if (!response.ok) {
          throw new Error(`Server error: ${response.status}`);
        }
    
        const data: boolean = await response.json();
        return data;
      } catch (error) {
        console.error('Error fetching books:', error);
        throw error;
      }
}


export async function editUser(user: User): Promise<User> {
  try {
     const queryParams = new URLSearchParams({
      id: user.id,
      firstName: user.fullName.split(" ")[0],
      lastName: user.fullName.split(" ")[1],
      email: user.email,
      passportNumber: user.passportNumber,
      phoneNumber: user.phoneNumber,
    });

    const response = await fetch(`https://localhost:7287/api/users/update?${queryParams.toString()}`, {
      method: "PUT",
    });

    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }

    const updatedUser: User = await response.json();
    return updatedUser;
  } catch (error) {
    console.error("Error editing user:", error);
    throw error;
  }
}

export async function addUser(command: AddUserCommand) {
  try {
    const response = await fetch("https://localhost:7287/api/users", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(command),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Server error: ${response.status} - ${errorText}`);
    }

    const createdUser = await response.json();
    return createdUser;
  } catch (error) {
    console.error("Error adding user:", error);
    throw error;
  }
}