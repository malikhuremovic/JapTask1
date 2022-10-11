import { useContext } from 'react';
import { Redirect, Route } from 'react-router-dom';
import routes from '../Data/routes';
import UserContext from '../Store/userContext';

const ProtectedRoute = ({ children, path, roles }) => {
  const { userDataState } = useContext(UserContext);
  if (userDataState) {
    if (!roles.includes(userDataState.role)) {
      return <h1>Unauthorized. 401.</h1>;
    }
  } else if (!userDataState) {
    return <Redirect to={routes.logout} />;
  }
  return <Route path={path}>{children}</Route>;
};

export default ProtectedRoute;
