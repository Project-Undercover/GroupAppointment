import { StyleSheet, Image, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";
import ReservationInfo from "./ReservationInfo";
import Spacer from "../../../components/Spacer";

const ReservationCard = ({ instructure }) => {
  return (
    <View style={styles.container}>
      <View className="flex-row justify-between">
        <TextComponent mediumBold style={styles.headerText}>
          Instructure - {instructure}
        </TextComponent>
        <View style={styles.timerContainer}>
          <TextComponent mediumBold style={styles.timerText}>
            24 min
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
        <ReservationInfo />
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
    ...theme.SHADOW.lightShadow,
  },
  headerText: {
    fontSize: 16,
  },
  timerText: {
    fontSize: 13,
  },
  timerContainer: {
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 4,
    backgroundColor: theme.COLORS.secondary2,
  },
});
