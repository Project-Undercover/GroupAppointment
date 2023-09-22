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
import ParticipantsBottomSheet from "../../components/ParticipantsBottomSheet/ParticipantsBottomSheet";
import ImageExpandModal from "../../components/Modals/ImageExpandModal";
import ListSkeltons from "../../components/ListSkeltons";

const Sessions = () => {
  const [date, setDate] = useState(moment());
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showImageModal, setShowImageModal] = useState(false);

  const [currentSuccessTitle, setCurrentSuccessTitle] = useState("");
  const { loadingSkelton } = useLoadingContext();
  const { sessions } = useSelector((state) => state.sessions);
  const { userChildren } = useSelector((state) => state.users);

  const sessionActions = SessionActions();
  const userActions = UserActions();
  const [showSessionSheet, setShowSessionSheet] = useState(false);
  const [showPartSheet, setShowPartSheet] = useState(false);
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
  const handleShowPartSheet = () => {
    setShowPartSheet((prev) => !prev);
  };

  const handleCloseSessionSheet = () => {
    setShowSessionSheet(false);
  };

  const handleShowSuccessModal = (title) => {
    handleRefreshSessions();
    setCurrentSuccessTitle(title);
    setShowSuccessModal(true);
  };
  const handleExpandImage = (session) => {
    setCurrentSession(session);
    setShowImageModal(true);
  };
  return (
    <>
      <View className="flex-1">
        <CustomeStatusBar />
        <AppHeader />
        <View className="flex-1">
          <DatePickerStrip date={date} handleChangeDate={handleChangeDate} />
          <AddSessionBar date={date} />
          {loadingSkelton ? (
            <ListSkeltons
              numOfItems={20}
              itemWidth={"100%"}
              itemHeight={130}
              itemRadius={5}
            />
          ) : null}

          {sessions?.length === 0 && !loadingSkelton ? (
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
              data={loadingSkelton ? [] : sessions}
              loading={loadingSkelton}
              handlePressSession={handlePressSession}
              handleExpandImage={handleExpandImage}
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
            handleShowPartSheet={handleShowPartSheet}
          />
        ) : null}
        {showPartSheet ? (
          <ParticipantsBottomSheet
            handleShowSheet={handleShowPartSheet}
            sessionId={currentSession?.id}
          />
        ) : null}
      </View>
      <SuccessModal
        visible={showSuccessModal}
        message={currentSuccessTitle}
        // title={currentSuccessTitle}
        setShowModal={setShowSuccessModal}
      />
      <ImageExpandModal
        visible={showImageModal}
        image={currentSession?.image}
        setShowModal={setShowImageModal}
      />
    </>
  );
};

export default Sessions;

const styles = StyleSheet.create({});
