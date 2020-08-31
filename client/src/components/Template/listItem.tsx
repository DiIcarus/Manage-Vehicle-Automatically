import React from "react";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import ListSubheader from "@material-ui/core/ListSubheader";
import DashboardIcon from "@material-ui/icons/Dashboard";
import ShoppingCartIcon from "@material-ui/icons/ShoppingCart";
import PeopleIcon from "@material-ui/icons/People";
import BarChartIcon from "@material-ui/icons/BarChart";
import LayersIcon from "@material-ui/icons/Layers";
import AssignmentIcon from "@material-ui/icons/Assignment";
import PersonOutlineIcon from "@material-ui/icons/PersonOutline";
import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import ConfirmationNumberIcon from "@material-ui/icons/ConfirmationNumber";
import CommuteIcon from "@material-ui/icons/Commute";

export const mainListItems = (func: any, func1: any, func2: any) => (
  <div>
    <ListItem button onClick={func}>
      <ListItemIcon>
        <ConfirmationNumberIcon />
      </ListItemIcon>
      <ListItemText primary="Register Ticket" />
    </ListItem>
    <ListItem button onClick={func1}>
      <ListItemIcon>
        <CommuteIcon />
      </ListItemIcon>
      <ListItemText primary="Register Vehicle" />
    </ListItem>
    <ListItem button onClick={func2}>
      <ListItemIcon>
        <BarChartIcon />
      </ListItemIcon>
      <ListItemText primary="Reports" />
    </ListItem>
  </div>
);

export const secondaryListItems = (func0: any, func: any) => (
  <div>
    <ListSubheader inset>User Manager</ListSubheader>
    <ListItem button onClick={func0}>
      <ListItemIcon>
        <PersonOutlineIcon />
      </ListItemIcon>
      <ListItemText primary="Info User" />
    </ListItem>
    <ListItem button onClick={func}>
      <ListItemIcon>
        <ExitToAppIcon />
      </ListItemIcon>
      <ListItemText primary="Sign Out" />
    </ListItem>
  </div>
);
