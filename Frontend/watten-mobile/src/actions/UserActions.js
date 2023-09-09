import UserRepository from "../repository/UserRepository";

import { useAlertsContext } from "../hooks/useAlertsContext";
import {
  CREATE_USER_SUCCESS,
  FETCH_USERS_SUCCESS,
  FETCH_USER_PROFILE_SUCCESS,
  FILTER_USERS,
} from "../constants/actionTypes";
import { useLoadingContext } from "../hooks/useLoadingContext";
import { useNavigation } from "@react-navigation/native";
import { useTranslation } from "react-i18next";
const UserActions = () => {
  const userRepository = UserRepository();
  const { showSuccess, showError } = useAlertsContext();
  const { setLoading } = useLoadingContext();
  const navigation = useNavigation();
  const { t } = useTranslation();
  const fetchUsers = (customeSearch) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await userRepository.getUsers(customeSearch);
        dispatch({
          type: FETCH_USERS_SUCCESS,
          payload: response?.data,
        });
      } catch (error) {
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const createUser = ({ firstName, lastName, children, mobileNumber }) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await userRepository.createUser({
          firstName,
          lastName,
          children,
          mobileNumber,
        });
        console.log(response);

        NavigateUsers();
        // dispatch({
        //   type: FETCH_USERS_SUCCESS,
        //   payload: response?.data,
        // });
      } catch (error) {
        console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const fetchProfile = () => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await userRepository.getProfile();
        dispatch({ type: FETCH_USER_PROFILE_SUCCESS, payload: response?.data });
      } catch (error) {
        console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };
  const NavigateUsers = () => {
    navigation.goBack();
  };

  const filterUsers = (filter) => {
    return (dispatch) => {
      dispatch({ type: FILTER_USERS, payload: filter });
    };
  };
  return { fetchUsers, filterUsers, createUser, fetchProfile };
};

export default UserActions;
