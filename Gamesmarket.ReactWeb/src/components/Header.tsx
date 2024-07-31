import { useContext } from "react";
import { ShoppingCartOutlined } from "@mui/icons-material";
import {
  AppBar,
  Box,
  IconButton,
  Link,
  Toolbar,
  Typography,
} from "@mui/material";
import { Link as ReactRouterLink, useNavigate } from "react-router-dom";
import { Context } from "../main";
import { observer } from "mobx-react-lite";
import Search from "./Search";
import AccountMenu from "./AccountMenu";
import AdminPanel from "./AdminPanel";

const Header = () => {
  const { rootStore } = useContext(Context);
  const { authStore } = rootStore;
  const navigate = useNavigate();

  const handleIconClick = (path: string) => {
    if (!authStore.isAuth) {
      navigate("/login");
    } else {
      navigate(path);
    }
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Box
          sx={{
            mb: 3,
            ml: 2,
            flexGrow: 1,
            display: "flex",
            alignItems: "center",
          }}
        >
          <Link
            component={ReactRouterLink}
            to="/"
            color="inherit"
            underline="none"
          >
            <Typography variant="h6" noWrap>
              GamesMarket
            </Typography>
          </Link>
          {authStore.isAdmin || authStore.isModerator ? <AdminPanel /> : null}
        </Box>
        <Search />
        <Box
          sx={{
            mb: 3,
            mr: 2,
            flexGrow: 1,
            display: "flex",
            justifyContent: "flex-end",
          }}
        >
          <IconButton color="inherit" onClick={() => handleIconClick("/cart")}>
            <ShoppingCartOutlined sx={{ mr: 1, width: 30, height: 30 }} />
          </IconButton>
          <AccountMenu />
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default observer(Header);
