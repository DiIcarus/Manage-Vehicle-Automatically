import React from "react";
import "./App.css";
import "axios";
import Axios, { AxiosResponse, AxiosError } from "axios";
function App() {
  // Axios.get('http://127.0.0.1:5000/')
  // .then((res:AxiosResponse)=>{
  //   console.log(res.data.Hello)
  // })
  // .catch((err:AxiosError)=>{
  //   console.log(err.response?.data)
  // })
  const toDecima = (hex: string): number => {
    return parseInt("0x" + hex);
  };
  const convertHexa2RGBA = (hexa_string: string, opacity: number) => {
    const split_str = hexa_string.split("#")[1];
    const RR = toDecima(split_str.charAt(0) + split_str.charAt(1));
    const GG = toDecima(split_str.charAt(2) + split_str.charAt(3));
    const BB = toDecima(split_str.charAt(4) + split_str.charAt(5));
    return `rgba(${RR},${GG},${BB},${opacity > 0 ? opacity / 100 : opacity})`;
  };
  React.useEffect(() => {
    console.log(convertHexa2RGBA("#ffaacc", 80));
  });
  return <div className="App">Hello debugger</div>;
}

export default App;
