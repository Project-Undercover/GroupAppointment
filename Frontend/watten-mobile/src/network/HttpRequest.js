import axios, { AxiosError, AxiosResponse } from "axios";
import { API_BASE_URL } from "../constants/appConstants";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { getLanguageFromStorage, getToken } from "../utils/storage";

export default async function HttpRequest(
  url,
  method,
  data,
  contentType = "application/json"
) {
  //   const token = await getToken();
  const token = await getToken();
  const lang = await getLanguageFromStorage();
  return axios
    .request({
      url: url,
      baseURL: API_BASE_URL,
      method: method,
      data: data,
      // cancelToken: cancelToken ? cancelToken() : null,
      headers: {
        "Content-Type": contentType,
        Language: lang,
        Authorization: `Bearer ${token}`,
      },
    })
    .then((result) => {
      return Promise.resolve(result);
    })
    .catch((err) => {
      // console.log(err);
      return Promise.reject(err.response);
    });
}
