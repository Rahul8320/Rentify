import { createSlice } from "@reduxjs/toolkit";
import { isTokenValid } from "@/lib/verifyToken";
import { AuthToken, AuthUser } from "@/Models/auth";
import { LocalStorageConfig } from "@/config/localStorageConfig";

export interface AuthState {
  user: AuthUser | null;
  token: string | null;
  expiration: string | null;
  isValidToken: boolean;
}

const initialState: AuthState = {
  user: JSON.parse(localStorage.getItem(LocalStorageConfig.userKey)!) || null,
  token: localStorage.getItem(LocalStorageConfig.tokenKey) || null,
  expiration: localStorage.getItem(LocalStorageConfig.expirationKey) || null,
  isValidToken: isTokenValid(
    localStorage.getItem(LocalStorageConfig.tokenKey),
    localStorage.getItem(LocalStorageConfig.expirationKey)
  ),
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action: { payload: AuthToken }) => {
      const { token, expiration } = action.payload;
      state.token = token;
      state.expiration = expiration;
      state.isValidToken = isTokenValid(token, expiration);
      localStorage.setItem(LocalStorageConfig.tokenKey, token);
      localStorage.setItem(LocalStorageConfig.expirationKey, expiration);
    },
    logout: (state) => {
      state.token = null;
      state.expiration = null;
      state.isValidToken = false;
      localStorage.removeItem(LocalStorageConfig.tokenKey);
      localStorage.removeItem(LocalStorageConfig.expirationKey);
    },
    verify: (state, action: { payload: AuthUser }) => {
      state.user = action.payload;
      localStorage.setItem(
        LocalStorageConfig.userKey,
        JSON.stringify(action.payload)
      );
    },
  },
});

export const { login, logout, verify } = authSlice.actions;

export default authSlice.reducer;
