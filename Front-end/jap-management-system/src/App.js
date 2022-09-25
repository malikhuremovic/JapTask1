import { BrowserRouter, Route, Switch } from 'react-router-dom';

import LoginPage from './Pages/LoginPage';
import LandingPage from './Pages/LandingPage';

import routes from './Data/routes';

import './App.module.css';

function App() {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path={routes.index}>
          <LandingPage />
        </Route>
        <Route path={routes.login}>
          <LoginPage />
        </Route>
      </Switch>
    </BrowserRouter>
  );
}

export default App;
