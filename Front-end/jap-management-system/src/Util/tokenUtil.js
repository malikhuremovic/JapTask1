const setAccessToken = token => {
  localStorage.setItem('access_token', token);
};

const getAccessToken = () => {
  localStorage.getItem('access_token');
};

let utils = {
  setAccessToken,
  getAccessToken
};

export default utils;
