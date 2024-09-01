import { styled } from "@mui/material/styles";
import { Select } from "@mui/material";

export const StyledSelect = styled(Select)(() => ({
  color: "white",
  backgroundColor: "#1d1c1f",
  "& .MuiOutlinedInput-notchedOutline": {
    borderColor: "#434147",
  },
  "& .MuiSvgIcon-root": {
    color: "white",
  },
}));
