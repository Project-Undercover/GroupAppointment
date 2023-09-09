import { StyleSheet, View } from "react-native";
import React from "react";
import InfoRow from "../../../components/InfoRow";
import TextComponent from "../../../components/TextComponent";
import moment from "moment";
import {
  AntDesign,
  Ionicons,
  Feather,
  MaterialCommunityIcons,
} from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import Spacer from "../../../components/Spacer";
import ChildList from "../../../components/ChildList/ChildList";
const ReservationInfo = () => {
  return (
    <View className="flex-1">
      <View style={styles.infoContainer}>
        <InfoRow
          icon={
            <Ionicons
              name="md-calendar-sharp"
              size={17}
              color={theme.COLORS.primary}
            />
          }
          value={moment().format("LL")}
        />
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

        <View className="flex-row">
          <MaterialCommunityIcons
            name="human-male-child"
            color={theme.COLORS.primary}
            size={19}
          />
          <Spacer space={5} />
          <ChildList data={[]} />
        </View>
      </View>
    </View>
  );
};

export default ReservationInfo;

const styles = StyleSheet.create({
  infoContainer: {
    gap: 6,
    justifyContent: "flex-start",
    alignItems: "flex-start",
  },
});
