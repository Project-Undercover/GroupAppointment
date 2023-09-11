import { StyleSheet, FlatList, View, RefreshControl } from "react-native";
import React from "react";
import UserCard from "./UserCard";
import Spacer from "../../../components/Spacer";
import theme from "../../../../utils/theme";
import { useLoadingContext } from "../../../../hooks/useLoadingContext";
const UsersList = ({ data, handleRefreshUsers }) => {
  const { loading } = useLoadingContext();
  return (
    <View className="flex-1">
      <FlatList
        onEndReachedThreshold={0.5}
        style={{ flex: 1 }}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={{
          paddingBottom: 100,
          padding: 5,
        }}
        ItemSeparatorComponent={<Spacer space={6} />}
        data={loading ? [] : data}
        keyExtractor={(item) => item?.id}
        refreshControl={
          <RefreshControl
            colors={[theme.COLORS.primary]}
            onRefresh={() => {
              handleRefreshUsers();
            }}
          />
        }
        renderItem={({ item }) => <UserCard user={item} />}
      />
    </View>
  );
};

export default UsersList;

const styles = StyleSheet.create({});
