import React, { useState, useEffect } from "react";
import Link from "@material-ui/core/Link";
import { makeStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Title from "./Title";
import axios from "axios";
import { XAxis } from "recharts";

// Generate Order Data

const rows: any = [];

function preventDefault(event: any) {
  event.preventDefault();
}

const useStyles = makeStyles((theme) => ({
  seeMore: {
    marginTop: theme.spacing(3),
  },
}));
interface Props {
  user_id: string;
}

export default function Orders(props: Props) {
  const { user_id } = props;
  const classes = useStyles();
  const [arr, setArray] = useState([]);
  useEffect(() => {
    axios
      .get("http://127.0.0.1:5000/user/vehicle", {
        headers: {
          Authorization: "Bearer " + sessionStorage.getItem("AccessToken"),
        },
      })
      .then((res: any) => {
        setArray(res.data.info);
        console.log(res);
      })
      .catch((err: any) => {
        console.log(err);
      });
  }, []);
  return (
    <React.Fragment>
      <Title>Your Vehicles</Title>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell>Gmail</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Phone number</TableCell>
            <TableCell>Ticket Available</TableCell>
            <TableCell>Ticket Counter</TableCell>
            <TableCell align="right">Vehicle Id</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {arr.map((row: any) => {
            if (row.user_id === user_id) {
              return (
                <TableRow key={row.gmail}>
                  <TableCell>{row.gmail}</TableCell>
                  <TableCell>{row.name_owner}</TableCell>
                  <TableCell>{row.phone_number}</TableCell>
                  <TableCell>{+row.ticket_available}</TableCell>
                  <TableCell>{row.ticket_counter}</TableCell>
                  <TableCell align="right">{row.vehicle_id}</TableCell>
                </TableRow>
              );
            }
          })}
        </TableBody>
      </Table>
    </React.Fragment>
  );
}
