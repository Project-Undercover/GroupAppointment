import {
  StyleSheet,
  TouchableOpacity,
  View,
  ActivityIndicator,
} from "react-native";
import { useState } from "react";
import EntryCard from "./EntryCard";
import { useTranslation } from "react-i18next";
import OTPInputView from "@twotalltotems/react-native-otp-input";
import TextComponent from "../../../components/TextComponent";
import theme from "../../../../utils/theme";
import Spacer from "../../../components/Spacer";
import DefaultButton from "../../../components/DefaultButton";
import AuthActions from "../../../../actions/AuthActions";
import { useDispatch, useSelector } from "react-redux";
import { AntDesign } from "@expo/vector-icons";
const OTP = ({ number = "0547973441" }) => {
  const { isLoading, user } = useSelector((state) => state.auth);
  const authActions = AuthActions();

  const dispatch = useDispatch();
  const { t } = useTranslation();
  const [code, setCode] = useState("");

  const handleVerify = () => {
    dispatch(authActions.verifyCode(code));
  };

  const goBack = () => {
    dispatch(authActions.backToLogin());
  };

  return (
    <EntryCard title={t("verify")}>
      <TouchableOpacity className="absolute right-5 top-5" onPress={goBack}>
        <AntDesign name={"left"} size={24} color={theme.COLORS.black} />
      </TouchableOpacity>
      <Spacer space={10} />

      <View className="flex-1">
        <TextComponent style={styles.text}>
          {t("enter_otp") + " " + user?.phone}
        </TextComponent>
        <OTPInputView
          style={{ width: "100%", height: 90 }}
          pinCount={5}
          onCodeChanged={(code) => {
            setCode(code);
          }}
          disabled={isLoading}
          keyboardType="numeric"
          autoFocusOnLoad
          codeInputFieldStyle={styles.underlineStyleBase}
          codeInputHighlightStyle={styles.underlineStyleHighLighted}
          onCodeFilled={(code) => {
            console.log(`Code is ${code}, you are good to go!`);
          }}
        />
      </View>
      <DefaultButton
        text={t("verify")}
        onPress={handleVerify}
        disabled={isLoading}
        icon={isLoading ? <ActivityIndicator /> : null}
      />
    </EntryCard>
  );
};

export default OTP;

const styles = StyleSheet.create({
  borderStyleBase: {
    width: 40,
    height: 45,
  },

  borderStyleHighLighted: {
    borderColor: theme.COLORS.secondary,
  },
  text: {
    color: theme.COLORS.primary,
    fontSize: 13,
    paddingVertical: 5,
    paddingHorizontal: 4,
  },
  underlineStyleBase: {
    color: "black",
    fontWeight: "600",
    fontSize: 17,
    borderRadius: 10,
    width: 58,
    height: 54,
    borderWidth: 1,
    borderBottomWidth: 1,
  },

  underlineStyleHighLighted: {
    borderColor: theme.COLORS.secondary,
  },
});
