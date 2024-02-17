import { Link, useNavigate } from "react-router-dom";
import { isAuthenticated } from "../../pages/accounts/Utils/AuthHandler";

const AuthNav = () => {
  const navigate = useNavigate();
  const userName = localStorage.getItem("username");

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    localStorage.removeItem("username");
    navigate("/");
  };

  return isAuthenticated() ? (
    <div>
      <span style={{ marginRight: "10px" }}>{userName}</span>
      <button onClick={handleLogout}>Logout</button>
    </div>
  ) : (
    <ul className="navbar-nav mb-2 mb-lg-0">
      <li className="nav-item">
        <Link className="nav-link active" aria-current="page" to="/login">
          Sign in / Register
        </Link>
      </li>
    </ul>
  );
};

export default AuthNav;
