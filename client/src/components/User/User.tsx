import React, { useState, ChangeEvent } from "react";
import {
  Avatar,
  Button,
  CssBaseline,
  TextField,
  FormControlLabel,
  Checkbox,
  Grid,
  Box,
  Typography,
  Container,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { LockOutlined } from "@material-ui/icons";
import { fetchSignIn } from "../../service/api/sign_in";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";
import Chart from "./../Chart/Chart";
import Header from "./../AppBar/AppBar";
import DrawerFeature from "./../DrawerFeature";
import TableInfo from "./../TableInfo";
import SignIn from "./../SignIn/SignIn";
import Register from "./../Register/Register";
import RegisterTicket from "./../RegisterTicket/RegisterTicket";
import RegisterVehicle from "./../RegisterVehicle/RegisterVehicle";
import SendCodePage from "./../SendCodePage/SendCodePage";
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
    <Route path="users/users">
      <User />
    </Route>
    <Route path="users/register-vehicle">
      <RegisterVehicle />
    </Route>
    <PrivateRoute path="users/register-ticket">
      <RegisterTicket />
    </PrivateRoute>
    <PrivateRoute path="users/user">
      <User />
    </PrivateRoute>
  </Switch>
);
export default function User() {
  return (
    <React.Fragment>
      {renderRouter()}
      <Grid
        container
        spacing={3}
        style={{
          padding: "1rem",
        }}
      >
        <Grid item xs={12}>
          <Header />
        </Grid>
        <Grid item xs={12} sm={3}>
          {UserInfo("as", "asd")}
        </Grid>
        <Grid item xs={12} sm={3}>
          <Container
            style={{
              width: "auto",
              margin: "1rem",
              borderColor: "black",
              borderWidth: "1px",
              justifyContent: "center",
              alignItems: "center",
              borderRadius: "5px",
              padding: "1rem",
            }}
          >
            <Typography
              style={{
                fontWeight: "bold",
                fontSize: "32",
                textAlign: "center",
              }}
            >
              Số liệu đáng chú ý
            </Typography>
            {Deposits("aa", "aaa")}
            {Deposits("aa", "aaa")}
            {Deposits("aa", "aaa")}
          </Container>
          {/* <Chart /> */}
        </Grid>
        <Grid item xs={12} sm={6}>
          <TableInfo />
        </Grid>
      </Grid>
    </React.Fragment>
  );
}
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

function preventDefault(event: any) {
  event.preventDefault();
}

const useStyles = makeStyles({
  depositContext: {
    flex: 1,
  },
});
const Deposits = (main_text: string, sub_text: string) => {
  const classes = useStyles();
  return (
    <div
      style={{
        width: "auto",
        margin: "1rem",
        backgroundColor: "white",
        borderColor: "black",
        borderWidth: "2px",
        justifyContent: "center",
        alignItems: "center",
        padding: "1rem",
      }}
    >
      <Typography>Recent Deposits</Typography>
      <Typography component="p" variant="h4">
        {main_text}
      </Typography>
      <Typography color="textSecondary" className={classes.depositContext}>
        {sub_text}
      </Typography>
    </div>
  );
};

const UserInfo = (main_text: string, sub_text: string) => {
  const classes = useStyles();
  return (
    <div
      style={{
        width: "auto",
        margin: "1rem",
        backgroundColor: "white",
        borderColor: "black",
        borderWidth: "2px",
        justifyContent: "center",
        alignItems: "center",
        padding: "1rem",
      }}
    >
      <Typography>Recent Deposits</Typography>
      <Typography component="p" variant="h4">
        {main_text}
      </Typography>
      <Typography color="textSecondary" className={classes.depositContext}>
        {sub_text}
      </Typography>
    </div>
  );
};
