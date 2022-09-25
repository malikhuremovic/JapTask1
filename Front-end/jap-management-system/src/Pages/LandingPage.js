import React from 'react';
import { Redirect } from 'react-router-dom';

import tokenUtil from '../Util/tokenUtil';

import Navigation from '../Components/Navigation';
import LandingComponent from '../Components/LandingComponent';

import classes from './LandingPage.module.css';

const LandingPage = () => {
  const token = tokenUtil.getAccessToken();
  return (
    <div className={classes.landingPage}>
      {!token && <Redirect to="/login" />}
      <Navigation />
      <LandingComponent />
    </div>
  );
};

export default LandingPage;
