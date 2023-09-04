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
    width: 57,
    height: 21,
    backgroundColor: theme.COLORS.lightGreen,
    alignItems: "center",
    justifyContent: "center",
    // position: "absolute",
    borderRadius: 4,
    // end: 5,
    // top: 4,
  },
  text: {
    color: theme.COLORS.green,
    fontSize: 10,
  },
});
