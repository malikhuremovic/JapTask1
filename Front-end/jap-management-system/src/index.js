import React from 'react';
import ReactDOM from 'react-dom/client';

import App from './App';
import utils from './Util/tokenUtil';

import { UserContextProvider } from './Store/userContext';
import { logoutUser } from './Util/userUtil';
import { BrowserRouter } from 'react-router-dom';

import services from './Services/userService';

const initialize = async () => {
  const token = utils.getAccessToken();
  let user = null;
  if (!token) {
    return null;
  }
  let response;
  try {
    response = await services.getUser(token);
    user = response.data.data;
  } catch {
    if (response.request.status === 401 || response.request.status === 403) {
      alert('Token has expired, please log in again');
      logoutUser();
    }
  }
  return user;
};

const startApplication = user => {
  const root = ReactDOM.createRoot(document.getElementById('root'));
  root.render(
    <React.StrictMode>
      <UserContextProvider userData={user}>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </UserContextProvider>
    </React.StrictMode>
  );
};

initialize().then(startApplication);
