import { createSlice } from "@reduxjs/toolkit";
import { AuthState } from "./authSlice";

export interface RootState {
  property: PropertyState;
  auth: AuthState;
}

export interface PropertyState {
  propertyCount: number;
  apiHits: number;
}

const initialState: PropertyState = {
  propertyCount: 0,
  apiHits: 0,
};

export const propertySlice = createSlice({
  name: "property",
  initialState,
  reducers: {
    updateProperties: (state, action: { payload: number }) => {
      state.propertyCount = action.payload;
    },
    updateApiHits: (state) => {
      state.apiHits = state.apiHits + 1;
    },
  },
});

export const { updateProperties, updateApiHits } = propertySlice.actions;

export default propertySlice.reducer;
