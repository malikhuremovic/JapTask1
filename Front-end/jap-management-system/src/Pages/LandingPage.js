import React from 'react';

import StudentDetailsPage from './Student/StudentDetailsPage';
import MainLandingComponent from '../Components/Student/MainLandingComponent';

import classes from './Style/LandingPage.module.css';

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
