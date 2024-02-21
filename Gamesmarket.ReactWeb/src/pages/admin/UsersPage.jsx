import React, { useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import { AdminHandlers } from "./Utils/AdminHandlers";

const UsersPage = () => {
  const { users, handleRevokeAll, handleChangeRole } = AdminHandlers();
  const [selectedRole, setSelectedRole] = useState("");
  const [selectedEmail, setSelectedEmail] = useState("");

  const RoleChanger = async () => {
    if (selectedRole && selectedEmail) {
      await handleChangeRole(selectedEmail, selectedRole);
      setSelectedRole("");
      setSelectedEmail("");
    } else {
      toast.error("Both role and email need to be selected correctly.");
    }
  };

  return (
    <div className="row mt-3">
      <div className="col-md-8">
        <div className="mb-3">
          <button className="btn btn-danger" onClick={handleRevokeAll}>
            Revoke All Tokens
          </button>
        </div>
        <table className="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Refresh Token Expiry Time</th>
              <th>Email</th>
              <th>Role</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.id}>
                <td>{user.id}</td>
                <td>{user.name}</td>
                <td>{user.refreshTokenExpiryTime}</td>
                <td>{user.email}</td>
                <td>{user.role}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="col-md-4 ">
        <div className="mb-3">
          <button className="btn btn-primary" onClick={RoleChanger}>
            Change Role
          </button>
        </div>
        <div className="mb-3">
          <select
            className="form-select"
            value={selectedRole}
            onChange={(e) => setSelectedRole(e.target.value)}
          >
            <option value="" disabled>
              Select Role
            </option>
            <option value="User">User</option>
            <option value="Moderator">Moderator</option>
            <option value="Administrator">Administrator</option>
          </select>
        </div>
        <div className="mb-3">
          <input
            type="text"
            className="form-control"
            placeholder="Enter User Email"
            value={selectedEmail}
            onChange={(e) => setSelectedEmail(e.target.value)}
          />
        </div>
      </div>
      <ToastContainer />
    </div>
  );
};

export default UsersPage;
