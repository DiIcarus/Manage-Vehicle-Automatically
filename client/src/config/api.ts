interface EndPoints {
  sign_in: string;
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
  end_points: { sign_in: "/login" },
};
