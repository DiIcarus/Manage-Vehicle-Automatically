import { createStyles, Theme, makeStyles } from "@material-ui/core";

export const useStyle = makeStyles((theme: Theme) =>
  createStyles({
    container: {
      backgroundColor: theme.palette.background.paper,
      border: `0 solid ${theme.palette.divider}`,
    },
    header: {
      color: theme.palette.text.primary,
      fontFamily: theme.typography.fontFamily,
      fontWeight: theme.typography.fontWeightBold,
      fontSize: "1.2rem",
    },
  })
);
