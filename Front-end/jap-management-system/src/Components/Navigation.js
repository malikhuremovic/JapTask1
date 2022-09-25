import logo from '../Assets/logo.svg';
import navigationIcon from '../Assets/nav-icon.svg';
import userIcon from '../Assets/userIcon.png';
import classes from './Navigation.module.css';

import { isAuthenticated } from '../Util/checkAuthenticated';

const Navigation = () => {
  let hasToken = isAuthenticated();
  return (
    <header>
      <div className={classes.header__top}>
        <div className={classes.logo}>
          <img src={logo} alt="jap program" />
        </div>
        {hasToken && (
          <nav>
            <img src={navigationIcon} alt="navigation" />
          </nav>
        )}
      </div>
      {hasToken && (
        <div className={classes.user}>
          <img src={userIcon} alt="user" />
          <span>Welcome, John Doe</span>
        </div>
      )}
    </header>
  );
};

export default Navigation;
