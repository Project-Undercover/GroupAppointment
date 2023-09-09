import { StyleSheet, Image, View } from "react-native";
import { useState, useCallback, useEffect } from "react";
import AppHeader from "../../components/AppHeader";
import { useFocusEffect } from "@react-navigation/native";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DatePickerStrip from "./components/DatePickerStrip";
import SessionList from "../../components/SessionList";
import AddSessionBar from "./components/AddSessionBar";
import SessionActions from "../../../actions/SessionActions";
import moment from "moment";
import { useDispatch, useSelector } from "react-redux";
import { useTranslation } from "react-i18next";
import TextComponent from "../../components/TextComponent";
import theme from "../../../utils/theme";
const Sessions = () => {
  const [date, setDate] = useState(moment());
  const { sessions } = useSelector((state) => state.sessions);
  const sessionActions = SessionActions();
  const dispatch = useDispatch();
  const { t } = useTranslation();

  const handleChangeDate = (date) => {
    fetchSessions(date);
    setDate(date);
  };

  useFocusEffect(
    useCallback(() => {
      fetchSessions(moment());
      setDate(moment());
    }, [])
  );

  const fetchSessions = (date) => {
    const interval = timeInterval(date?.locale("he"));
    dispatch(sessionActions.fetchSessions({ ...interval }));
  };
  const timeInterval = (date) => {
    const startDate = moment(date)
      .startOf("day")
      ?.format("yyyy-MM-DDTHH:mm:ssZ");

    const endDate = moment(date).endOf("day")?.format("yyyy-MM-DDTHH:mm:ssZ");

    return { startDate, endDate };
  };

  const handleRefreshSessions = () => {
    fetchSessions(date.locale("he"));
  };

  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <AppHeader />
      <View className="flex-1">
        <DatePickerStrip date={date} handleChangeDate={handleChangeDate} />
        <AddSessionBar date={date} />
        {sessions?.length === 0 ? (
          <View className="flex-1 items-center mt-10">
            <Image
              className="w-20 h-20"
              source={require("../../../assets/icons/empty.png")}
            />
            <TextComponent style={styles.noSessionsText}>
              {t("no_sessions")}
            </TextComponent>
          </View>
        ) : (
          <SessionList
            data={sessions}
            handleRefreshSessions={handleRefreshSessions}
          />
        )}
      </View>
    </View>
  );
};

export default Sessions;

const styles = StyleSheet.create({
  noSessionsText: {
    fontSize: 20,
    color: theme.COLORS.red,
  },
});
