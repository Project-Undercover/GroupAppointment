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
  const getChildren = async () => {
    const data = await apiCall("Users/GetChildren", RequestMethod.POST);
    return data;
  };

  const getRoles = async () => {
    const data = await apiCall("Enums/Roles", RequestMethod.GET);
    return data;
  };

  const editUser = async ({
    id,
    firstName,
    lastName,
    mobileNumber,
    children,
    isActive,
    role,
  }) => {
    const data = await apiCall("Users/Edit", RequestMethod.POST, {
      id,
      firstName,
      lastName,
      mobileNumber,
      children,
      isActive,
      role,
    });
    return data;
  };

  return {
    getUsers,
    createUser,
    getProfile,
    getUserHomeData,
    getChildren,
    editUser,
    getRoles,
  };
};

export default UserRepository;
