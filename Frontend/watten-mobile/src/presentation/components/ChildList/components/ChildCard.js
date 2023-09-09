import { StyleSheet, Text, View } from "react-native";
import React from "react";
import TextComponent from "../../TextComponent";

import theme from "../../../../utils/theme";
const ChildCard = ({ value }) => {
  return (
    <View style={styles.container}>
      <TextComponent>{value}</TextComponent>
    </View>
  );
};

export default ChildCard;

const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    alignItems: "center",
    paddingHorizontal: 14,
    marginHorizontal: 2,
    paddingVertical: 3,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
  },
});
