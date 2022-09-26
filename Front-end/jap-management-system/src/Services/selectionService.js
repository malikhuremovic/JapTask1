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

const fetchSelectionsParams = params => {
  let query = '';
  for (let param in params) {
    query += `${param}=${params[param]}&`;
  }
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Selection/get?${query}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const addSelection = data => {
  const token = tokenUtil.getAccessToken();
  return axios.post(config.API_URL + '/Selection/add', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const modifySelection = data => {
  const token = tokenUtil.getAccessToken();
  return axios.put(config.API_URL + '/Selection/modify', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const deleteSelection = data => {
  const token = tokenUtil.getAccessToken();
  return axios.delete(config.API_URL + `/Selection/delete?id=${data.id}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const services = {
  fetchAllSelections,
  fetchSelectionsParams,
  addSelection,
  modifySelection,
  deleteSelection
};

export default services;
