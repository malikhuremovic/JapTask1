import axios from 'axios';
import config from '../Data/config';
import tokenUtil from '../Util/tokenUtil';

const login = data => {
  return axios.post(config.API_URL + '/Auth/login', data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};

const getUser = () => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + '/Auth/get/user', {
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + token
    }
  });
};

const services = {
  login,
  getUser
};

export default services;
