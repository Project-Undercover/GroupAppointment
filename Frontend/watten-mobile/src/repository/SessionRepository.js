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

  const editSession = async (formData) => {
    const data = await apiCall(
      "Sessions/Edit",
      RequestMethod.POST,
      formData,
      "multipart/form-data"
    );
    return data;
  };

  const addParticipants = async ({ sessionId, children }) => {
    const data = await apiCall("Sessions/AddParticipants", RequestMethod.POST, {
      sessionId,
      children,
    });

    return data;
  };

  const unBookSession = async ({ sessionId }) => {
    const data = await apiCall(
      `Sessions/DeleteUserSessionParticipants/${sessionId}`,
      RequestMethod.DELETE
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

  const getSessionParticipants = async (sessionId) => {
    const url = `Sessions/GetSessionParticipants?sessionId=${sessionId}`;
    const data = await apiCall(url, RequestMethod.GET);
    return data;
  };

  const getInstructors = async () => {
    const data = await apiCall("Sessions/GetInstructors", RequestMethod.GET);
    return data;
  };
  return {
    createSession,
    editSession,
    getSessions,
    getUserSessions,
    getInstructors,
    addParticipants,
    unBookSession,
    getSessionParticipants,
  };
};

export default SessionRepository;
