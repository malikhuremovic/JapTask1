import axios from 'axios';
import config from '../Data/config';
import tokenUtil from '../Util/tokenUtil';

const addProgram = data => {
  const token = tokenUtil.getAccessToken();
  return axios.post(config.API_URL + '/Program', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const editProgram = data => {
  const token = tokenUtil.getAccessToken();
  return axios.put(config.API_URL + '/Program/modify', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const deleteProgram = data => {
  const token = tokenUtil.getAccessToken();
  return axios.delete(config.API_URL + '/Program/delete?id=' + data.id, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const fetchAllPrograms = () => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Program/get/all`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const fetchOrderedProgramItems = id => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Program/get/items?id=${id}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const fetchAllProgramsWithParams = params => {
  let query = '';
  for (let param in params) {
    query += `${param}=${params[param]}&`;
  }
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Program/get/all/params?${query}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const modifyProgramItemsOrder = data => {
  const token = tokenUtil.getAccessToken();
  return axios.patch(config.API_URL + `/Program/modify/items/order`, data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const services = {
  addProgram,
  editProgram,
  deleteProgram,
  fetchAllPrograms,
  fetchAllProgramsWithParams,
  fetchOrderedProgramItems,
  modifyProgramItemsOrder
};

export default services;
