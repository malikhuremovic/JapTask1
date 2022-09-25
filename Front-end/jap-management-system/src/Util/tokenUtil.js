const setAccessToken = token => {
  localStorage.setItem('access_token', token);
};

const getAccessToken = () => {
  return localStorage.getItem('access_token');
};

let utils = {
  setAccessToken,
  getAccessToken
};

export default utils;
