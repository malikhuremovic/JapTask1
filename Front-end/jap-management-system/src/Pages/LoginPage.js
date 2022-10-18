import { useState } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Form } from 'react-bootstrap';

import userService from '../Services/userService';
import tokenUtil from '../Util/tokenUtil';

import classes from './LoginPage.module.css';

const LoginPage = () => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  let INITIAL_LOGIN_INFO_STATE = { show: false, success: false };
  const [loginInfo, setLoginInfo] = useState(INITIAL_LOGIN_INFO_STATE);

  const handleFormSubmit = async ev => {
    ev.preventDefault();

    const userData = {
      userName: userName.trim(),
      password: password.trim()
    };

    await userService
      .login(userData)
      .then(response => {
        setLoginInfo(() => {
          tokenUtil.setAccessToken(response.data.data.token);
          window.location.replace('/');
          let state = { show: true, success: true };
          return state;
        });
      })
      .catch(err => {
        setLoginInfo(() => {
          let state = { show: true, success: false };
          return state;
        });
        console.log(err);
      });
  };

  const handleUsernameInput = ev => {
    setUserName(ev.target.value);
  };

  const handlePasswordInput = ev => {
    setPassword(ev.target.value);
  };

  if (tokenUtil.getAccessToken()) {
    return <Redirect to="/" />;
  }

  return (
    <div className={classes.loginPage}>
      <div className="modal d-flex justify-content-center align-items-center">
        <div className={classes.floatingModal}>
          <h3>Login</h3>
          <Form
            className={classes.floatingModalForm}
            onSubmit={handleFormSubmit}
          >
            <Form.Group className="mb-3" controlId="userName">
              <Form.Label>Username</Form.Label>
              <Form.Control
                type="text"
                placeholder="Username"
                onInput={handleUsernameInput}
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="password">
              <Form.Label>Password</Form.Label>
              <Form.Control
                className="w-100"
                type="password"
                placeholder="Password"
                onInput={handlePasswordInput}
              />
            </Form.Group>
            <Button
              className={
                (loginInfo.show ? 'mb-3' : '') + ' ' + classes.loginBtn
              }
              variant="primary"
              type="submit"
            >
              Submit
            </Button>
            {loginInfo.show && (
              <Button
                className={classes.errorMessageBtn}
                variant={loginInfo.success ? 'success' : 'danger'}
                disabled
              >
                &nbsp; &nbsp;&nbsp;&nbsp;
                {loginInfo.success ? 'Success!' : 'Please, try again'}
                &nbsp;&nbsp;&nbsp;&nbsp;
              </Button>
            )}
          </Form>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
