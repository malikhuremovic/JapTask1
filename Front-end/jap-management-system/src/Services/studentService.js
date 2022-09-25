import axios from 'axios';
import config from '../Data/axiosConfig';
import tokenUtil from '../Util/tokenUtil';

const fetchAllStuents = params => {
  let query = '';
  for (let param in params) {
    query += `${param}=${params[param]}&`;
  }
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Student/get?${query}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const fetchStudentById = id => {
  const token = tokenUtil.getAccessToken();
  return axios.get(config.API_URL + `/Student/get/id?id=${id}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const addStudent = data => {
  const token = tokenUtil.getAccessToken();
  return axios.post(config.API_URL + '/Student/add', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const addComment = data => {
  const token = tokenUtil.getAccessToken();
  return axios.post(config.API_URL + '/Student/add/comment', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const modifyStudent = data => {
  const token = tokenUtil.getAccessToken();
  return axios.put(config.API_URL + '/Student/modify', data, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const deleteStudent = data => {
  const token = tokenUtil.getAccessToken();
  return axios.delete(config.API_URL + `/Student/delete?id=${data.id}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    }
  });
};

const services = {
  fetchAllStuents,
  fetchStudentById,
  addStudent,
  addComment,
  modifyStudent,
  deleteStudent
};

export default services;
