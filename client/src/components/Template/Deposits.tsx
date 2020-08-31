import React from "react";
import Link from "@material-ui/core/Link";
import { makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import Title from "./Title";

function preventDefault(event: any) {
  event.preventDefault();
}

const useStyles = makeStyles({
  depositContext: {
    flex: 1,
  },
});
interface Props {
  title: string;
  main_text: string;
  sub_text: string[];
}

export default function Deposits(props: Props) {
  const { title, main_text, sub_text } = props;
  const classes = useStyles();
  return (
    <React.Fragment>
      <Title>{title}</Title>
      <Typography
        component="p"
        variant="h4"
        style={{
          margin: "1rem 0",
        }}
      >
        {main_text}
      </Typography>
      {sub_text.map((value: any) => (
        <Typography
          color="textSecondary"
          // className={classes.depositContext}
          
        >
          {value}
        </Typography>
      ))}
    </React.Fragment>
  );
}
