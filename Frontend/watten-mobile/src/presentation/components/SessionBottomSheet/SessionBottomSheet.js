import React, { useCallback, useMemo, useRef, useState } from "react";
import { View, Image, StyleSheet, TouchableOpacity } from "react-native";
import BottomSheet, { BottomSheetBackdrop } from "@gorhom/bottom-sheet";
import TextComponent from "../TextComponent";
import theme from "../../../utils/theme";
import Header from "./components/Header";
import ReservationInfo from "../../screens/home/components/ReservationInfo";
import DefaultButton from "../DefaultButton";
import { useTranslation } from "react-i18next";
import { useDispatch, useSelector } from "react-redux";
import SessionActions from "../../../actions/SessionActions";
import SuccessModal from "../Modals/SuccessModal";
import { Feather } from "@expo/vector-icons";
import { Mode, UserRoles } from "../../../utils/Enums";
import { useNavigation } from "@react-navigation/native";

const SessionBottomSheet = ({
  session,
  userChildren,
  handleShowSuccessModal,
  handleShowPartSheet = () => {},
  handleShowSheet = () => {},
}) => {
  const dispatch = useDispatch();
  const [selectedChildren, setSelectedChildren] = useState(userChildren);
  const sessionActions = SessionActions();
  const role = useSelector((state) => state.auth.user.role);
  const { t } = useTranslation();
  const navigation = useNavigation();
  const bottomSheetModalRef = useRef(null);

  const snapPoints = useMemo(() => ["50%"], []);

  const handleSheetChanges = useCallback((index) => {}, []);
  const renderBackdrop = useCallback(
    (props) => (
      <BottomSheetBackdrop
        {...props}
        disappearsOnIndex={-1}
        appearsOnIndex={0}
      />
    ),
    []
  );

  const handleRemoveChild = (id) => {
    if (selectedChildren?.length <= 1) return;

    const filteredData = selectedChildren?.filter((child) => child.id !== id);
    setSelectedChildren(filteredData);
  };

  const handleBookSession = () => {
    const _children = selectedChildren.map((child) => child.id);
    dispatch(
      sessionActions.bookSession({
        children: _children,
        sessionId: session?.id,
        handleShowSuccess: () =>
          handleShowSuccessModal(t("booked_success") + "-" + session?.title),
      })
    );
    handleShowSheet();
  };

  const handleUnbookSession = () => {
    dispatch(
      sessionActions.unBookSession({
        sessionId: session?.id,
        handleShowSuccess: () =>
          handleShowSuccessModal(t("unbooked_success") + "-" + session?.title),
      })
    );
    handleShowSheet();
  };
  const IsParticipating = () => {
    return session?.isParticipating;
  };

  const ButtonAction = () => {
    return IsParticipating() ? handleUnbookSession : handleBookSession;
  };

  const ButtonText = () => {
    return IsParticipating() ? t("unbook") : t("book_now");
  };

  const handleNavigateEditSession = () => {
    navigation.navigate("session-manager", {
      mode: Mode.Edit,
      session: session,
    });
    handleShowSheet();
  };

  return (
    <BottomSheet
      backdropComponent={renderBackdrop}
      ref={bottomSheetModalRef}
      index={0}
      snapPoints={snapPoints}
      onClose={handleShowSheet}
      enablePanDownToClose
      onChange={handleSheetChanges}
    >
      <View style={styles.contentContainer}>
        <Header title={session?.title} onClose={handleShowSheet} />
        <View className="flex-row items-center justify-between  w-full">
          <View className="flex-row items-center gap-2">
            <Image
              style={styles.image}
              source={require("../../../assets/icons/user.png")}
            />
            <TextComponent style={styles.text}>
              {session?.instructor}
            </TextComponent>
          </View>
          {role === UserRoles.Admin ? (
            <View className="flex-row gap-2">
              <TouchableOpacity
                style={styles.buttonAction}
                onPress={handleNavigateEditSession}
              >
                <Feather name="edit" size={18} color={theme.COLORS.primary} />
              </TouchableOpacity>
              <TouchableOpacity
                style={styles.buttonAction}
                onPress={handleShowPartSheet}
              >
                <Feather name="users" size={18} color={theme.COLORS.primary} />
              </TouchableOpacity>
            </View>
          ) : null}
        </View>
        <View style={styles.spreator}></View>
        <View className="w-full">
          <ReservationInfo
            instructor={session?.instructor}
            startTime={session?.startDate}
            endTime={session?.endDate}
            children={selectedChildren}
            showCloseChildIcon={
              !IsParticipating() && selectedChildren?.length > 1
            }
            locationName={session?.locationName}
            handleRemoveChild={handleRemoveChild}
          />
        </View>
        <View className="items-center w-full my-5">
          <DefaultButton
            text={ButtonText()}
            onPress={ButtonAction()}
            containerStyle={
              IsParticipating() && {
                backgroundColor: "black",
              }
            }
          />
        </View>
      </View>
    </BottomSheet>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: "rgba(0, 0, 0, 0.1)",
    justifyContent: "center",
    alignItems: "center",

    ...theme.SHADOW.lightShadow,
  },
  contentContainer: {
    paddingHorizontal: 14,
    paddingTop: 14,
    alignItems: "flex-start",
  },
  text: {
    color: theme.COLORS.black,
    fontSize: 15,
  },
  image: {
    width: 50,
    height: 50,
  },
  spreator: {
    height: 1,
    width: "100%",
    backgroundColor: theme.COLORS.primary,
    marginVertical: 5,
    marginBottom: 10,
  },
  buttonAction: {
    backgroundColor: "white",
    borderRadius: 5,
    alignItems: "center",
    justifyContent: "center",
    padding: 2,
    ...theme.SHADOW.lightShadow,
  },
});

export default SessionBottomSheet;
