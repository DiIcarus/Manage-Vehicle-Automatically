import React, { useState, useEffect } from "react";
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
import { InitState, RootDispatcher } from "../../store/root-reducer";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
  useParams,
} from "react-router-dom";
import { fetchSendCode } from "../../service/api/send_code";
interface GlobalStorage {
  name: string;
  token: string;
}
const JWT = require("jwt-client");
export default function AcceptCode() {
  //
  let history = useHistory();
  let location = useLocation();
  let { id } = useParams();
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
  const [text, setText] = useState("");
  useEffect(() => {
    let a: string = id;
    const send_code = a.split("&&")[0];
    const vehicle_id = a.split("&&")[1];
    let formData = new FormData();
    formData.append("vehicle_id", vehicle_id);
    formData.append("send_code", send_code);
    const pro = fetchSendCode(formData);
    pro
      .then((res: any) => {
        console.log(res);
        setText("Cảm ơn bạn đã xác nhận khóa !!");
      })
      .catch((err: any) => {
        console.log(err);
        setText("Quá trình gửi mã xác nhận có vấn đề, mời bạn thử lại");
      });
  });
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          {text}
        </Typography>
        <Button
          variant="outlined"
          onClick={() => {
            history.push("/");
          }}
          style={{
            margin: "1rem",
          }}
        >
          Ghé thăm chúng tôi
        </Button>
      </div>
    </Container>
  );
}
