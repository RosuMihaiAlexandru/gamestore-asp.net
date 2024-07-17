import { FC, useContext, useState } from "react";
import { Context } from "../../../main";

const LoginForm: FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const { authStore } = useContext(Context).rootStore;

  return (
    <div>
      <input
        onChange={(e) => setEmail(e.target.value)}
        value={email}
        type="text"
        placeholder="Email"
      />
      <input
        onChange={(e) => setPassword(e.target.value)}
        value={password}
        type="password"
        placeholder="Password"
      />
      <button onClick={() => authStore.login(email, password)}>Log in</button>
    </div>
  );
};

export default LoginForm;
