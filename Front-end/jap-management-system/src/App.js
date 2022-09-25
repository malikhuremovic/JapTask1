import { BrowserRouter, Route, Switch } from 'react-router-dom';

import LoginPage from './Pages/LoginPage';
import LandingPage from './Pages/LandingPage';
import StudentDetailsPage from './Pages/StudentDetailsPage';

import routes from './Data/routes';

import './App.module.css';
import PageLayoutWrapper from './Pages/PageLayoutWrapper';

function App() {
  return (
    <PageLayoutWrapper>
      <BrowserRouter>
        <Switch>
          <Route exact path={routes.studentDetails}>
            <StudentDetailsPage />
          </Route>
          <Route exact path={routes.index}>
            <LandingPage />
          </Route>
          <Route path={routes.login}>
            <LoginPage />
          </Route>
        </Switch>
      </BrowserRouter>
    </PageLayoutWrapper>
  );
}

export default App;
