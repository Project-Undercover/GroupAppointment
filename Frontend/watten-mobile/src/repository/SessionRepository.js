import apiCall from "../network/apiCall";
import { RequestMethod } from "../utils/Enums";

const SessionRepository = () => {
  const createSession = async (formData) => {
    const data = await apiCall(
      "Sessions/Create",
      RequestMethod.POST,
      formData,
      "multipart/form-data"
    );
    return data;
  };

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

  const getInstructors = async () => {
    const data = await apiCall("Sessions/GetInstructors", RequestMethod.GET);
    return data;
  };
  return { createSession, getSessions, getUserSessions, getInstructors };
};

export default SessionRepository;
