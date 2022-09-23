import { BrowserRouter, Route } from 'react-router-dom';
import LoginPage from './Pages/LoginPage';
import routes from './Data/routes';
import './App.module.css';
import LandingPage from './Pages/LandingPage';

function App() {
  return (
    <BrowserRouter>
      <Route exact path={routes.index}>
        <LandingPage />
      </Route>
      <Route path={routes.login}>
        <LoginPage />
      </Route>
    </BrowserRouter>
  );
}

export default App;
