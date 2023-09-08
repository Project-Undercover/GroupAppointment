import { StyleSheet, Text, View, ActivityIndicator } from "react-native";
import { useState } from "react";
import EntryCard from "./EntryCard";
import { useTranslation } from "react-i18next";
import DefaultInput from "../../../components/DefaultInput";
import { AntDesign } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import TextComponent from "../../../components/TextComponent";
import DefaultButton from "../../../components/DefaultButton";
import { useDispatch, useSelector } from "react-redux";
import AuthActions from "../../../../actions/AuthActions";
const Login = () => {
  const { isLoading } = useSelector((state) => state.auth);
  const [phone, setPhone] = useState("");
  const authActions = AuthActions();
  const { t } = useTranslation();

  const dispatch = useDispatch();
  const onChangePhone = (value) => {
    setPhone(value);
  };

  const handleLogin = () => {
    console.log(phone);
    dispatch(authActions.login(phone));
  };
  return (
    <EntryCard title={t("login")}>
      <View className="flex-1">
        <DefaultInput
          onChange={onChangePhone}
          value={phone}
          keyboardType="numeric"
          editable={!isLoading}
          placeholder={t("enter") + " " + t("phoneNumber")}
          icon={
            <AntDesign name="phone" color={theme.COLORS.primary} size={20} />
          }
        />
        <TextComponent style={styles.otpText}>{t("send_otp")}</TextComponent>
      </View>
      <DefaultButton
        text={t("login")}
        disabled={isLoading}
        onPress={handleLogin}
        icon={isLoading ? <ActivityIndicator /> : null}
      />
    </EntryCard>
  );
};

export default Login;

const styles = StyleSheet.create({
  otpText: {
    color: theme.COLORS.primary,
    fontSize: 13,
    paddingVertical: 10,
    paddingHorizontal: 4,
  },
});
