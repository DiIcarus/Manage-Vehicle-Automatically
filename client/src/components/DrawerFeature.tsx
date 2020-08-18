import React from 'react'
import { makeStyles, useTheme, Theme, createStyles } from '@material-ui/core/styles';
import { Hidden, Drawer , Divider, List, ListItem, ListItemIcon, ListItemText} from '@material-ui/core'
import { Inbox, Mail} from '@material-ui/icons'

const drawerWidth = 240;

const useStyle = makeStyles((theme:Theme)=>createStyles({
  toolbar:theme.mixins.toolbar,
  drawer:{
    marginRight:theme.spacing(2),
    [theme.breakpoints.up('sm')]:{
      width:drawerWidth,
      flexShrink:0
    }
  },
  drawerPaper:{
    width: drawerWidth
  }
}))
interface Props{
  window?:()=> Window;
}
const DrawerFeature=(props:Props)=>{
  const {window} = props
  const classes = useStyle()
  const theme = useTheme();
  const [mobileOpen, setMobileOpen]=React.useState(false)
  const handleDrawerToggle = () =>{
    setMobileOpen(!mobileOpen);
  }
  
  const drawer = (
    <div>
      {/* <div className={classes.toolbar} /> */}
      <Divider />
      <List>
        {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
          <ListItem button key={text}>
            <ListItemIcon>{index % 2 === 0 ? <Inbox /> : <Mail />}</ListItemIcon>
            <ListItemText primary={text} />
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        {['All mail', 'Trash', 'Spam'].map((text, index) => (
          <ListItem button key={text}>
            <ListItemIcon>{index % 2 === 0 ? <Inbox /> : <Mail />}</ListItemIcon>
            <ListItemText primary={text} />
          </ListItem>
        ))}
      </List>
    </div>
  )
  const container = window !== undefined ? () => window().document.body : undefined;
  return (
    <React.Fragment>
      <nav
      className={classes.drawer}
      aria-label="mailbox folders"
      >
        <Hidden smUp implementation="css">
          <Drawer
          container={document.body}
          variant="temporary"
          anchor={theme.direction==='rtl'? 'right':'left'}
          open={mobileOpen}
          onClose={handleDrawerToggle}
          classes={{
            paper:classes.drawerPaper
          }}
          ModalProps={{
            keepMounted: true
          }}
          >
            {drawer}
          </Drawer>
        </Hidden>
        <Hidden xsDown implementation="css">
          <Drawer
            classes={{
              paper: classes.drawerPaper,
            }}
            variant="permanent"
            open
          >
            {drawer}
          </Drawer>
        </Hidden>
      </nav>
    </React.Fragment>
  )
}
export default DrawerFeature
