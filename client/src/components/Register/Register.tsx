import React, { useState } from "react";
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
import { fetchRegister } from "./../../service/api/register";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";
export default function Register() {
  let history = useHistory();
  let location = useLocation();
  const classes = useStyles();
  const [name, setName] = useState("");
  const [phone_number, setPhoneNumber] = useState("");
  const [dob, setDob] = useState("");
  const [gmail, setGmail] = useState("");
  const [password, setPassword] = useState("");
  const [password_confirm, setPasswordConfirm] = useState("");
  const onSubmit = () => {
    if (password !== password_confirm) {
      alert("Password confirm wrong!!");
      return;
    }
    let formData = new FormData();
    formData.append("gmail", gmail);
    formData.append("phone_number", phone_number);
    formData.append("dob", dob);
    formData.append("name", name);
    formData.append("password", password);
    const pro = fetchRegister(formData);
    pro.then((res: any) => {
      if (res.data.status === 201) {
        history.push("/");
      } else {
        alert(res.data.message);
      }
    });
  };
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <form className={classes.form} noValidate>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                autoComplete="fname"
                name="Name"
                variant="outlined"
                required
                fullWidth
                id="Name"
                label="Name"
                autoFocus
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="email"
                label="Email Address"
                name="email"
                autoComplete="email"
                value={gmail}
                onChange={(e) => setGmail(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="phoneNumber"
                label="Phone Number"
                type="phoneNumber"
                id="phoneNumber"
                autoComplete="current-password"
                value={phone_number}
                onChange={(e) => setPhoneNumber(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="Dob"
                label="Date of Birth"
                type="Dob"
                id="Dob"
                autoComplete="current-password"
                value={dob}
                onChange={(e) => setDob(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="password"
                label="Confirm Password"
                type="password"
                id="password"
                autoComplete="current-password"
                value={password_confirm}
                onChange={(e) => setPasswordConfirm(e.target.value)}
              />
            </Grid>
          </Grid>
          <Button
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
            onClick={onSubmit}
          >
            Sign Up
          </Button>
          <Grid container justify="flex-end">
            <Grid item>
              <Button
                variant="text"
                onClick={() => {
                  history.push("/user");
                }}
              >
                Bạn muốn đăng nhập ?
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}
