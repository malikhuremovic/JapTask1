import { useEffect } from 'react';
import { logoutUser } from '../Util/userUtil';

import routes from '../Data/routes';

const LogoutPage = () => {
  useEffect(() => {
    logoutUser();
  }, []);
  return window.location.replace(routes.login);
};

export default LogoutPage;
