import SessionRepository from "../repository/SessionRepository";

import { useLoadingContext } from "../hooks/useLoadingContext";
import { useAlertsContext } from "../hooks/useAlertsContext";
import { useTranslation } from "react-i18next";
import { FETCH_SESSIONS_SUCCESS } from "../constants/actionTypes";
const SessionActions = () => {
  const sessionRepository = SessionRepository();
  const { setLoading } = useLoadingContext();
  const { showError, showSuccess } = useAlertsContext();
  const { t } = useTranslation();

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
        // console.log(error?.data);
        const messg = error?.data?.message ? error?.data?.message : t("error");
        showError(messg);
      }
      setLoading(false);
    };
  };

  return { fetchSessions };
};

export default SessionActions;
