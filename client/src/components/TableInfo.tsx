import React from "react";
import {
  Typography,
  Table,
  TableHead,
  TableCell,
  TableRow,
  TableBody,
  Link,
} from "@material-ui/core";
import { makeStyles, Theme, createStyles } from "@material-ui/core";
/*
 * - input : data, function to C_R_UD data
 * - output: view (Read)
 */
function createData(id: number, date: string, name: string, shipTo: string) {
  return { id, date, name, shipTo };
}
const rows = [
  createData(0, "16 Mar, 2019", "Elvis Presley", "Tupelo, MS"),
  createData(1, "16 Mar, 2019", "Paul McCartney", "London, UK"),
  createData(2, "16 Mar, 2019", "Tom Scholz", "Boston, MA"),
  createData(3, "16 Mar, 2019", "Michael Jackson", "Gary, IN"),
  createData(4, "15 Mar, 2019", "Bruce Springsteen", "Long Branch, NJ"),
];

interface Props {
  // data:any,
  // create?:()=>void,
  // del?:()=>void,
  // update?:()=>void,
}
const useStyle = makeStyles((theme: Theme) =>
  createStyles({
    tableStyle: {
      backgroundColor: theme.palette.background.paper,
      color: theme.palette.text.primary,
    },
    header: {
      color: theme.palette.text.primary,
      fontFamily: theme.typography.fontFamily,
      fontWeight: theme.typography.fontWeightBold,
      fontSize: "1.2rem",
    },
    rows_data: {
      color: theme.palette.text.secondary,
    },
  })
);
const TableInfo = (props: Props) => {
  // const {data, create, update, del} = props
  const classes = useStyle();
  const renderFeatureBar = () => {
    return <React.Fragment></React.Fragment>;
  };
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
          </TableRow>
        </TableHead>
        <TableBody className={classes.rows_data}>
          {rows.map((row) => (
            <TableRow key={row.id}>
              <TableCell>{row.date}</TableCell>
              <TableCell>{row.name}</TableCell>
              <TableCell>{row.shipTo}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </React.Fragment>
  );
};

export default TableInfo;
interface rowProps {
  data: any[];
}
const renderRow = (props: rowProps) => {
  const { data } = props;
  return <React.Fragment></React.Fragment>;
};
