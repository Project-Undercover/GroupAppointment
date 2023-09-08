import apiCall from "../network/apiCall";
import { RequestMethod } from "../utils/Enums";

const AuthRepository = () => {
  const login = async ({ phone }) => {
    const data = await apiCall("Auth/Login", RequestMethod.POST, {
      username: phone,
    });

    return data;
  };

  const verifyCode = async ({ code, requestId }) => {
    console.log("----", code, requestId);
    const data = await apiCall("Auth/VerifyCode", RequestMethod.POST, {
      requestId,
      code,
    });

    return data;
  };

  return { login, verifyCode };
};

export default AuthRepository;
