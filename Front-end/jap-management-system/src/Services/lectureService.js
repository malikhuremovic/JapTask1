import axios from 'axios';
import config from '../Data/config';
import tokenUtil from '../Util/tokenUtil';

const fetchAllLectures = params => {
  let query = '';
  for (let param in params) {
    query += `${param}=${params[param]}&`;
  }
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Item/get/all/params?${query}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const fetchAll = () => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + '/Item/get/all', {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const addLecture = data => {
  const token = tokenUtil.getAccessToken();
  return axios.post(config.API_URL + '/Item/add', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const modifyLecture = data => {
  const token = tokenUtil.getAccessToken();
  return axios.put(config.API_URL + '/Item/modify', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const deleteLecture = data => {
  const token = tokenUtil.getAccessToken();
  return axios.delete(config.API_URL + `/Item/delete?id=${data.id}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const services = {
  fetchAllLectures,
  fetchAll,
  addLecture,
  modifyLecture,
  deleteLecture
};

export default services;
