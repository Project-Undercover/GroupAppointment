import { StyleSheet, TouchableOpacity } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";

const ProfileInfoRow = ({
  icon,
  value = "",
  selectInput,
  disabled = true,
  onPress,
}) => {
  return (
    <TouchableOpacity
      disabled={disabled}
      onPress={onPress}
      style={styles.container}
      activeOpacity={0.7}
    >
      {selectInput}
      {icon}
      <TextComponent>{value}</TextComponent>
    </TouchableOpacity>
  );
};

export default ProfileInfoRow;

const styles = StyleSheet.create({
  container: {
    borderRadius: 5,
    flexDirection: "row",
    padding: 10,
    gap: 5,
    alignItems: "center",
    backgroundColor: theme.COLORS.lightGray2,
    marginVertical: 7,
  },
});
