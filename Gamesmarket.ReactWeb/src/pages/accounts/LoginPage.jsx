import { Link, useNavigate } from "react-router-dom";
import { AccountHandler } from "./Utils/AccountHandler";

function LoginPage() {
  const { userData, handleChange, handleLogin } = AccountHandler();
  const { email, password } = userData;
  const navigate = useNavigate();

  const handleLoginClick = () => {
    handleLogin(email, password);
    navigate("/");
  };

  return (
    <div className="container-fluid bg-light py-4">
      <div className="row justify-content-center">
        <div className="col-md-6 bg-white p-4 rounded">
          <h2 className="text-center mb-4">Sign in</h2>
          <form>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Email"
                value={email}
                onChange={handleChange}
                name="email"
              />
            </div>
            <div className="mb-3">
              <input
                type="password"
                className="form-control"
                placeholder="Password"
                value={password}
                onChange={handleChange}
                name="password"
              />
            </div>
            <div className="mb-3">
              <button
                type="button"
                className="btn btn-primary w-100"
                onClick={handleLoginClick}
              >
                Login
              </button>
            </div>
          </form>
          <p className="text-center mb-0">
            New to GamesMarket? Start your journey!{" "}
            <Link to="/register"> Create an account</Link>
          </p>
        </div>
      </div>
    </div>
  );
}

export default LoginPage;
