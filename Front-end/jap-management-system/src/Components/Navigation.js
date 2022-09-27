import logo from '../Assets/logo.svg';
import userIcon from '../Assets/userIcon.png';
import classes from './Navigation.module.css';

import { isAuthenticated } from '../Util/checkAuthenticated';
import React from 'react';
const Navigation = () => {
  const openNav = () => {
    document.getElementById('mySidemenu').style.width = '450px';
  };

  const closeNav = () => {
    document.getElementById('mySidemenu').style.width = '0';
  };

  let hasToken = isAuthenticated();
  return (
    <React.Fragment>
      <header>
        <div className={classes.header__top}>
          {hasToken && (
            <React.Fragment>
              <div id="mySidemenu" className={classes.sidemenu}>
                <a href="#" className={classes.close} onClick={closeNav}>
                  &times;
                </a>
                <div className={classes.sm_wrapper}>
                  <a href="/">Student Management</a>
                  <a href="/selections">Selection Management</a>
                  <a href="/program">Program Management</a>
                  <a href="/logout" className={classes.logout}>
                    Log out
                  </a>
                </div>
              </div>
              <div id="pg-content">
                <div
                  style={{ fontSize: 50, cursor: 'pointer', color: 'black' }}
                  onClick={openNav}
                >
                  &#9776;
                </div>
              </div>
            </React.Fragment>
          )}
          <div className={classes.logo}>
            <img src={logo} alt="jap program" />
            {hasToken && (
              <div className={classes.user}>
                <img src={userIcon} alt="user" />
                <span>Welcome, John Doe</span>
              </div>
            )}
          </div>
        </div>
      </header>
    </React.Fragment>
  );
};

export default Navigation;
