import { StyleSheet, View } from "react-native";
import { useCallback } from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DefaultHeader from "../../components/DefaultHeader";
import { useTranslation } from "react-i18next";
import { useFocusEffect } from "@react-navigation/native";
import { useDispatch, useSelector } from "react-redux";
import moment from "moment";
import SessionActions from "../../../actions/SessionActions";
import HistoryList from "./components/HistoryList";
const History = () => {
  const { t } = useTranslation();
  const { historySessions } = useSelector((state) => state.sessions);
  const sessionActions = SessionActions();
  const dispatch = useDispatch();
  useFocusEffect(
    useCallback(() => {
      const endDate = moment().startOf("day")?.format("yyyy-MM-DDTHH:mm:ssZ");
      console.log(endDate);
      // fetchSessions(moment());
      dispatch(
        sessionActions.fetchHistorySessions({ startDate: null, endDate })
      );
    }, [])
  );
  const handleRefreshSessions = () => {
    const endDate = moment().startOf("day")?.format("yyyy-MM-DDTHH:mm:ssZ");
    dispatch(sessionActions.fetchHistorySessions({ startDate: null, endDate }));
  };
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={t("history")} />
      <HistoryList
        data={historySessions}
        handleRefreshSessions={handleRefreshSessions}
      />
    </View>
  );
};

export default History;

const styles = StyleSheet.create({});
