import React from 'react';

import StudentDetailsPage from './StudentDetailsPage';
import MainLandingComponent from '../Components/Students/MainLandingComponent';

import classes from './LandingPage.module.css';

const LandingPage = ({ role }) => {
  if (role === 'Admin') {
    return (
      <div className={classes.landingPage}>
        <MainLandingComponent />
      </div>
    );
  } else if (role === 'Student') {
    return <StudentDetailsPage />;
  }
};

export default LandingPage;
