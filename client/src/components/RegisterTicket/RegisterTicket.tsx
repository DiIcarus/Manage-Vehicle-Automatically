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
import { fetchRegisterTicket } from "./../../service/api/register_ticket";
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

interface GlobalStorage {
  name: string;
  token: string;
}
export default function RegisterTicket() {
  //
  let history = useHistory();
  let location = useLocation();
  //
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
  //
  const classes = useStyles();
  const [vehicle_id, setVehicleId] = useState("");
  const [duration, setDuration] = useState("");
  const onClick = () => {
    let formData = new FormData();
    formData.append("vehicle_id", vehicle_id);
    formData.append("duration", duration);
    const pro = fetchRegisterTicket(formData, token);
    pro.then((res: any) => {
      alert(res.data.message);
    });
  };
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Register ticket
        </Typography>
        <form className={classes.form} noValidate>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="Vehicle Id"
                label="Vehicle Id"
                name="vehicle id"
                autoComplete="vehicle_id"
                value={vehicle_id}
                onChange={(e) => setVehicleId(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="duration"
                label="Duration"
                id="duration"
                value={duration}
                onChange={(e) => setDuration(e.target.value)}
              />
            </Grid>
          </Grid>
          <Button
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
            onClick={onClick}
          >
            Submit
          </Button>
          <Grid container justify="flex-end">
            <Grid item>
              <Button
                variant="text"
                onClick={() => {
                  history.push("/user");
                }}
              >
                Trở về
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}
