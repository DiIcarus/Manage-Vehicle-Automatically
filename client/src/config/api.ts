interface EndPoints {
  register_ticket: string;
  sign_in: string;
  register: string;
  register_vehicle: string;
  send_code:string
}
interface Config {
  schema: "http://" | "https://";
  host: string;
  port: number;
  end_points: EndPoints;
}
export const config: Config = {
  schema: "http://",
  host: "127.0.0.1",
  port: 5000,
  end_points: {
    sign_in: "/login",
    register_ticket: "/register-ticket",
    register: "/register",
    register_vehicle: "/register-vehicle",
    send_code:"/send-code"
  },
};
