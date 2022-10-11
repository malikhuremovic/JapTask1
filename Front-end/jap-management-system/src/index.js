import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import utils from './Util/tokenUtil';
import { UserContextProvider } from './Store/userContext';
import { logoutUser } from './Util/userUtil';
import services from './Services/userService';
import { BrowserRouter } from 'react-router-dom';

const initialize = async () => {
  const token = utils.getAccessToken();
  let user;
  if (!token) {
    return null;
  }
  try {
    const response = await services.getUser(token);
    user = response.data.data;
  } catch (err) {
    alert('Token has expired, please log in again');
    logoutUser();
    user = null;
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
