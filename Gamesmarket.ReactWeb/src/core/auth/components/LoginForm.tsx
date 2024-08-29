import { FC, useContext, useState } from "react";
import { Context } from "../../../main";
import {
  Button,
  TextField,
  Link,
  Box,
  Typography,
  IconButton,
  InputAdornment,
  Paper,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { Link as ReactRouterLink } from "react-router-dom";

const LoginForm: FC = () => {
  const { authStore, cartStore } = useContext(Context).rootStore;
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [showPassword, setShowPassword] = useState<boolean>(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    await authStore.login(email, password);
    await cartStore.getOrders();
  };

  const handleClickShowPassword = () => {
    setShowPassword(!showPassword);
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        minHeight: "65vh",
        backgroundColor: "#18161c",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <Paper
        elevation={24}
        sx={{
          padding: 4,
          width: 400,
          maxWidth: "90%",
          backgroundColor: "#2c2f33",
          color: "#fff",
        }}
      >
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email Address"
            name="email"
            autoComplete="email"
            autoFocus
            InputProps={{ style: { color: "#fff" } }}
            InputLabelProps={{ style: { color: "#ccc" } }}
            onChange={(e) => setEmail(e.target.value)}
            value={email}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type={showPassword ? "text" : "password"}
            id="password"
            autoComplete="current-password"
            onChange={(e) => setPassword(e.target.value)}
            value={password}
            InputProps={{
              style: { color: "#fff" },
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    onClick={handleClickShowPassword}
                    edge="end"
                    sx={{ color: "#fff" }}
                  >
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
            InputLabelProps={{ style: { color: "#ccc" } }}
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{
              mt: 2,
              backgroundColor: "#ff4020",
              color: "#fff",
              "&:hover": {
                backgroundColor: "#e0391d",
              },
            }}
          >
            Sign In
          </Button>
          <Link
            component={ReactRouterLink}
            to="/register"
            variant="body2"
            sx={{ color: "inherit" }}
          >
            {"Don't have an account? Sign Up"}
          </Link>
        </Box>
      </Paper>
    </Box>
  );
};

export default LoginForm;
