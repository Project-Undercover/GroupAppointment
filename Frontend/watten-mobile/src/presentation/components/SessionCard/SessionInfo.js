import { View, StyleSheet } from "react-native";
import React from "react";
import TextComponent from "../TextComponent";
import InfoRow from "../InfoRow";
import { AntDesign, Ionicons, Feather } from "@expo/vector-icons";
import theme from "../../../utils/theme";
import Spacer from "../Spacer";
import Status from "./Status";
const SessionInfo = ({
  t,
  startTime,
  endTime,
  locationName,
  instructor,
  title,
}) => {
  return (
    <View style={styles.container}>
      <View className="flex-row justify-between">
        <TextComponent mediumBold style={styles.infoTitle}>
          {title}
        </TextComponent>
        <Status status={t("available")} />
      </View>
      <Spacer space={5} />
      <View style={styles.colContainer}>
        <InfoRow
          icon={
            <AntDesign
              name="clockcircleo"
              size={17}
              color={theme.COLORS.primary}
            />
          }
          value={startTime + " - " + endTime}
        />
        <InfoRow
          icon={
            <Ionicons
              name="location-outline"
              size={17}
              color={theme.COLORS.primary}
            />
          }
          value={locationName}
        />
        <InfoRow
          icon={<Feather name="user" color={theme.COLORS.primary} size={17} />}
          value={instructor}
        />
      </View>
    </View>
  );
};

export default SessionInfo;

const styles = StyleSheet.create({
  container: {
    justifyContent: "flex-start",
    padding: 10,
    flex: 1,
  },
  infoTitle: {
    fontSize: 14,
  },
  colContainer: {
    gap: 6,
    justifyContent: "flex-start",
    alignItems: "flex-start",
  },
});
