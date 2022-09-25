import React from 'react';
import { Redirect } from 'react-router-dom';
import tokenUtil from '../Util/tokenUtil';

const LandingPage = () => {
  const token = tokenUtil.getAccessToken();
  return <React.Fragment>{!token && <Redirect to="/login" />}</React.Fragment>;
};

export default LandingPage;
