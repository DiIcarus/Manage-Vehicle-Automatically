import { createStyles, Theme, makeStyles } from "@material-ui/core";
export const useStyle = makeStyles((theme: Theme) =>
  createStyles({
    text: {
      padding: theme.spacing(2, 2, 0),
    },
    paper: {
      paddingBottom: 50,
    },
    list: {
      marginBottom: theme.spacing(2),
    },
    subheader: {
      backgroundColor: theme.palette.background.paper,
    },
    appBar: {
      top: "auto",
      bottom: 0,
    },
    grow: {
      flexGrow: 1,
    },
    fabButton: {
      position: "absolute",
      zIndex: 1,
      top: -30,
      left: 0,
      right: 0,
      margin: "0 auto",
    },
    app_bar: {
      backgroundColor: theme.palette.background.paper,
      boxShadow: "none",
      border: `0 solid ${theme.palette.divider}`,
      position: "fixed",
      top: 0,
      // height: 50,
    },
    user_info: {
      color: theme.palette.text.primary,
      margin: "0 1rem",
    },
    tool_bar: {
      display: "flex",
      flexDirection: "row",
      justifyContent: "flex-end",
      border: `1px solid ${theme.palette.divider}`,
      color: theme.palette.text.secondary,
    },
    user_content: {
      display: "flex",
    },
    icon_button: {
      margin: "0 1rem",
    },
    menu_icon: {
      color: theme.palette.text.primary,
    },
  })
);
