import { styled } from "@mui/material/styles";
import { DataGrid, gridClasses } from "@mui/x-data-grid";

const StyledDataGrid = styled(DataGrid)(({ theme }) => ({
  [`& .${gridClasses.row}`]: {
    "&:nth-of-type(odd)": {
      backgroundColor: theme.palette.action.hover,
    },
  },
  [`& .${gridClasses.columnHeader}`]: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
    "& .MuiDataGrid-sortIcon": {
      color: theme.palette.common.white,
    },
    "& .MuiDataGrid-iconSeparator": {
      color: theme.palette.common.white,
    },
    "& .MuiDataGrid-menuIconButton": {
      color: theme.palette.common.white,
    },
    "& .MuiDataGrid-checkboxInput": {
      color: theme.palette.common.white,
    },
  },
}));

export default StyledDataGrid;
