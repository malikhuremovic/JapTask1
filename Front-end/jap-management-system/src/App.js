import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom';

import LoginPage from './Pages/LoginPage';
import LandingPage from './Pages/LandingPage';
import StudentDetailsPage from './Pages/StudentDetailsPage';
import ProgramPage from './Pages/ProgramPage';

import { isAuthenticated } from './Util/checkAuthenticated';
import routes from './Data/routes';

import './App.module.css';
import PageLayoutWrapper from './Pages/PageLayoutWrapper';
import MainSelectionComponent from './Components/Selection/MainSelectionComponent';

function App() {
  return (
    <PageLayoutWrapper>
      <BrowserRouter>
        {!isAuthenticated() && <Redirect to="/login" />}
        <Switch>
          <Route path={routes.selections}>
            <MainSelectionComponent />
          </Route>
          <Route path={routes.programDetails}>
            <ProgramPage />
          </Route>
          <Route path={routes.studentDetails}>
            <StudentDetailsPage />
          </Route>
          <Route path={routes.login}>
            <LoginPage />
          </Route>
          <Route path={routes.index}>
            <LandingPage />
          </Route>
        </Switch>
      </BrowserRouter>
    </PageLayoutWrapper>
  );
}

export default App;
