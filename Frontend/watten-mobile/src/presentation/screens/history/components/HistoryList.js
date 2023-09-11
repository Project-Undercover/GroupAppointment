import { View, Text, StyleSheet, RefreshControl, FlatList } from "react-native";
import React from "react";
import Spacer from "../../../components/Spacer";
import theme from "../../../../utils/theme";
import ReservationCard from "../../home/components/ReservationCard";
const HistoryList = ({ data, handleRefreshSessions, loading }) => {
  return (
    <View className="flex-1 p-2">
      <Spacer space={4} />
      <FlatList
        onEndReachedThreshold={0.5}
        style={{ flex: 1 }}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={{
          paddingBottom: 100,
          alignItems: "flex-start",
          justifyContent: "center",
          padding: 5,
        }}
        ItemSeparatorComponent={<Spacer space={6} />}
        data={loading ? [] : data}
        keyExtractor={(item) => item?.id}
        refreshControl={
          <RefreshControl
            colors={[theme.COLORS.primary]}
            onRefresh={() => {
              handleRefreshSessions();
            }}
          />
        }
        renderItem={({ item }) => (
          <ReservationCard session={item} isHistory={true} />
        )}
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

export default HistoryList;
