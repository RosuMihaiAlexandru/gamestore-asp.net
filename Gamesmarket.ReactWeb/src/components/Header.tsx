import { ShoppingCartOutlined } from "@mui/icons-material";
import {
  AppBar,
  Box,
  IconButton,
  Link,
  Toolbar,
  Typography,
} from "@mui/material";
import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { Context } from "../main";
import { observer } from "mobx-react-lite";
import Search from "./Search";
import AccountMenu from "./AccountMenu";

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
        <Box sx={{ flexGrow: 1, display: "flex", alignItems: "center" }}>
          <Link href="/" color="inherit" underline="none">
            <Typography variant="h6" noWrap>
              GamesMarket
            </Typography>
          </Link>
        </Box>
        <Search />
        <Box sx={{ flexGrow: 1, display: "flex", justifyContent: "flex-end" }}>
          <IconButton color="inherit" onClick={() => handleIconClick("/")}>
            <ShoppingCartOutlined sx={{ width: 30, height: 30 }} />
          </IconButton>
          <AccountMenu />
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default observer(Header);
