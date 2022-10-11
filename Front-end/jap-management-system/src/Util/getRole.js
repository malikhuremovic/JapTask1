import { loadUser } from './userUtil';

export const getUserRole = role => {
  const user = loadUser();
  if (user) {
    return user.role === role;
  } else {
    throw new Error('User does not exist');
  }
};
