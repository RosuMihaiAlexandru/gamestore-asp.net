export const account_menu = {
  paper: {
    overflow: "visible",
    mt: 1.5,
    "& .MuiAvatar-root": {
      width: 30,
      height: 30,
      ml: -0.5,
      mr: 1,
    },
    "&::before": {
      content: '""',
      display: "block",
      position: "absolute",
      top: 0,
      right: 14,
      width: 10,
      height: 10,
      bgcolor: "background.paper",
      transform: "translateY(-50%) rotate(45deg)",
      zIndex: 0,
    },
  },
};
