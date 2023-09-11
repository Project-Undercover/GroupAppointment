import apiCall from "../network/apiCall";
import { RequestMethod } from "../utils/Enums";

const SessionRepository = () => {
  const getSessions = async (customSearch) => {
    const data = await apiCall("Sessions/GetAllDT", RequestMethod.POST, {
      customSearch,
    });

    return data;
  };

  const getUserSessions = async (customSearch) => {
    const data = await apiCall("Sessions/GetUserSessions", RequestMethod.POST, {
      customSearch,
    });

    return data;
  };

  return { getSessions, getUserSessions };
};

export default SessionRepository;
