import { useState, useEffect } from 'react';

import ProgramTable from '../Components/ProgramTable';

import programIcon from '../Assets/programIcon.png';

import programService from '../Services/programService';

import classes from './ProgramPage.module.css';

const ProgramPage = () => {
  const [programs, setPrograms] = useState([]);

  useEffect(() => {
    programService
      .fetchAllPrograms()
      .then(response => {
        setPrograms(() => response.data.data);
        console.log(response.data.data);
      })
      .catch(err => console.log(err));
  }, []);

  return (
    <div className={classes.container}>
      <div className={classes.top}>
        <img src={programIcon} alt="student" />
      </div>
      <ProgramTable programs={programs} />
    </div>
  );
};

export default ProgramPage;
