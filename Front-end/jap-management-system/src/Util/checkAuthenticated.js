import tokenUtil from './tokenUtil';

export const isAuthenticated = () => {
  return Boolean(tokenUtil.getAccessToken());
};
