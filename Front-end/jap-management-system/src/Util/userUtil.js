export const logoutUser = () => {
  localStorage.removeItem('access_token');
};
