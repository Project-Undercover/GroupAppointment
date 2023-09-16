import { StyleSheet, Image, View, TouchableOpacity } from "react-native";
import React, { useMemo } from "react";
import TextComponent from "../../../components/TextComponent";
import { Feather } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import ExternalActions from "../../../../actions/ExternalActions";
import { useNavigation } from "@react-navigation/native";
import { Mode } from "../../../../utils/Enums";
const UserCard = ({ user }) => {
  const navigation = useNavigation();
  const externalActions = ExternalActions();
  const handleOpenWhatsApp = () => {
    externalActions.openWhatsApp({ phoneNumber: user?.mobileNumber });
  };
  const handleCall = () => {
    externalActions.callPhone({ phoneNumber: user?.mobileNumber });
  };

  const handleNavigateEditUser = () => {
    navigation.navigate("user-manager", {
      mode: Mode.Edit,
      user: user,
    });
  };

  const UserStatusColor = useMemo(() => {
    const color = user?.isActive ? theme.COLORS.green : theme.COLORS.red;
    return color;
  }, [user]);

  return (
    <TouchableOpacity
      style={styles.container}
      activeOpacity={0.6}
      onPress={handleNavigateEditUser}
    >
      <View className="flex-row items-center">

        <View>
          <View
            style={[styles.point, , { backgroundColor: UserStatusColor, position: 'absolute', zIndex: 2 }]}
          ></View>
          <Image
            style={styles.userImage}
            source={require("../../../../assets/icons/user.png")}
          />
        </View>

        <View className="mx-3">
          <TextComponent mediumBold>
            {user?.firstName + " " + user?.lastName}
          </TextComponent>
          <TextComponent mediumBold>{user?.mobileNumber}</TextComponent>
        </View>
      </View>

      <View className="flex-row items-center gap-2">
        <TouchableOpacity onPress={handleCall}>
          <Feather name="phone-call" size={20} color={theme.COLORS.green2} />
        </TouchableOpacity>
        <TouchableOpacity onPress={handleOpenWhatsApp}>
          <Image
            style={styles.icon}
            source={require("../../../../assets/icons/whatsapp.png")}
          />
        </TouchableOpacity>
      </View>
    </TouchableOpacity>
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
  point: {
    width: 10,
    height: 10,
    marginHorizontal: 5,
    borderRadius: 30,
    ...theme.SHADOW.lightShadow,
  },
});
