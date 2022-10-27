import React, { useCallback, useEffect, useState } from 'react';

import studentService from '../../Services/studentService';

import classes from '../Style/MainComponent.module.css';
import PersonalReportTable from './PersonalReportTable';

const PersonalReportComponent = () => {
  const [personalReport, setPersonalReport] = useState([]);

  const handleFetchReport = useCallback(() => {
    studentService
      .fetchReport()
      .then(response => {
        setPersonalReport(response.data.data);
      })
      .catch(err => console.log(err));
  }, []);

  useEffect(() => {
    handleFetchReport();
  }, [handleFetchReport]);

  return (
    <div className={classes.table__container}>
      <div style={{ marginLeft: 20, padding: 15 }}>
        <h4>Personal program track &darr;</h4>
      </div>
      <PersonalReportTable personalReport={personalReport} />
    </div>
  );
};

export default PersonalReportComponent;
