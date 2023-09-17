import SessionRepository from "../repository/SessionRepository";

import { useLoadingContext } from "../hooks/useLoadingContext";
import { useAlertsContext } from "../hooks/useAlertsContext";
import { useTranslation } from "react-i18next";
import {
  FETCH_HISTORY_SUCCESS,
  FETCH_INSTRUCTORS_SUCCESS,
  FETCH_SESSIONS_SUCCESS,
} from "../constants/actionTypes";
import { useNavigation } from "@react-navigation/native";

const SessionActions = () => {
  const sessionRepository = SessionRepository();
  const { setLoading } = useLoadingContext();
  const { showError, showSuccess } = useAlertsContext();
  const { t } = useTranslation();
  const navigation = useNavigation();

  const createSession = (formData) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await sessionRepository.createSession(formData);
        console.log(response);
        showSuccess(response?.message);
        NavigateSession();
      } catch (error) {
        // console.log(error);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const fetchSessions = ({ startDate, endDate }) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await sessionRepository.getSessions({
          startDate,
          endDate,
        });

        dispatch({ type: FETCH_SESSIONS_SUCCESS, payload: response?.data });
      } catch (error) {
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const fetchInstructors = () => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await sessionRepository.getInstructors();
        dispatch({ type: FETCH_INSTRUCTORS_SUCCESS, payload: response?.data });
      } catch (error) {
        console.log(error);
        // const messg = error?.data?.message ? error?.data?.message : t("error");
        // showError(messg);
      }
      setLoading(false);
    };
  };
  const fetchHistorySessions = ({ startDate, endDate }) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await sessionRepository.getUserSessions({
          startDate,
          endDate,
        });
        dispatch({ type: FETCH_HISTORY_SUCCESS, payload: response?.data });
      } catch (error) {
        console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  const bookSession = ({ sessionId, children, handleShowSuccess }) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        console.log(sessionId, children);
        const response = await sessionRepository.addParticipants({
          sessionId,
          children,
        });
        handleShowSuccess();
      } catch (error) {
        console.log(error);
      }
      setLoading(false);
    };
  };

  const unBookSession = ({ sessionId, handleShowSuccess }) => {
    return async (dispatch) => {
      setLoading(true);
      try {
        const response = await sessionRepository.unBookSession({
          sessionId,
        });
        handleShowSuccess();
      } catch (error) {
        console.log(error);
      }
      setLoading(false);
    };
  };

  const NavigateSession = () => {
    navigation.goBack();
  };

  return {
    fetchSessions,
    fetchHistorySessions,
    createSession,
    fetchInstructors,
    bookSession,
    unBookSession,
  };
};

export default SessionActions;
