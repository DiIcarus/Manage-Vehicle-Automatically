import axios, { AxiosError, AxiosResponse } from "axios";
import { config } from "./../../config/api";
interface ResponseSignIn {
  status: string;
  message: string;
  access_token: string;
}
const getURL = () => {
  return config.schema + config.host + ":" + config.port;
};
export const fetchSignIn = async (formData: FormData) => {
  try {
    console.log(getURL() + config.end_points.sign_in);
    const response: AxiosResponse<ResponseSignIn> = await axios.post(
      getURL() + config.end_points.sign_in,
      formData
    );
    console.log(response);
    return response;
  } catch (err) {
    const error: AxiosError<ResponseSignIn> = err;
    console.log(error.response?.data);
    return error;
  }
};
