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
  try {
    const response = await services.getUser(token);
    user = response.data.data;
  } catch (err) {
    if (err.request.status === 401 || err.request.status === 403) {
      alert('Authentication has failed. Invalid token. Please log in again.');
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
