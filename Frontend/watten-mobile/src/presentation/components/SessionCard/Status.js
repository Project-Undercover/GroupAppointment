import { StyleSheet, Text, View } from "react-native";
import React from "react";
import TextComponent from "../TextComponent";
import theme from "../../../utils/theme";
const Status = ({ status }) => {
  return (
    <View style={styles.container}>
      <TextComponent mediumBold style={styles.text}>
        {status}
      </TextComponent>
    </View>
  );
};

export default Status;

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 20,
    paddingVertical: 3,
    backgroundColor: theme.COLORS.lightGreen,
    alignItems: "center",
    justifyContent: "center",
    borderRadius: 4,
  },
  text: {
    color: theme.COLORS.green,
    fontSize: 10,
    padding: 2,
  },
});
