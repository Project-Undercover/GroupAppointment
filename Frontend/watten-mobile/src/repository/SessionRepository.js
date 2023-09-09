import apiCall from "../network/apiCall";
import { RequestMethod } from "../utils/Enums";

const SessionRepository = () => {
  const getSessions = async (customSearch) => {
    console.log("---", customSearch);
    const data = await apiCall("Sessions/GetAllDT", RequestMethod.POST, {
      customSearch,
    });

    return data;
  };

  return { getSessions };
};

export default SessionRepository;
