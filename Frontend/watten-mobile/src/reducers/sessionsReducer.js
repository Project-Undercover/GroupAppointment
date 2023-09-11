import {
  FETCH_HISTORY_SUCCESS,
  FETCH_SESSIONS_SUCCESS,
} from "../constants/actionTypes";
const initialState = {
  sessions: [],
  historySessions: [],
  error: null,
};

const sessionsReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_SESSIONS_SUCCESS:
      return {
        ...state,
        sessions: action.payload,
      };
    case FETCH_HISTORY_SUCCESS:
      return {
        ...state,
        historySessions: action.payload,
      };

    default:
      return state;
  }
};

export default sessionsReducer;
