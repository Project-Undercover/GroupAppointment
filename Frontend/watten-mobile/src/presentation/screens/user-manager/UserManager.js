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
import { useTranslation } from "react-i18next";
import DefaultButton from "../../components/DefaultButton";
import UserActions from "../../../actions/UserActions";
import { useDispatch } from "react-redux";

const UserManager = ({ route }) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [mobileNumber, setMobileNumber] = useState("");
  const [inputItems, setInputItems] = useState([]);
  const dispatch = useDispatch();
  const userActions = UserActions();

  const { t } = useTranslation();
  const { mode } = route.params;

  const ManagerTitle = useMemo(() => {
    return mode === Mode.Add ? t("create_user") : t("edit_user");
  }, [mode]);

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
        item.id === id ? { ...item, name: value } : item
      )
    );
  };

  const handleCreateUser = () => {
    const children = inputItems.map((item) => item?.name);

    dispatch(
      userActions.createUser({ firstName, lastName, mobileNumber, children })
    );
  };

  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={ManagerTitle} />
      <Spacer space={20} />
      <ScrollView
        className="flex-1"
        contentContainerStyle={{
          paddingBottom: 120,
          alignItems: "left",
          flexGrow: 1,
          padding: 14,
        }}
      >
        <View className="flex-row">
          <DefaultInput
            placeholder={t("enter") + " " + t("firstName")}
            label={t("firstName")}
            onChange={setFirstName}
            wrapperStyle={{ width: "50%" }}
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />
          <Spacer space={3} />
          <DefaultInput
            placeholder={t("enter") + " " + t("lastName")}
            label={t("lastName")}
            onChange={setLastName}
            wrapperStyle={{ width: "50%" }}
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />
        </View>
        <Spacer space={15} />
        <DefaultInput
          placeholder={t("enter") + " " + t("phoneNumber")}
          onChange={setMobileNumber}
          keyboardType="numeric"
          label={t("phoneNumber")}
          wrapperStyle={{ width: "100%" }}
          icon={<Feather name="phone" color={theme.COLORS.primary} size={20} />}
        />
        <Spacer space={15} />

        <View className="flex-1 gap-[10px]">
          <TouchableOpacity
            className="flex-row items-center gap-2"
            onPress={handlePressAddChild}
          >
            <AntDesign
              name="plussquare"
              size={26}
              color={theme.COLORS.primary}
            />
            <TextComponent style={styles.addText}>
              {t("add_child")}
            </TextComponent>
          </TouchableOpacity>
          <Spacer space={15} />
          {inputItems.map((item) => (
            <InputItem
              key={item.id}
              id={item.id}
              t={t}
              handleRemoveInput={() => handleRemoveInput(item.id)}
              handleChangeInput={handleChangeInput}
            />
          ))}
        </View>
        <Spacer space={20} />
        <View className="items-center w-full justify-end">
          <DefaultButton text={t("create_user")} onPress={handleCreateUser} />
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
