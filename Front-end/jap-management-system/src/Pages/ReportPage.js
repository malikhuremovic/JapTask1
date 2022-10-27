import React, { useState, useEffect, useCallback } from 'react';
import ReportTable from '../Components/Report/ReportTable';
import reportIcon from '../Assets/reportIcon.png';
import selectionService from '../Services/selectionService';

import classes from './Style/ProgramPage.module.css';
const ReportPage = () => {
  const [selections, setSelections] = useState([]);

  const fetchAllSelections = useCallback(() => {
    selectionService.fetchSelectionsReport().then(response => {
      setSelections(response.data.data);
    });
  }, []);

  useEffect(() => {
    fetchAllSelections();
  }, [fetchAllSelections]);

  return (
    <div className={classes.container}>
      <div className={classes.top}>
        <img src={reportIcon} alt="student" />
        <span className={classes.pageCaption}>Report</span>
      </div>
      <ReportTable selections={selections} />
    </div>
  );
};

export default ReportPage;
