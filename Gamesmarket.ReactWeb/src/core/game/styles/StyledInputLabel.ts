import { styled } from "@mui/material/styles";
import { InputLabel } from "@mui/material";

export const StyledInputLabel = styled(InputLabel)(() => ({
  color: "white",
  "&.Mui-focused": {
    color: "white",
  },
}));
