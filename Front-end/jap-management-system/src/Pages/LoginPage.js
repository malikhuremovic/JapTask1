import { useState } from 'react';
import { Redirect, useHistory } from 'react-router-dom';
import { Button, Form } from 'react-bootstrap';

import userService from '../Services/userService';
import tokenUtil from '../Util/tokenUtil';

import classes from './LoginPage.module.css';

const LoginPage = () => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const history = useHistory();

  let INITIAL_LOGIN_INFO_STATE = { show: false, success: false };
  const [loginInfo, setLoginInfo] = useState(INITIAL_LOGIN_INFO_STATE);

  let token = tokenUtil.getAccessToken();

  const handleFormSubmit = async ev => {
    ev.preventDefault();

    const userData = {
      userName,
      password
    };

    await userService
      .login(userData)
      .then(response => {
        setLoginInfo(() => {
          tokenUtil.setAccessToken(response.data.data);
          history.push('/');
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

  return (
    <div className={classes.loginPage}>
      {token && <Redirect to="/" />}
      <div className="modal d-flex justify-content-center align-items-center">
        <Form className={classes.modal} onSubmit={handleFormSubmit}>
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
              type="password"
              placeholder="Password"
              onInput={handlePasswordInput}
            />
          </Form.Group>
          <Button
            className={loginInfo.show ? 'mb-3' : ''}
            variant="primary"
            type="submit"
          >
            Submit
          </Button>
          {loginInfo.show && (
            <Button variant={loginInfo.success ? 'success' : 'danger'} disabled>
              &nbsp; &nbsp;&nbsp;&nbsp;
              {loginInfo.success ? 'Success!' : 'Please, try again'}
              &nbsp;&nbsp;&nbsp;&nbsp;
            </Button>
          )}
        </Form>
      </div>
    </div>
  );
};

export default LoginPage;
