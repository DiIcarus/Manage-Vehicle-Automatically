import axios, { AxiosError, AxiosResponse } from "axios";
import { config } from "./../../config/api";

const getURL = () => {
  return config.schema + config.host + ":" + config.port;
};
export const fetchRegisterVehicle = async (
  formData: FormData,
  token: string
) => {
  try {
    console.log(getURL() + config.end_points.register_vehicle);
    const response: any = await axios.post(
      getURL() + config.end_points.register_vehicle,
      formData,
      {
        headers: {
          Authorization: token,
        },
      }
    );
    console.log(response);
    return response;
  } catch (err) {
    const error: any = err;
    console.log(error.response?.data);
    return error;
  }
};
