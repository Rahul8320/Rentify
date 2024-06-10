import { combineReducers, configureStore } from "@reduxjs/toolkit";
import propertyReducer from "./propertySlice";
import authReducer from "./authSlice";

const rootReducer = combineReducers({
  auth: authReducer,
  property: propertyReducer,
});

const store = configureStore({
  reducer: rootReducer,
});

export default store;
