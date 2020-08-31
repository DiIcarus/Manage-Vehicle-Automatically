import React, { useState } from "react";
import clsx from "clsx";
import { makeStyles } from "@material-ui/core/styles";
import CssBaseline from "@material-ui/core/CssBaseline";
import Drawer from "@material-ui/core/Drawer";
import Box from "@material-ui/core/Box";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import List from "@material-ui/core/List";
import Typography from "@material-ui/core/Typography";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import Badge from "@material-ui/core/Badge";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Paper from "@material-ui/core/Paper";
import MenuIcon from "@material-ui/icons/Menu";
import CloseIcon from "@material-ui/icons/Close";
import NotificationsIcon from "@material-ui/icons/Notifications";
import { mainListItems, secondaryListItems } from "./listItem";
import Chart from "./Chart";
import Deposits from "./Deposits";
import Orders from "./Orders";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import { InitState, RootDispatcher } from "../../store/root-reducer";
import TableInfo from "./../TableInfo";
import SignIn from "./../SignIn/SignIn";
import Register from "./../Register/Register";
import RegisterTicket from "./../RegisterTicket/RegisterTicket";
import RegisterVehicle from "./../RegisterVehicle/RegisterVehicle";
import SendCodePage from "./../SendCodePage/SendCodePage";
const JWT = require("jwt-client");
function PrivateRoute({ children, ...rest }: any) {
  return (
    <Route
      {...rest}
      render={({ location }: any) =>
        true ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/users",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
}
const renderRouter = () => (
  <Switch>
    <Route exact path="/">
      <SignIn />
    </Route>
    <Route path="users/register">
      <Register />
    </Route>
    <Route path="users/send-code">
      <SendCodePage />
    </Route>
    <Route path="users/users">{/* <User /> */}</Route>
    <Route path="users/register-vehicle">
      <RegisterVehicle />
    </Route>
    <PrivateRoute path="users/register-ticket">
      <RegisterTicket />
    </PrivateRoute>
    <PrivateRoute path="users/user">{/* <User /> */}</PrivateRoute>
  </Switch>
);

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  toolbar: {
    paddingRight: 24, // keep right padding when drawer closed
  },
  toolbarIcon: {
    display: "flex",
    alignItems: "center",
    justifyContent: "flex-end",
    padding: "0 8px",
    ...theme.mixins.toolbar,
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  menuButton: {
    marginRight: 36,
  },
  menuButtonHidden: {
    display: "none",
  },
  title: {
    flexGrow: 1,
  },
  drawerPaper: {
    position: "relative",
    whiteSpace: "nowrap",
    width: drawerWidth,
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerPaperClose: {
    overflowX: "hidden",
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    width: theme.spacing(7),
    [theme.breakpoints.up("sm")]: {
      width: theme.spacing(9),
    },
  },
  appBarSpacer: theme.mixins.toolbar,
  content: {
    flexGrow: 1,
    height: "100vh",
    overflow: "auto",
  },
  container: {
    paddingTop: theme.spacing(4),
    paddingBottom: theme.spacing(4),
  },
  paper: {
    padding: theme.spacing(2),
    display: "flex",
    overflow: "auto",
    flexDirection: "column",
  },
  fixedHeight: {
    height: 240,
  },
}));
interface GlobalStore {
  name: string;
}
interface TokenInfo {
  dob: number;
  gmail: string;
  id_owner: string;
  id_user: string;
  is_admin: boolean;
  password: string;
  phone_number: string;
  private_code: string;
  public_code: string;
  vehicle_ids: string[];
}
function timeConverter(UNIX_timestamp: number) {
  var a = new Date(UNIX_timestamp * 1000);
  var months = [
    "Jan",
    "Feb",
    "Mar",
    "Apr",
    "May",
    "Jun",
    "Jul",
    "Aug",
    "Sep",
    "Oct",
    "Nov",
    "Dec",
  ];
  var year = a.getFullYear();
  var month = months[a.getMonth()];
  var date = a.getDate();
  var hour = a.getHours();
  var min = a.getMinutes();
  var sec = a.getSeconds();
  var time = date + " " + month + " " + year;
  return time;
}
export default function Dashboard() {
  let history = useHistory();
  const dispath = useDispatch();
  const rootDispatcher = new RootDispatcher(dispath);
  const { name } = useSelector<InitState, GlobalStore>((state: InitState) => {
    return {
      name: state.name,
    };
  });

  //
  //
  const classes = useStyles();
  const [open, setOpen] = React.useState(true);

  const [dob, setDoB] = React.useState("");
  const [gmail, setGmail] = React.useState("");
  const [id_owner, setIdOwner] = React.useState("");
  const [id_user, setIdUser] = React.useState("");
  const [id_admin, setIdAdmin] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [phone_number, setPhonenumber] = React.useState("");
  const [private_code, setPrivateCode] = React.useState("");
  const [public_code, setPublicCode] = React.useState("");
  const [user_namer, setUserName] = React.useState("");
  const [vehicle_ids, setVehicleId] = React.useState([]);

  const handleDrawerOpen = () => {
    setOpen(true);
  };
  const handleDrawerClose = () => {
    setOpen(false);
  };
  const fixedHeightPaper = clsx(classes.paper, classes.fixedHeight);
  React.useEffect(() => {
    let session = JWT.read(sessionStorage.getItem("AccessToken"));
    console.log(session.claim.identity);
    setDoB(session.claim.identity.dob);
    setGmail(session.claim.identity.gmail);
    setIdOwner(session.claim.identity.id_owner);
    setIdUser(session.claim.identity.id_user);
    setIdAdmin(session.claim.identity.id_admin);
    setPassword(session.claim.identity.password);
    setPhonenumber(session.claim.identity.phone_number);
    setPrivateCode(session.claim.identity.private_code);
    setPublicCode(session.claim.identity.public_code);
    setUserName(session.claim.identity.user_name);
    setVehicleId(session.claim.identity.vehicle_ids);
  }, []);

  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar
        position="absolute"
        className={clsx(classes.appBar, open && classes.appBarShift)}
      >
        <Toolbar className={classes.toolbar}>
          <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            onClick={handleDrawerOpen}
            className={clsx(
              classes.menuButton,
              open && classes.menuButtonHidden
            )}
          >
            <MenuIcon />
          </IconButton>
          <Typography
            component="h1"
            variant="h6"
            color="inherit"
            noWrap
            className={classes.title}
          >
            {"WEB FOR USER MANAGE THEIR INFO"}
          </Typography>
        </Toolbar>
      </AppBar>
      <Drawer
        variant="permanent"
        classes={{
          paper: clsx(classes.drawerPaper, !open && classes.drawerPaperClose),
        }}
        open={open}
      >
        <div className={classes.toolbarIcon}>
          <IconButton onClick={handleDrawerClose}>
            <CloseIcon />
          </IconButton>
        </div>
        <Divider />
        <List>
          {mainListItems(
            () => {
              history.push("/register-ticket");
            },
            () => {
              history.push("/register-vehicle");
            },
            () => {
              history.push("/user");
            }
          )}
        </List>
        <Divider />
        <List>
          {secondaryListItems(
            () => {
              history.push("/register-vehicle");
            },
            () => {
              rootDispatcher.updateToken("");
              sessionStorage.setItem("AccessToken", "");
              history.push("/");
            }
          )}
        </List>
      </Drawer>
      <main className={classes.content}>
        <div className={classes.appBarSpacer} />
        <Container maxWidth="lg" className={classes.container}>
          <Grid container spacing={3}>
            <Grid item xs={12} md={8} lg={6}>
              <Paper className={fixedHeightPaper}>
                <Deposits
                  title={"USER INFO:"}
                  sub_text={[
                    "Gmail: " + gmail,
                    "Phone number: " + phone_number,
                    "Date of birth: " + timeConverter(+dob),
                  ]}
                  main_text={name}
                />
              </Paper>
            </Grid>
            <Grid item xs={12} md={4} lg={3}>
              <Paper className={fixedHeightPaper}>
                <Deposits
                  title={"PRIVATE KEY"}
                  main_text={private_code}
                  sub_text={[""]}
                />
              </Paper>
            </Grid>
            <Grid item xs={12} md={4} lg={3}>
              <Paper className={fixedHeightPaper}>
                <Deposits
                  title={"PUBLIC KEY"}
                  main_text={public_code}
                  sub_text={[""]}
                />
              </Paper>
            </Grid>
            <Grid item xs={12}>
              <Paper className={classes.paper}>
                <Orders user_id={id_user} />
              </Paper>
            </Grid>
          </Grid>
        </Container>
      </main>
    </div>
  );
}
