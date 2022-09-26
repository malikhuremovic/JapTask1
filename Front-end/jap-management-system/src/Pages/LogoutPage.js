import { useEffect } from 'react';

const LogoutPage = () => {
  useEffect(() => {
    localStorage.removeItem('access_token');
    window.location.replace('/');
  }, []);
};

export default LogoutPage;
