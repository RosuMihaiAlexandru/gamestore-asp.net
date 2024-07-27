import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { Context } from "../../../main";
import { GridColDef, GridPaginationModel } from "@mui/x-data-grid";
import { Box, CircularProgress } from "@mui/material";
import StyledDataGrid from "../styles/StyledDataGrid";

const UsersForm = () => {
  const { rootStore } = useContext(Context);
  const { userStore } = rootStore;

  useEffect(() => {
    userStore.getUsers();
  }, [userStore]);

  const handlePaginationModelChange = (
    paginationModel: GridPaginationModel,
  ) => {
    userStore.setPageSize(paginationModel.pageSize);
  };

  if (userStore.isLoading) {
    return <CircularProgress />;
  }

  const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 100 },
    { field: "name", headerName: "Name", width: 150 },
    { field: "email", headerName: "Email", width: 200 },
    {
      field: "refreshTokenExpiryTime",
      headerName: "Token Expiry Time",
      width: 250,
    },
    { field: "role", headerName: "Role", width: 200 },
  ];

  return (
    <Box sx={{ height: userStore.getBoxHeight(), width: "100%" }}>
      <StyledDataGrid
        rows={userStore.users}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: { page: 0, pageSize: 5 },
          },
        }}
        pageSizeOptions={[5, 10, 25]}
        checkboxSelection
        onPaginationModelChange={handlePaginationModelChange}
      />
    </Box>
  );
};

export default observer(UsersForm);
