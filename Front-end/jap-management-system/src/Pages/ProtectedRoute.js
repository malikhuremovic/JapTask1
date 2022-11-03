import React, { useContext } from 'react';
import { Redirect, Route } from 'react-router-dom';

import UserContext from '../Store/userContext';

import routes from '../Data/routes';
import { Button } from 'react-bootstrap';

const ProtectedRoute = ({ children, path, roles }) => {
  const { userDataState } = useContext(UserContext);
  if (userDataState) {
    if (!roles.includes(userDataState.role)) {
      return (
        <Button
          style={{
            position: 'absolute',
            left: '50%',
            top: '35%',
            width: '60%',
            height: '15vh',
            fontSize: '45px',
            transform: 'translateX(-50%)'
          }}
          disabled
          variant="danger"
        >
          Unauthorized | 401.
        </Button>
      );
    }
  } else if (!userDataState) {
    return <Redirect to={routes.logout} />;
  }
  return <Route path={path}>{children}</Route>;
};

export default ProtectedRoute;
