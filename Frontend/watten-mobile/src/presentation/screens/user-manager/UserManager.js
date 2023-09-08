import {
  StyleSheet,
  TouchableOpacity,
  View,
  ScrollView,
  LayoutAnimation,
} from "react-native";
import { useMemo, useState } from "react";
import { Mode } from "../../../utils/Enums";
import DefaultHeader from "../../components/DefaultHeader";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DefaultInput from "../../components/DefaultInput";
import { Feather, AntDesign } from "@expo/vector-icons";
import theme from "../../../utils/theme";
import Spacer from "../../components/Spacer";
import TextComponent from "../../components/TextComponent";
import InputItem from "./components/InputItem";
const UserManager = ({ route }) => {
  const { mode } = route.params;

  const ManagerTitle = useMemo(() => {
    return mode === Mode.Add ? "Add User" : "Edit User";
  }, [mode]);
  const [inputItems, setInputItems] = useState([]);

  const handlePressAddChild = () => {
    const newItem = {
      id: Date.now(),
    };
    setInputItems([...inputItems, newItem]);

    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
  };

  const handleRemoveInput = (id) => {
    setInputItems((prev) => prev.filter((item) => item.id !== id));
  };
  const handleChangeInput = (id, value) => {
    setInputItems((prevItems) =>
      prevItems.map((item) =>
        item.id === id ? { ...item, data: value } : item
      )
    );
  };
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={ManagerTitle} />
      <Spacer space={20} />
      <ScrollView
        className="flex-1 px-5"
        contentContainerStyle={{ paddingBottom: 100 }}
      >
        <View className="flex-row">
          <DefaultInput
            placeholder={"Enter First name"}
            label="First name"
            wrapperStyle={{ width: "50%" }}
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />
          <Spacer space={3} />
          <DefaultInput
            placeholder={"Enter last name"}
            label="Last name"
            wrapperStyle={{ width: "50%" }}
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />
        </View>
        <Spacer space={15} />
        <DefaultInput
          placeholder={"Enter phone number"}
          label="Phone number"
          icon={<Feather name="phone" color={theme.COLORS.primary} size={20} />}
        />
        <Spacer space={15} />
        <TouchableOpacity
          className="flex-row items-center gap-2"
          onPress={handlePressAddChild}
        >
          <AntDesign name="plussquare" size={26} color={theme.COLORS.primary} />
          <TextComponent style={styles.addText}>Add child</TextComponent>
        </TouchableOpacity>
        <Spacer space={15} />
        <View style={{ flex: 1, gap: 10 }}>
          {inputItems.map((item) => (
            <InputItem
              key={item.id}
              handleRemoveInput={() => handleRemoveInput(item.id)}
              handleChangeInput={() => handleChangeInput(item.id)}
            />
          ))}
        </View>
      </ScrollView>
    </View>
  );
};

export default UserManager;

const styles = StyleSheet.create({
  addText: {
    fontSize: 17,
  },
});
