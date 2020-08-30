import axios, { AxiosError, AxiosResponse } from "axios";
import { config } from "./../../config/api";

const getURL = () => {
  return config.schema + config.host + ":" + config.port;
};
export const fetchSendCode = async (formData: FormData) => {
  try {
    console.log(getURL() + config.end_points.send_code);
    const response: any = await axios.post(
      getURL() + config.end_points.send_code,
      formData
    );
    console.log(response);
    return response;
  } catch (err) {
    const error: any = err;
    console.log(error.response?.data);
    return error;
  }
};
