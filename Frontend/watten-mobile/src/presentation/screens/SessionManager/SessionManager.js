import { StyleSheet, Text, View } from "react-native";
import { useMemo } from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DefaultHeader from "../../components/DefaultHeader";
import { Mode } from "../../../utils/Enums";
import moment from "moment";
import TextComponent from "../../components/TextComponent";
import theme from "../../../utils/theme";
import Inputs from "./components/Inputs";
import globalStyles from "../../../utils/theme/globalStyles";
import Spacer from "../../components/Spacer";
const SessionManager = ({ route, navigation }) => {
  const { mode, date } = route.params;

  const ManagerTitle = useMemo(() => {
    return mode === Mode.Add ? "Add Session" : "Edit Session";
  }, [mode]);

  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={ManagerTitle} />
      <View className="px-4 py-5 flex-1">
        <View className="w-auto" style={{ alignSelf: "flex-start" }}>
          <TextComponent mediumBold style={styles.dateText}>
            {moment(date).format("LL")}
          </TextComponent>
          <View style={globalStyles.underLine}></View>
        </View>
        <Spacer space={20} />
        <Inputs />
      </View>
    </View>
  );
};

export default SessionManager;

const styles = StyleSheet.create({
  dateText: {
    fontSize: 18,
  },
});
