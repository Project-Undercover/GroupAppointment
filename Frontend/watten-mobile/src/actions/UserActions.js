import UserRepository from "../repository/UserRepository";
import { useAlertsContext } from "../hooks/useAlertsContext";
import {
  CREATE_USER_SUCCESS,
  FETCH_USERS_SUCCESS,
  FETCH_USER_PROFILE_SUCCESS,
  FILTER_USERS,
  FETCH_USER_HOME_DATA_SUCCESS,
  FETCH_USER_CHILDREN_SUCCESS,
  FETCH_ROLES_SUCCESS,
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
        showSuccess(response?.message);
        NavigateUsers();
      } catch (error) {
        console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const fetchChildren = () => {
    return async (dispatch) => {
      try {
        const response = await userRepository.getChildren();
        dispatch({
          type: FETCH_USER_CHILDREN_SUCCESS,
          payload: response?.data,
        });
      } catch (error) {
        console.log(error);
      }
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

  const fetchUserHomeData = (startDate) => {
    return async (dispatch) => {
      setLoading(true);

      try {
        const response = await userRepository.getUserHomeData({ startDate });
        dispatch({
          type: FETCH_USER_HOME_DATA_SUCCESS,
          payload: response?.data,
        });
      } catch (error) {
        console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const editUser = ({
    id,
    firstName,
    lastName,
    children,
    mobileNumber,
    isActive,
    role,
  }) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await userRepository.editUser({
          id,
          firstName,
          lastName,
          children,
          mobileNumber,
          isActive,
          role,
        });
        showSuccess(response?.message);
        NavigateUsers();
      } catch (error) {
        console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const fetchRoles = () => {
    return async (dispatch) => {
      try {
        const response = await userRepository.getRoles();
        dispatch({ type: FETCH_ROLES_SUCCESS, payload: response?.data });
      } catch (error) {
        console.log(error);
      }
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
  return {
    fetchUsers,
    filterUsers,
    createUser,
    fetchProfile,
    fetchUserHomeData,
    fetchChildren,
    editUser,
    fetchRoles,
  };
};

export default UserActions;
