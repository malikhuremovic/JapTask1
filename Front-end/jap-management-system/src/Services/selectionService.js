import axios from 'axios';
import config from '../Data/axiosConfig';
import tokenUtil from '../Util/tokenUtil';

const fetchAllSelections = () => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Selection/get/all`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const services = {
  fetchAllSelections
};

export default services;
