import React from 'react';

import MainLandingComponent from '../Components/MainLandingComponent';

import classes from './LandingPage.module.css';

const LandingPage = () => {
  return (
    <div className={classes.landingPage}>
      <MainLandingComponent />
    </div>
  );
};

export default LandingPage;
