import React, { useContext } from 'react';
import UserContext from '../Store/userContext';

import logo from '../Assets/logo.svg';
import userIcon from '../Assets/userIcon.png';

import { Link } from 'react-router-dom';

import classes from './Navigation.module.css';
import routes from '../Data/routes';

const Navigation = () => {
  const { userDataState } = useContext(UserContext);
  const openNav = () => {
    document.getElementById('mySidemenu').style.width = '450px';
  };

  const closeNav = () => {
    document.getElementById('mySidemenu').style.width = '0';
  };
  return (
    <React.Fragment>
      <header>
        <div className={classes.header__top}>
          {userDataState && (
            <React.Fragment>
              <div id="mySidemenu" className={classes.sidemenu}>
                <Link to="#" className={classes.close} onClick={closeNav}>
                  &times;
                </Link>
                <div className={classes.sm_wrapper}>
                  {userDataState.role === 'Admin' && (
                    <Link onClick={closeNav} to="/">
                      Student Management
                    </Link>
                  )}
                  {userDataState.role === 'Admin' && (
                    <Link onClick={closeNav} to={routes.selections}>
                      Selection Management
                    </Link>
                  )}
                  {userDataState.role === 'Admin' && (
                    <Link onClick={closeNav} to={routes.programDetails}>
                      Program Management
                    </Link>
                  )}
                  {userDataState.role === 'Admin' && (
                    <Link onClick={closeNav} to={routes.lectures}>
                      Lectures Management
                    </Link>
                  )}
                  {userDataState.role === 'Admin' && (
                    <Link onClick={closeNav} to={routes.report}>
                      Report
                    </Link>
                  )}
                  {userDataState.role === 'Student' && (
                    <Link onClick={closeNav} to={routes.index}>
                      My Profile
                    </Link>
                  )}
                  <Link onClick={closeNav} to="/logout" className={classes.logout}>
                    Log out
                  </Link>
                </div>
              </div>
              <div id="pg-content">
                <div style={{ fontSize: 50, cursor: 'pointer', color: 'black' }} onClick={openNav}>
                  &#9776;
                </div>
              </div>
            </React.Fragment>
          )}
          <div className={classes.logo}>
            <img src={logo} alt="jap program" />
            {userDataState && (
              <div className={classes.user}>
                <img src={userIcon} alt="user" />
                <span>{userDataState.userName}</span>
              </div>
            )}
          </div>
        </div>
      </header>
    </React.Fragment>
  );
};

export default Navigation;
