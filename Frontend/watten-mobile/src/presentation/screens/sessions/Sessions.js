import { StyleSheet, Text, View } from "react-native";
import { useState, useCallback } from "react";
import AppHeader from "../../components/AppHeader";
import { useFocusEffect } from "@react-navigation/native";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DatePickerStrip from "./components/DatePickerStrip";
import SessionList from "../../components/SessionList";
import AddSessionBar from "./components/AddSessionBar";
import moment from "moment";
const Sessions = () => {
  const [date, setDate] = useState(moment());

  const handleChangeDate = (date) => {
    setDate(date);
  };

  console.log(date);
  useFocusEffect(
    useCallback(() => {
      setDate(moment());
    }, [])
  );

  return (
    <View className="flex-1 bg-white">
      <CustomeStatusBar />
      <AppHeader />
      <View className="flex-1">
        <DatePickerStrip date={date} handleChangeDate={handleChangeDate} />
        <AddSessionBar date={date} />
        <SessionList />
      </View>
    </View>
  );
};

export default Sessions;

const styles = StyleSheet.create({});
