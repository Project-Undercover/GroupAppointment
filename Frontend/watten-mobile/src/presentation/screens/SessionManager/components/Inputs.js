import {
  StyleSheet,
  Text,
  View,
  I18nManager,
  TouchableWithoutFeedback,
  Keyboard,
  Button,
  Pressable,
  ScrollView,
} from "react-native";
import { useState, useRef } from "react";
import DefaultInput from "../../../components/DefaultInput";
import {
  Entypo,
  Feather,
  Ionicons,
  MaterialCommunityIcons,
  AntDesign,
} from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import TextComponent from "../../../components/TextComponent";
import Spacer from "../../../components/Spacer";
import DateTimePicker from "@react-native-community/datetimepicker";
import DropDownPicker from "react-native-dropdown-picker";
import DefaultButton from "../../../components/DefaultButton";
import UploadImageCard from "./UploadImageCard";
import { useTranslation } from "react-i18next";
import { windowWidth } from "../../../../utils/dimensions";
const Inputs = () => {
  const [openInstructorsSelector, setpenInstructorsSelector] = useState(false);
  const { t } = useTranslation();
  const [value, setValue] = useState(["wissam"]);
  const [items, setItems] = useState([
    { label: "wissam", value: "wissam" },
    { label: "tarek", value: "tarek" },
  ]);
  const [date, setDate] = useState(new Date());
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());
  const [mode, setMode] = useState("date");
  const [showTimePicker, setShowTimePicker] = useState(false);
  const scrollViewRef = useRef(null);
  const startTimeRef = useRef(null);

  const toggleTimePicker = (ref) => {
    if (showTimePicker) return;
    setShowTimePicker((prev) => !prev);
  };

  const handleChangeStartTime = (event, selectedDate) => {
    const currentDate = selectedDate;
    // setShowTimePicker(false);
    setStartDate(currentDate);
  };
  const handleChangeEndTime = (event, selectedDate) => {
    const currentDate = selectedDate;
    // setShowTimePicker(false);
    setEndDate(currentDate);
  };
  const handleWithoutFeedback = () => {
    if (showTimePicker) {
      setShowTimePicker(false);
    }
    Keyboard.dismiss();
  };
  const scrollTo = (inputRef) => {
    if (inputRef.current) {
      inputRef.current.focus();
      scrollViewRef.current.scrollTo({
        y: inputRef.current.offsetTop,
        animated: true,
      });
    }
  };
  return (
    <TouchableWithoutFeedback onPress={handleWithoutFeedback}>
      <ScrollView
        style={{ flex: 1 }}
        ref={scrollViewRef}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={{ paddingBottom: 120 }}
      >
        <View className="flex-start flex-1" style={{ rowGap: 20 }}>
          <DefaultInput
            placeholder={t("enter") + " " + t("session_title")}
            label={t("session_title")}
            icon={
              <MaterialCommunityIcons
                name="subtitles-outline"
                color={theme.COLORS.primary}
                size={20}
              />
            }
          />
          <DefaultInput
            placeholder={t("enter") + " " + t("session_address")}
            label={t("session_address")}
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />

          <View style={styles.dropContainer}>
            <TextComponent style={styles.label}>
              {t("session_instructure")}
            </TextComponent>
            <DropDownPicker
              open={openInstructorsSelector}
              value={value}
              listMode="SCROLLVIEW"
              items={items}
              setOpen={setpenInstructorsSelector}
              setValue={setValue}
              setItems={setItems}
              stickyHeader
              dropDownContainerStyle={{
                borderWidth: 1,
                borderColor: theme.COLORS.secondary2,
                padding: 10,
                zIndex: 999,
              }}
              multiple={true}
              style={styles.input}
              maxHeight={300}
              mode="BADGE"
              placeholderStyle={{
                color: theme.COLORS.gray1,
                fontSize: 14,
                fontFamily: theme.FONTS.primaryFontRegular,
                textAlign: "left",
                // textAlign: I18nManager.isRTL ? "right" : "left",
              }}
              placeholder={t("session_instructure")}
              badgeDotColors={[theme.COLORS.primary]}
            />
          </View>

          <DefaultInput
            placeholder={t("enter") + " " + t("session_max")}
            label={t("session_max")}
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />
          <View className="flex-row  justify-between">
            <DefaultInput
              containerStyle={{ width: windowWidth * 0.4 }}
              placeholder={t("enter") + " " + t("session_start")}
              label={t("session_start")}
              onFocus={toggleTimePicker}
              component={
                <DateTimePicker
                  testID="dateTimePicker"
                  value={date}
                  mode={"time"}
                  is24Hour={true}
                  display={"calendar"}
                  onChange={handleChangeStartTime}
                />
              }
              icon={
                <AntDesign
                  name="clockcircleo"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
            <DefaultInput
              placeholder={t("enter") + " " + t("session_end")}
              label={t("session_end")}
              containerStyle={{ width: windowWidth * 0.4 }}
              onFocus={toggleTimePicker}
              component={
                <DateTimePicker
                  testID="dateTimePicker"
                  value={endDate}
                  mode={"time"}
                  is24Hour={true}
                  display={"calendar"}
                  onChange={handleChangeEndTime}
                />
              }
              icon={
                <AntDesign
                  name="clockcircleo"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
          </View>

          <UploadImageCard />
          <Spacer space={5} />
          <View className="items-center">
            <DefaultButton text={t("create_session")} />
          </View>
        </View>
      </ScrollView>
    </TouchableWithoutFeedback>
  );
};

export default Inputs;

const styles = StyleSheet.create({
  label: {
    fontSize: 15,
    color: theme.COLORS.secondaryPrimary,
    paddingStart: 15,
    marginBottom: 5,
  },
  input: {
    height: 48,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#fff",
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
    borderRadius: 5,
    overflow: "hidden",
  },
  dropContainer: {
    zIndex: 2,
  },
  dateContainer: {
    flex: 0.5,
  },
});
