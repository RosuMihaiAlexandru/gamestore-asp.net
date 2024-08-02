import { styled } from "@mui/material/styles";
import { DataGrid, gridClasses } from "@mui/x-data-grid";

const StyledDataGrid = styled(DataGrid)(({ theme }) => ({
  [`& .${gridClasses.row}`]: {
    "&:nth-of-type(odd)": {
      backgroundColor: theme.palette.action.hover,
    },
  },
  [`& .${gridClasses.columnHeader}`]: {
    backgroundColor: "#1c1b22", // Darker background for headers
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
  "& .MuiDataGrid-cell": {
    color: "#ffffff", // White text color for cells
  },
  "& .MuiButtonBase-root": {
    color: "#ffffff", // White checkbox color
  },
  "& .MuiDataGrid-row:hover": {
    backgroundColor: "#2c2f33", // Slightly darker row on hover
  },
  "& .MuiDataGrid-footerContainer": {
    backgroundColor: "#ccc", // Darker background for footer
    color: theme.palette.common.white,
  },
}));

export default StyledDataGrid;
