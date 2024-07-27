import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { Context } from "../main";
import { observer } from "mobx-react-lite";

const PrivateRoute = observer(({ roles = [] }: { roles?: string[] }) => {
  const { rootStore } = useContext(Context);
  const { authStore } = rootStore;

  if (!authStore.isAuth) {
    return <Navigate to="/login" />;
  }

  if (roles.length && !roles.includes(authStore.user.role)) {
    return <Navigate to="/accessDenied" />;
  }

  return <Outlet />;
});

export default PrivateRoute;
