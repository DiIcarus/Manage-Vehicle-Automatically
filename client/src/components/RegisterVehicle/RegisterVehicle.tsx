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
import { fetchRegisterVehicle } from "../../service/api/register_vehicle";

interface GlobalStorage {
  name: string;
  token: string;
}
const JWT = require("jwt-client");
export default function RegisterTicket() {
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
  const [vehicle_id, setVehicleId] = useState("");

  const onClick = () => {
    let session = JWT.read(token);
    let user_id = session.claim.identity.id_user;

    let formData = new FormData();
    formData.append("vehicle_id", vehicle_id);
    formData.append("user_ids", user_id);
    const pro = fetchRegisterVehicle(formData, token);
    pro
      .then((res: any) => {
        console.log(res);
        alert(res.data.message);
        history.push("/user");
      })
      .catch((err: any) => {
        alert("Error:" + err.response.data.message);
        console.log(err);
      });
  };
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Register Vehicle
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
