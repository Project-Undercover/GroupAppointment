import { View, Text, StyleSheet, RefreshControl, FlatList } from "react-native";
import React from "react";
import SessionCard from "./SessionCard/SessionCard";
import Spacer from "./Spacer";
import theme from "../../utils/theme";
const SessionList = () => {
  const list = [{ id: 1 }, { id: 2 }, { id: 3 }, { id: 4 }, { id: 5 }];

  return (
    <View className="flex-1">
      <Spacer space={4} />
      <FlatList
        onEndReachedThreshold={0.5}
        style={{ flex: 1 }}
        // columnWrapperStyle={{ justifyContent: "space-between", gap: 30 }}

        showsVerticalScrollIndicator={false}
        contentContainerStyle={{
          paddingBottom: 100,

          alignItems: "flex-start",
          justifyContent: "center",
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
        renderItem={({ item }) => <SessionCard />}
      />
    </View>
  );
};
const styles = StyleSheet.create({
  container: {
    padding: 10,
    // flex: 1,
  },
});

export default SessionList;
