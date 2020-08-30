import React from "react";
import { useSelector, useDispatch } from "react-redux";
import { InitState, RootDispatcher } from "../../store/root-reducer";
import {
  Container,
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  Avatar,
  Icon,
  MenuItem,
  Divider,
  Button,
} from "@material-ui/core";
import { Menu } from "@material-ui/icons";
import { useStyle } from "./style";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";

interface Props {}
interface GlobalStore {
  name: string;
}
const Header: React.FC<Props> = () => {
  let history = useHistory();
  let location = useLocation();
  const classes = useStyle();
  const { name } = useSelector<InitState, GlobalStore>((state: InitState) => {
    return {
      name: state.name,
    };
  });
  const dispath = useDispatch();
  const rootDispatcher = new RootDispatcher(dispath);

  return (
    <React.Fragment>
      <AppBar position="relative" className={classes.app_bar}>
        <Toolbar className={classes.tool_bar}>
          <Button
            color="default"
            variant="outlined"
            style={{
              margin: "0 1rem",
            }}
            onClick={() => {
              history.push("/register-ticket");
            }}
          >
            Đăng ký vé tháng
          </Button>
          <Button
            color="default"
            variant="outlined"
            style={{
              margin: "0 1rem",
            }}
            onClick={() => {
              history.push("/register-vehicle");
            }}
          >
            Đăng ký xe
          </Button>

          <Divider orientation="vertical" flexItem />
          <Button
            color="primary"
            variant="text"
            style={{
              margin: "0 1rem",
            }}
          >
            {name}
          </Button>
          <Divider orientation="vertical" flexItem />
          <Button
            color="secondary"
            variant="text"
            style={{
              margin: "0 1rem",
            }}
            onClick={() => {
              rootDispatcher.updateToken("");
              sessionStorage.setItem("AccessToken", "");
              history.push("/");
            }}
          >
            Đăng xuất
          </Button>
          <Divider orientation="vertical" flexItem />
        </Toolbar>
      </AppBar>
    </React.Fragment>
  );
};
export default Header;

{
  /* <p>{name}</p> redux
<button
onClick={()=>{
  rootDispatcher.updateName("Updated name")
}}
>Button</button> */
}
