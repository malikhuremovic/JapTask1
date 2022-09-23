import axios from "axios";
import config from "../Data/axiosConfig";

const login = (data) => {
  return axios.post(config.API_URL + "/Auth/login", data, {
    headers: {
      "Content-Type": "application/json",
    },
  });
};

const services = {
  login,
};

export default services;
