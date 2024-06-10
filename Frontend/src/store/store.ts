import { configureStore } from "@reduxjs/toolkit";
import propertyReducer from "./propertySlice";

const store = configureStore({
  reducer: propertyReducer,
});

export default store;
