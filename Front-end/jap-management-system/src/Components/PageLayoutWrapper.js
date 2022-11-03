import React from 'react';

import Navigation from '../Components/Navigation';

const PageLayoutWrapper = ({ children }) => {
  return (
    <React.Fragment>
      <Navigation />
      {children}
    </React.Fragment>
  );
};

export default PageLayoutWrapper;
