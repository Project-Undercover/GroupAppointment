import { View, Image, SafeAreaView, TouchableOpacity } from "react-native";
import React from "react";
import { Ionicons, Entypo } from "@expo/vector-icons";
import TextComponent from "../../../components/TextComponent";

const Header = () => {
  return (
    <View>
      <SafeAreaView />
      <View className="justify-between flex-row items-center px-4 py-3">
        <View className="flex-row gap-4">
          <TouchableOpacity>
            <Entypo name="menu" size={26} color="black" />
          </TouchableOpacity>
          <TouchableOpacity>
            <Ionicons name="notifications" size={26} color="black" />
          </TouchableOpacity>
        </View>
        <Image
          className="w-28 h-16"
          source={require("../../../../assets/imgs/logo.png")}
        />
      </View>
    </View>
  );
};

export default Header;
