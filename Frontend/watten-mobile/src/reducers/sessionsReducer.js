import {
  FETCH_HISTORY_SUCCESS,
  FETCH_INSTRUCTORS_SUCCESS,
  FETCH_SESSIONS_SUCCESS,
  FETCH_SESSION_PARTICIPANTS_SUCCESS,
} from "../constants/actionTypes";
const initialState = {
  sessions: [],
  historySessions: [],
  instructors: [],
  sessionsParticipants: [],
  error: null,
};

const sessionsReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_SESSIONS_SUCCESS:
      return {
        ...state,
        sessions: action.payload,
      };
    case FETCH_INSTRUCTORS_SUCCESS:
      return { ...state, instructors: action.payload };
    case FETCH_HISTORY_SUCCESS:
      return {
        ...state,
        historySessions: action.payload,
      };

    case FETCH_SESSION_PARTICIPANTS_SUCCESS:
      return { ...state, sessionsParticipants: action.payload };

    default:
      return state;
  }
};

export default sessionsReducer;
