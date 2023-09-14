import { StyleSheet, TouchableOpacity, View } from "react-native";
import React from "react";
import TextComponent from "../../TextComponent";
import { AntDesign } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
const ChildCard = ({ value, showCloseChildIcon, handleRemoveChild }) => {
  return (
    <View style={styles.container}>
      {showCloseChildIcon ? (
        <TouchableOpacity style={styles.close} onPress={handleRemoveChild}>
          <AntDesign
            name="closecircleo"
            size={15}
            color={theme.COLORS.primary}
          />
        </TouchableOpacity>
      ) : null}
      <TextComponent>{value}</TextComponent>
    </View>
  );
};

export default ChildCard;

const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    flexDirection: "row",
    alignItems: "center",
    paddingHorizontal: 14,
    marginHorizontal: 2,
    paddingVertical: 3,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
    position: "relative",
  },
  close: {
    marginHorizontal: 2,
  },
});
