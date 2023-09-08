import { StyleSheet, Image, View, TouchableOpacity } from "react-native";
import React from "react";
import TextComponent from "../../../components/TextComponent";
import { Feather } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
const UserCard = () => {
  return (
    <View style={styles.container}>
      <View className="flex-row items-center">
        <Image
          style={styles.userImage}
          source={require("../../../../assets/icons/user.png")}
        />
        <View className="mx-3">
          <TextComponent mediumBold>Wissam kabha</TextComponent>
          <TextComponent mediumBold>0547973441</TextComponent>
        </View>
      </View>

      <View className="flex-row items-center gap-2">
        <TouchableOpacity>
          <Feather name="phone-call" size={20} color={theme.COLORS.green2} />
        </TouchableOpacity>
        <TouchableOpacity>
          <Image
            style={styles.icon}
            source={require("../../../../assets/icons/whatsapp.png")}
          />
        </TouchableOpacity>
      </View>
    </View>
  );
};

export default UserCard;

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "space-between",
    borderBottomWidth: 1,
    borderColor: theme.COLORS.gray1,
    paddingBottom: 10,
  },
  userImage: {
    width: 50,
    height: 50,
  },
  icon: {
    width: 25,
    height: 25,
  },
});
