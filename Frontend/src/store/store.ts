import { configureStore } from "@reduxjs/toolkit";
import ispReducer from "./ispSlice";

const store = configureStore({
  reducer: ispReducer,
});

export default store;
