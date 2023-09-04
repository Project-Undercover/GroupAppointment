import { combineReducers } from "redux";
import authReducer from "./authReducer";
import sessionsReducer from "./sessionsReducer";
const rootReducer = combineReducers({
  auth: authReducer,
  sessions: sessionsReducer,
});

export default rootReducer;
