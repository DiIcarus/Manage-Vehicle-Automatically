import React from "react";
import {
  useTheme,
  Box,
  Typography,
  makeStyles,
  createStyles,
  Theme,
} from "@material-ui/core";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Label,
  ResponsiveContainer,
} from "recharts";
import { useStyle } from "./style";
// Generate Sales Data
function createData(time: string, amount: number) {
  return { time: time, amount: amount };
}

const data = [
  createData("00:00", 0),
  createData("03:00", 300),
  createData("06:00", 600),
  createData("09:00", 800),
  createData("12:00", 1500),
  createData("15:00", 2000),
  createData("18:00", 2400),
  createData("21:00", 2400),
  createData("24:00", 2550),
];

export default function Chart() {
  const theme = useTheme();
  const classes: any = useStyle();

  return (
    <React.Fragment>
      <Box className={classes.container}>
        <Typography className={classes.header}>Header</Typography>
        <ResponsiveContainer width={700} height={250}>
          <LineChart
            data={data}
            margin={{
              top: 16,
              right: 16,
              bottom: 0,
              left: 24,
            }}
          >
            <XAxis dataKey="time" stroke={theme.palette.text.secondary} />
            <YAxis stroke={theme.palette.text.secondary}>
              <Label
                angle={270}
                position="left"
                style={{
                  textAnchor: "middle",
                  fill: theme.palette.text.primary,
                }}
              >
                Sales ($)
              </Label>
            </YAxis>
            <Line
              type="monotone"
              dataKey="amount"
              stroke={theme.palette.primary.main}
              dot={true}
            />
          </LineChart>
        </ResponsiveContainer>
      </Box>
    </React.Fragment>
  );
}
