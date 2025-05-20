import { editUser, type User } from "@/services/userService";
import { useState } from "react";

type UserEditProps = {
  user: User;
  onClose: () => void;
};

const UserEdit = ({ user, onClose }: UserEditProps) => {
  const [id, setId] = useState(user.id);
  const [firstName, setFirstName] = useState(user.fullName.split(' ')[0] || "");
  const [lastName, setLastName] = useState(user.fullName.split(' ')[1] || "");
  const [email, setEmail] = useState(user.email);
  const [passportNumber, setPassportNumber] = useState(user.passportNumber);
  const [phoneNumber, setPhoneNumber] = useState(user.phoneNumber);

  const onSave = async () => {
    const updatedUser: User = {
      id,
      fullName: `${firstName} ${lastName}`,
      email,
      passportNumber,
      phoneNumber,
      status
    };

    try {
      const response = await editUser(updatedUser);
      console.log("User updated:", response);
      onClose();
      window.location.reload();
    } catch (error) {
      console.error("Error saving user:", error);
    }
  }

  return (
    <div className="fixed inset-0 bg-opacity-10 flex justify-center items-center z-50">
      <div className="bg-white rounded-lg p-6 shadow-2xl w-[400px]">
        <h2 className="text-xl font-bold mb-4">Edit User</h2>
        <div className="flex flex-col gap-1">
          <input
            type="text"
            placeholder="id"
            value={id}
            onChange={(e) => setId(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
            disabled
          />
          <input
            type="text"
            placeholder="First name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="text"
            placeholder="Last name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="text"
            placeholder="Passport number"
            value={passportNumber}
            onChange={(e) => setPassportNumber(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
          <input
            type="text"
            placeholder="Phone number"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
            className="p-2 bg-gray-100 rounded-md w-full pl-4"
          />
        </div>
        <button
          className="mt-4 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
          onClick={onClose}
        >
          Close
        </button>
        <button
          className="mt-4 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 ml-1"
          onClick={onSave}
        >
          Save
        </button>
      </div>
    </div>
  );
};

export default UserEdit;
