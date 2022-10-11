import { Route, Switch } from 'react-router-dom';

import routes from './Data/routes';

import LoginPage from './Pages/LoginPage';
import LandingPage from './Pages/LandingPage';
import StudentDetailsPageAdmin from './Pages/StudentDetailsPageAdmin';
import ProgramPage from './Pages/ProgramPage';
import LogoutPage from './Pages/LogoutPage';
import PageLayoutWrapper from './Pages/PageLayoutWrapper';
import MainSelectionComponent from './Components/Selection/MainSelectionComponent';
import './App.module.css';
import UserContext from './Store/userContext';
import { useContext } from 'react';
import ProtectedRoute from './Pages/ProtectedRoute';
import React from 'react';

function App() {
  const { userDataState } = useContext(UserContext);
  return (
    <PageLayoutWrapper>
      <Switch>
        <Route exact path={routes.login}>
          <LoginPage />
        </Route>
        <ProtectedRoute exact path={routes.logout} roles={['Admin', 'Student']}>
          <LogoutPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.selections} roles={['Admin']}>
          <MainSelectionComponent />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.programDetails} roles={['Admin']}>
          <ProgramPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.studentDetails} roles={['Admin']}>
          <StudentDetailsPageAdmin />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.index} roles={['Admin', 'Student']}>
          <LandingPage role={userDataState ? userDataState.role : ''} />
        </ProtectedRoute>
        <Route path="*">
          <h1>404 Not found</h1>
        </Route>
      </Switch>
    </PageLayoutWrapper>
  );
}

export default App;
