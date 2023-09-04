import { View, StyleSheet } from "react-native";
import React from "react";
import TextComponent from "../TextComponent";
import InfoRow from "../InfoRow";
import { AntDesign, Ionicons, Feather } from "@expo/vector-icons";
import theme from "../../../utils/theme";
import Spacer from "../Spacer";
import Status from "./Status";
const SessionInfo = () => {
  return (
    <View style={styles.container}>
      <View className="flex-row justify-between">
        <TextComponent mediumBold style={styles.infoTitle}>
          Title of the session
        </TextComponent>
        <Status status={"Available"} />
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
          value={"11:30 - 11:45"}
        />
        <InfoRow
          icon={
            <Ionicons
              name="location-outline"
              size={17}
              color={theme.COLORS.primary}
            />
          }
          value={"Bartaa Basma est"}
        />
        <InfoRow
          icon={<Feather name="user" color={theme.COLORS.primary} size={17} />}
          value={"Sabreen arar"}
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
