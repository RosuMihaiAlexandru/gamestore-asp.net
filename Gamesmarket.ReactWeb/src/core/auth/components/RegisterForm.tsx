import { FC, useContext, useState } from "react";
import { Context } from "../../../main";

const RegisterForm: FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [passwordConfirm, setpasswordConfirm] = useState<string>("");
  const [birthDate, setBirthDate] = useState<Date | null>(null);
  const [name, setName] = useState<string>("");
  const { authStore } = useContext(Context).rootStore;

  const handleBirthDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setBirthDate(e.target.value ? new Date(e.target.value) : null);
  };

  return (
    <div>
      <input
        onChange={(e) => setEmail(e.target.value)}
        value={email}
        type="text"
        placeholder="Email"
      />
      <input
        onChange={handleBirthDateChange}
        value={birthDate ? birthDate.toISOString().substring(0, 10) : ""}
        type="date"
        placeholder="Birth Date"
      />
      <input
        onChange={(e) => setPassword(e.target.value)}
        value={password}
        type="password"
        placeholder="password"
      />
      <input
        onChange={(e) => setpasswordConfirm(e.target.value)}
        value={passwordConfirm}
        type="password"
        placeholder="password"
      />
      <input
        onChange={(e) => setName(e.target.value)}
        value={name}
        type="text"
        placeholder="name"
      />
      <button
        onClick={() =>
          authStore.register(email, birthDate, password, passwordConfirm, name)
        }
      >
        Register
      </button>
    </div>
  );
};

export default RegisterForm;
