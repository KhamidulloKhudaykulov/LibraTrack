import type { NavigateFunction } from "react-router-dom";

export interface User {
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

export async function getUsers(navigate: NavigateFunction): Promise<User[]> {
  try {
    const response = await fetch("https://localhost:7287/api/users", {
      method: "GET",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (response.status === 401) {
      navigate("/login")
      return [];
    }

    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }

    const data: User[] = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching users:", error);
    throw error;
  }
}

export async function blockUser(id: string, navigate: NavigateFunction): Promise<boolean> {
  try {
    const response = await fetch(`https://localhost:7287/api/users/block?id=${id}`, {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (response.status === 401) {
      navigate("/login")
      return false;
    }

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

export async function activeUser(id: string, navigate: NavigateFunction): Promise<boolean> {
  try {
    const response = await fetch(`https://localhost:7287/api/users/active?id=${id}`, {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (response.status === 401) {
      navigate("/login")
      return false;
    }

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


export async function editUser(user: User, navigate: NavigateFunction): Promise<User> {
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
      credentials: "include"
    });

    if (response.status === 401) {
      navigate("/login")
      throw new Error("Unauthorized");
    }

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

export async function addUser(command: AddUserCommand, navigate: NavigateFunction) {
  try {
    const response = await fetch("https://localhost:7287/api/users", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(command),
    });

    if (response.status === 401) {
      navigate("/login")
      return [];
    }

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