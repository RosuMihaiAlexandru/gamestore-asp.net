import { FC, useContext, useState } from "react";
import { Context } from "../../../main";
import {
  Box,
  Typography,
  TextField,
  Button,
  IconButton,
  InputAdornment,
  Alert,
  Paper,
  Link,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { validatePassword } from "../utils/validatePassword";
import { Link as ReactRouterLink } from "react-router-dom";

const RegisterForm: FC = () => {
  const { authStore, cartStore } = useContext(Context).rootStore;
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [passwordConfirm, setPasswordConfirm] = useState<string>("");
  const [birthDate, setBirthDate] = useState<Date | null>(null);
  const [name, setName] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string | null>(null);
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const [showPasswordConfirm, setShowPasswordConfirm] =
    useState<boolean>(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const passwordValidationError = validatePassword(password);
    if (passwordValidationError) {
      setPasswordError(passwordValidationError);
      return;
    }

    if (password !== passwordConfirm) {
      setPasswordError("Passwords do not match");
      return;
    }
    await authStore.register(email, birthDate, password, passwordConfirm, name);
    await cartStore.getOrders();
  };

  const handleBirthDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setBirthDate(e.target.value ? new Date(e.target.value) : null);
  };

  const handleClickShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const handleClickShowPasswordConfirm = () => {
    setShowPasswordConfirm(!showPasswordConfirm);
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        minHeight: "95vh",
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
        <Typography variant="h4" align="center" gutterBottom>
          Register
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
            name="birthDate"
            type="date"
            id="birthDate"
            autoComplete="bday"
            InputProps={{ style: { color: "#fff" } }}
            InputLabelProps={{ style: { color: "#ccc" } }}
            onChange={handleBirthDateChange}
            value={birthDate ? birthDate.toISOString().substring(0, 10) : ""}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="name"
            label="Name"
            name="name"
            autoComplete="name"
            InputProps={{ style: { color: "#fff" } }}
            InputLabelProps={{ style: { color: "#ccc" } }}
            onChange={(e) => setName(e.target.value)}
            value={name}
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
            onChange={(e) => {
              setPassword(e.target.value);
              setPasswordError(null); // Clear error message when user types
            }}
            value={password}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="passwordConfirm"
            label="Confirm Password"
            type={showPasswordConfirm ? "text" : "password"}
            id="passwordConfirm"
            autoComplete="current-password"
            InputProps={{
              style: { color: "#fff" },
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    onClick={handleClickShowPasswordConfirm}
                    edge="end"
                    sx={{ color: "#fff" }}
                  >
                    {showPasswordConfirm ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
            InputLabelProps={{ style: { color: "#ccc" } }}
            onChange={(e) => setPasswordConfirm(e.target.value)}
            value={passwordConfirm}
          />
          {passwordError && <Alert severity="error">{passwordError}</Alert>}
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
            Sign up
          </Button>
          <Typography align="center" sx={{ mt: 2 }}>
            <Link
              component={ReactRouterLink}
              to="/login"
              variant="body2"
              sx={{ color: "inherit" }}
            >
              {"Already have an account? Sign in"}
            </Link>
          </Typography>
        </Box>
      </Paper>
    </Box>
  );
};

export default RegisterForm;
