import { FETCH_SESSIONS_SUCCESS } from "../constants/actionTypes";
const initialState = {
  isLoggedIn: false,
  token: "",
  username: "",
};

const sessionsReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_SESSIONS_SUCCESS:
      return {
        ...state,
      };
    default:
      return state;
  }
};

export default sessionsReducer;
