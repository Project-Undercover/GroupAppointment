import moment from "moment";
import i18next from "../utils/i18n";
import AuthRepository from "../repository/AuthRepository";
import {
  LOGIN_REQUEST,
  LOGIN_FAILURE,
  LOGIN_SUCCESS,
  VERIFY_SUCCESS,
  LOGOUT_SUCCESS,
  CHANGE_LANG_SUCCESS,
  USER_IN_STORAGE_SUCCESS,
} from "../constants/actionTypes";
import { useNavigation } from "@react-navigation/native";
import { useAlertsContext } from "../hooks/useAlertsContext";
import { useTranslation } from "react-i18next";
import "moment/locale/ar";
import "moment/locale/he";
// import * as Updates from "expo-updates";

import {
  cleanData,
  getDataFromStorage,
  getLanguageFromStorage,
  storeData,
  storeLanguage,
} from "../utils/storage";
const AuthActions = () => {
  const authRepository = AuthRepository();
  const navigation = useNavigation();
  const { t } = useTranslation();
  const { showSuccess, showError } = useAlertsContext();
  const login = (phone) => {
    return async (dispatch) => {
      dispatch({ type: LOGIN_REQUEST });
      try {
        const response = await authRepository.login({
          phone,
        });
        dispatch({
          type: LOGIN_SUCCESS,
          payload: { requestId: response?.data?.verificationId, phone: phone },
        });
      } catch (error) {
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
        dispatch({ type: LOGIN_FAILURE, payload: error?.data });
      }
    };
  };

  const verifyCode = (code) => {
    return async (dispatch, getState) => {
      dispatch({ type: LOGIN_REQUEST });
      const requestId = getState()?.auth?.requestId;
      try {
        const response = await authRepository.verifyCode({
          requestId,
          code,
        });
        dispatch({
          type: VERIFY_SUCCESS,
          payload: response?.data,
        });
        setTimeout(() => {
          // showSuccess(t("verify_success"));
          NavigateHome();
        }, 1000);
      } catch (error) {
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
        dispatch({ type: LOGIN_FAILURE, payload: error?.data });
      }
    };
  };

  const changeLanguage = (lang) => {
    return async (dispatch) => {
      if (i18next.language === lang) return;

      i18next
        .changeLanguage(lang)
        .then(() => {
          console.log("language changed successfully", lang);
          // moment.locale(lang);
          storeLanguage(lang);
          dispatch({ type: CHANGE_LANG_SUCCESS, payload: lang });
        })
        .catch((error) => {
          console.log(error);
        });
    };
  };

  const checkLanguageInStorage = () => {
    return async (dispatch) => {
      const lang = await getLanguageFromStorage();

      dispatch(changeLanguage(lang));
    };
  };

  const checkUserInStorage = () => {
    return async (dispatch) => {
      try {
        const authData = await getDataFromStorage();
        const currentDate = moment();
        const tokenIsValid = moment(authData?.user?.expiresAt).isAfter(
          currentDate
        );
        if (tokenIsValid && authData?.user && authData?.token) {
          dispatch({
            type: USER_IN_STORAGE_SUCCESS,
            payload: authData?.user,
          });
          NavigateHome();
        } else {
          NavigateEntry();
        }
      } catch (error) {
        console.log(error);
        NavigateEntry();
      }
    };
  };

  const NavigateEntry = () => {
    navigation.reset({
      index: 0,
      routes: [{ name: "entry" }],
    });
  };

  const NavigateHome = () => {
    navigation.reset({
      index: 0,
      routes: [{ name: "app-drawer" }],
    });
  };

  const logout = () => {
    return async (dispatch) => {
      dispatch({ type: LOGOUT_SUCCESS });
      await cleanData();
      NavigateEntry();
    };
  };
  const backToLogin = () => {
    return async (dispatch) => {
      dispatch({ type: LOGOUT_SUCCESS });
    };
  };
  return {
    checkUserInStorage,
    changeLanguage,
    login,
    verifyCode,
    backToLogin,
    logout,
    checkLanguageInStorage,
  };
};

export default AuthActions;
