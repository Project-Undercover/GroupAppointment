import { FETCH_SESSIONS_SUCCESS } from "../constants/actionTypes";
const initialState = {
  sessions: [],
  error: null,
};

const sessionsReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_SESSIONS_SUCCESS:
      return {
        ...state,
        sessions: action.payload,
      };
    default:
      return state;
  }
};

export default sessionsReducer;
