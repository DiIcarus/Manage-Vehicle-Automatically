import React, { useState, ChangeEvent, useEffect } from "react";
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
import { LockOutlined } from "@material-ui/icons";
import { useStyles } from "./style";
import { fetchSignIn } from "./../../service/api/sign_in";
import { useDispatch, useSelector } from "react-redux";
import { InitState, RootDispatcher } from "./../../store/root-reducer";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";
import User from "./../User/User";

const JWT = require("jwt-client");
interface GlobalStorage {
  name: string;
  token: string;
}
export default function SignIn() {
  //
  let history = useHistory();
  let location = useLocation();
  //state
  const { name, token } = useSelector<InitState, GlobalStorage>(
    (state: InitState) => {
      return {
        name: state.name,
        token: state.token,
      };
    }
  );
  const dispatch = useDispatch();
  const rootDispatcher = new RootDispatcher(dispatch);
  // rootDispatcher.updateName()
  //
  const classes = useStyles();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const fetchApi = async () => {
    let formData: FormData = new FormData();
    formData.append("gmail", email.trim());
    formData.append("password", password.trim());
    const result = await fetchSignIn(formData);
    console.log(result);
    rootDispatcher.updateToken("Bearer " + result.data.access_token);
    if (result.data.access_token) {
      sessionStorage.setItem("AccessToken", result.data.access_token);
      let session = JWT.read(result.data.access_token);
      console.log(session.claim.identity);
      rootDispatcher.updateName(session.claim.identity.user_name);
      history.replace("/user");
    }
  };

  useEffect(() => {});
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography
          component="h1"
          variant="h5"
          style={{
            textAlign: "center",
            width: "800px",
            margin: "1rem",
            fontWeight: "bold",
          }}
        >
          {"QUẢN LÝ NHÀ XE (Web user quản lý thông tin)"}
        </Typography>
        <Typography component="h1" variant="h5">
          {"Đăng nhập"}
        </Typography>
        <form className={classes.form} noValidate>
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email Address"
            name="email"
            autoComplete="email"
            defaultValue={email}
            onChange={(e: ChangeEvent<HTMLInputElement>) => {
              setEmail(e.currentTarget.value);
            }}
            autoFocus
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            defaultValue={password}
            onChange={(e: ChangeEvent<HTMLInputElement>) => {
              setPassword(e.currentTarget.value);
            }}
            autoComplete="current-password"
          />
          <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
            label="Remember me"
          />
          <Button
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
            onClick={fetchApi}
          >
            Sign In
          </Button>
          <Grid container>
            <Grid item xs>
              <Button
                variant="text"
                onClick={() => {
                  history.push("/forgot-pasword");
                }}
              >
                Bạn quên mật khẩu ?
              </Button>
            </Grid>
            <Grid item>
              <Button
                variant="text"
                onClick={() => {
                  history.push("/register");
                }}
              >
                Bạn muốn đăng ký ?
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}
