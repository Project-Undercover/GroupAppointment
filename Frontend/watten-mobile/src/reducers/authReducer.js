import { LOGIN_SUCCESS } from "../constants/actionTypes";

const initialState = {
  isLoggedIn: false,
  token: "",
  username: "",
};

const authReducer = (state = initialState, action) => {
  switch (action.type) {
    case LOGIN_SUCCESS:
      return {
        ...state,
      };
    default:
      return state;
  }
};

export default authReducer;
