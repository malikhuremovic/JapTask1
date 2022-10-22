import { Redirect, Route, Switch } from 'react-router-dom';

import React, { useContext } from 'react';

import UserContext from './Store/userContext';

import LoginPage from './Pages/LoginPage';
import LandingPage from './Pages/LandingPage';
import StudentDetailsPageAdmin from './Pages/StudentDetailsPageAdmin';
import ProgramPage from './Pages/ProgramPage';
import ReportPage from './Pages/ReportPage';
import LogoutPage from './Pages/LogoutPage';
import PageLayoutWrapper from './Pages/PageLayoutWrapper';
import ProtectedRoute from './Pages/ProtectedRoute';

import SelectionsPage from './Pages/SelectionsPage';
import LecturesPage from './Pages/LecturesPage';

import routes from './Data/routes';

import './App.module.css';

function App() {
  const { userDataState } = useContext(UserContext);
  return (
    <PageLayoutWrapper>
      <Switch>
        <Route exact path={routes.login}>
          <LoginPage />
        </Route>
        {!userDataState && <Redirect to={routes.login} />}
        <ProtectedRoute exact path={routes.logout} roles={['Admin', 'Student']}>
          <LogoutPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.selections} roles={['Admin']}>
          <SelectionsPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.programDetails} roles={['Admin']}>
          <ProgramPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.lectures} roles={['Admin']}>
          <LecturesPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.report} roles={['Admin']}>
          <ReportPage />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.studentDetails} roles={['Admin']}>
          <StudentDetailsPageAdmin />
        </ProtectedRoute>
        <ProtectedRoute exact path={routes.index} roles={['Admin', 'Student']}>
          <LandingPage role={userDataState ? userDataState.role : ''} />
        </ProtectedRoute>
      </Switch>
    </PageLayoutWrapper>
  );
}

export default App;
