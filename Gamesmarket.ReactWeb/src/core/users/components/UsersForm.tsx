import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { Context } from "../../../main";
import { GridColDef, GridPaginationModel } from "@mui/x-data-grid";
import { Box, Grid } from "@mui/material";
import StyledDataGrid from "../styles/StyledDataGrid";
import ChangeRoleForm from "./UI/ChangeRoleForm";
import Snack from "./UI/Snack";
import Load from "./UI/Load";

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
    return <Load />;
  }

  const handleCloseSnack = () => {
    userStore.closeSnack();
  };

  const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 100 },
    { field: "name", headerName: "Name", width: 100 },
    { field: "email", headerName: "Email", width: 200 },
    {
      field: "refreshTokenExpiryTime",
      headerName: "Token Expiry Time",
      width: 250,
    },
    { field: "role", headerName: "Role", width: 125 },
  ];

  return (
    <Grid container spacing={2} sx={{ height: "100%", width: "100%", mt: 2 }}>
      <Grid item xs={9}>
        <Box sx={{ height: "100%", width: "100%" }}>
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
      </Grid>
      <Grid item xs={3}>
        <ChangeRoleForm />
      </Grid>
      <Snack
        isOpen={userStore.snackOpen}
        handleClose={handleCloseSnack}
        message={userStore.snackMessage}
        severity={userStore.snackSeverity}
      />
    </Grid>
  );
};

export default observer(UsersForm);
