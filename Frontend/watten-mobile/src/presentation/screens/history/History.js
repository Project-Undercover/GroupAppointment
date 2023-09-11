import { StyleSheet, View, Image } from "react-native";
import { useCallback } from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DefaultHeader from "../../components/DefaultHeader";
import { useTranslation } from "react-i18next";
import { useFocusEffect } from "@react-navigation/native";
import { useDispatch, useSelector } from "react-redux";
import moment from "moment";
import SessionActions from "../../../actions/SessionActions";
import HistoryList from "./components/HistoryList";
import theme from "../../../utils/theme";
import { useLoadingContext } from "../../../hooks/useLoadingContext";
import TextComponent from "../../components/TextComponent";
import globalStyles from "../../../utils/theme/globalStyles";
const History = () => {
  const { t } = useTranslation();
  const { historySessions } = useSelector((state) => state.sessions);
  const sessionActions = SessionActions();
  const { loading } = useLoadingContext();
  const dispatch = useDispatch();
  useFocusEffect(
    useCallback(() => {
      const endDate = moment().startOf("day")?.format("yyyy-MM-DDTHH:mm:ssZ");
      dispatch(
        sessionActions.fetchHistorySessions({ startDate: null, endDate })
      );
    }, [])
  );
  const handleRefreshSessions = () => {
    const endDate = moment()
      .locale("he")
      .startOf("day")
      ?.format("yyyy-MM-DDTHH:mm:ssZ");
    dispatch(sessionActions.fetchHistorySessions({ startDate: null, endDate }));
  };
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={t("history")} />
      {historySessions?.length === 0 && !loading ? (
        <View className="flex-1 items-center mt-10">
          <Image
            className="w-20 h-20"
            source={require("../../../assets/icons/empty.png")}
          />
          <TextComponent style={globalStyles.noSessionsText}>
            {t("no_history")}
          </TextComponent>
        </View>
      ) : (
        <HistoryList
          data={historySessions}
          loading={loading}
          handleRefreshSessions={handleRefreshSessions}
        />
      )}
    </View>
  );
};

export default History;
