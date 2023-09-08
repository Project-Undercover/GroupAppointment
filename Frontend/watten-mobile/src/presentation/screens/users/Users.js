import { StyleSheet, View, TouchableOpacity } from "react-native";
import React from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import AppHeader from "../../components/AppHeader";
import TextComponent from "../../components/TextComponent";
import globalStyles from "../../../utils/theme/globalStyles";
import DefaultInput from "../../components/DefaultInput";
import { Feather } from "@expo/vector-icons";
import theme from "../../../utils/theme";
import Spacer from "../../components/Spacer";
import UsersList from "./components/UsersList";
import { AntDesign } from "@expo/vector-icons";
import UsersBar from "./components/UsersBar";
const Users = ({ navigation }) => {
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <AppHeader />
      <View className="flex-1 px-4 py-5 ">
        <UsersBar navigation={navigation} />

        <Spacer space={5} />
        <DefaultInput
          placeholder={"Search"}
          icon={
            <Feather name="search" color={theme.COLORS.primary} size={20} />
          }
        />
        <Spacer space={8} />
        <UsersList />
      </View>
    </View>
  );
};

export default Users;

const styles = StyleSheet.create({});
