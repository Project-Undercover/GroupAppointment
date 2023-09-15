import { StyleSheet, TouchableOpacity, View } from "react-native";
import React from "react";
import { AntDesign, MaterialCommunityIcons } from "@expo/vector-icons";
import DefaultInput from "../../../components/DefaultInput";
import theme from "../../../../utils/theme";
const InputItem = ({ id, value, handleRemoveInput, handleChangeInput, t }) => {
  return (
    <View className="flex-row items-center my-2">
      <TouchableOpacity className=" mt-6" onPress={handleRemoveInput}>
        <AntDesign name="minus" size={30} color={theme.COLORS.secondary2} />
      </TouchableOpacity>
      <DefaultInput
        label={t("name")}
        placeholder={t("enter") + " " + t("child_name")}
        wrapperStyle={{ width: "90%" }}
        value={value}
        onChange={(value) => handleChangeInput(id, value)}
        icon={
          <MaterialCommunityIcons
            name="human-child"
            size={24}
            value={value}
            color={theme.COLORS.secondary2}
          />
        }
      />
    </View>
  );
};

export default InputItem;

const styles = StyleSheet.create({});
