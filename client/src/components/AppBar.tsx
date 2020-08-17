import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { InitState, RootDispatcher } from '../store/root-reducer'
import { Container, AppBar, Toolbar, IconButton, Typography, Avatar, Icon, MenuItem} from '@material-ui/core'
import { createStyles, Theme, makeStyles} from '@material-ui/core'
import { Menu } from '@material-ui/icons'

const useStyle = makeStyles((theme:Theme)=>createStyles({
  text: {
    padding: theme.spacing(2, 2, 0),
  },
  paper: {
    paddingBottom: 50,
  },
  list: {
    marginBottom: theme.spacing(2),
  },
  subheader: {
    backgroundColor: theme.palette.background.paper,
  },
  appBar: {
    top: 'auto',
    bottom: 0,
  },
  grow: {
    flexGrow: 1,
  },
  fabButton: {
    position: 'absolute',
    zIndex: 1,
    top: -30,
    left: 0,
    right: 0,
    margin: '0 auto',
  },
}))
interface Props{
}
interface GlobalStore{
  name:string
}
const Header:React.FC<Props> = () => {
  const {name} = useSelector<InitState, GlobalStore>((state:InitState)=>{
    return{
      name: state.name
    }
  })
  const dispath = useDispatch();
  const rootDispatcher = new RootDispatcher(dispath);

  return(
    <React.Fragment>
      <AppBar
      position='relative'
      >
        <Toolbar>
          <IconButton>
            <Menu 
            />
          </IconButton>
          <Typography>
          </Typography>
          <div>
            <IconButton>
              <Avatar alt='img user' src='https://lh3.googleusercontent.com/fife/ABSRlIqsFQXZGkvBL3Y2KTaZAc10MOSXxsP4RB0JsnIiK5dkQix6-ziORYwN8MPDSKfqxyP6TaKuzovXQynszcjgPvfMJL07XXpvgD-k1Ad1RiVnvIqh4h0VvqXv8lZ49uSBaWIc-sZyORWGGokD7mfK51y8RWThSrE4sf2ZVAPcZSJrtngJKGyP4XALI2mT3Jxn-V1ckcD4nV4OfAz6ZP5REar9d13AN_4-jY_PsXlzUII_kf6Z28jC-oCqYOKKivjKYwpkYo1POaitQEH6RpPMdawPbI00Z5zfyx0fQtKLePyzn4ePuJMTc2h4uihPzBlXUetTCJ1UdQDaBsxdn34Xm6NB56Vgi1mFUkoPPgrcVpTAzf6e0aogipIh6FYEl1XJz92NJHgsyS5cqdA3JAT2CYM1vR5Jix21r2o-QSSXlSl7fRkj8Ll8AFJbByfbTCNyylI4V5jX_8e1r0XLYW-8ny5imwGQLO7yS8fbCuYfvhRAT0ivAkzFOP_dSYofT23gRMyklFsTOKKtrqDbqnyC9QDCt7x1c5ovEENxB7KuGbYill14PhG3B5WDo8pzd5Pnv1dtmmnF1XUB_u9L6XKjWbka5xeT0yVi24q_e8ArAl0ywV6ZCrNsjvRgVg-q2RSPen5tvVuSIONaq8mrXOBg4PuQ2rK205QmvLk4wrJTdmkVTkDubWDV9KKgjjRKhQvvWxN1U3f178dRJOdwEaYQQe7Gm-KBahOwF2KQmd5YqAwuYA=s32-c'/>
            </IconButton>
          </div>
        </Toolbar>
      </AppBar>
    </React.Fragment>
  )
}
export default Header;

{/* <p>{name}</p>
<button
onClick={()=>{
  rootDispatcher.updateName("Updated name")
}}
>Button</button> */}