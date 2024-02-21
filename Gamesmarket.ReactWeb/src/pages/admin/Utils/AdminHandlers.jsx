import React, { useState, useEffect } from "react";
import {
  getUsers,
  revokeAll,
  changeRole,
} from "../../../common/services/api/account/AccountApi";
import { toast } from "react-toastify";

export const AdminHandlers = () => {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await getUsers();
        setUsers(response);
      } catch (error) {
        console.error("Error getting users:", error);
        toast.error("Failed load all users.");
      }
    };
    fetchUsers();
  }, []);

  const handleRevokeAll = async () => {
    try {
      await revokeAll();
      const response = await getUsers();
      setUsers(response);
      toast.success("Users revoked successfully!");
    } catch (error) {
      console.error("Error revoking tokens:", error);
      toast.error("Failed to revoke all users tokens.");
    }
  };

  const handleChangeRole = async (email, newRole) => {
    try {
      await changeRole(email, newRole);
      const response = await getUsers();
      setUsers(response);
      toast.success(`The user ${email} now have ${newRole} role!`);
    } catch (error) {
      console.error("Error changing user role:", error);
      toast.error("Failed to change user role.");
    }
  };

  return { users, handleRevokeAll, handleChangeRole };
};
