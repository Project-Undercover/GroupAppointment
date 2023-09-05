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

const Inputs = () => {
  const [openInstructorsSelector, setpenInstructorsSelector] = useState(false);
  const [value, setValue] = useState(["wissam"]);
  const [items, setItems] = useState([
    { label: "wissam", value: "wissam" },
    { label: "tarek", value: "tarek" },
  ]);
  const [date, setDate] = useState(new Date());
  const [mode, setMode] = useState("date");
  const [showTimePicker, setShowTimePicker] = useState(false);
  const scrollViewRef = useRef(null);
  const startTimeRef = useRef(null);

  const toggleTimePicker = (ref) => {
    if (showTimePicker) return;
    setShowTimePicker((prev) => !prev);
  };

  const handleChangeTime = (event, selectedDate) => {
    const currentDate = selectedDate;
    setShowTimePicker(false);
    setDate(currentDate);
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
            placeholder={"Enter session title"}
            label="Session title"
            icon={
              <MaterialCommunityIcons
                name="subtitles-outline"
                color={theme.COLORS.primary}
                size={20}
              />
            }
          />
          <DefaultInput
            placeholder={"Enter session address"}
            label="Session address"
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />

          <View style={styles.dropContainer}>
            <TextComponent style={styles.label}>
              Session Instructure
            </TextComponent>
            <DropDownPicker
              open={openInstructorsSelector}
              value={value}
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
                textAlign: I18nManager.isRTL ? "right" : "left",
              }}
              placeholder="Session Instructure"
              badgeDotColors={[theme.COLORS.primary]}
            />
          </View>

          <DefaultInput
            placeholder={"Enter max participants"}
            label="Session max participants"
            icon={
              <Feather name="user" color={theme.COLORS.primary} size={20} />
            }
          />
          <View className="flex-row">
            <DefaultInput
              containerStyle={{ width: "90%" }}
              placeholder={"Enter start time"}
              label="Session start time"
              onFocus={toggleTimePicker}
              icon={
                <AntDesign
                  name="clockcircleo"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
            <DefaultInput
              placeholder={"Enter end time"}
              label="Session end time"
              containerStyle={{ width: "90%" }}
              onFocus={toggleTimePicker}
              icon={
                <AntDesign
                  name="clockcircleo"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
          </View>
          <View className="w-full justify-start items-start">
            {showTimePicker && (
              <DateTimePicker
                testID="dateTimePicker"
                value={date}
                mode={"time"}
                is24Hour={true}
                display={"default"}
                onChange={handleChangeTime}
              />
            )}
          </View>

          <UploadImageCard />
          <Spacer space={5} />
          <View className="items-center">
            <DefaultButton text="Create Session" />
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
