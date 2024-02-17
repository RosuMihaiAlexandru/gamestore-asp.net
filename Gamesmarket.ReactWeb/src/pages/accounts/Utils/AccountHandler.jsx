import { useState } from "react";
import {
  login,
  register,
} from "../../../common/services/api/account/AccountApi";

export const AccountHandler = () => {
  const [userData, setUserData] = useState({
    email: "",
    birthDate: "",
    password: "",
    passwordConfirm: "",
    name: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUserData((prevUserData) => ({
      ...prevUserData,
      [name]: value,
    }));
  };

  const handleLogin = async (email, password) => {
    try {
      const response = await login(email, password);
      console.log("User Loged In:", response);
    } catch (error) {
      console.error("Error registering user:", error);
    }
  };

  const handleRegister = async (userData) => {
    try {
      const response = await register(userData);
      console.log("User registered:", response);
    } catch (error) {
      console.error("Error registering user:", error);
    }
  };

  return { userData, handleChange, handleLogin, handleRegister };
};
