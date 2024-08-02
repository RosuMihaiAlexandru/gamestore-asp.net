import { styled } from "@mui/system";
import Pagination from "@mui/material/Pagination";

const StyledPagination = styled(Pagination)(() => ({
  "& .MuiPaginationItem-root": {
    color: "#fff",
  },
}));

export default StyledPagination;
