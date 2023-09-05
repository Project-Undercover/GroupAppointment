import { StyleSheet, Text, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";

const ProfileInfoCard = ({ title, children }) => {
  return (
    <View style={styles.container}>
      <TextComponent mediumBold style={styles.tileText}>
        {title}
      </TextComponent>
      <View style={styles.spreator}></View>
      {children}
    </View>
  );
};

export default ProfileInfoCard;

const styles = StyleSheet.create({
  container: {
    backgroundColor: theme.COLORS.white,
    borderRadius: 5,
    paddingHorizontal: 10,
    paddingVertical: 10,
    ...theme.SHADOW.lightShadow,
  },
  tileText: {
    paddingVertical: 10,

    fontSize: 15,
    borderColor: theme.COLORS.primary,
  },
  spreator: {
    height: 1,
    backgroundColor: theme.COLORS.primary,
    marginBottom: 10,
  },
});
