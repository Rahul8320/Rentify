import { createSlice } from "@reduxjs/toolkit";

export interface PropertyState {
  propertyCount: number;
  apiHits: number;
  isLogin: boolean;
}

interface UpdateStateActionPayload {
  payload: number;
  type: string;
}

const initialState: PropertyState = {
  propertyCount: 0,
  apiHits: 0,
  isLogin: true,
};

export const propertySlice = createSlice({
  name: "property",
  initialState,
  reducers: {
    updateProperties: (state, action: UpdateStateActionPayload) => {
      state.propertyCount = action.payload;
    },
    updateApiHits: (state) => {
      state.apiHits = state.apiHits + 1;
    },
    updateLogin: (state, action) => {
      state.isLogin = action.payload;
    },
  },
});

export const { updateProperties, updateApiHits } = propertySlice.actions;

export default propertySlice.reducer;
