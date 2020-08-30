import React, { useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import "./App.css";
import Header from "./components/AppBar/AppBar";
import DrawerFeature from "./components/DrawerFeature";
import TableInfo from "./components/TableInfo";
import SignIn from "./components/SignIn/SignIn";
import Register from "./components/Register/Register";
import Chart from "./components/Chart/Chart";
import RegisterTicket from "./components/RegisterTicket/RegisterTicket";
import RegisterVehicle from "./components/RegisterVehicle/RegisterVehicle";
import SendCodePage from "./components/SendCodePage/SendCodePage";
import AcceptCode from "./components/AcceptCode/AcceptCode";
import User from "./components/User/User";
import { CssBaseline, Grid, Typography } from "@material-ui/core";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { InitState, RootDispatcher } from "./store/root-reducer";
interface GlobalStorage {
  name: string;
  token: string;
}
const App: React.FC = () => {
  const { name, token } = useSelector<InitState, GlobalStorage>(
    (state: InitState) => {
      return {
        name: state.name,
        token: state.token,
      };
    }
  );
  function PrivateRoute({ children, ...rest }: any) {
    return (
      <Route
        {...rest}
        render={({ location }: any) =>
          token !== "" ? (
            children
          ) : (
            <Redirect
              to={{
                pathname: "/",
                state: { from: location },
              }}
            />
          )
        }
      />
    );
  }
  // const ref: HTMLDivElement = React.createRef<HTMLDivElement>();
  useEffect(() => {
    // sessionStorage.setItem("Token", "asdfghjkl");
    return () => {};
  }, []);
  const renderRouter = () => (
    <Switch>
      <Route exact path="/">
        <SignIn />
      </Route>
      <Route path="/info/:id">
        <AcceptCode />
      </Route>
      <Route path="/register">
        <Register />
      </Route>
      <PrivateRoute path="/send-code">
        <SendCodePage />
      </PrivateRoute>
      <PrivateRoute path="/register-vehicle">
        <RegisterVehicle />
      </PrivateRoute>
      <PrivateRoute path="/register-ticket">
        <RegisterTicket />
      </PrivateRoute>
      <PrivateRoute path="/user">
        <User />
      </PrivateRoute>
    </Switch>
  );
  return (
    <React.Fragment>
      <Router>
        <div
          style={{
            paddingTop: 50,
          }}
        >
          {renderRouter()}
        </div>
        <CssBaseline />
      </Router>
    </React.Fragment>
  );
};

export default App;
