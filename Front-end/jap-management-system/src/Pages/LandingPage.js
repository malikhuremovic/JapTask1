import React from 'react';
import { Redirect } from 'react-router-dom';

import tokenUtil from '../Util/tokenUtil';

import MainLandingComponent from '../Components/MainLandingComponent';

import classes from './LandingPage.module.css';

const LandingPage = () => {
  const token = tokenUtil.getAccessToken();
  return (
    <div className={classes.landingPage}>
      {!token && <Redirect to="/login" />}
      <MainLandingComponent />
    </div>
  );
};

export default LandingPage;
