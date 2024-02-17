import { Link, useNavigate } from "react-router-dom";
import { AccountHandler } from "./Utils/AccountHandler";

function RegistrationPage() {
  const { userData, handleChange, handleRegister } = AccountHandler();
  const { email, birthDate, password, passwordConfirm, name } = userData;
  const navigate = useNavigate();

  const handleRegisterClick = () => {
    handleRegister(userData);
    navigate("/");
  };

  return (
    <div className="container-fluid bg-light py-4">
      <div className="row justify-content-center">
        <div className="col-md-6 bg-white p-4 rounded">
          <h2 className="text-center mb-4">Create an account</h2>
          <form>
            <div className="mb-3">
              <input
                type="email"
                className="form-control"
                placeholder="Email"
                value={email}
                onChange={handleChange}
                name="email"
              />
            </div>
            <div className="mb-3">
              <input
                type="date"
                className="form-control"
                placeholder="Birth Date"
                value={birthDate}
                onChange={handleChange}
                name="birthDate"
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
              <input
                type="password"
                className="form-control"
                placeholder="Confirm Password"
                value={passwordConfirm}
                onChange={handleChange}
                name="passwordConfirm"
              />
            </div>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Name"
                value={name}
                onChange={handleChange}
                name="name"
              />
            </div>
            <ul>
              <li>At least 6 characters</li>
              <li>Special character or a digit</li>
              <li>At least 1 small letter</li>
              <li>At least 1 capital letter</li>
              <li>At least 1 number</li>
            </ul>
            <div className="mb-3">
              <button
                type="button"
                className="btn btn-primary w-100"
                onClick={handleRegisterClick}
              >
                Create account
              </button>
            </div>
          </form>
          <p className="text-center mb-0">
            Have an account already? <Link to="/login">Sign in</Link>
          </p>
        </div>
      </div>
    </div>
  );
}

export default RegistrationPage;
