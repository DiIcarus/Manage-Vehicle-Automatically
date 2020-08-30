import React, { useState } from "react";
import {
  Avatar,
  Button,
  CssBaseline,
  TextField,
  FormControlLabel,
  Checkbox,
  Link,
  Grid,
  Box,
  Typography,
  Container,
} from "@material-ui/core";
import { LockOutlined } from "@material-ui/icons";
import { useStyles } from "./style";
import { fetchSendCode } from "../../service/api/send_code";
export default function SendCodePage() {
  const classes = useStyles();
  const [vehicle_id, setVehicleId] = useState("");
  const [send_code, setSendCode] = useState("");
  const onClick = () => {
    let formData = new FormData();
    formData.append("vehicle_id", vehicle_id);
    formData.append("send_code", send_code);
    const pro = fetchSendCode(formData);
    pro
      .then((res: any) => {
        console.log(res);
      })
      .catch((err: any) => {
        console.log(err);
      });
  };
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Send Code Page
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
                name="send_code"
                label="Send Code"
                id="send code"
                autoComplete="send code"
                value={send_code}
                onChange={(e) => setSendCode(e.target.value)}
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
              <Link href="#" variant="body2">
                Already have an account? Sign in
              </Link>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}
