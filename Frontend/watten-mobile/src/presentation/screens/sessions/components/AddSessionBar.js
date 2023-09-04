import { View, StyleSheet, TouchableOpacity } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import { AntDesign } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import { useNavigation } from "@react-navigation/native";
import { SessionMode } from "../../../../utils/Enums";

const AddSessionBar = ({ date }) => {
  const navigation = useNavigation();
  const handlePressBar = () => {
    navigation.navigate("session-manager", {
      mode: SessionMode.ADD,
      date: date?.format(),
    });
  };

  return (
    <TouchableOpacity
      style={styles.container}
      activeOpacity={0.8}
      onPress={handlePressBar}
    >
      <TextComponent style={styles.text} mediumBold>
        Add new session
      </TextComponent>
      <TouchableOpacity activeOpacity={0.5} onPress={handlePressBar}>
        <AntDesign name="plussquare" size={26} color={theme.COLORS.primary} />
      </TouchableOpacity>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    backgroundColor: theme.COLORS.white,
    padding: 10,
    margin: 5,
    marginTop: 20,
    borderRadius: 5,
    ...theme.SHADOW.lightShadow,
  },
  text: {
    fontSize: 15,
    color: theme.COLORS.secondaryPrimary,
  },
});
export default AddSessionBar;
