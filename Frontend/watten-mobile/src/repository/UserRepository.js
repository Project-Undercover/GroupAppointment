import apiCall from "../network/apiCall";
import { RequestMethod } from "../utils/Enums";

const UserRepository = () => {
  const getUsers = async (customeSearch = {}) => {
    const data = await apiCall(
      "Users/GetAllDT",
      RequestMethod.POST,
      customeSearch
    );
    return data;
  };
  const createUser = async ({
    firstName,
    lastName,
    mobileNumber,
    children,
  }) => {
    const data = await apiCall("Users/Create", RequestMethod.POST, {
      firstName,
      lastName,
      mobileNumber,
      children,
    });
    return data;
  };

  const getUserHomeData = async ({ startDate }) => {
    const data = await apiCall("Users/HomeData", RequestMethod.POST, {
      from: startDate,
    });
    return data;
  };

  const getProfile = async () => {
    const data = await apiCall("Users/GetProfile", RequestMethod.GET);
    return data;
  };

  return { getUsers, createUser, getProfile, getUserHomeData };
};

export default UserRepository;
