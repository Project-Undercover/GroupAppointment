import { StyleSheet, TouchableOpacity, View } from "react-native";
import React from "react";
import { AntDesign, MaterialCommunityIcons } from "@expo/vector-icons";
import DefaultInput from "../../../components/DefaultInput";
import theme from "../../../../utils/theme";
const InputItem = ({ handleRemoveInput, handleChangeInput }) => {
  return (
    <View className="flex-row items-center ">
      <TouchableOpacity className=" mt-6" onPress={handleRemoveInput}>
        <AntDesign name="minus" size={30} color={theme.COLORS.secondary2} />
      </TouchableOpacity>
      <DefaultInput
        label={"Name"}
        placeholder={"Enter child name"}
        wrapperStyle={{ width: "90%" }}
        onChange={handleChangeInput}
        icon={
          <MaterialCommunityIcons
            name="human-child"
            size={24}
            color={theme.COLORS.secondary2}
          />
        }
      />
    </View>
  );
};

export default InputItem;

const styles = StyleSheet.create({});
