import React from 'react';
import './App.css';
import Header from './components/AppBar';
import DrawerFeature from './components/DrawerFeature';
import TableInfo from './components/TableInfo';
import SignIn from './components/SignIn';
import Register from './components/Register';
import Chart from './components/Chart';
import {CssBaseline, Grid} from '@material-ui/core';
const App:React.FC = () => {
  return (
    <React.Fragment>
      {/* <div> */}
        {/* <SignIn/> */}
        {/* <Register/>
      </div> */}
      
      <CssBaseline />
      <Header/>
      <Grid>
        {/* <DrawerFeature /> */}
        <TableInfo/>
        <Chart/>
      </Grid>
    </React.Fragment>
  );
}

export default App;
