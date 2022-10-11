import React from 'react';

const UserContext = React.createContext({
  userDataState: null,
  isUserAuthenticated: false,
  handleSetUser: () => {}
});

export default UserContext;

export const UserContextProvider = ({ children, userData }) => {
  return (
    <UserContext.Provider
      value={{
        userDataState: userData
      }}
    >
      {children}
    </UserContext.Provider>
  );
};
