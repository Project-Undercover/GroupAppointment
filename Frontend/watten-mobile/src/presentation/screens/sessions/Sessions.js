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
import { useLoadingContext } from "../../../hooks/useLoadingContext";
import globalStyles from "../../../utils/theme/globalStyles";
import SessionBottomSheet from "../../components/SessionBottomSheet/SessionBottomSheet";
import UserActions from "../../../actions/UserActions";
import SuccessModal from "../../components/Modals/SuccessModal";

const Sessions = () => {
  const [date, setDate] = useState(moment());
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [currentSuccessTitle, setCurrentSuccessTitle] = useState("");
  const { loading } = useLoadingContext();
  const { sessions } = useSelector((state) => state.sessions);
  const { userChildren } = useSelector((state) => state.users);
  const sessionActions = SessionActions();
  const userActions = UserActions();
  const [showSessionSheet, setShowSessionSheet] = useState(false);
  const [currentSession, setCurrentSession] = useState(null);
  const dispatch = useDispatch();
  const { t } = useTranslation();

  const handleChangeDate = (date) => {
    fetchSessions(date);
    setDate(date);
  };

  useFocusEffect(
    useCallback(() => {
      fetchSessions(moment());
      fetchChildren();
      setDate(moment());
    }, [])
  );

  const fetchSessions = (date) => {
    const interval = timeInterval(date?.locale("he"));
    dispatch(sessionActions.fetchSessions({ ...interval }));
  };
  const fetchChildren = () => {
    dispatch(userActions.fetchChildren());
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

  const handlePressSession = (session) => {
    setCurrentSession(session);
    setShowSessionSheet(true);
  };

  const handleCloseSessionSheet = () => {
    setShowSessionSheet(false);
  };

  const handleShowSuccessModal = (title) => {
    handleRefreshSessions();
    setCurrentSuccessTitle(title);
    setShowSuccessModal(true);
  };
  return (
    <>
      <View className="flex-1">
        <CustomeStatusBar />
        <AppHeader />
        <View className="flex-1">
          <DatePickerStrip date={date} handleChangeDate={handleChangeDate} />
          <AddSessionBar date={date} />
          {sessions?.length === 0 && !loading ? (
            <View className="flex-1 items-center mt-10">
              <Image
                className="w-20 h-20"
                source={require("../../../assets/icons/empty.png")}
              />
              <TextComponent style={globalStyles.noSessionsText}>
                {t("no_sessions")}
              </TextComponent>
            </View>
          ) : (
            <SessionList
              data={sessions}
              loading={loading}
              handlePressSession={handlePressSession}
              handleRefreshSessions={handleRefreshSessions}
            />
          )}
        </View>
        {showSessionSheet ? (
          <SessionBottomSheet
            handleShowSheet={handleCloseSessionSheet}
            session={currentSession}
            userChildren={userChildren}
            handleShowSuccessModal={handleShowSuccessModal}
          />
        ) : null}
      </View>
      <SuccessModal
        visible={showSuccessModal}
        message={currentSuccessTitle}
        // title={currentSuccessTitle}
        setShowModal={setShowSuccessModal}
      />
    </>
  );
};

export default Sessions;

const styles = StyleSheet.create({});
