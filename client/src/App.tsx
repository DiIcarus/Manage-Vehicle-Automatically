import React from 'react';
import './App.css';
import Header from './components/AppBar';
import DrawerFeature from './components/DrawerFeature';
import {CssBaseline} from '@material-ui/core';
const App:React.FC = () => {
  return (
    <React.Fragment>
      <CssBaseline />
      <Header/>
      <DrawerFeature />
    </React.Fragment>
  );
}

export default App;
