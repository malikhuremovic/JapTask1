import axios from 'axios';
import config from '../Data/axiosConfig';
import tokenUtil from '../Util/tokenUtil';

const fetchAllPrograms = () => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Program/get/all`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const services = {
  fetchAllPrograms
};

export default services;
