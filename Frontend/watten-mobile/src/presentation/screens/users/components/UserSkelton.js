import { StyleSheet, Text, View } from "react-native";
import React from "react";
import CustomeSkeleton from "../../../components/CustomeSkeleton";
const UserSkelton = ({}) => {
  return (
    <View className="">
      <View className="flex-row items-center ">
        <CustomeSkeleton
          customeStyle={{ width: 50, height: 50, borderRadius: 30 }}
        />
        <View className="mx-5">
          <CustomeSkeleton
            customeStyle={{ width: 120, height: 14, borderRadius: 10 }}
          />
          <CustomeSkeleton
            customeStyle={{ width: 120, height: 14, borderRadius: 10 }}
          />
        </View>
      </View>
      <CustomeSkeleton
        customeStyle={{ width: "100%", height: 2, borderRadius: 10 }}
      />
    </View>
  );
};

export default UserSkelton;
