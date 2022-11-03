import MainProgramComponent from '../../Components/Program/MainProgramComponent';
import programIcon from '../../Assets/programIcon.png';

import classes from '../Style/ProgramPage.module.css';

const ProgramPage = () => {
  return (
    <div className={classes.container}>
      <div className={classes.top}>
        <img src={programIcon} alt="student" />
        <span className={classes.pageCaption}>Program(s)</span>
      </div>
      <MainProgramComponent />
    </div>
  );
};

export default ProgramPage;
