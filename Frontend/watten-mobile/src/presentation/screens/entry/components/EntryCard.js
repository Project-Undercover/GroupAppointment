import { StatusBar, StyleSheet, Text, View } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";
const EntryCard = ({ title, children }) => {
  return (
    <View style={styles.container}>
      <TextComponent style={styles.title} mediumBold>
        {title}
      </TextComponent>
      {children}
    </View>
  );
};

export default EntryCard;

const styles = StyleSheet.create({
  container: {
    width: "89%",
    height: 300,
    borderRadius: 7,
    backgroundColor: theme.COLORS.white,
    ...theme.SHADOW.lightShadow,

    padding: 10,
  },
  title: {
    textAlign: "center",
    fontSize: 32,
  },
});
