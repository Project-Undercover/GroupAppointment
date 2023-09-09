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
    console.log(firstName, lastName, mobileNumber, children);
    const data = await apiCall("Users/Create", RequestMethod.POST, {
      firstName,
      lastName,
      mobileNumber,
      children,
    });
    return data;
  };

  return { getUsers, createUser };
};

export default UserRepository;
