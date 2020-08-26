import React from "react";
import "./App.css";
import Header from "./components/AppBar/AppBar";
import DrawerFeature from "./components/DrawerFeature";
import TableInfo from "./components/TableInfo";
import SignIn from "./components/SignIn/SignIn";
import Register from "./components/Register/Register";
import Chart from "./components/Chart/Chart";
import { CssBaseline, Grid } from "@material-ui/core";
const App: React.FC = () => {
  // const ref: HTMLDivElement = React.createRef<HTMLDivElement>();
  return (
    <React.Fragment>
      <Header />
      <div
        style={{
          paddingTop: 50,
        }}
      >
        <SignIn />
        {/* <Register /> */}
      </div>

      <CssBaseline />
      {/* <Grid container>
        <Grid item xs={12}></Grid>
        <Grid item xs={3}>
          <DrawerFeature />
        </Grid>
        <Grid item xs={9}>
          <TableInfo />
          <Chart />
        </Grid>
      </Grid> */}
    </React.Fragment>
  );
};

export default App;
