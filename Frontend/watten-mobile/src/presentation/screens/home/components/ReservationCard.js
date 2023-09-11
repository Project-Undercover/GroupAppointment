import { StyleSheet, Image, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";
import ReservationInfo from "./ReservationInfo";
import Spacer from "../../../components/Spacer";
import { useTranslation } from "react-i18next";
import moment from "moment";
import { windowWidth } from "../../../../utils/dimensions";
const ReservationCard = ({ session, isHistory }) => {
  const {
    maxParticipants,
    participantsCount,
    startDate,
    endDate,
    locationName,
    instructor,
    title,
    children,
  } = session;
  const { t } = useTranslation();

  return (
    <View style={styles.container}>
      <View className="flex-row justify-between">
        <TextComponent mediumBold style={styles.headerText}>
          {t("instructure") + " - " + session?.instructor}
        </TextComponent>
        <View style={styles.timerContainer}>
          <TextComponent mediumBold style={styles.timerText}>
            {!isHistory
              ? moment(endDate).diff(
                  moment()?.format("yyyy-MM-DDTHH:mm:ssZ"),
                  "hours"
                ) + t("min")
              : t("session_ended")}
          </TextComponent>
        </View>
      </View>
      <Spacer space={7} />
      <View className="flex-row items-start">
        <Image
          className="w-14 h-14"
          source={require("../../../../assets/icons/user.png")}
        />
        <Spacer space={5} />
        <ReservationInfo
          instructor={instructor}
          startTime={startDate}
          endTime={endDate}
          children={children}
          locationName={locationName}
        />
      </View>
    </View>
  );
};

export default ReservationCard;

const styles = StyleSheet.create({
  container: {
    backgroundColor: theme.COLORS.white,
    paddingHorizontal: 20,
    paddingVertical: 20,
    borderRadius: 14,
    width: windowWidth * 0.9,
    alignSelf: "stretch",
    ...theme.SHADOW.lightShadow,
  },
  headerText: {
    fontSize: 16,
  },
  timerText: {
    fontSize: 13,
    color: theme.COLORS.secondaryPrimary,
  },
  timerContainer: {
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 4,
    backgroundColor: theme.COLORS.secondary2,
  },
});
