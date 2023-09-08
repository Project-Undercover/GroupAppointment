import { StyleSheet, FlatList, View, RefreshControl } from "react-native";
import React from "react";
import UserCard from "./UserCard";
import Spacer from "../../../components/Spacer";
import theme from "../../../../utils/theme";
const UsersList = () => {
  const list = [{ id: 1 }, { id: 2 }, { id: 3 }, { id: 4 }, { id: 5 }];
  return (
    <View className="flex-1">
      <FlatList
        onEndReachedThreshold={0.5}
        style={{ flex: 1 }}
        // columnWrapperStyle={{ justifyContent: "space-between", gap: 30 }}

        showsVerticalScrollIndicator={false}
        contentContainerStyle={{
          paddingBottom: 100,

          padding: 5,
        }}
        ItemSeparatorComponent={<Spacer space={6} />}
        data={list}
        keyExtractor={(item) => item?.id}
        refreshControl={
          <RefreshControl
            colors={[theme.COLORS.primary]}
            // onRefresh={() => {
            //   handleRefreshOrders();
            // }}
          />
        }
        renderItem={({ item }) => <UserCard />}
      />
    </View>
  );
};

export default UsersList;

const styles = StyleSheet.create({});
