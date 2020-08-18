import React from 'react'
import { Typography, Table, TableHead, TableCell, TableRow, TableBody, Link, } from '@material-ui/core'
import { makeStyles, Theme, createStyles } from '@material-ui/core'
/*
* - input : data, function to C_R_UD data
* - output: view (Read)
*/
function createData(id:number, date:string, name:string, shipTo:string, paymentMethod:string , amount:number) {
  return { id, date, name, shipTo, paymentMethod, amount };
}
const rows = [
  createData(0, '16 Mar, 2019', 'Elvis Presley', 'Tupelo, MS', 'VISA ⠀•••• 3719', 312.44),
  createData(1, '16 Mar, 2019', 'Paul McCartney', 'London, UK', 'VISA ⠀•••• 2574', 866.99),
  createData(2, '16 Mar, 2019', 'Tom Scholz', 'Boston, MA', 'MC ⠀•••• 1253', 100.81),
  createData(3, '16 Mar, 2019', 'Michael Jackson', 'Gary, IN', 'AMEX ⠀•••• 2000', 654.39),
  createData(4, '15 Mar, 2019', 'Bruce Springsteen', 'Long Branch, NJ', 'VISA ⠀•••• 5919', 212.79),
];

interface Props{
  // data:any,
  // create?:()=>void,
  // del?:()=>void,
  // update?:()=>void,
}
const useStyle = makeStyles((theme:Theme)=>createStyles({
  tableStyle:{
    backgroundColor: theme.palette.background.paper,
    color:theme.palette.text.primary
  },
  header:{
    color:theme.palette.text.primary,
    fontFamily: theme.typography.fontFamily,
    fontWeight: theme.typography.fontWeightBold,
    fontSize: '1.2rem'
  },
  rows_data:{
    color:theme.palette.text.secondary
  }
}))
const TableInfo = (props:Props) => {
  // const {data, create, update, del} = props
  const classes = useStyle()
  const renderFeatureBar=()=>{
    return(
      <React.Fragment>
      
      </React.Fragment>
    )
  }
  function preventDefault() {
    // event.preventDefault();
  }
  return (
    <React.Fragment>
      <Table size="small" className={classes.tableStyle}>
        <TableHead>
          <TableRow>
            <Typography className={classes.header}>Table Name</Typography>
          </TableRow>
          <TableRow>
            <TableCell>Date</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Ship To</TableCell>
            <TableCell>Payment Method</TableCell>
            <TableCell align="right">Sale Amount</TableCell>
          </TableRow>
        </TableHead>
        <TableBody className={classes.rows_data}>
          {rows.map((row) => (
            <TableRow key={row.id}>
              <TableCell>{row.date}</TableCell>
              <TableCell>{row.name}</TableCell>
              <TableCell>{row.shipTo}</TableCell>
              <TableCell>{row.paymentMethod}</TableCell>
              <TableCell align="right">{row.amount}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </React.Fragment>
  )
}

export default TableInfo
interface rowProps{
  data:any[]
}
const renderRow=(props:rowProps)=>{
  const {data} = props
  return (
    <React.Fragment>
    </React.Fragment>
  )

}

