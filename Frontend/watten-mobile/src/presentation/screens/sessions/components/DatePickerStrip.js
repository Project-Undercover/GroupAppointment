import { useState } from "react";
import { View, StyleSheet, I18nManager } from "react-native";
import CalendarStrip from "react-native-calendar-strip";
import theme from "../../../../utils/theme";
import { AntDesign } from "@expo/vector-icons";
// import "moment/locale/ar";
// import "moment/locale/he";
const DatePickerStrip = ({ date, handleChangeDate }) => {
  return (
    <View style={styles.container}>
      <CalendarStrip
        scrollable
        onDateSelected={(date) => handleChangeDate(date)}
        selectedDate={date}
        style={{
          height: 100,
          paddingTop: 20,
          paddingBottom: 10,
        }}
        calendarHeaderStyle={{
          color: theme.COLORS.secondaryPrimary,
          fontSize: 17,
          fontFamily: theme.FONTS.primaryFontSemibold,
          marginBottom: 5,
        }}
        dateNumberStyle={{ color: theme.COLORS.primary, fontSize: 13 }}
        iconContainer={{
          flex: 0.1,
          transform: [{ rotate: I18nManager.isRTL ? "180deg" : "0deg" }],
        }}
        dateNameStyle={{
          color: theme.COLORS.primary,
          fontFamily: theme.FONTS.primaryFontSemibold,
          fontSize: 12,
        }}
        highlightDateNameStyle={{
          fontFamily: theme.FONTS.primaryFontMedium,
          fontSize: 13,
        }}
        highlightDateNumberStyle={{
          color: theme.COLORS.white,
          width: 24,
          height: 24,
          paddingTop: 5,
          fontSize: 12,
        }}
        dateContainerStyle={{
          alignItems: "center",
          justifyContent: "center",
          backgroundColor: "red",
        }}
        highlightDateNumberContainerStyle={{
          alignItems: "center",
          alignContent: "center",
          textAlign: "center",
          justifyContent: "center",
          borderRadius: 30,
          //   marginBottom: 5,
          backgroundColor: theme.COLORS.primary,
        }}
        // iconRight={I18nManager.isRTL? require("../../../../assets/icons/left-chevron.png")}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    width: "100%",
    // height: 100,
    backgroundColor: theme.COLORS.white,
    ...theme.SHADOW.shadowBar,
  },
});
export default DatePickerStrip;
