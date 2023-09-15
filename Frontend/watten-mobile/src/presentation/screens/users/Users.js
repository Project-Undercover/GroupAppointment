import { StyleSheet, View } from "react-native";
import React, { useEffect, useCallback, useState } from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import AppHeader from "../../components/AppHeader";
import DefaultInput from "../../components/DefaultInput";
import { Feather } from "@expo/vector-icons";
import theme from "../../../utils/theme";
import Spacer from "../../components/Spacer";
import UsersList from "./components/UsersList";
import UsersBar from "./components/UsersBar";
import { useTranslation } from "react-i18next";
import UserActions from "../../../actions/UserActions";
import { useDispatch, useSelector } from "react-redux";
import { useFocusEffect } from "@react-navigation/native";

const Users = ({ navigation }) => {
  const { t } = useTranslation();
  const { searchedUsers } = useSelector((state) => state.users);
  const [searchInput, setSearchInput] = useState("");
  const dispatch = useDispatch();
  const userActions = UserActions();

  useFocusEffect(
    useCallback(() => {
      dispatch(userActions.fetchUsers());
      dispatch(userActions.fetchRoles());
    }, [])
  );

  const filterUsers = (value) => {
    dispatch(userActions.filterUsers(value));
    setSearchInput(value);
  };
  const handleRefreshUsers = () => {
    dispatch(userActions.fetchUsers());
    clearInput();
  };

  const clearInput = () => {
    setSearchInput("");
  };

  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <AppHeader />
      <View className="flex-1 px-4 py-5 ">
        <UsersBar navigation={navigation} />

        <Spacer space={5} />
        <DefaultInput
          value={searchInput}
          onChange={filterUsers}
          placeholder={t("search_user")}
          icon={
            <Feather name="search" color={theme.COLORS.primary} size={20} />
          }
        />
        <Spacer space={8} />
        <UsersList
          data={searchedUsers}
          handleRefreshUsers={handleRefreshUsers}
        />
      </View>
    </View>
  );
};

export default Users;

const styles = StyleSheet.create({});
