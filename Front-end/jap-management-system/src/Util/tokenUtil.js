const setAccessToken = token => {
  localStorage.setItem('access_token', token);
};

const getAccessToken = () => {
  return localStorage.getItem('access_token');
};

const removeAccessToken = () => {
  localStorage.removeItem('access_token');
};

let utils = {
  setAccessToken,
  getAccessToken,
  removeAccessToken
};

export default utils;
