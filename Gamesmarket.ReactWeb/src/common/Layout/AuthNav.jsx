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
      <Link to="/cart" style={{ marginRight: "10px" }}>
        <img
          src="/cart.svg"
          alt="Cart"
          style={{ width: "30px", height: "30px" }}
        />
      </Link>
      <Link to="/" onClick={handleLogout} style={{ color: "black" }}>
        Logout
      </Link>
      <Link to="/" style={{ marginLeft: "10px" }} onClick={handleLogout}>
        <img
          src="/logout.svg"
          alt="Logout"
          style={{ width: "30px", height: "30px" }}
        />
      </Link>
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
